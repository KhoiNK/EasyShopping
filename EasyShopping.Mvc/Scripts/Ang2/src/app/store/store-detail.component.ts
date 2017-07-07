﻿import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { StoreServices } from './store.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ProductAddComponent } from '../product/product-add.component';
import { OrderServices } from '../order/order.service';


@Component({
    selector: 'store-detail',
    templateUrl: 'Store/StoreDetail',
    providers: [StoreServices, OrderServices]
})
export class StoreDetailComponent implements OnInit, OnDestroy {
    public CART: string = "cart";
    public id: number;
    public store: any;
    public subscription: Subscription;
    constructor(private storeservice: StoreServices, private activatedRoute: ActivatedRoute, private orderService: OrderServices) {
        this.store = {};
    }

    ngOnInit() {
        this.subscription = this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.storeservice.GetStoreById(this.id)
            .subscribe(res => {
                return this.store = res;
            });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    AddToCart(productId: number) {
        let order: any = {};
        let cartId = localStorage.getItem(this.CART);
        if (cartId == null) {
            order.productId = productId;
            this.orderService.AddToCart(order).subscribe((res: any) => {
                localStorage.setItem(this.CART, JSON.stringify(res.ID));
                alert("Added Successfully");
            }, err => {
                console.log(err);
            });
        } else {
            order.productId = productId;
            order.cartId = +cartId;
            this.orderService.AddToCart(order).subscribe((res: any) => {
                if (res.status == 200) {
                    alert("Added Successfully!");
                }
            }, err => {
                console.log(err);
            });
        }
    }
}