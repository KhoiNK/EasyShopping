import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthService, AuthService } from './auth.service';

// Avoid name not found warnings
declare var Auth0Lock: any;
declare var window: any;

@Component({
    template: `
        <input [(ngModel)]="username" name="username" />
    `
})
export class LoginComponent implements OnInit {

    public username: string;
    public password: string;

    constructor(private authService: IAuthService, private router: Router) {

    }

    ngOnInit() {
        if (this.authService.isAuthenticated()) {
            this.router.navigate(['/']);
        } else {
            this.authService
                .authenticatedObservable()
                .subscribe((authenticated) => {
                    if (authenticated) {
                        this.router.navigate(['/']);
                    }
                });
        }
    }

    private login() {
        // Validate
        this.authService.signIn(this.username, this.password);
    }
}
