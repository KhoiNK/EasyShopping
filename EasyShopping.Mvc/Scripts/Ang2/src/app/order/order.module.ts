import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { routing } from './order.routing';
import { OrderComponent } from './order.component';
import { OrderList } from './order-list.component';
import { OrderDetailComponent } from './order-detail.component';
import { CheckOutComponent } from './order-checkout.component';

@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        //ContactModule,
        routing
    ],
    declarations: [
        OrderComponent,
        OrderList,
        OrderDetailComponent,
        CheckOutComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class OrderModule { }