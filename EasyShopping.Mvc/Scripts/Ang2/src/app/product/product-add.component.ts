import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UploadService } from '../upload/upload-image.service';
import { CountryServices } from '../country/country.service';
import { ProductTypeService } from './product-type.service';
import { NgForm } from '@angular/forms';
import { Base64EncodeService } from '../upload/base64Encode.service';

//import { FirebaseService } from '../firebase/firebase.service';
//import { Upload } from '../firebase/Upload';

@Component({
    selector: 'product-add',
    templateUrl: 'Product/AddProduct',
    providers: [ProductService, UploadService, CountryServices, ProductTypeService, Base64EncodeService]
})

export class ProductAddComponent implements OnInit, OnDestroy {
    public product: any;
    public storeid: number;
    public countries: any[];
    public types: any[];
    private subscription: Subscription;
    constructor(private productservice: ProductService
        , private activateRoute: ActivatedRoute
        , private router: Router
        , private uploadservice: UploadService
        , private countryService: CountryServices
        , private productTypeService: ProductTypeService
        , private el: ElementRef
        , private b64: Base64EncodeService
    ) {
        this.product = {};
    };

    ngOnInit() {
        this.countryService.GetCountryList().subscribe((res: any) => this.countries = res);
        this.productTypeService.GetList().subscribe((res: any) => this.types = res);
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.storeid = params['storeId'];
        });
    }

    setCountryId(countryid: Number) {
        this.product.ManufacturedCountryID = countryid;
    }

    saveProduct() {
        this.product.StoreID = this.storeid;
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        let file: File = inputEl.files[0];
        let thumbailImg: string = this.b64.GetB64(file);
        this.uploadservice.UploadImage(thumbailImg).subscribe((res: any) => {
            this.product.ThumbailLink = res.data.link;
            this.product.ThumbailCode = res.data.id;
            this.productservice.AddProduct(this.product).subscribe(
                (res: any) => {
                    if (res.ID) {
                        alert("Added successfully");
                        this.router.navigate(['/stores/store-detail/' + this.product.StoreID]);
                    }
                }, err => {
                    console.log(err);
                });
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}