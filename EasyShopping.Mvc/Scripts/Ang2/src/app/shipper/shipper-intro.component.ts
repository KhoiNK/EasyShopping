import { Component, OnInit } from '@angular/core';
import { ShipperServices } from './shipper.service';

@Component({
    selector: 'shipper-intro',
    templateUrl: 'Shipper/Apply',
    providers: [ShipperServices]
})
export class ShipperIntroComponent implements OnInit {
    public isShipper: boolean = false;
    public isApplied: boolean = false;

    constructor(private shipperSrv: ShipperServices) {

    }

    ngOnInit() {
        this.shipperSrv.IsApplied().subscribe((res: any) => {
            this.isApplied = res;
        }, err => {
            console.log(err);
        });
        this.shipperSrv.IsShipper().subscribe((res: any) => {
            this.isShipper = res;
        }, err => {
            console.log(err);
        })
    }
}