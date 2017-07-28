import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;

@Injectable()
export class MessageServices {
    private apiUrl = window.GlobalSettings.ApiBase + 'Message';

    constructor(private _http: AuthHttp) {

    }

    GetMessThumb(): Observable<any> {
        return this._http.get(this.apiUrl).map(res => res.json());
    }

    GetUnread(): Observable<any> {
        return this._http.get(this.apiUrl + "/Count").map(res => res.json());
    }
}