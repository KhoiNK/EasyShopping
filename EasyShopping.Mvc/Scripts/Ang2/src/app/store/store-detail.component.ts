import { Component, OnInit, OnDestroy} from '@angular/core';
import { StoreServices } from './store.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ProductAddComponent } from '../product/product-add.component';
import { OrderServices } from '../order/order.service';
import { PartnerService } from '../partner/partner.service';


@Component({
    selector: 'store-detail',
    templateUrl: 'Store/StoreDetail',
    providers: [StoreServices, OrderServices]
})
export class StoreDetailComponent implements OnInit, OnDestroy {
    public CART: string = "cart";
    public id: number;
    public store: any;
    public subscription: Subscription;
    public isOwner: boolean = false;
    public isAllowed: boolean = false;
    constructor(private storeservice: StoreServices
        , private activatedRoute: ActivatedRoute
        , private orderService: OrderServices
        , private partnerService: PartnerService) {
        this.store = {};
    }

    ngOnInit() {
        this.subscription = this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.LoadData(this.id);
        this.storeservice.CheckAllowance(this.id).subscribe(res => {
            if (res == true) {
                this.isAllowed = true;
            }
        }, err => {
            console.log(err);
        });
        this.storeservice.CheckOwner(this.id).subscribe(res => {
            if (res == true == true) {
                this.isOwner = true;
            }
        }, err => {
            console.log(err);
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    Apply() {
        this.partnerService.Apply(this.store.ID).subscribe((res: any) => {
            if (res == true) {
                alert("Apply Succesfully!");
                this.LoadData(this.id);
            }
        }, err => {
            console.log(err);
        });
    }

    LoadData(id: number) {
        this.storeservice.GetStoreById(id)
            .subscribe(res => {
                return this.store = res;
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