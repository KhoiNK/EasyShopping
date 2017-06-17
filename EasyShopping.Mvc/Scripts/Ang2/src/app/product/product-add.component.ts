import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'product-add',
    templateUrl: 'Product/AddProduct',
    providers: [ProductService]
})

export class ProductAddComponent {
    public product: any;
    constructor(private productservice: ProductService, private activateRoute: ActivatedRoute, private router: Router) {
        this.product = {}
    };

    saveProduct() {
        this.productservice.AddProduct(this.product).subscribe(
            (res: any) => {
                if (res != null) {
                    this.router.navigate['/product'];
                }
            }, err => {
                console.log(err);
            });
    }
}