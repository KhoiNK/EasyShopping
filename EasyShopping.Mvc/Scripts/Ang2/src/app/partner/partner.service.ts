import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class PartnerService {
    private apiUrl = window.GlobalSettings.ApiBase + 'Partner';

    constructor(private _http: AuthHttp) {

    }

    Apply(storeId: number): Observable<any> {
        return this._http.get(this.apiUrl + "/Apply/" + storeId).map(res => res.json());
    }

    GetList(storeId: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetList/" + storeId).map(res => res.json());
    }

    Approve(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/Approve/" + id).map(res => res.json());
    }
}