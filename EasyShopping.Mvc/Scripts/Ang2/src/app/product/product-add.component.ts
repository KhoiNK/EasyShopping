import { Component, OnInit, ElementRef, OnDestroy, Input } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UploadService } from '../upload/upload-image.service';
import { CountryServices } from '../country/country.service';
import { ProductTypeService } from './product-type.service';
import { Base64EncodeService } from '../upload/base64Encode.service';
import { Observable } from 'rxjs/Observable';

//import { FirebaseService } from '../firebase/firebase.service';
//import { Upload } from '../firebase/Upload';

@Component({
    selector: 'product-add',
    templateUrl: 'Product/AddProduct',
    providers: [ProductService, UploadService, CountryServices, ProductTypeService, Base64EncodeService]
})

export class ProductAddComponent implements OnInit, OnDestroy {
    public product: any;
    public countries: any[];
    public types: any[];
    private subscription: Subscription;
    public storeId: number;
    public message: string;
    public thumbailImg: string;
    public productType: string = "";
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
            this.storeId = params['storeId'];
        });
    }

    setCountryId(countryid: Number) {
        this.product.ManufacturedCountryID = countryid;
    }

    SetType() {
        this.productType = this.types.filter(x => x.ID == this.product.ProductTypeID)[0].Name;
    }

    setThumbailImg() {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        let file: File = inputEl.files[0];
        var reader = new FileReader();
        reader.onload = this._handleReaderLoaded.bind(this);
        reader.readAsBinaryString(file);
    }

    _handleReaderLoaded(readerEvt: any) {
        var binaryString = readerEvt.target.result;
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#previewImg');
        inputEl.setAttribute('src', "data:image/jpeg;base64," + btoa(binaryString));
        this.thumbailImg = btoa(binaryString);
        //console.log(btoa(binaryString));
    }

    saveProduct() {
        this.product.StoreID = this.storeId;
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        if (inputEl.files[0] == null) {
            alert("Add image please!");
            return false;
        }
        if (inputEl.files.length > 0) {
            let file: File = inputEl.files[0];
            
            this.uploadservice.UploadImage(this.thumbailImg).subscribe((res: any) => {
                this.product.ThumbailLink = res.data.link;
                this.product.ThumbailCode = res.data.id;
                this.AddProduct(this.product);
            }, err => {
                return false;
            });

        }
        else {
            this.AddProduct(this.product);
        }
    }

    AddProduct(product: any) {
        this.productservice.AddProduct(product).subscribe(
            (res: any) => {
                if (res.ID) {
                    this.SetMessage();
                    setTimeout(() => {
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