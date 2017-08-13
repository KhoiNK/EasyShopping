import { OnInit, Component } from '@angular/core';
import { ShipperServices } from './shipper.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'shipper-list',
    templateUrl: '/Shipper/GetApproveList',
    providers: [ShipperServices]
})
export class ShipperListComponent implements OnInit {
    public searchkey: number;
    public shipper: any;
    public subscription: Subscription;
    public shippers: any[];
    public page: number;

    constructor(private shipperSrvc: ShipperServices) {
        this.shipper = {};
        this.page = 1;
    }

    ngOnInit() {
        this.LoadData();
    }

    LoadData() {
        if (this.searchkey == null || this.searchkey == undefined) {
            this.GetShippers(1);
        } else {
            this.shipperSrvc.GetById(this.searchkey).subscribe((res: any) => {
                this.shipper = res;
            }, error => {
                console.log(error);
            });
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

    GetShippers(page: number) {
        this.shipperSrvc.GetAll(page).subscribe((res: any) => {
            this.shippers = res;
        }, err => {
            console.log(err);
        })
    }

    Approve(id: number) {
        this.shipperSrvc.Approve(id).subscribe((res: any) => {
            if (res == true) {
                alert("Approve successfully!");
            }
        }, err => {
            console.log(err);
        });
    }
}