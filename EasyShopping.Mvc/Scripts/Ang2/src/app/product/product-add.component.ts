import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FirebaseService } from '../firebase/firebase.service';
import { Upload } from '../firebase/Upload';

@Component({
    selector: 'product-add',
    templateUrl: 'Product/AddProduct',
    providers: [ProductService, FirebaseService]
})

export class ProductAddComponent {
    public product: any;
    private storeid: number;
    private files: FileList;
    private uploadFile: Upload;
    constructor(private productservice: ProductService
        , private activateRoute: ActivatedRoute
        , private router: Router
        , private afSvc: FirebaseService
    ) {
        this.product = {};
    };

    setStoreId(id: number) {
        this.product.StoreID = id;
    }

    saveProduct(event: Event) {
        let formData: FormData = new FormData();
        formData.append('files', FileList[0], FileList[0].name);
        this.uploadFile = event.target['files'][0];
        this.afSvc.pushUpload(this.uploadFile, this.product.StoreID);
        //this.files = event.target.files;
        let file = this.files.item[0];
        this.productservice.AddProduct(this.product).subscribe(
            (res: any) => {
                if (res != null) {
                    this.router.navigate['/product'];
                }
            }, err => {
                console.log(err);
            });
    }
}