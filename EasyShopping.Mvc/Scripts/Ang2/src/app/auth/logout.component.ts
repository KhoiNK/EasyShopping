import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthService, AuthService } from './auth.service';

@Component({
    selector: 'my-logout'
})

export class LogoutComponent implements OnInit {

    constructor(private authService: IAuthService, private router: Router) {

    }

    ngOnInit() {
        this.authService.logOut();
        this.router.navigate(['/']);
    }
}