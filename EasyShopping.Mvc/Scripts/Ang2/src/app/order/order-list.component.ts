import { Component, OnInit, OnDestroy } from '@angular/core';
import { OrderServices } from './order.service';

@Component({
    selector: 'order-list',
    templateUrl: 'Order/OrderList',
    providers: [OrderServices]
})

export class OrderList implements OnInit {
    public id: number;
    public orders: any[];

    constructor(private orderService: OrderServices) {

    }

    ngOnInit() {
        this.orderService.GetOrderByUserId().subscribe((res: any) => {
            this.orders = res;
        }, err => {
            console.log(err);
        });
    }

}