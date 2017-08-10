import { Component, OnInit, ElementRef } from '@angular/core';
import { ShipperServices } from './shipper.service';

@Component({
    selector: 'shipper-intro',
    templateUrl: 'Shipper/Apply',
    providers: [ShipperServices]
})
export class ShipperIntroComponent implements OnInit {
    public isShipper: boolean = false;
    public isApplied: boolean = false;
    public shipper: any;
    public message: string;

    constructor(private shipperSrv: ShipperServices, private el: ElementRef) {
        this.shipper = {};
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

    SetTotal(total: number) {
        this.shipper.Total = total;
    }

    SetDeposit(deposit: number) {
        this.shipper.Deposit = deposit;
    }

    BuyPackage() {
        this.shipperSrv.BuyPackage(this.shipper).subscribe((res: any) => {
            if (res == true) {
                let input: HTMLElement = this.el.nativeElement.querySelector('#succMess');
                this.message = "Purchased successfully!";
                input.removeAttribute('hidden');
                setTimeout(() => {
                    input.hidden = true;
                }, 2000);
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