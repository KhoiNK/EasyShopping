import { Injectable } from '@angular/core';
import { RequestOptions, Http, URLSearchParams, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;
const ClientID: any = "Client-ID 69327a355faae60";

@Injectable()
export class UploadService {
    private apiUrl = "https://api.imgur.com/3/image";

    constructor(private _http: Http) {

    }

    UploadImage(file: string): Observable<any> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.delete("Authorization");
        headers.append("Authorization", ClientID);
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this.apiUrl, file, options).map(res => res.json());
    }
}