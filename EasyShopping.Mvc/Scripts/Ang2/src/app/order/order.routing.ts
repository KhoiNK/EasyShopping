import { Routes, RouterModule } from '@angular/router';

import { OrderComponent } from './order.component';
import { OrderList } from './order-list.component';
import { OrderDetailComponent } from './order-detail.component';
import { CheckOutComponent } from './order-checkout.component';

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
        },
        {
            path: 'order-checkout/:id',
            component: CheckOutComponent
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);