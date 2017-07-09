import { Component, OnInit, OnDestroy } from '@angular/core';
import { OrderServices } from './order.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
    selector: 'order-detail',
    templateUrl: 'Order/OrderDetail',
    providers: [OrderServices]
})

export class OrderDetailComponent implements OnInit, OnDestroy {
    public id: number;
    public order: any;
    public subscription: Subscription;
    public Math: any;
    constructor(private orderService: OrderServices, private activateRoute: ActivatedRoute) {
        this.Math = Math;
        this.order = {};
    }

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
        });


        this.orderService.GetOrderDetail(this.id).subscribe((res: any) => {
            this.order = res;
            
        }, err => {
            console.log(err);
        });
        //.subscribe(country => this.country = country);
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
