import { OnInit, Component, ElementRef } from '@angular/core';
import { ShipperServices } from './shipper.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'shipper-list',
    templateUrl: '/Shipper/GetApproveList',
    providers: [ShipperServices]
})
export class ShipperListComponent implements OnInit {
    public searchkey: number;
    public shippers: any[];
    public shippersList: any[];
    public page: number;
    public shipper: any;
    public Total: number = 0;
    public Deposit: number = 0;
    public message: string = "";

    constructor(private shipperSrvc: ShipperServices, private el: ElementRef) {
        this.page = 1;
        this.shipper = {};
    }

    ngOnInit() {
        let user = JSON.parse(localStorage.getItem("profile"));
        if (user.role == "Admin") {
            this.GetShippers(this.page);
        }
    }

    LoadByPage(command: string) {
        if (command == "next") {
            this.page = this.page + 1;
            this.GetShippers(this.page);
        }
        if (command == "back") {
            this.page = this.page - 1;
            this.GetShippers(this.page);
        }
    }

    SearchShipper() {
        if (this.searchkey == null || this.searchkey == undefined || this.searchkey =="") {
            this.shippers = this.shippersList;
        } else {
            this.shippers = this.shippersList.filter(x => x.Shipper == this.searchkey);
        }
    }

    SetShipper(id: number) {
        this.shipper = this.shippersList.filter(x => x.ID == id)[0];
    }

    GetShippers(page: number) {
        this.shipperSrvc.GetAll(page).subscribe((res: any) => {
            this.shippersList = res;
            this.shippers = res;
        }, err => {
            console.log(err);
        })
    }

    SaveChange() {
        this.shipper.Total = this.Total;
        this.shipper.Deposit = this.Deposit;
        this.shipperSrvc.BuyPackage(this.shipper).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Updated succesfully!");
                window.location.reload();
            }
        }, err => {
            this.SetErrMess("Updated failed!");
            console.log(err);
            });
    }

    Approve(id: number) {
        this.shipperSrvc.Approve(id).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Updated succesfully!");
                this.GetShippers(this.page);
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
}