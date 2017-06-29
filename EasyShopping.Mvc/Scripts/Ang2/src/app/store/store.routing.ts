import { Routes, RouterModule } from '@angular/router';
import { StoreListComponent } from './store-list.component';
import { StoreComponent } from './store.component';
import { StoreDetailComponent } from './store-detail.component';
import { StoreListByUserComponent } from './store-listbyuserid.component';

const appRoutes: Routes = [{
    path: '',
    component: StoreComponent,
    data: {
        title: 'Store'
    },
    children: [
        {
            path: '/:page',
            component: StoreListComponent,
        }, {
            path: 'store-detail/:id',
            component: StoreDetailComponent,
        },
        {
            path: 'store-by-user-id/:id',
            component: StoreListByUserComponent,
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);