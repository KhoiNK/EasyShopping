import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs/Observable';
import { StoreServices } from './store.service';

@Component({
    selector: 'store-upgrade',
    templateUrl: 'Store/UpgradeStore',
    providers: [StoreServices]
})

export class StoreUpgradeComponent implements OnInit, OnDestroy {
    public storeId: number;
    public store: any;
    public subcription: Subscription;
    public package: any;
    public message: string;
    public packageid: number;
    public slot: number;

    constructor(private storeSrv: StoreServices, private el: ElementRef, private activatedRoute: ActivatedRoute) {
        this.store = {};
        this.package = {};
    }

    ngOnInit() {
        this.subcription = this.activatedRoute.params.subscribe(params => {
            this.storeId = params['id'];
        });
        this.LoadStore();
    }

    LoadStore() {
        this.storeSrv.GetStoreById(this.storeId).subscribe((res: any) => {
            this.store = res;
        }, err => {
            console.log(err);
        });
    }

    SetPackage(id: number, slot: number) {
        this.packageid = id;
        this.slot = slot;
    }

    Purchase() {
        this.package.ObjectID = this.storeId;
        this.package.PackageID = this.packageid;
        this.storeSrv.UpgradeStore(this.package).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Purchased successfully!");
                setTimeout(() => {
                    window.location.reload();
                }, 2000);
            }
        }, err => {
            this.SetErrMessage("Purchased failed!");
            setTimeout(() => {
                window.location.reload();
            }, 2000);
            console.log(err);
        });
    }

    SetMessage(mess: string) {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#succMess');
        inputEl.removeAttribute('hidden');
        this.message = mess;
        setTimeout(() => {
            inputEl.hidden = true;
        }, 3000);
    }

    SetErrMessage(mess: string) {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#errMess');
        inputEl.removeAttribute('hidden');
        this.message = mess;
        setTimeout(() => {
            inputEl.hidden = true;
        }, 3000);
    }

    ngOnDestroy() {
        this.subcription.unsubscribe();
    }
}