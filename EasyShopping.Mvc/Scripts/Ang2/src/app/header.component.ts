import { Component, OnInit } from '@angular/core';
import { IAuthService, AuthService } from './auth/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'my-header',
    templateUrl: '/Home/Header',
})

export class Header implements OnInit {
    public isSignedIn: boolean;

    constructor(private authService: IAuthService, private router: Router) {

    }

    ngOnInit() {
        if (this.authService.isAuthenticated()) {
            this.isSignedIn = this.authService.isAuthenticated();
        }
        this.authService
            .authenticatedObservable()
            .subscribe((res) => {
                this.isSignedIn = res.isAuthenticated;
            });
    }

    public logout() {
        this.authService.logOut();
        this.router.navigate(['/']);
    }
}