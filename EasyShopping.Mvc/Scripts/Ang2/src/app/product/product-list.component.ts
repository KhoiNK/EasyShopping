import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from './product.service';
import { OrderServices } from '../order/order.service';
import { GlobalService } from '../global-observable.service';
import { Subscription } from 'rxjs';
import { ProductTypeService } from './product-type.service';
import { order } from '../order/Order';

@Component({
    selector: 'product-list',
    templateUrl: '/Product',
    providers: [ProductService, OrderServices, GlobalService, ProductTypeService]
})

export class ProductListComponent implements OnInit{
    public products: any[];
    public CART: string = "cart";
    public PROFILE: string = 'profile';
    public subscription: Subscription;
    public types: any[];
    public message: string = "";
    public cart: order = new order();

    constructor(private productservice: ProductService
        , private orderService: OrderServices
        , private globalSrv: GlobalService
        , private productTypeSrv: ProductTypeService
        , private el: ElementRef
        , private router: Router
    ) {
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.productservice.GetList().subscribe((res: any) => {
            this.products = res;
        }, err => {
            console.log(err);
        });
        this.productTypeSrv.GetList().subscribe((res: any) => {
            this.types = res;
        }, err => {
            console.log(err);
        });
    }
    AddToCart(productId: number) {
        let order: any = {};
        let cart = localStorage.getItem(this.CART);
        if (cart == null) {
            order.productId = productId;
            this.orderService.AddToCart(order).subscribe((res: any) => {
                localStorage.setItem(this.CART, JSON.stringify(res.ID));
                this.cart.cartID = res.ID;
                this.cart.products.push(productId);
                localStorage.setItem("order", JSON.stringify(this.cart));
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
            order.cartId = cart;
            let oldcart = JSON.parse(localStorage.getItem("order"));
            this.orderService.AddToCart(order).subscribe((res: any) => {
                let existproduct: any[] = oldcart.products;
                if (JSON.stringify(res) == 'true') {
                    if (existproduct.some(x => x == productId) == false) {
                        oldcart.products.push(productId);
                        localStorage.setItem("order", JSON.stringify(oldcart));
                    }
                    this.SetMessage();
                }
            }, err => {
                console.log(err);
            });
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
}