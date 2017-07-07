import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class ProductService {
    private apiUrl = window.GlobalSettings.ApiBase + 'Product';

    constructor(private _http: AuthHttp) {

    }

    //GetList(id: number): Observable<any> {
    //    return this._http.get(this.apiUrl + "/" + id).map(res => res.json());
    //}

    AddProduct(data: any): Observable<any> {
        return this._http.post(this.apiUrl, data).map(res => res.json());
    }

    GetApproveList(): Observable<any[]> {
        return this._http.get(this.apiUrl + "/GetApproveList").map(res => res.json());
    }

    Approve(id: number): Observable<any> {
        return this._http.put(this.apiUrl + "/Approve", id).map(res => res.json());
    }

    GetDetail(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/" + id).map(res => res.json());
    }
}