import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { OrderServices } from '../order/order.service';

@Component({
    selector: 'product-list',
    templateUrl: '/Product',
    providers: [ProductService, OrderServices]
})

export class ProductListComponent implements OnInit {
    public products: any[];
    public CART: string = "cart";
    constructor(private productservice: ProductService, private orderService: OrderServices) {
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.productservice.GetList().subscribe((res: any) => {
            this.products = res;
        }, err => {
            console.log(err);
        });
    }
    AddToCart(productId: number) {
        let order: any = {};
        let cartId = localStorage.getItem(this.CART);
        if (cartId == null) {
            order.productId = productId;
            this.orderService.AddToCart(order).subscribe((res: any) => {
                localStorage.setItem(this.CART, JSON.stringify(res.ID));
                alert("Added Successfully");
            }, err => {
                console.log(err);
            });
        } else {
            order.productId = productId;
            order.cartId = cartId;
            this.orderService.AddToCart(order).subscribe((res: any) => {
                if (JSON.stringify(res) == 'true') {
                    alert("Added Successfully into order ID: " + cartId);
                }
            }, err => {
                console.log(err);
            });
        }
    }
}