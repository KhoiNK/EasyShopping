import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { OrderServices } from '../order/order.service';
import { ProductTypeService } from './product-type.service';

@Component({
    selector: 'product-detail',
    templateUrl: 'Product/ProductDetail',
    providers: [ProductService, OrderServices, ProductTypeService]
})

export class ProductDetailComponent implements OnInit, OnDestroy {
    public id: number;
    public product: any;
    public subscription: Subscription;
    public sameproducts: any[];
    public CART: string = "cart";
    public PROFILE: string = 'profile';
    public message: string = "";
    public types: any[];

    constructor(private productService: ProductService
        , private activatedRoute: ActivatedRoute
        , private orderService: OrderServices
        , private el: ElementRef
        , private productTypeSrv: ProductTypeService
    ) {
        this.product = {};
    }

    ngOnInit() {
        this.subscription = this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.GetData();
    }

    AddToCart(productId: number) {
        if (localStorage.getItem(this.PROFILE) == {}) {
            alert("please login first!");
            event.preventDefault();
        }
        else {
            let order: any = {};
            let cartId = localStorage.getItem(this.CART);
            if (cartId == null) {
                order.productId = productId;
                this.orderService.AddToCart(order).subscribe((res: any) => {
                    localStorage.setItem(this.CART, JSON.stringify(res.ID));
                    this.SetMessage();
                }, err => {
                    this.SetErrMess("Please login first");
                    console.log(err);
                });
            } else {
                order.productId = productId;
                order.cartId = cartId;
                this.orderService.AddToCart(order).subscribe((res: any) => {
                    if (JSON.stringify(res) == 'true') {
                        this.SetMessage();
                    }
                }, err => {
                    this.SetErrMess("Added failed");
                    console.log(err);
                });
            }
        }
    }

    SetMessage() {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#cartMess');
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

    GetData() {
        this.productService.GetDetail(this.id).subscribe((res: any) => {
            if (res.ID) {
                this.product = res;
                this.GetSameData("" + this.product.Name);
            }
        }, err => {
            console.log(err);
        });

        this.productTypeSrv.GetList().subscribe((res: any) => {
            this.types = res;
        }, err => {
            console.log(err);
        });
    }

    GetSameData(name: string) {
        this.productService.GetByName(name).subscribe((res: any) => {
            if (res) {
                this.sameproducts = res;
            }
        }, err => {
            console.log(err);
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}