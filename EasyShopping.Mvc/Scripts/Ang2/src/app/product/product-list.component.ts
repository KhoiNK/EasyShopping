import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from './product.service';
import { OrderServices } from '../order/order.service';
import { GlobalService } from '../global-observable.service';
import { Subscription } from 'rxjs';
import { ProductTypeService } from './product-type.service';

@Component({
    selector: 'product-list',
    templateUrl: '/Product',
    providers: [ProductService, OrderServices, GlobalService, ProductTypeService]
})

export class ProductListComponent implements OnInit, OnDestroy {
    public products: any[];
    public CART: string = "cart";
    public PROFILE: string = 'profile';
    public subscription: Subscription;
    public types: any[];
    public message: string = "";
    constructor(private productservice: ProductService
        , private orderService: OrderServices
        , private globalSrv: GlobalService
        , private productTypeSrv: ProductTypeService
        , private el: ElementRef
        , private router: Router
    ) {
    }

    ngOnInit() {
        //this.loadData();
        this.subscription = this.globalSrv.globalMess$.subscribe((res: any) => {
            if (res == "") {
                this.loadData();
            }
            else {
                this.LoadWithName(res);
            }
        }, err => {
            console.log(err);
            });
    }

    loadData() {
        this.productservice.GetList().subscribe((res: any) => {
            this.products = res;
        }, err => {
            console.log(err);
        });
        this.productTypeSrv.GetWithTarget().subscribe((res: any) => {
            this.types = res;
        }, err => {
            console.log(err);
        });
    }
    AddToCart(productId: number) {
        if (localStorage.getItem(this.PROFILE) == {}) {
            alert("please login first!");
            event.preventDefault();
        }
        else {
            let order: any = {};
            let cartId = localStorage.getItem(this.CART);
            if (cartId == null) {
                order.productId = productId;
                this.orderService.AddToCart(order).subscribe((res: any) => {
                    localStorage.setItem(this.CART, JSON.stringify(res.ID));
                    this.SetMessage();
                }, err => {
                    this.SetErrMess("Please login first");
                    setTimeout(() => {
                        this.router.navigate(['/login']);
                    }, 3000);
                    console.log(err);
                });
            } else {
                order.productId = productId;
                order.cartId = cartId;
                this.orderService.AddToCart(order).subscribe((res: any) => {
                    if (JSON.stringify(res) == 'true') {
                        this.SetMessage();
                    }
                }, err => {
                    this.SetErrMess("Added failed");
                    console.log(err);
                });
            }
        }
    }

    SetMessage() {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#cartMess');
        inputel.removeAttribute('hidden');
        setTimeout(() => {
            inputel.hidden = true;
        }, 1000);
    }

    SetErrMess(mess: string) {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#errMess');
        this.message = mess;
        inputel.removeAttribute('hidden');
        setTimeout(() => {
            inputel.hidden = true;
        }, 5000);
    }

    LoadWithName(name: string) {
        this.productservice.GetByName(name).subscribe((res: any) => {
            this.products = res;
        }, err => {
            console.log(err);
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}