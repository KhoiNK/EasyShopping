import { Routes, RouterModule } from '@angular/router';

import { OrderComponent } from './order.component';
import { OrderList } from './order-list.component';

const appRoutes: Routes = [{
    path: '',
    component: OrderComponent,
    data: {
        title: 'Order'
    },
    children: [
        {
            path: '',
            component: OrderList
        },
        {
            path: 'order-detail/:id',
            component: 
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);