import { Component, ElementRef, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ShipperServices } from './shipper.service';

@Component({
    selector: 'shipper-apply',
    templateUrl: 'Shipper/Apply',
    providers: [ShipperServices]
})

export class ShipperApplyComponent {
    public shipper: any;
    public message: string = "";
    public error: string = "";
    constructor(private shipperSrv: ShipperServices, private router: Router, private el: ElementRef) {
        this.shipper = {};
    }

    SaveChange() {
        this.shipperSrv.Apply(this.shipper).subscribe((res: any) => {
            if (res.ID) {
                let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#messageModal');
                inputEl.click();
                this.shipper = res;
            }
        }, err => {
            this.error = "Applied failed!";
            console.log(err);
        });
    }
}

