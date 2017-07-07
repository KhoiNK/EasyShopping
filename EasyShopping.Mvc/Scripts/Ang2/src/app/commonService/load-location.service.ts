import { Injectable } from '@angular/core';
import { RequestOptions, Http, URLSearchParams, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt/angular2-jwt';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

const API_KEY: string = "AIzaSyCIzpyPuW4FjgRxhMnVBOSoq0j7uRNkl7Q";

@Injectable()
export class LoadLocationService {
    private apirUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";
    //ex: https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=YOUR_API_KEY
    constructor(private _http: Http) {

    }

    TrackLocation(address: string, district: string, city: string, country: string): Observable<any> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let result: any;
        let options = new RequestOptions({ headers: headers });
        let teststring: string = this.apirUrl + address + "," + district + "," + city + "," + country + "&key=" + API_KEY;
        this._http.get(this.apirUrl + address + "," + district + "," + city + "," + country + "&key=" + API_KEY, options).map(res => {
            res.json();
            result = res.json();
            console.log("result: " + result + "and " + res);
        });
        return result;
    }
}