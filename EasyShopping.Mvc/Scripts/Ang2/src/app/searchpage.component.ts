import { Component, OnInit, ElementRef, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ProductService } from '../app/product/product.service';
import { StoreServices } from '../app/store/store.service';

@Component({
    selector: 'my-searchpage',
    templateUrl: '/Home/SearchPage',
    providers: [ProductService, StoreServices]
})

export class SearchPageComponent implements OnInit{
    public products: any[];
    public stores: any[];
    public searchname: string;
    public subscription: Subscription;

    constructor(private productservice: ProductService
        , private activateRoute: ActivatedRoute
        , private storeservice: StoreServices
        , private router: Router) {

    }

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.searchname = params['searchname'];
            this.LoadProduct(this.searchname);
            this.LoadStore(this.searchname);
        });
    }

    LoadProduct(name: string) {
        name = this.searchname;
        this.productservice.GetByName(name).subscribe((res: any) => {
            if (res) {
                this.products = res;
            }
            else {
                this.products = [];
            }
        }, err => {
            console.log(err);
        });
    }

    LoadStore(name: string) {
        name = this.searchname;
        this.storeservice.GetListStore(name).subscribe((res: any) => {
            if (res) {
                this.stores = res;
            }
            else {
                this.stores = [];
            }
        }, err => {
            console.log(err);
        });
    }

}