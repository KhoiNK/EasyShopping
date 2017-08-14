import { Routes, RouterModule } from '@angular/router';
import { StoreListComponent } from './store-list.component';
import { StoreComponent } from './store.component';
import { StoreDetailComponent } from './store-detail.component';
import { StoreListByUserComponent } from './store-listbyuserid.component';
import { StoreAddComponent } from './store-add.component';
import { StoreApproveList } from './store-approve-list.component';
import { StoreEditComponent } from './store-edit.component';
import { StoreUpgradeComponent } from './store-upgrade.component';
import { StoreIntroComponent } from './store-intro.component';
import { GetOrderByStoreComponent } from './store-getorderbystoreId.component';

const appRoutes: Routes = [{
    path: '',
    component: StoreComponent,
    data: {
        title: 'Store'
    },
    children: [
        {
            path: '',
            component: StoreListComponent,
        }, {
            path: 'store-detail/:id',
            component: StoreDetailComponent,
        },
        {
            path: 'store-by-user-id/:id',
            component: StoreListByUserComponent,
        },
        {
            path: 'store-add',
            component: StoreAddComponent,
        },
        {
            path: 'store-approve-list',
            component: StoreApproveList,
        },
        {
            path: 'store-edit/:id',
            component: StoreEditComponent
        },
        {
            path: 'store-upgrade/:id',
            component: StoreUpgradeComponent 
        },
        {
            path: 'store-intro',
            component: StoreIntroComponent
        },
        {
            path: 'store-getorder/:id',
            component: GetOrderByStoreComponent
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);