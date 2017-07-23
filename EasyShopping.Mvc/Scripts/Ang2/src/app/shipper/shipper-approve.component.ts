import { OnInit, Component } from '@angular/core';
import { ShipperServices } from './shipper.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'shipper-list',
    templateUrl: '/Shipper/GetApproveList',
    providers: [ShipperServices]
})
export class ShipperListComponent {
    public searchkey: number;
    public shipper: any;
    public subscription: Subscription;
    constructor(private shipperSrvc: ShipperServices) {
        this.shipper = {};
    }

    LoadData() {
        this.shipperSrvc.GetById(this.searchkey).subscribe((res: any) => {
            this.shipper = res;
        }, error => {
            console.log(error);
        });
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