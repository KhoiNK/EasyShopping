import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'product-approve-list',
    templateUrl: "/Product/ApproveList",
    providers: [ProductService]
})

export class ProductApproveListComponent implements OnInit {
    public products: any[];
    public subscription: Subscription;
    private id: number;
    constructor(private productService: ProductService, private router: Router, private activateRoute: ActivatedRoute) {

    }

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.LoadData(this.id);
    }

    LoadData(id: number) {
        this.productService.GetApproveList(id).subscribe((res: any) => {
            this.products = res;
        }, err => {
            console.log(err);
        });
    }

    Approve(id: number) {
        this.productService.Approve(id).subscribe((res: any) => {
            if (res == true) {
                this.router.navigate(['/products/product-approve-list']);
            }
            alert("Approvement failed!");
        }, err => {
            console.log(err);
        });
    }

}