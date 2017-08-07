import { Routes, RouterModule } from '@angular/router';
import { ShipperComponent } from './shipper.component';
import { ShipperApplyComponent } from './shipper-apply.component';
import { ShipperListComponent } from './shipper-approve.component';
import { ShipperGetByStoreComponent } from './shipper-getbystoreId.component';
import { ShipperIntroComponent} from './shipper-intro.component';

const appRoutes: Routes = [{
    path: '',
    component: ShipperComponent,
    data: {
        title: 'Shipper'
    },
    children: [
        {
            path: '',
            component: ShipperIntroComponent,
        },
        {
            path: 'shipperlist',
            component: ShipperListComponent
        },
        {
            path: 'shipper-store-list/:id',
            component: ShipperGetByStoreComponent
        },
        {
            path: 'shipper-register',
            component: ShipperApplyComponent
        }
    ]
}];
export const routing = RouterModule.forChild(appRoutes);