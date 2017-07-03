﻿import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { routing } from './product.routing';
import { ProductListComponent } from './product-list.component';
import { ProductComponent } from './product.component';
import { ProductAddComponent } from './product-add.component';



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
        ProductListComponent
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ]
})

export class ProductModule { }