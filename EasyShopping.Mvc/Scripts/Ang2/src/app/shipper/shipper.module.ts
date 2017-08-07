import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { routing } from './shipper.routing';
import { ShipperComponent } from './shipper.component';
import { ShipperApplyComponent } from './shipper-apply.component';
import { ShipperListComponent } from './shipper-approve.component';
import { ShipperGetByStoreComponent } from './shipper-getbystoreId.component';
import { ShipperIntroComponent } from './shipper-intro.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        routing
    ],
    declarations: [
        ShipperComponent,
        ShipperGetByStoreComponent,
        ShipperListComponent,
        ShipperApplyComponent,
        ShipperIntroComponent
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ]
})
export class ShipperModule { }
