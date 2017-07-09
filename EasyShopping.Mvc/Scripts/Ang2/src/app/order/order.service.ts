import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class OrderServices {
    private apiUrl = window.GlobalSettings.ApiBase + 'Order';

    constructor(private _http: AuthHttp) {

    }

    AddToCart(data: any): Observable<any> {
        return this._http.post(this.apiUrl, data).map(res => res.json());
    }

    GetOrderByUserId(): Observable<any[]> {
        return this._http.get(this.apiUrl).map(res => res.json());
    }

    GetOrderDetail(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/" + id).map(res => res.json());
    }
}