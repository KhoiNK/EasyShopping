import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './product.component';
import { ProductAddComponent } from './product-add.component';
import { ProductApproveListComponent } from './product-approve-list.component';
import { ProductDetailComponent } from './product-detail.component';
import { ProductListComponent } from './product-list.component';
import { ProductEditComponent } from './product-edit.component';

const appRoutes: Routes = [{
    path: '',
    component: ProductComponent,
    data: {
        title: 'Product'
    },
    children: [
        {
            path: '',
            component: ProductListComponent,
            //canActivate: [AuthGuard]
        },
        {
            path: 'product-add/:storeId',
            component: ProductAddComponent,
            //canActivate: [AuthGuard]
        }, 
        {
            path: 'product-approve-list/:id',
            component: ProductApproveListComponent,
        },
        {
            path: 'product-detail/:id',
            component: ProductDetailComponent,
        },
        {
            path: 'product-edit/:id',
            component: ProductEditComponent,
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);
