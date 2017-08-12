import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { OrderServices } from '../order/order.service';
import { ProductTypeService } from './product-type.service';
import { order } from '../order/Order';

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
    public cart: order = new order();

    constructor(private productService: ProductService
        , private activatedRoute: ActivatedRoute
        , private orderService: OrderServices
        , private el: ElementRef
        , private productTypeSrv: ProductTypeService
        , private router: Router
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
        let order: any = {};
        let cart = localStorage.getItem(this.CART);
        if (cart == null) {
            order.productId = productId;
            this.orderService.AddToCart(order).subscribe((res: any) => {
                localStorage.setItem(this.CART, JSON.stringify(res.ID));
                this.cart.cartID = res.ID;
                this.cart.products.push(productId);
                localStorage.setItem("order", JSON.stringify(this.cart));
                this.SetMessage();
            }, err => {
                this.SetErrMess("Please login first");
                setTimeout(() => {
                    this.router.navigate(['/login']);
                }, 3000);
                console.log(err);
            });
        } else {
            order.productId = productId;
            order.cartId = cart;
            let oldcart = JSON.parse(localStorage.getItem("order"));
            this.orderService.AddToCart(order).subscribe((res: any) => {
                let existproduct: any[] = oldcart.products;
                if (JSON.stringify(res) == 'true') {
                    if (existproduct.some(x => x == productId) == false) {
                        oldcart.products.push(productId);
                        localStorage.setItem("order", JSON.stringify(oldcart));
                    }
                    this.SetMessage();
                }
            }, err => {
                console.log(err);
            });
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