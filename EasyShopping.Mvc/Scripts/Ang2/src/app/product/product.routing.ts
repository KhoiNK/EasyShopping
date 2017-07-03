import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './product.component';
import { ProductListComponent } from './product-list.component';
import { ProductAddComponent } from './product-add.component';

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
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);
