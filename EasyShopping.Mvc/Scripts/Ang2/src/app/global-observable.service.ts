import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/share';
import { Observer } from 'rxjs/Observer';
import { Subject } from 'rxjs/Subject';
import { tokenNotExpired } from 'angular2-jwt';

@Injectable()
export class GlobalService {

    public _sysmess = new Subject<string>();
    public _isSignedin = new Subject<boolean>();

    globalMess$ = this._sysmess.asObservable();
    tokenexpired = this._isSignedin.asObservable();

    changeMess(key: string) {
        this._sysmess.next(key);
    }

    CheckToken() {
        this._isSignedin.next(tokenNotExpired('id_token'));
    }
}