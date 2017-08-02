import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ShipperServices } from './shipper.service';
import { OrderServices } from '../order/order.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'shipper-getbystore',
    templateUrl: 'Shipper/GetByStore',
    providers: [ShipperServices, OrderServices]
})

export class ShipperGetByStoreComponent implements OnInit, OnDestroy {
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
    ) {

    };

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.storeid = params['id'];
        });
        this.LoadData();
        this.LoadOrder();
    }

    LoadData() {
        this.shipperSrvc.GetByStoreId(this.storeid).subscribe((res: any) => {
            this.shippers = res;
        }, err => {
            console.log(err);
        })
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
                this.SetMessage();
                this.LoadData();
            }
        }, err => {
            console.log(err);
        });
    }

    SetMessage() {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#succMess');
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
                this.SetMessage();
            }
        }, err => {
            this.SetErrMess("Cancelled failed");
            console.log(err);
        });
    }

    AcceptOrder(id: number) {
        this.orderSrv.AcceptOrder(id).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage();
            }
        }, err => {
            console.log(err);
            this.SetErrMess("Accepted failed");
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}