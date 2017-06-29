import { OnInit, Component } from '@angular/core';
import { StoreServices } from './store.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'store-list',
    templateUrl: '/Store',
    providers: [StoreServices]
})
export class StoreListComponent implements OnInit {
    public page: Number;
    public stores: any[];
    public subscription: Subscription;
    constructor(private storeservice: StoreServices, private activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {
        this.subscription = this.activatedRoute.params.subscribe(params => {
            this.page = params['page'];
        });
        this.LoadData(this.page);
    }

    LoadData(page: Number) {
        this.storeservice.GetListStore(page).subscribe((res: any) => {
            this.stores = res;
        }, error => {
            console.log(error);
        });
    }

     
}