import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UploadService } from '../upload/upload-image.service';
import { CountryServices } from '../country/country.service';
//import { FirebaseService } from '../firebase/firebase.service';
//import { Upload } from '../firebase/Upload';

@Component({
    selector: 'product-add',
    templateUrl: 'Product/AddProduct',
    providers: [ProductService, UploadService, CountryServices]
})

export class ProductAddComponent implements OnInit {
    public product: any;
    private storeid: number;
    private files: FileList;
    private countries: any[];
    //private uploadFile: Upload;
    constructor(private productservice: ProductService
        , private activateRoute: ActivatedRoute
        , private router: Router
        , private uploadservice: UploadService
        , private countryService: CountryServices
        //, private afSvc: FirebaseService
    ) {
        this.product = {};
    };

    ngOnInit() {
        this.countryService.GetCountryList().subscribe(res => this.countries = res);
    }

    setStoreId(id: number) {
        this.product.StoreID = id;
    }

    setCountryId(countryid: number) {
        this.product.ManufacturedCountryID = countryid;
    }

    saveProduct(event: Event) {
        //let formData: FormData = new FormData();
        //formData.append('files', FileList[0], FileList[0].name);
        let thumbailImg: File = event.target['files'][0];
        this.uploadservice.UploadImage(thumbailImg).subscribe((res: any) => {
            this.product.ThumbailLink = res.data.link;
            this.product.ThumbailCode = res.data.id;
            this.productservice.AddProduct(this.product).subscribe(
                (res: any) => {
                    if (res != null) {
                        this.router.navigate['/product'];
                    }
                }, err => {
                    console.log(err);
                });
        });
        //this.afSvc.pushUpload(this.uploadFile, this.product.StoreID);
        //this.files = event.target.files;
    }
}