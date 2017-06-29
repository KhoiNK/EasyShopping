import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


//import { ContactModule } from '../contact/contact.module';
import { UserComponent } from './user.component';
import { UserListComponent } from './user-list.component';
import { routing } from './user.routing';
import { UserDetailComponent } from './user-detail.component';
import { UserEditComponent } from './user-edit.component';
import { UserServices } from './user.service';

@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        //ContactModule,
        routing
    ],
    declarations: [
        UserComponent,
        UserListComponent,
        UserDetailComponent,
        UserEditComponent
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ]
})
export class UserModule { }
