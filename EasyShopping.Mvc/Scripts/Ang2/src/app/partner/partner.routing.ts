import { Routes, RouterModule } from '@angular/router';
import { PartnerComponent } from './partner.component';
import { partnerListComponent } from './part-list.component';

const appRoutes: Routes = [{
    path: '',
    component: PartnerComponent,
    data: {
        title: 'Store'
    },
    children: [
        {
            path: '',
            component: PartnerComponent,
        },
        {
            path: 'partner-list/:id',
            component: partnerListComponent,
        }
    ]
}];

export const routing = RouterModule.forChild(appRoutes);