import { Component, OnInit, ElementRef } from '@angular/core';
import { IAuthService, AuthService } from './auth/auth.service';
import { UserServices } from './user/user.service';
import { ProductService } from './product/product.service';
import { Router } from '@angular/router';
import { tokenNotExpired } from 'angular2-jwt';
import { GlobalService } from './global-observable.service';

const PROFILE: string = 'profile',
    CART: string = 'cart';

@Component({
    selector: 'my-header',
    templateUrl: '/Home/Header',
    providers: [UserServices, ProductService, GlobalService]
})

export class Header implements OnInit {
    public isSignedIn: boolean = false;
    public user: any;
    public profile: any;
    public role: any;
    public searchkey: string = "";
    public products: any[];

    constructor(private authService: IAuthService
        , private router: Router
        , private userservice: UserServices
        , private productSrv: ProductService
        , private el: ElementRef
        , private globalSrv: GlobalService) {
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

    SearchProduct() {
        if (this.searchkey.trim() != "") {
            this.productSrv.GetByName(this.searchkey).subscribe((res: any) => {
                this.products = res;
            }, err => {
                console.log(err);
            })
        }
        if (this.searchkey.trim() == "") {
            this.products = [];
        }
    }

    SetSearchKey() {
        this.globalSrv.changeSearchProduct(this.searchkey);
        this.router.navigate(['/searchs', this.searchkey]);
    }
}