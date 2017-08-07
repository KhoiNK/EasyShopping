import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class ProductTypeService {
    private apiUrl = window.GlobalSettings.ApiBase + 'ProductType';

    constructor(private _http: AuthHttp) {

    }

    GetList(): Observable<any> {
        return this._http.get(this.apiUrl).map(res => res.json());
    }

    GetWithTarget(): Observable<any> {
        return this._http.get(this.apiUrl + "/GetWithTarget/").map(res => res.json());
    }
}

