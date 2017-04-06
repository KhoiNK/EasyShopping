import { Routes, RouterModule } from '@angular/router';

import { loginRoutes } from './auth/login.routing';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/users',
        pathMatch: 'full'
    },
    {
        path: 'users', //tu dat path url
        loadChildren: 'app/user/user.module#UserModule'
    },
    //{
    //    path: 'contracts',
    //    loadChildren: 'app/contract/contract.module#ContractModule'
    //},
    ...loginRoutes
];

export const routing = RouterModule.forRoot(routes);