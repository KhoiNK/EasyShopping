import { Component, OnInit } from '@angular/core';
import { IAuthService, AuthService } from './auth/auth.service';
import { UserServices } from './user/user.service';
import { Router } from '@angular/router';
import { tokenNotExpired } from 'angular2-jwt';

const PROFILE: string = 'profile',
    CART: string = 'cart';

@Component({
    selector: 'my-header',
    templateUrl: '/Home/Header',
    providers: [UserServices]
})

export class Header implements OnInit {
    public isSignedIn: boolean = false;
    public user: any;
    public profile: any;
    public role: any;

    constructor(private authService: IAuthService, private router: Router, private userservice: UserServices) {
        let cacheProfile = localStorage.getItem(PROFILE);
        let cartId = localStorage.getItem(CART);
        this.profile = JSON.parse(cacheProfile) || {};
        this.user = this.userservice.GetUser(this.profile.userName).subscribe((res: any) => {
            this.user = res;
        }) || {};
        this.role = localStorage.getItem("role");
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
                    this.userservice.GetUser(this.profile.userName).subscribe((res: any) => {
                        this.user = res;
                    });
                }
            });
    }

    loggedIn() {
        return tokenNotExpired();
    }

    public logout() {
        this.authService.logOut();
        this.user = {};
        this.isSignedIn = false;
        this.router.navigate(['/']);
    }
}