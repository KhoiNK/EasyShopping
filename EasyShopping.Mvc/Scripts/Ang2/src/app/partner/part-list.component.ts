import { Component, OnInit, ElementRef, OnDestroy, Input } from '@angular/core';
import { PartnerService } from './partner.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'product-add',
    templateUrl: 'Partner/PartnerList',
    providers: [PartnerService]
})

export class partnerListComponent implements OnInit, OnDestroy {
    private subscription: Subscription;
    public partners: any[];
    public storeId: number;
    public message: string;

    constructor(private partnerservice: PartnerService
        , private activateRoute: ActivatedRoute
        , private router: Router
        , private el: ElementRef) {

    };

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.storeId = params['id'];
        });
        this.LoadData();
    }

    LoadData() {
        this.partnerservice.GetList(this.storeId).subscribe((res: any) => {
            this.partners = res;
        }, err => {
            console.log(err);
        });
    }

    Approve(id: number) {
        this.partnerservice.Approve(id).subscribe((res: any) => {
            if (res == true) {
                alert("Approve successfully!");
                this.LoadData();
            }
        }, err => {
            console.log(err);
        })
    }

    RemovePartner(id: number) {
        var result = confirm("Are you sure want to remove this partner?");
        if (result) {
            this.partnerservice.RemovePartner(id).subscribe((res: any) => {
                if (res == true) {
                    this.SetMessage("Updated successfully!");
                    this.LoadData();
                }
                else {
                    alert("Remove failed!");
                }
            }, err => {
                console.log(err);
            });
        }
    }

    SetMessage(mess: string) {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#cartMess');
        this.message = mess;
        inputel.removeAttribute('hidden');
        setTimeout(() => {
            inputel.hidden = true;
        }, 1000);
    }

    SetErrMess(mess: string) {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#errMess');
        this.message = mess;
        inputel.removeAttribute('hidden');
        setTimeout(() => {
            inputel.hidden = true;
        }, 5000);
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}