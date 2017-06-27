import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { routing } from './store.routing';
import { StoreListComponent } from './store-list.component';
import { StoreDetailComponent } from './store-detail.component';
import { StoreListByUserComponent } from './store-listbyuserid.component';
import { StoreComponent } from './store.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        routing,
    ],
    declarations: [
        StoreComponent,
        StoreListComponent,
        StoreDetailComponent,
        StoreListByUserComponent,
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ]
})

export class StoreModule { }