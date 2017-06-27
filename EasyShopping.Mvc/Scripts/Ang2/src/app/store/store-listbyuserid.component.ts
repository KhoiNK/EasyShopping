import { Component, OnInit, OnDestroy } from '@angular/core';
import { StoreServices } from './store.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'store-detail-user-id',
    templateUrl: 'Store/StoreListByUser',
    providers: [StoreServices]
})

export class StoreListByUserComponent implements OnInit, OnDestroy {
    public id: number;
    public stores: any[];
    public subscription: Subscription;
    constructor(private storeservice: StoreServices, private activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {
        this.subscription = this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.storeservice.GetStoreByUserId(this.id)
            .subscribe(res => {
                this.stores = res;
            })
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

}