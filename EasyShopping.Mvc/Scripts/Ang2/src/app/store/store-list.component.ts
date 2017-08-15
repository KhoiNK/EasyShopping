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
    public storesList: any[];
    public stores: any[];
    public subscription: Subscription;
    public page: number;
    constructor(private storeservice: StoreServices, private activatedRoute: ActivatedRoute) {
        this.page = 1;
    }

    LoadData() {
        this.storeservice.GetList(this.page).subscribe((res: any) => {
            this.storesList = res;
            this.stores = res;
        }, error => {
            console.log(error);
        });
    }

    SearchStore() {
        if (this.searchkey == "" || this.searchkey == undefined || this.searchkey == null) {
            this.stores = this.storesList;
        } else {
            this.stores = this.storesList.filter(x => (x.Name == this.searchkey) || (x.ID == this.searchkey));
        }
    }
}