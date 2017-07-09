import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { routing } from './order.routing';
import { OrderComponent } from './order.component';
import { OrderList } from './order-list.component';
import { OrderDetailComponent } from './order-detail.component';

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
        OrderDetailComponent
    ]
})
export class OrderModule { }