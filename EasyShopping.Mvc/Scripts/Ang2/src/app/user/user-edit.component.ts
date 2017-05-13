import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserServices } from './user.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'user-edit',
    templateUrl: 'User/EditUser',
    providers: [UserServices]
})

export class UserEditComponent {
    public id: number;
    public user: any;
    public subscription: Subscription;

    constructor(private userservice: UserServices, private activateRoute: ActivatedRoute, private router : Router) {
        this.user = {}
    };

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
        });

        this.userservice.GetUserByID(this.id)
            .subscribe(user => {
                return this.user = user;
                //return this.countryservice.GetSingleCountry(this.user['CountryID']);
            });
    };

    editUser() {
        this.userservice.EditUser(this.user).subscribe(
            (res: any) => {
                if (res == "true") {
                    alert("Edit successful!");
                }
                this.router.navigate['/'];
            }, err => {
                console.log(err);
            });
    }
}