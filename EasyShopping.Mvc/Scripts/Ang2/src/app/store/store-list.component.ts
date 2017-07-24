import { OnInit, Component } from '@angular/core';
import { StoreServices } from './store.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'store-list',
    templateUrl: '/Store',
    providers: [StoreServices]
})
export class StoreListComponent {
    public searchkey: string;
    public stores: any[];
    public subscription: Subscription;
    constructor(private storeservice: StoreServices, private activatedRoute: ActivatedRoute) {

    }

    LoadData() {
        this.storeservice.GetListStore(this.searchkey).subscribe((res: any) => {
            this.stores = res;
        }, error => {
            console.log(error);
        });
    }
}