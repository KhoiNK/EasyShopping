import { Routes, RouterModule } from '@angular/router';

import { loginRoutes } from './auth/login.routing';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/products',
        pathMatch: 'full'
    },
    {
        path: 'users', //tu dat path url
        loadChildren: 'app/user/user.module#UserModule'
    },
    {
        path: 'stores',
        loadChildren : 'app/store/store.module#StoreModule'
    },
    {
        path: 'products',
        loadChildren: 'app/product/product.module#ProductModule'
    },
    {
        path: 'orders',
        loadChildren: 'app/order/order.module#OrderModule'
    },
    {
        path: 'shippers',
        loadChildren: 'app/shipper/shipper.module#ShipperModule'
    },
    {
        path: 'partners',
        loadChildren: 'app/partner/partner.module#PartnerModule'
    }
    //{
    //    path: 'contracts',
    //    loadChildren: 'app/contract/contract.module#ContractModule'
    //},
    ...loginRoutes
];

export const routing = RouterModule.forRoot(routes);