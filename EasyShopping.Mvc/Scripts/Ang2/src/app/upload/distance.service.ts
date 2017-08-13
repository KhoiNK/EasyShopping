import { Injectable } from '@angular/core';
import { RequestOptions, Http, URLSearchParams, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

declare var window: any;
const ApiKey: any = "&key=AIzaSyAnEYt5edclSFmFHCSgp665PgUvIesC_jo";

@Injectable()
export class DistanceService {
    private apiUrlOrgin = "https://maps.googleapis.com/maps/api/directions/json?origin=";
    private apiUrlDes = "destination=";

    constructor(private _http: AuthHttp) {

    }

    Distance(origin: string, destination: string): Observable<any> {
        return this._http.get(this.apiUrlOrgin + origin + this.apiUrlDes + destination + ApiKey).map(res => res.json());
    }
}