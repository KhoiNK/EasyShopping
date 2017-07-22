import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { StoreServices } from './store.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductAddComponent } from '../product/product-add.component';
import { OrderServices } from '../order/order.service';
import { PartnerService } from '../partner/partner.service';
import { CountryServices } from '../country/country.service';
import { ProductTypeService } from '../product/product-type.service';
import { ProductService } from '../product/product.service';
import { Base64EncodeService } from '../upload/base64Encode.service';
import { UploadService } from '../upload/upload-image.service';

@Component({
    selector: 'store-detail',
    templateUrl: 'Store/StoreDetail',
    providers: [
        StoreServices,
        OrderServices,
        PartnerService,
        CountryServices,
        ProductTypeService,
        Base64EncodeService,
        UploadService,
    ]
})
export class StoreDetailComponent implements OnInit {
    public CART: string = "cart";
    public id: number;
    public store: any;
    public subscription: Subscription;
    public isOwner: boolean = false;
    public isAllowed: boolean = false;
    public product: any;
    public countries: any[];
    public types: any[];
    constructor(private storeservice: StoreServices
        , private activatedRoute: ActivatedRoute
        , private orderService: OrderServices
        , private partnerService: PartnerService
        , private router: Router;
        //, private countryService: CountryServices
        //, private productTypeService: ProductTypeService
        //, private productservice: ProductService
        //, private el: ElementRef
        //, private router: Router
        //, private b64: Base64EncodeService
        /*, private uploadservice: UploadService*/) {
        this.store = {};
        this.product = {};
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
            //this.LoadData(this.id);
        });
        this.storeservice.CheckOwner(this.id).subscribe(res => {
            if (res == true == true) {
                this.isOwner = true;
            }
        }, err => {
            console.log(err);
            //this.LoadData(this.id);
        });
        //this.countryService.GetCountryList().subscribe((res: any) => this.countries = res);
        //this.productTypeService.GetList().subscribe((res: any) => this.types = res);
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
    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    SetRecruitment() {
        this.store.IsRecruiting = true;
        this.storeservice.EditStore(this.store).subscribe((res: any) => {
            if (res == true) {
                alert("Update successfully!");
                this.router.navigate['/stores/store-detail/' + this.store.ID];
            }
        }, err => {
            console.log(err);
        });
    }

    DisableRecruitment() {
        this.store.IsRecruiting = false;
        this.storeservice.EditStore(this.store).subscribe((res: any) => {
            if (res == true) {
                alert("Update successfully!");
                this.router.navigate['/stores/store-detail/' + this.store.ID];
            }
        }, err => {
            console.log(err);
        });
    }
    //saveProduct() {
    //    this.product.StoreID = this.id;
    //    let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
    //    if (inputEl.files.length > 0) {
    //        let file: File = inputEl.files[0];
    //        let thumbailImg: string = this.b64.GetB64(file);
    //        this.uploadservice.UploadImage(thumbailImg).subscribe((res: any) => {
    //            this.product.ThumbailLink = res.data.link;
    //            this.product.ThumbailCode = res.data.id;
    //            this.AddProduct(this.product);
    //        });
    //    }
    //    this.AddProduct(this.product);
    //}

    //AddProduct(product: any) {
    //    this.productservice.AddProduct(product).subscribe(
    //        (res: any) => {
    //            if (res.ID) {
    //                alert("Added successfully");
    //                this.router.navigate(['/stores/store-detail/' + product.StoreID]);
    //            }
    //        }, err => {
    //            console.log(err);
    //        });
    //}
}