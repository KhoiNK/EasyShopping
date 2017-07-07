import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class DistrictService {
    private apiUrl = window.GlobalSettings.ApiBase + 'District';

    constructor(private _http: AuthHttp) {

    }

    GetAll(): Observable<any[]> {
        return this._http.get(this.apiUrl).map(res => res.json());
    }

    GetById(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/" + id).map(res => res.json());
    }

}