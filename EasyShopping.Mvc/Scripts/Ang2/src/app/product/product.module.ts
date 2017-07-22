import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { routing } from './product.routing';
import { ProductListComponent } from './product-list.component';
import { ProductComponent } from './product.component';
import { ProductAddComponent } from './product-add.component';
import { ProductApproveListComponent } from './product-approve-list.component';
import { ProductDetailComponent } from './product-detail.component';
import { ProductEditComponent } from './product-edit.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        routing

    ],
    declarations: [
        ProductAddComponent,
        ProductComponent,
        ProductListComponent,
        ProductApproveListComponent,
        ProductDetailComponent,
        ProductEditComponent
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ]
})

export class ProductModule { }