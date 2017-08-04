import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CountryServices } from '../country/country.service';
import { ProductTypeService } from './product-type.service';
import { NgForm } from '@angular/forms';
import { Base64EncodeService } from '../upload/base64Encode.service';
import { UploadService } from '../upload/upload-image.service';

@Component({
    selector: 'product-add',
    templateUrl: 'Product/Edit',
    providers: [ProductService, UploadService, CountryServices, ProductTypeService, Base64EncodeService]
})

export class ProductEditComponent implements OnInit, OnDestroy {
    public product: any;
    public countries: any[];
    public types: any[];
    private subscription: Subscription;
    public id: number;
    public message: string = "";
    private b64String: string;
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
            this.id = params['id'];
        });
        this.productservice.GetDetail(this.id).subscribe((res: any) => {
            if (res.ID) {
                this.product = res;
            }
            else {
                alert("Cannot find this product");
            }
        }, err => {
            alert("Cannot find this product");
        });
    }

    setB64() {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        let file: File = inputEl.files[0];
        var reader = new FileReader();
        reader.onload = this._handleReaderLoaded.bind(this);
        console.log(this);
        reader.readAsBinaryString(file);
    }

    _handleReaderLoaded(readerEvt: any) {
        var binaryString = readerEvt.target.result;
        this.b64String = btoa(binaryString);
        //console.log(btoa(binaryString));
    }

    SaveChange() {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        if (inputEl.files.length > 0) {
            let file: File = inputEl.files[0];
            return new Promise<void>((resolve) => {
                resolve(this.setB64());
            }).then(result => {
                this.uploadservice.UploadImage(this.b64String).subscribe((res: any) => {
                    this.product.ThumbailLink = res.data.link;
                    this.product.ThumbailCode = res.data.id;
                    this.EditProduct(this.product);
                }, err => {
                    alert("Your uploaded file is wrong format");
                    return false;
                });
            }, err => {
                console.log(err);
            }).catch(err => console.log(err));
            //let thumbailImg: string = this.b64.GetB64(file);
        }
        else {
            this.EditProduct(this.product);
        }
    }

    EditProduct(product: any) {
        this.productservice.Edit(product).subscribe(
            (res: any) => {
                if (res == true) {
                    this.SetMessage();
                    setTimeout(result => {
                        this.router.navigate(['/stores/store-detail/' + product.StoreID]);
                    }, 3000);
                }
            }, err => {
                this.SetErrMess("Added failed");
                console.log(err);
            });
    }

    SetMessage() {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#succMess');
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

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}