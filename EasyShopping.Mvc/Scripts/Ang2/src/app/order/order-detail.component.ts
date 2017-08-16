import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { OrderServices } from './order.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { GlobalService } from '../global-observable.service';


@Component({
    selector: 'order-detail',
    templateUrl: 'Order/OrderDetail',
    providers: [OrderServices]
})

export class OrderDetailComponent implements OnInit, OnDestroy {
    public id: number;
    public details: any[];
    public subscription: Subscription;
    public Math: any;
    public message: string;
    public data: any;
    public order: any;
    constructor(private orderService: OrderServices
        , private activateRoute: ActivatedRoute
        , private el: ElementRef
        , private gloSrv: GlobalService) {
        this.Math = Math;
        this.data = {};
        this.order = {};
    }

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.orderService.GetDetail(this.id).subscribe((res: any) => {
            this.order = res;
        }, err => {
            console.log(err);
        });
        this.LoadData(this.id);
        //.subscribe(country => this.country = country);
        
    }

    LoadData(id: number) {
        this.orderService.GetOrderDetail(id).subscribe((res: any) => {
            this.details = res;
        }, err => {
            console.log(err);
        });
    }

    ChangeQuantity(id: number) {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector("#item" + id);
        let quantity = inputEl.value;
        this.data.ID = id;
        this.data.Quantity = quantity;
        this.orderService.ChangeQuantity(this.data).subscribe((res: any) => {
            if (res == true) {
                this.message = "Changed successfully!";
            }
            else {
                this.message = "Changed failed!";
            }
        });
        return;
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
