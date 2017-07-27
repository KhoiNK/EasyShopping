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
    public orderslist: any[];

    constructor(private orderService: OrderServices) {

    }

    ngOnInit() {
        this.orderService.GetOrderByUserId().subscribe((res: any) => {
            this.orderslist = res;
            this.LoadData(4);
        }, err => {
            console.log(err);
        });
    }

    LoadData(id: number) {
        this.orders = this.orderslist.filter((x) => x.StatusID == id);
    }
}