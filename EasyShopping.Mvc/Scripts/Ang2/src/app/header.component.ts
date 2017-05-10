import { Component, OnInit } from '@angular/core';
import { IAuthService, AuthService } from './auth/auth.service';
import { UserServices } from './user/user.service';
import { Router } from '@angular/router';

const PROFILE: string = 'profile';

@Component({
    selector: 'my-header',
    templateUrl: '/Home/Header',
    providers: [UserServices]
})

export class Header implements OnInit {
    public isSignedIn: boolean;
    public userprofile: any;
    public user: any;
    public name: String;
    constructor(private authService: IAuthService, private router: Router, private userservice: UserServices) {
        let cacheProfile = localStorage.getItem(PROFILE);
        this.userprofile = JSON.parse(cacheProfile);
        this.user = {};
    }

    ngOnInit() {
        //if (this.authService.isAuthenticated()) {
        //    this.isSignedIn = this.authService.isAuthenticated();
        //    this.name = this.authService.getProfile().name;
        //    this.userservice.GetUser(this.name).subscribe((res: any) => {
        //        this.user = res;
        //    });
        //}
        this.authService
            .authenticatedObservable()
            .subscribe((res) => {
                this.isSignedIn = res.isAuthenticated;
                if (this.isSignedIn) {
                    this.name = this.userprofile['name'];
                    this.userservice.GetUser(this.name).subscribe((res: any) => {
                        this.user = res;
                    });
                }
            });
    }

    public logout() {
        this.authService.logOut();
        this.router.navigate(['/']);
    }
}