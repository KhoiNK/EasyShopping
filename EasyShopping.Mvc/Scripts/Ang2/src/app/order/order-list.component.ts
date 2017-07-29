import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { OrderServices } from './order.service';

@Component({
    selector: 'order-list',
    templateUrl: 'Order/OrderList',
    providers: [OrderServices]
})

export class OrderList implements OnInit {
    public id: number = 0;
    public orders: any[];
    public Math: any;
    public data: any;
    public message: string = "";
    public orderdetail: any;
    public total: number = 0;

    constructor(private orderService: OrderServices, private el: ElementRef) {
        this.Math = Math;
        this.data = {};
        this.orderdetail = {};
    }

    ngOnInit() {
        this.LoadData();
    }

    LoadData() {
        this.orderService.GetOrderByUserId().subscribe((res: any) => {
            this.orders = res;
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

    SetOrderDetail(id: number) {
        this.total = 0;
        this.orderdetail = this.orders.filter(x => x.ID == id);
        this.id = id;
        this.orderdetail[0].details.forEach((component: any) => {
            this.total = this.total + (+component.Quantity * +component.Price);
        });
    }

    ResetValue() {
        this.total = 0;
        this.orderdetail = {};
        this.id = 0;
    }
}