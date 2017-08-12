import { Component, OnInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
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
    public inputElement: HTMLInputElement;
    public statusId: number = 1;
    public page: number = 1;

    constructor(private orderService: OrderServices, private el: ElementRef) {
        this.Math = Math;
        this.data = {};
        this.orderdetail = {};
    }

    ngOnInit() {
        this.LoadData(4);

    }

    LoadData(id: number) {
        //this.orderService.GetOrderByUserId().subscribe((res: any) => {
        //    this.orders = res;
        //}, err => {
        //    console.log(err);
        //});

        if (id == null || id == undefined) {
            id = 4;
        }
        this.statusId = id;
        this.orderService.GetByStatus(id).subscribe((res: any) => {
            this.orders = res;
        }, err => {
            console.log(err);
        });
    }

    LoadAll(command: string) {
        if (command == "init") {
            this.LoadByUser(this.page);
        }
        if (command == "next") {
            this.page = this.page + 1;
            this.LoadByUser(this.page);
        }
        if (command == "back") {
            this.page = this.page - 1;
            this.LoadByUser(this.page);
        }
    }

    LoadByUser(page: number) {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector("#nextPage");
        this.orderService.GetOrderByUserId(page).subscribe((res: any) => {
            if (res != []) {
                this.orders = res;
            }
            if (res.lenght <= 0) {
                inputEl.hidden = true;
            }
        }, err => {
            console.log(err);
        });
    }

    ChangeQuantity(id: number, command: string, productId: number) {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector("#item" + id);
        let quantity = inputEl.value;
        this.data.ID = id;
        this.data.ProductID = productId;
        if (command == "add") {
            this.data.Quantity = +quantity + 1;
        }
        if (command == "minus") {
            this.data.Quantity = +quantity - 1; 
        }
        this.orderService.ChangeQuantity(this.data).subscribe((res: any) => {
            if (res == true) {
                if (command == "add") {
                    inputEl.value = "" + (parseInt(inputEl.value) + 1);
                }
                if (command == "minus") {
                    inputEl.value = "" + (parseInt(inputEl.value) - 1);
                }
                this.SetMessage("Changed successfully!");
                this.LoadData(this.statusId);
                this.SetOrderDetail(this.id);
            }
            else {
                this.SetMessage("Changed failed!");
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

    RemoveItem(id: number, orderid: number) {
        this.orderService.RemoveItem(id).subscribe((res: any) => {
            if (res == true) {
                let order:any = JSON.parse(localStorage.getItem("order"));
                let details: any[] = this.orders.filter(x => x.ID == orderid)[0].details;
                let productId: number = details.filter(x => x.ID == id)[0].ProductID;
                var index = order.products.indexOf(productId);
                order.products.splice(index,1);
                localStorage.setItem("order", JSON.stringify(order));    
                this.SetMessage("Removed successfully!");
                this.LoadData(this.statusId);
            }
            if (res == false) {
                this.SetMessage("Removed failed!");
            }
        }, err => {
            this.SetMessage("Removed failed!");
            console.log(err);
        });
    }

    RemoveOrder(id: number) {
        this.orderService.RemoveOrder(id).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Removed successfully!");
                localStorage.removeItem("cart");
                localStorage.removeItem("order");
                this.LoadData(this.statusId);
            }
            if (res == false) {
                this.SetMessage("Removed failed!");
            }
        }, err => {
            this.SetMessage("Removed failed!");
            console.log(err);
        });
    }

    SetMessage(mess: string) {
        this.inputElement = this.el.nativeElement.querySelector('#cartMess');
        this.inputElement.removeAttribute('hidden');
        setTimeout(() => {
            this.inputElement.hidden = true;
        }, 1000);
        this.message = mess;
    }
}