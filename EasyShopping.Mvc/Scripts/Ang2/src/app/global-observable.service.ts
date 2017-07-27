import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/share';
import { Observer } from 'rxjs/Observer';

@Injectable()
export class GlobalService {
    public _searchproduct = new BehaviorSubject<string>("");

    searchproduct$ = this._searchproduct.asObservable();

    changeSearchProduct(key: string) {
        this._searchproduct.next(key);
    }

    //public _searchproduct: string = "";
    //searchproduct$: Observable<number>;
    //private _observer: Observer<string>;

    //constructor() {
    //    this.searchproduct$ = new Observable(observer => {
    //        this._observer = observer;
    //    }).share();
    //}

    //changeSearchProduct(key: string) {
    //    this._searchproduct = key;
    //    this._observer.next(key);
    //}

    //GetSearchKey() {
    //    return this._searchproduct;
    //}
}