import { Component, OnInit, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { StoreServices } from './store.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'store-approve-list',
    templateUrl: 'Store/ApproveList',
    providers: [StoreServices]
})

export class StoreApproveList implements OnInit {
    public searchkey: string;
    public storesList: any[];
    public stores: any[];
    public subscription: Subscription;
    public page: number;

    constructor(private storeService: StoreServices, private router: Router, private el: ElementRef) {
        this.page = 1;
    }

    ngOnInit() {
        this.LoadData();
    }

    LoadData() {
        this.storeService.GetList(this.page).subscribe((res: any) => {
            this.storesList = res;
            this.stores = res;
        }, error => {
            console.log(error);
        });
    }

    SearchStore() {
        let search = this.searchkey;
        if (this.searchkey == "" || this.searchkey == null || this.searchkey == undefined) {
            this.stores = this.storesList;
        } else {
            this.stores = this.storesList.filter(x => (x.Name == this.searchkey) || (x.ID == this.searchkey));
        }
    }

    Approve(id: number) {
        this.storeService.Approve(id).subscribe((res: any) => {
            if (res == true) {
                let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#approveMess');
                inputEl.removeAttribute('hidden');
                setTimeout(() => {
                    this.LoadData();
                }, 2000);
                
            }
        }, err => {
            console.log(err);
            
        });
    }
}