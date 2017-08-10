import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';

declare var window: any;

@Injectable()
export class ShipperServices {
    private apiUrl = window.GlobalSettings.ApiBase + 'Shipper';

    constructor(private _http: AuthHttp) {

    }

    GetById(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetApproveList/" + id).map(res => res.json());
    }

    Approve(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/Approve/" + id).map(res => res.json());
    }

    GetByStoreId(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetByStore/" + id).map(res => res.json());
    }

    RejectShipper(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/Reject/" + id).map(res => res.json());
    }

    Apply(data: any): Observable<any> {
        return this._http.post(this.apiUrl + "/Apply", data).map(res => res.json());
    }

    IsShipper(): Observable<any> {
        return this._http.get(this.apiUrl + "/IsShipper").map(res => res.json());
    }

    IsApplied(): Observable<any> {
        return this._http.get(this.apiUrl + "/IsApplied").map(res => res.json());
    } 

    BuyPackage(data: any): Observable<any> {
        return this._http.put(this.apiUrl + "/BuyPackage", data).map(res => res.json());
    }
}
