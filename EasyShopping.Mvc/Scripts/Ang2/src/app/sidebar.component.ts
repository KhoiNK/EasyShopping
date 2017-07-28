import { Component, OnInit } from '@angular/core';
import { ProductTypeService } from '../app/product/product-type.service';
@Component({
    selector: 'my-sidebar',
    templateUrl: 'Home/SideBar'
})

export class SideBar implements OnInit {
    public types: any[];
    constructor(private typeSrv: ProductTypeService) {

    }

    ngOnInit() {
        this.typeSrv.GetWithTarget().subscribe((res: any) => {
            if (res) {
                this.types = res;
            }
        }, err => {
            console.log(err);
        });
    }
}