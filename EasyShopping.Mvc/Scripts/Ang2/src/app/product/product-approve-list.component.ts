import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { Router } from '@angular/router';

@Component({
    selector: 'product-approve-list',
    templateUrl: "/Product/ApproveList",
    providers: [ProductService]
})

export class ProductApproveListComponent implements OnInit {
    public products: any[];
    constructor(private productService: ProductService, private router: Router) {

    }

    ngOnInit() {
        this.LoadData();
    }

    LoadData() {
        this.productService.GetApproveList().subscribe((res: any) => {
            this.products = res;
        }, err => {
            console.log(err);
        });
    }

    Approve(id: number) {
        this.productService.Approve(id).subscribe((res: any) => {
            if (res.status == 200) {
                this.router.navigate(['/products/product-approve-list']);
            }
        });
    }
    
}