import { Component, ElementRef, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ShipperServices } from './shipper.service';
import { UserServices } from '../user/user.service';

@Component({
    selector: 'shipper-apply',
    templateUrl: 'Shipper/Register',
    providers: [ShipperServices, UserServices]
})

export class ShipperApplyComponent implements OnInit {
    public shipper: any;
    public message: string = "";
    public error: string = "";
    public user: any;
    constructor(private shipperSrv: ShipperServices, private router: Router, private el: ElementRef, private userSrv: UserServices) {
        this.shipper = {};
        this.user = {};
    }

    ngOnInit() {
        this.userSrv.GetUserDetail().subscribe((res: any) => {
            this.user = res;
        }, err => {
            console.log(err);
        });
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

