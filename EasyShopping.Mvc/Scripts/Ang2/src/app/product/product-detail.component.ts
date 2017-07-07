import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'product-detail',
    templateUrl: 'Product/ProductDetail',
    providers: [ProductService]
})

export class ProductDetailComponent implements OnInit, OnDestroy {
    public id: number;
    public product: any;
    public subscription: Subscription;

    constructor(private productService: ProductService
        , private activatedRoute: ActivatedRoute
    ) {
        this.product = {};
    }

    ngOnInit() {
        this.subscription = this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.productService.GetDetail(this.id).subscribe(res => { return this.product = res; });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}