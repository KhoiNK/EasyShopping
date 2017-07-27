import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class ProductService {
    private apiUrl = window.GlobalSettings.ApiBase + 'Product';
    //private approveUrl = window.GlobalSetting.ApiBase + 'Approvement';

    constructor(private _http: AuthHttp) {

    }

    GetList(): Observable<any> {
        return this._http.get(this.apiUrl).map(res => res.json());
    }

    AddProduct(data: any): Observable<any> {
        return this._http.post(this.apiUrl, data).map(res => res.json());
    }

    GetApproveList(id: number): Observable<any[]> {
        return this._http.get(this.apiUrl + "/ApproveList/" + id).map(res => res.json());
    }

    Approve(id: number): Observable<any> {
        return this._http.put(this.apiUrl + "/Approvement", id).map(res => res.json());
    }

    GetDetail(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetDetail/" + id).map(res => res.json());
    }

    Edit(data: any): Observable<any> {
        return this._http.put(this.apiUrl + "/EditProduct", data).map(res => res.json());
    }

    GetByName(data: string): Observable<any> {
        return this._http.get(this.apiUrl + "/GetWithName/" + data).map(res => res.json());
    }

    Delete(id: number): Observable<any> {
        return this._http.delete(this.apiUrl + "/" + id).map(res => res.json());
    }
}