import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ApprovementServices } from '../approvement/approve-product.service';
import {StoreServices } from '../store/store.service';

@Component({
    selector: 'product-approve-list',
    templateUrl: "/Product/ApproveList",
    providers: [ProductService, ApprovementServices, StoreServices]
})

export class ProductApproveListComponent implements OnInit {
    public products: any[];
    public subscription: Subscription;
    private id: number;
    public isAllowed: boolean = false;
    public isOwner: boolean = false;
    constructor(private productService: ProductService
        , private router: Router
        , private activateRoute: ActivatedRoute
        , private approveSrv: ApprovementServices
        , private storeservice: StoreServices) {

    }

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.LoadData(this.id);
        this.storeservice.CheckAllowance(this.id).subscribe(res => {
            if (res == true) {
                this.isAllowed = true;
            }
        }, err => {
            console.log(err);
            //this.LoadData(this.id);
        });
        this.storeservice.CheckOwner(this.id).subscribe(res => {
            if (res == true == true) {
                this.isOwner = true;
            }
        }, err => {
            console.log(err);
            //this.LoadData(this.id);
        });
    }

    LoadData(id: number) {
        this.productService.GetApproveList(id).subscribe((res: any) => {
            this.products = res;
        }, err => {
            console.log(err);
        });
    }

    Approve(id: number) {
        this.approveSrv.ApproveProduct(id).subscribe((res: any) => {
            if (res == true) {
                alert("Approve successfully!");
                this.LoadData(this.id);
            }
            else {
                alert("Approvement failed!");
            }
        }, err => {
            console.log(err);
        });
    }

}