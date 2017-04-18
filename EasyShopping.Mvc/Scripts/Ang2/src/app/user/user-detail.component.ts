import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserServices } from './user.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'user-list',
    templateUrl: 'User/UserDetail',
    providers: [UserServices]
})

export class UserDetailComponent implements OnInit, OnDestroy{
    public id: number
    public user: any;
    public subscription: Subscription;
    constructor(private userservice: UserServices, private activateRoute: ActivatedRoute) {
        this.user = {};
    }

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
        });

        this.userservice.GetUserByID(this.id).subscribe(res => this.user = res);
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
