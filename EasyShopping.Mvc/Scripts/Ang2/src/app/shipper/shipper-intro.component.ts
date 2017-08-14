import { Component, OnInit, ElementRef } from '@angular/core';
import { ShipperServices } from './shipper.service';
import { UserServices } from '../user/user.service';
import { Http } from '@angular/http';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';

@Component({
    selector: 'shipper-intro',
    templateUrl: 'Shipper/Apply',
    providers: [ShipperServices, UserServices]
})
export class ShipperIntroComponent implements OnInit {
    public isShipper: boolean = false;
    public isApplied: boolean = false;
    public package: any;
    public shipper: any;
    public message: string;
    private apiUrl: string = "http://test18062017.azurewebsites.net/api/users/";
    private paypal: any;
    public user: any;

    constructor(private shipperSrv: ShipperServices
        , private el: ElementRef
        , private userSrv: UserServices
        , private _http: AuthHttp) {
        this.package = {};
        this.shipper = {};
        this.user = {};
    }

    ngOnInit() {
        this.user = JSON.parse(localStorage.getItem("profile"));
        this.shipperSrv.GetByUser().subscribe((res: any) => {
            this.shipper = res;
        }, err => {
            console.log(err);
        });

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



    SetPackage(total: number, pack: string) {
        this.package.Total = total;
        this.package.Pack = pack;
    }

    BuyPackage() {
        if (this.package.Pack == "Total") {
            this.shipper.Total = this.package.Total;
            this.shipper.Deposit = 0;
        }
        if (this.package.Pack == "Deposit") {
            this.shipper.Deposit = this.package.Total;
            this.shipper.Total = 0;
        }
        this.shipperSrv.BuyPackage(this.shipper).subscribe((res: any) => {
            if (res == true) {
                let input: HTMLElement = this.el.nativeElement.querySelector('#succMess');
                this.message = "Purchased successfully!";
                input.removeAttribute('hidden');
                setTimeout(() => {
                    input.hidden = true;
                }, 2000);
                window.location.reload();
            }
            else {
                let input: HTMLElement = this.el.nativeElement.querySelector('#errMess');
                this.message = "Purchased failed!";
                input.removeAttribute('hidden');
                setTimeout(() => {
                    input.hidden = true;
                }, 2000);
            }
        }, err => {
            let input: HTMLElement = this.el.nativeElement.querySelector('#errMess');
            this.message = "Purchased failed!";
            input.removeAttribute('hidden');
            setTimeout(() => {
                input.hidden = true;
            }, 2000);
            console.log(err);
        });
    }
}