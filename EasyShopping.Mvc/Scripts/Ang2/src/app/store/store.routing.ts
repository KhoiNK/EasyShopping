import { Routes, RouterModule } from '@angular/router';
import { StoreListComponent } from './store-list.component';
import { StoreComponent } from './store.component';

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
        }
    ]
}]

export class routing = RouterModule.forChild(appRoutes);