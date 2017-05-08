import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

declare var window: any;

@Injectable()
export class CountryServices {
    private apiUrl = window.GlobalSettings.ApiBase + 'Country';

    constructor(private _http: Http) {

    }

    GetCountryList(): Observable<any[]> {
        return this._http.get(this.apiUrl).map((response: Response) => response.json());
    }

    GetSingleCountry(id: number | string): Observable<any> {
        return this._http.get(`${this.apiUrl}/${id}`).map((res: Response) => res.json()); 
    }
}