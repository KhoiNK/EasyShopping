﻿import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';

declare var window: any;

@Injectable()
export class UserServices {
    private apiUrl = window.GlobalSettings.ApiBase + 'User';

    constructor(private _http: AuthHttp) {

    }

    GetUserList(): Observable<any[]> {
        return this._http.get(this.apiUrl).map(res => res.json())
    }

    GetUserByID(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/" + id).map(res => res.json())
    }

    EditUser(id: number, data: any): Observable<any> {
        return this._http.put(this.apiUrl + id, data).map(res => res.json())
    }
}