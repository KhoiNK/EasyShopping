import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class StoreServices {
    private apiUrl = window.GlobalSettings.ApiBase + 'Store';

    constructor(private _http: AuthHttp) {

    }

    GetListStore(page: Number): Observable<any[]> {
        return this._http.get(this.apiUrl + "?page=", page).map(res => res.json());
    }

    GetStoreById(id: number): Observable<any> {
        return this._http.get(this.apiUrl + "/Get/" + id).map(res => res.json());
    }

    GetStoreByUserId(id: number): Observable<any[]> {
        return this._http.get(this.apiUrl + "/GetByUserId/" + id).map(res => res.json());
    }

    GetStoreByName(name: string): Observable<any[]> {
        return this._http.get(this.apiUrl + "/?storename=" + name).map(res => res.json());
    }

    CreateStore(data: any): Observable<any> {
        return this._http.post(this.apiUrl, data).map(res => res.json());
    }

    CheckOwner(storeId: number): Observable<any> {
        return this._http.get(this.apiUrl + "/IsOwner/" + storeId).map(res => res.json());
    }

    CheckAllowance(storeId: number): Observable<any> {
        return this._http.get(this.apiUrl + "/GetAllowance/" + storeId).map(res => res.json());
    }

    EditStore(data: any): Observable<any> {
        return this._http.put(this.apiUrl + data.ID, data).map(res => res.json());
    }

    RemoveStore(id: number): Observable<any> {
        return this._http.delete(this.apiUrl + "/" + id).map(res => res.json());
    }


}