import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';

declare var window: any;

@Injectable()
export class ProductTypeService {
    private apiUrl = window.GlobalSettings.ApiBase + 'ProductType';

    constructor(private _http: AuthHttp) {

    }

    GetList(): Observable<any[]> {
        return this._http.get(this.apiUrl).map(res => res.json());
    }
}

