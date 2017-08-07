import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ShipperServices } from './shipper.service';
import { OrderServices } from '../order/order.service';
import { Subscription } from 'rxjs';
import { GlobalService } from '../global-observable.service';

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
        , private gloSrv: GlobalService
    ) {

    };

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe((params:any) => {
            this.storeid = params['id'];
        });
        this.LoadData();
        this.LoadOrder();
        this.gloSrv.GetLoad().subscribe((res: any) => {
            if (res == true) {
                this.LoadData();
                this.LoadOrder();
                this.gloSrv.ClearLoadPage();
            }
        }, err => {
            console.log(err);
        });
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
                this.SetMessage("Rejected Successfully!");
                this.LoadData();

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
                this.gloSrv.SetLoadPage();
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
                this.gloSrv.SetLoadPage();
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