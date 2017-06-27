import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserServices } from './user.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { CountryServices } from '../country/country.service';

@Component({
    selector: 'user-list',
    templateUrl: 'User/UserDetail',
    providers: [UserServices, CountryServices]
})

export class UserDetailComponent implements OnInit, OnDestroy {
    public id: number;
    public user: any;
    public subscription: Subscription;
    public country: any;
    constructor(private userservice: UserServices, private activateRoute: ActivatedRoute, private countryservice: CountryServices) {
        this.user = {};
        this.country = {};
    }

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
        });

        this.userservice.GetUserByID(this.id)
            .subscribe(user => {
                return this.user = user;
                //return this.countryservice.GetSingleCountry(this.user['CountryID']);
            });
            //.subscribe(country => this.country = country);
    }
    
    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
