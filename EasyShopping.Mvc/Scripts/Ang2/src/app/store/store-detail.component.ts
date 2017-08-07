import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { StoreServices } from './store.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderServices } from '../order/order.service';
import { PartnerService } from '../partner/partner.service';
import { ProductService } from '../product/product.service';
import { ProductTypeService } from '../product/product-type.service';

@Component({
    selector: 'store-detail',
    templateUrl: 'Store/StoreDetail',
    providers: [
        StoreServices,
        OrderServices,
        PartnerService,
        ProductService,
        ProductTypeService
    ]
})
export class StoreDetailComponent implements OnInit {
    public CART: string = "cart";
    public id: number;
    public store: any;
    public subscription: Subscription;
    public isOwner: boolean = false;
    public isAllowed: boolean = false;
    public isApplied: boolean = false;
    private command: string = "";
    public message: string = "";
    public tempId: number = 0;
    public types: any[];

    constructor(private storeservice: StoreServices
        , private activatedRoute: ActivatedRoute
        , private orderService: OrderServices
        , private partnerService: PartnerService
        , private router: Router
        , private productservice: ProductService
        , private el: ElementRef
        , private productTypeSrv: ProductTypeService
    ) {
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

        this.partnerService.IsApplied(this.id).subscribe(res => {
            this.isApplied = res;
        }, err => {
            console.log(err);
        });


    }

    Apply() {
        this.partnerService.Apply(this.store.ID).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Applied successfully!");
                window.location.reload();
            }
        }, err => {
            console.log(err);
        });
    }

    SetCommand(command: string, id: number) {
        this.command = command;
        this.tempId = id;
    }

    LoadData(id: number) {
        this.storeservice.GetStoreById(id)
            .subscribe(res => {
                return this.store = res;
            }, err => {
                console.log(err);
            });

        this.productTypeSrv.GetList().subscribe((res: any) => {
            this.types = res;
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
                this.SetMessage("Added successfully!");
            }, err => {
                console.log(err);
            });
        } else {
            order.productId = productId;
            order.cartId = cartId;
            this.orderService.AddToCart(order).subscribe((res: any) => {
                if (JSON.stringify(res) == 'true') {
                    this.SetMessage("Added successfully!");
                }
            }, err => {
                console.log(err);
            });
        }
    }
    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    SetRecruitment() {
        this.store.IsRecruiting = true;
        this.storeservice.EditStore(this.store).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Edited successfully!");
                window.location.reload();
            }
        }, err => {
            console.log(err);
        });
    }

    DisableRecruitment() {
        this.store.IsRecruiting = false;
        this.storeservice.EditStore(this.store).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Edited successfully!");
                window.location.reload();
            }
        }, err => {
            console.log(err);
        });
    }

    Remove() {
        if (this.command == "product") {
            this.productservice.Delete(this.tempId).subscribe((res: any) => {
                if (res == true) {
                    this.SetMessage("Removed successfully!");
                    this.LoadData(this.id);
                }
                else {
                    this.SetMessage("Removed failed!");
                    this.LoadData(this.id);
                }
            }, err => {
                console.log(err);
            });
        }
        if (this.command == "store") {
            this.storeservice.RemoveStore(this.tempId).subscribe((res: any) => {
                if (res == true) {
                    this.SetMessage("Removed successfully!");
                    this.router.navigate(['/']);
                }
            }, err => {
                console.log(err);
            });
        }
    }

    RemoveApply() {
        this.partnerService.RemoveApply(this.id).subscribe((res: any) => {
            if (res == true) {
                this.SetMessage("Removed successful!");
                setTimeout(() => {
                    window.location.reload();
                }, 1000);

            }
            else {
                alert("remove failed!");
            }
        }, err => {
            console.log(err);
        });
    }

    SetMessage(mess: string) {
        this.message = mess;
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#storeMess');
        inputEl.removeAttribute('hidden');
        setTimeout(() => {
            inputEl.hidden = true;
        }, 1000);
    }
}