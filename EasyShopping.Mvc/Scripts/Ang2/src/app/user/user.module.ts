import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

//import { ContactModule } from '../contact/contact.module';
import { UserComponent } from './user.component';
import { UserListComponent } from './user-list.component';
import { routing } from './user.routing';

@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        //ContactModule,
        routing,
    ],
    declarations: [
        UserComponent,
        UserListComponent
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ]
})
export class UserModule { }
