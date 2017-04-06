import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user.component';
import { UserListComponent } from './user-list.component';


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
            path: 'detail/:id',
            component: UserListComponent,
            //canActivate: [AuthGuard]
        },
    ]
}];

export const routing = RouterModule.forChild(appRoutes);
