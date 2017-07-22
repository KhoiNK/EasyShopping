import { Component, ViewChild } from '@angular/core';
import { ProductListComponent } from '../app/product/product-list.component';

@Component({
    selector: 'my-app',
    template: `
    <my-header></my-header>

    <div class="container body-content">
        <my-sidebar></my-sidebar>
        <router-outlet></router-outlet>
    </div>
    <my-footer></my-footer>
  `,
})
export class AppComponent {
    public searchkey: string;

    @ViewChild(ProductListComponent) productList: ProductListComponent;

    LoadData() {
        this.productList.LoadWithName(this.searchkey);
    }
}
