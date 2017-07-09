import { Routes, RouterModule } from '@angular/router';

import { OrderComponent } from './order.component';
import { OrderList } from './order-list.component';
import { OrderDetailComponent } from './order-detail.component';

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
            component: OrderDetailComponent
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);