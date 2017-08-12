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

    GetOrderByUserId(pageIndex: number): Observable<any[]> {
        return this._http.get(this.apiUrl + "/GetByUser/" + pageIndex).map(res => res.json());
    }

    GetByStore(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetByStore/" + id).map(res => res.json());
    }

    GetDetail(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetDetail/" + id).map(res => res.json());
    }

    GetOrderDetail(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetOrderDetail/" + id).map(res => res.json());
    }

    ChangeQuantity(data: any): Observable<any> {
        return this._http.put(this.apiUrl + "/ChangeQuantity", data).map(res => res.json());
    }

    CheckOut(data: any): Observable<any> {
        return this._http.put(this.apiUrl + "/CheckOut", data).map(res => res.json());
    }

    GetByStatus(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetByStatus/" + id).map(res => res.json());
    }

    RemoveItem(id: number): Observable<any> {
        return this._http.delete(this.apiUrl + "/RemoveItem/" + id).map(res => res.json());
    }

    RemoveOrder(id: number): Observable<any> {
        return this._http.delete(this.apiUrl + "/RemoveOrder/" + id).map(res => res.json());
    }

    AcceptOrder(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/AcceptOrder/" + id).map(res => res.json());
    }

    CancelOrder(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/CancelOrder/" + id).map(res => res.json());
    }

    EditOrder(data: any): Observable<any> {
        return this._http.put(this.apiUrl + "/EditOrder", data).map(res => res.json());
    }
}