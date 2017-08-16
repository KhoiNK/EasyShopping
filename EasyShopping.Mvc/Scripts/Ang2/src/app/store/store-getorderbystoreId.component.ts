import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ShipperServices } from '../shipper/shipper.service';
import { OrderServices } from '../order/order.service';
import { Subscription } from 'rxjs';
import { GlobalService } from '../global-observable.service';

@Component({
    selector: 'store-getshipperbystore',
    templateUrl: 'Shipper/GetByStore',
    providers: [ShipperServices, OrderServices]
})

export class GetOrderByStoreComponent implements OnInit, OnDestroy {
    public shippers: any[];
    private subscription: Subscription;
    private storeid: number;
    public orders: any[];
    public message: string = "";

    constructor(private shipperSrvc: ShipperServices
        , private activateRoute: ActivatedRoute
        , private router: Router
        , private orderSrv: OrderServices
        , private el: ElementRef
        , private gloSrv: GlobalService
    ) {

    };

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe((params: any) => {
            this.storeid = params['id'];
            this.LoadOrder();
        });
    }

    LoadOrder() {
        this.orderSrv.GetByStore(this.storeid).subscribe((res: any) => {
            this.orders = res;
        }, err => {
            console.log(err);
        });
    }

    RejectShipper(id: number) {
        this.shipperSrvc.RejectShipper(id).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Rejected Successfully!");
                this.LoadOrder();
            }
        }, err => {
            console.log(err);
        });
    }

    SetMessage(mess: string) {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#succMess');
        this.message = mess;
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

    CancelOrder(id: number) {
        this.orderSrv.CancelOrder(id).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Canceled successfully!");
                this.LoadOrder();
                this.gloSrv.SetLoadPage(true);
            }
        }, err => {
            this.SetErrMess("Cancelled failed");
            console.log(err);
        });
    }

    AcceptOrder(id: number) {
        this.orderSrv.AcceptOrder(id).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Approved successfully!");
                this.LoadOrder();
                this.gloSrv.SetLoadPage(true);
            }
        }, err => {
            console.log(err);
            this.SetErrMess("Accepted failed");
        });
    }

    ChangeTotal(id: number) {
        let input: HTMLInputElement = this.el.nativeElement.querySelector("#order" + id);
        let order = this.orders.filter(x => x.ID == id)[0];
        order.Total = input.value;
        this.orderSrv.EditOrder(order).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Updated successfully!");
                setTimeout(() => {
                    this.LoadOrder();
                });
            }
        }, err => {
            console.log(err);
        });
    }

    IsPaid(id: number) {
        let order = this.orders.filter(x => x.ID == id)[0];
        order.IsPaid = true;
        this.orderSrv.EditOrder(order).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Updated successfully!");
                setTimeout(() => {
                    this.LoadOrder();
                }, 1000);
            }
            else {
                alert("failed");
            }
        }, err => {
            console.log(err);
        });
    }

    ToCompleted(id: number) {
        let order = this.orders.filter(x => x.ID == id)[0];
        order.StatusID = 3;
        order.IsPaid = false;
        this.orderSrv.EditOrder(order).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Updated successfully!");
                setTimeout(() => {
                    this.LoadOrder();
                }, 2000);
            }
        }, err => {
            this.SetErrMess("Updated failed!");
            console.log(err);
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}