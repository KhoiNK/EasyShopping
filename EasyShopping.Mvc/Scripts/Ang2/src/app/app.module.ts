import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { Http, HttpModule, RequestOptions } from '@angular/http';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { AuthHttp, AuthConfig, provideAuth } from 'angular2-jwt/angular2-jwt';


import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login.component';
import { IAuthService, AuthService } from './auth/auth.service';
import { Header } from './header.component';
import { Footer } from './footer.component';
//import { GetListCountries } from './country-list/country-list.component';
import { routing } from './app.routing';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        CommonModule,
        routing
    ],
    declarations: [
        Header,
        Footer,
        //GetListCountries,
        AppComponent,
        LoginComponent,
    ], 
    providers: [
        { provide: IAuthService, useClass: AuthService },
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        AuthHttp,
        provideAuth({
            headerName: 'Authorization',
            headerPrefix: 'Bearer',
            tokenName: 'id_token',
            tokenGetter: () => {
                return localStorage.getItem('id_token');
            },
            noJwtError: true
        })
    ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }
