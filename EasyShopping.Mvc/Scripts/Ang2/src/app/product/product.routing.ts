import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './product.component';
import { ProductListComponent } from './product-list.component';
import { ProductAddComponent } from './product-add.component';
import { ProductApproveListComponent } from './product-approve-list.component';
import { ProductDetailComponent } from './product-detail.component';

const appRoutes: Routes = [{
    path: '',
    component: ProductComponent,
    data: {
        title: 'Product'
    },
    children: [
        {
            path: 'product-add/:storeId',
            component: ProductAddComponent,
            //canActivate: [AuthGuard]
        }, {
            path: 'product-approve-list',
            component: ProductApproveListComponent,
        }, {
            path: 'product-detail/:id',
            component: ProductDetailComponent,
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);
