import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { tokenNotExpired } from 'angular2-jwt';

// Avoid name not found warnings
declare var Auth0Lock: any;
declare var window: any;

const ACCESS_TOKEN: string = 'id_token',
    REFRESH_TOKEN: string = 'refresh_token',
    PROFILE: string = 'profile';

export abstract class IAuthService {
    abstract getProfile(): Profile;
    abstract isAuthenticated(): boolean;
    abstract isInRole(role: string): boolean;
    abstract authenticatedObservable(): Observable<IAuthenticatedEvent>;
    abstract signIn(username: string, password: string): void;
    abstract logOut(): void;
    abstract getAuthToken(): string;
}

export class Profile {
    public roles: Array<string>;
    public picture: string;
    public name: string;

    constructor(source?: any) {
        if (!source) { return; }
        this.roles = source.roles;
        this.picture = source.picture;
        this.name = source.name;
    }
}

export interface IAuthenticatedEvent {
    isAuthenticated: boolean;
    profile: Profile;
}

@Injectable()
export class AuthService extends IAuthService {

    private _authSubject: BehaviorSubject<IAuthenticatedEvent>;
    private _profile: Profile;

    get profile(): Profile {
        return this._profile;
    }

    constructor(private ngHttp: Http) {
        super();
        
        this._profile = new Profile;

        this._authSubject = new BehaviorSubject<IAuthenticatedEvent>({
            isAuthenticated: this.isAuthenticated(),
            profile: this._profile
        });
    }

    public getProfile(): Profile {
        return this._profile;
    }

    public isAuthenticated(): boolean {
        // Check if there's an unexpired JWT
        // This searches for an item in localStorage with key == 'id_token'
        return tokenNotExpired() ? true : false; // Somehow, tokenNotExpired() may return null
    }

    public authenticatedObservable(): Observable<IAuthenticatedEvent> {
        return this._authSubject.asObservable();
    }

    public isInRole(role: string): boolean {
        let roles: Array<string> = this._profile.roles;
        if (!roles) { return false; }
        return (-1 < roles.indexOf(role));
    }
    
    public logOut() {
        localStorage.removeItem(ACCESS_TOKEN);
        localStorage.removeItem(REFRESH_TOKEN);
        localStorage.removeItem(PROFILE);
        
        this._authSubject.next({
            isAuthenticated: false,
            profile: new Profile
        });
    }

    public getAuthToken(): string {
        return localStorage.getItem(ACCESS_TOKEN);
    }
    
    public signIn(username: string, password: string): void {
        this.ngHttp
            .post('login URL', {
                UserName: username,
                Password: password
            })
            .subscribe((response) => {
                if (response.ok) {
                    let json = response.json();
                    localStorage.setItem(ACCESS_TOKEN, json.access_token);
                    localStorage.setItem(REFRESH_TOKEN, json.refresh_token);
                    //localStorage.setItem(PROFILE, response.text()); // Save profile json to rebuild profile data after page refresh
                    //this._profile = new Profile(json);
                    this._authSubject.next({
                        isAuthenticated: true,
                        profile: this._profile
                    });
                }
            });
    }
}