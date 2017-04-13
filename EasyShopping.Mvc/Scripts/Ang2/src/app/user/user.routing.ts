import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user.component';
import { UserListComponent } from './user-list.component';
import { UserDetailComponent } from './user-detail.component';
import { UserEditComponent } from './user-edit.component';

const appRoutes: Routes = [{
    path: '',
    component: UserComponent,
    data: {
        title: 'User'
    },
    children: [
        {
            path: '',
            component: UserListComponent,
            //canActivate: [AuthGuard]
        }, {
            path: 'user-detail/:id',
            component: UserDetailComponent,
            //canActivate: [AuthGuard]
        }, {
            path: 'user-edit/:id',
            component: UserEditComponent,
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);
