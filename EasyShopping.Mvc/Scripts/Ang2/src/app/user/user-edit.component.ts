import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserServices } from './user.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'user-edit',
    templateUrl: 'User/EditUser',
    providers: [UserServices]
})

export class UserEditComponent {
    public id: number;
    public user: any;
    public subscription: Subscription;

    constructor(private userservice: UserServices, private activateRoute: ActivatedRoute) {

    };
}