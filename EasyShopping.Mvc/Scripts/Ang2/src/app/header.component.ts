import { Component, OnInit, ElementRef } from '@angular/core';
import { IAuthService, AuthService } from './auth/auth.service';
import { UserServices } from './user/user.service';
import { ProductService } from './product/product.service';
import { Router } from '@angular/router';
import { tokenNotExpired } from 'angular2-jwt';
import { MessageServices } from '../app/message/message.service';
import { Subject } from 'rxjs/Subject';
import { GlobalService } from './global-observable.service';
import { OrderServices } from './order/order.service';
import { order } from './order/Order';

const PROFILE: string = 'profile',
    CART: string = 'cart';

@Component({
    selector: 'my-header',
    templateUrl: '/Home/Header',
    providers: [UserServices, ProductService, MessageServices, GlobalService, OrderServices]
})

export class Header implements OnInit {
    public isSignedIn: boolean = false;
    public user: any;
    public profile: any;
    public role: any;
    public searchkey: string = "";
    public products: any[];
    public count: number = 0;
    public mess: any[];
    public systemMess: string = "";
    public cartitem: number;
    public order: order = new order();

    constructor(private authService: IAuthService
        , private router: Router
        , private userservice: UserServices
        , private productSrv: ProductService
        , private el: ElementRef
        , private messSrv: MessageServices
        , private gloSrv: GlobalService
        , private ordeSrv: OrderServices
    ) {
        let cacheProfile = {};
        let cartId = localStorage.getItem(CART);
        this.profile = {};
        this.user = {};
    }

    ngOnInit() {
        this.authService
            .authenticatedObservable()
            .subscribe((res) => {
                let cacheProfile = localStorage.getItem(PROFILE);
                this.profile = JSON.parse(cacheProfile)
                this.isSignedIn = res.isAuthenticated;
                if (this.isSignedIn == true) {
                    this.userservice.GetUser(this.profile.userName).subscribe((res: any) => {
                        this.user = res;
                    });
                    this.role = this.profile.role;
                    this.GetMessCount();
                }
                else {
                    this.isSignedIn = false;
                }
            });
        let order = JSON.parse(localStorage.getItem("order"));
        if (order == undefined || order == null) {
            this.SetCart();
        }
        //setInterval(() => {
        //    if (tokenNotExpired('id_token')) {
        //        this.GetMessCount();
        //    }
        //}, 3000);
        setInterval(() => {
            let order: any = JSON.parse(localStorage.getItem("order"));

            if (order == undefined || order == null) {
                this.cartitem = 0;
            }

            if (order != undefined || order != null) {
                this.cartitem = order.products.length;
            }
        }, 1000);
    }

    loggedIn() {
        return tokenNotExpired();
    }

    public logout() {
        this.authService.logOut();
        this.user = {};
        this.isSignedIn = false;
        this.mess = [];
        this.count = 0;
        this.role = {};
        this.router.navigate(['/']);
    }

    GetMessCount() {
        this.messSrv.GetUnread().subscribe((res: any) => {
            this.count = res;
        }, err => {

            console.log(err);
            this.count = 0;
        });
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

    GetMessage() {
        this.messSrv.GetMessThumb().subscribe((res: any) => {
            if (res) {
                this.mess = res;
            }
            else if (res.status === 400) {
                this.mess = [];
            }
            
        }, err => {
            console.log(err);
            this.mess = [];
            });
    }

    SetSearchKey() {
        this.router.navigate(['/searchs', this.searchkey]);
    }

    MarkAsRead(id: number) {
        let message: any = this.mess.filter(x => x.ID == id)[0];
        if (message.IsRead == false) {
            this.messSrv.MarkAsRead(id).subscribe((res: any) => {
                if (res == true) {
                    this.Navigator(message);
                }
            }, err => {
                console.log(err);
            });
        }
        else {
            this.Navigator(message);
        }
    }

    Navigator(message: any) {
        if (message.MessageType == 1) {
            this.router.navigate(['/orders/order-detail', message.DataID]);
        }
        if (message.MessageType == 2) {
            this.router.navigate(['/products/product-approve-list', message.DataID]);
        }
        if (message.MessageType == 3) {
            this.router.navigate(['/stores/store-detail', message.DataID]);
        }
        if (message.MessageType == 4) {
            this.router.navigate(['/shippers/shipper-store-list', message.DataID]);
        }
        if (message.MessageType == 5) {
            this.router.navigate(['/partners/partner-list', message.DataID]);
        }
    }

    SetNoti(mess: string) {
        this.systemMess = mess;
    }

    SetCart() {
        this.ordeSrv.GetByStatus(4).subscribe((res: any) => {
            this.order.cartID = res[0].ID;
            var details: any[] = res[0].details;
            details.forEach((component) => {
                this.order.products.push(component.ID);
            });
            localStorage.setItem("order", JSON.stringify(this.order));
            localStorage.setItem(CART, res[0].ID);
        });
    }
}