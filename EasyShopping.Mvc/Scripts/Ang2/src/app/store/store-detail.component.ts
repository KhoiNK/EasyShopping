import { Component, OnInit, OnDestroy } from '@angular/core';
import { StoreServices } from './store.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'store-detail',
    templateUrl: 'Store/StoreDetail',
    providers: [StoreServices]
})

export class StoreDetailComponent implements OnInit, OnDestroy {
    public id: number;
    public store: any;
    public subscription: Subscription;
    constructor(private storeservice: StoreServices, private activatedRoute: ActivatedRoute) {
        this.store = {};
    }

    ngOnInit() {
        this.subscription = this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.storeservice.GetStoreById(this.id)
            .subscribe(res => {
                this.store = res;
            })
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
    
}