import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ShipperServices } from './shipper.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'shipper-getbystore',
    templateUrl: 'Shipper/GetByStore',
    providers: [ShipperServices]
})

export class ShipperGetByStoreComponent implements OnInit, OnDestroy {
    public shippers: any[];
    private subscription: Subscription;
    private storeid: number;

    constructor(private shipperSrvc: ShipperServices
        , private activateRoute: ActivatedRoute
        , private router: Router
    ) {

    };

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.storeid = params['storeId'];
        });

        this.shipperSrvc.GetByStoreId(this.storeid).subscribe((res: any) => {
            this.shippers = res;
        }, err => {
            console.log(err);
        })
    }

    RejectShipper(id: number) {
        this.shipperSrvc.RejectShipper(id).subscribe((res: any) => {
            if (res == true) {
                alert("Reject successfully!");
            }
        }, err => {
            console.log(err);
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}