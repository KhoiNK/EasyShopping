import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';

@Component({
    selector: 'product-list',
    templateUrl: '/Product',
    providers: [ProductService]
})

export class ProductListComponent {
    public products: any;

    constructor(private productservice: ProductService) {
        this.products = {};
    }

    //loadData(storeid: number) {
    //    this.productservice.GetList(storeid).subscribe((res: any) => {
    //        this.products = res;
    //    });
    //}
}