import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

//import { ContactModule } from '../contact/contact.module';
import { LoginComponent } from './login.component';
import { routing } from './login.routing';

@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        //ContactModule,
        routing,
    ],
    declarations: [
        LoginComponent
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ]
})
export class LoginModule { }