import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { UserComponent } from './User/User.Component';
@NgModule({
    imports: [BrowserModule],
    declarations: [AppComponent, UserComponent], 
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
