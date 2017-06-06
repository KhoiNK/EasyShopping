import { OnInit, Component } from '@angular/core';
import { StoreServices } from './store.service';

@Component({
    selector: 'store-list',
    templateUrl: '/Store',
    providers: [StoreServices]
})
export class StoreListComponent implements OnInit {
    public stores: any[];
    constructor(private storeservice: StoreServices) {

    }

    ngOnInit() {
        this.LoadData();
    }

    LoadData() {
        this.storeservice.GetListStore().subscribe((res: any)=>{
            this.stores = res;
        }, error => {
            console.log(error);
        });
    }
}