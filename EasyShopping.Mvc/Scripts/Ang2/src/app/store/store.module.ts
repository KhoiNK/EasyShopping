import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { routing } from './store.routing';
import { StoreListComponent } from './store-list.component';
import { StoreDetailComponent } from './store-detail.component';
import { StoreListByUserComponent } from './store-listbyuserid.component';
import { StoreComponent } from './store.component';
import { StoreAddComponent } from './store-add.component';
import { StoreApproveList } from './store-approve-list.component';
import { StoreEditComponent } from './store-edit.component';
import { StoreUpgradeComponent } from './store-upgrade.component';
import { StoreIntroComponent } from './store-intro.component';

//import { GoogleMapComponent } from '../commonService/google-map.component';


@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        routing
    ],
    declarations: [
        StoreComponent,
        StoreListComponent,
        StoreDetailComponent,
        StoreListByUserComponent,
        StoreAddComponent,
        StoreApproveList,
        StoreEditComponent,
        StoreUpgradeComponent,
        StoreIntroComponent
        //GoogleMapComponent
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export class StoreModule { }