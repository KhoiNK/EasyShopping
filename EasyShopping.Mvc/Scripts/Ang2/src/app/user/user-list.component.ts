import { Component, OnInit } from '@angular/core';
import { UserServices } from './user.service';

//@Component({
//    selector: 'user-list',
//    template: `
//        <form>
//            <input [(model)]="user.Username" />
//            <input [(model)]="user.Password" />
//            <input type="submit" (click)="register" />
//        </form>
//    `,
//    providers: [UserServices]
//})

//class UserRegisterComponent implements OnInit {
//    public user: any;

//    ngOnInit() {

//    }

//    public register() {
//        // $http.post(user).....;
//    }
//}



@Component({
    selector: 'user-list',
    templateUrl: '/User',
    providers: [UserServices]
})
export class UserListComponent implements OnInit {

    public users: any[];
    constructor(private userService: UserServices) {

    }
    ngOnInit() {
        //*
        this.LoadData();
        //*/
    }

    DeleteUser(id: number) {
        let ConfirmResult = confirm("Are you sure want to delete this User?");
        if (ConfirmResult) {
            this.userService.RemoveUser(id).subscribe((res: any) => {
                if (res == "true") {
                    this.LoadData();
                }
            });
        }
    }

    LoadData() {
        this.userService.GetUserList().subscribe((response: any) => {
            this.users = response;
            console.log(response);
        }, error => {
            console.log(error);
        });
    }
}