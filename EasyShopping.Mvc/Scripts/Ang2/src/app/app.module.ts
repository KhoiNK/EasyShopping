import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Http, HttpModule, RequestOptions } from '@angular/http';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { AuthHttp, AuthConfig, provideAuth } from 'angular2-jwt/angular2-jwt';
import { AgmCoreModule } from 'angular2-google-maps/core';

import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login.component';
import { IAuthService, AuthService } from './auth/auth.service';
import { Header } from './header.component';
import { Footer } from './footer.component';
import { SideBar } from './sidebar.component';
import { SliderComponent } from './slider.component';
import { SearchPageComponent } from './searchpage.component';
import { routing } from './app.routing';
import { GlobalService } from './global-observable.service';
import { DistanceService } from './upload/distance.service';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        CommonModule,
        routing,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyAnEYt5edclSFmFHCSgp665PgUvIesC_jo',
            libraries: ["places"]
        }),
        ReactiveFormsModule,
    ],
    declarations: [
        Header,
        Footer,
        SideBar,
        SliderComponent,
        AppComponent,
        LoginComponent,
        SearchPageComponent
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
        }),
        GlobalService,
        DistanceService
    ],
    bootstrap: [AppComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
