import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthService, AuthService } from './auth.service';
import { GlobalService } from '../global-observable.service';

// Avoid name not found warnings
declare var Auth0Lock: any;
declare var window: any;

@Component({
    selector: 'my-login',
    templateUrl: 'User/Login'
})
export class LoginComponent implements OnInit {

    public username: string;
    public password: string;
    public err: string = "";


    constructor(private authService: IAuthService, private router: Router, private gloSrv: GlobalService) {
        //this.gloSrv.globalMess$.subscribe(res => {
        //    this.err = res;
        //}, err => {
        //    console.log(err);
        //});
    }

    ngOnInit() {
        if (this.authService.isAuthenticated()) {
            this.router.navigate(['/']);
        } else {
            this.authService
                .authenticatedObservable()
                .subscribe((res) => {
                    if (res.isAuthenticated) {
                        this.router.navigate(['/']);
                    }
                });
        }
        this.gloSrv.globalMess$.subscribe((res: any) => {
            this.err = res;
        });
    }

    private login() {
        // Validate
        this.authService.signIn(this.username, this.password);
    }
}
