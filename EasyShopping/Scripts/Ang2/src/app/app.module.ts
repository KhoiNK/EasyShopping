import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { Header } from './header.component';
import { Footer } from './footer.component';
import { UserComponent } from './user-list/user-list.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule
    ],
    declarations: [
        AppComponent,
        Header,
        Footer,
        UserComponent], 
    bootstrap:    [ AppComponent ]
})
export class AppModule { }
