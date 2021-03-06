﻿import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class RecruitServices {
    private apiUrl = window.GlobalSettings.ApiBase + 'Recruitment';

    constructor(private _http: AuthHttp) {

    }

    Create(data: any): Observable<any> {
        return this._http.post(this.apiUrl, data).map(res => res.json());
    }

    Update(data: any): Observable<any> {
        return this._http.put(this.apiUrl, data).map(res => res.json());
    }

    GetByStore(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/" + id).map(res => res.json());
    }
}