import { Routes, RouterModule } from '@angular/router';
import { ShipperComponent } from './shipper.component';
import { ShipperApplyComponent } from './shipper-apply.component';
import { ShipperListComponent } from './shipper-approve.component';
import { ShipperGetByStoreComponent } from './shipper-getbystoreId.component';

const appRoutes: Routes = [{
    path: '',
    component: ShipperComponent,
    data: {
        title: 'Shipper'
    },
    children: [
        {
            path: '',
            component: ShipperApplyComponent,
        },
        {
            path: 'shipperlist',
            component: ShipperListComponent
        },
        {
            path: 'shipper-store-list/:storeid',
            component: ShipperGetByStoreComponent
        }
    ]
}];
export const routing = RouterModule.forChild(appRoutes);