import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { StoreServices } from './store.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'store-approve-list',
    templateUrl: 'Store/ApproveList',
    providers: [StoreServices]
})

export class StoreApproveList {
    public searchkey: number;
    public store: any;

    constructor(private storeService: StoreServices, private router: Router) {
        this.store = {};
    }

    SearchStore() {
        if (this.searchkey != 0) {
            this.storeService.GetStoreById(this.searchkey).subscribe((res: any) => {
                this.store = res;
            }, err => {
                console.log(err);
                alert("Can not find this store");
            });
        }
    }

    Approve(id: number) {
        this.storeService.Approve(id).subscribe((res: any) => {
            if (res == true) {
                alert("Approve Successfully!");
                this.router.navigate['/stores/store-approve-list'];
            }
        }, err => {
            console.log(err);
            
        });
    }
}