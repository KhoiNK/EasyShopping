import { Component, OnInit, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { StoreServices } from './store.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'store-approve-list',
    templateUrl: 'Store/ApproveList',
    providers: [StoreServices]
})

export class StoreApproveList {
    public searchkey: number;
    public store: any;

    constructor(private storeService: StoreServices, private router: Router, private el: ElementRef) {
        this.store = {};
    }

    SearchStore() {
        if (this.searchkey != 0) {
            this.storeService.GetStoreById(this.searchkey).subscribe((res: any) => {
                this.store = res;
            }, err => {
                let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#errorMess');
                inputEl.removeAttribute('hidden');
                console.log(err);
            });
        }
    }

    Approve(id: number) {
        this.storeService.Approve(id).subscribe((res: any) => {
            if (res == true) {
                let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#approveMess');
                inputEl.removeAttribute('hidden');
                setTimeout(() => {
                    window.location.reload();
                }, 2000);
                
            }
        }, err => {
            console.log(err);
            
        });
    }
}