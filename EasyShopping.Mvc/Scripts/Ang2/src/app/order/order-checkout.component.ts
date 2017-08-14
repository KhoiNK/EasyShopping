import { Component, OnInit, OnDestroy, ElementRef, ViewChild, NgZone } from '@angular/core';
import { OrderServices } from './order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormControl } from '@angular/forms';
import { AgmCoreModule, MapsAPILoader, SebmGoogleMapMarker } from 'angular2-google-maps/core';
import { UserServices } from '../user/user.service';
import { GlobalService } from '../global-observable.service';
import { DistanceService } from '../upload/distance.service';

const CART: string = "cart";
@Component({
    selector: 'order-checkout',
    templateUrl: 'Order/CheckOut',
    providers: [OrderServices, UserServices]
})

export class CheckOutComponent implements OnInit, OnDestroy {
    public id: number;
    public note: string;
    public order: any;
    public subscription: Subscription;
    public location: any[];
    public address: string;
    public searchControl: FormControl;
    public zoom: number;
    public latitude: number;
    public longitude: number;
    public shippingfee: number = 0;
    public total: number = 0
    public phone: string;
    public user: any;
    private flag: boolean = false;
    public message: string = "";

    constructor(private orderService: OrderServices
        , private activateRoute: ActivatedRoute
        , private el: ElementRef
        , private router: Router
        , private mapsAPILoader: MapsAPILoader
        , private ngZone: NgZone
        , private userSrv: UserServices
        , private gloSrv: GlobalService
        , private distenaceSrv: DistanceService) {
        this.order = {};
        this.user = {};
    }
    @ViewChild("search")
    public searchElementRef: ElementRef;
    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.LoadData(this.id);
        //set google maps defaults
        this.zoom = 4;
        this.latitude = 39.8282;
        this.longitude = -98.5795;

        //create search FormControl
        this.searchControl = new FormControl();

        //set current position
        //this.setCurrentPosition();

        this.mapsAPILoader.load().then(() => {
            let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement, {
                types: ["address"]
            });

            autocomplete.addListener("place_changed", () => {
                this.ngZone.run(() => {
                    //get the place result
                    let place: google.maps.places.PlaceResult = autocomplete.getPlace();


                    //verify result
                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }

                    //set latitude, longitude and zoom
                    this.address = place.formatted_address;
                    let places = place.address_components;
                    this.setPosition(place.address_components);

                    this.zoom = 12;
                });
            });
        });
        //this.setCurrentPosition();

        //.subscribe(country => this.country = country);
    }

    TurnFlag() {
        this.flag = !this.flag;
    }

    private setCurrentPosition() {
        if ("geolocation" in navigator) {
            navigator.geolocation.getCurrentPosition((position) => {
                this.latitude = position.coords.latitude;
                this.longitude = position.coords.longitude;
                this.zoom = 12;
            });
        }
    }

    private setPosition(place: any[]) {
        this.location = place;
    }

    LoadData(id: number) {
        this.orderService.GetDetail(id).subscribe((res: any) => {
            this.order = res;
            let details: any[] = this.order.details;
            let weight: number = 0;
            details.forEach(item => {
                this.total = this.total + (+item.Price * +item.Quantity);
                weight = weight + (+item.Weight * +item.Quantity);
            });
            this.shippingfee = 50 * weight;
            this.total = this.total + this.shippingfee;
            //load Places Autocomplete
            this.searchElementRef.nativeElement.value = res.Address;
        }, err => {
            console.log(err);
        });

        this.userSrv.GetUserDetail().subscribe((res: any) => {
            this.user = res;
        }, err => {
            console.log(err);
        });
    }

    SaveChange() {
        if (this.flag == true) {
            this.order.Address = this.address;
            this.location.forEach((component) => {
                if (component.types[0] == 'administrative_area_level_2') { this.order.District = component.long_name; }
                if (component.types[0] == 'administrative_area_level_1') { this.order.City = component.long_name; }
                if (component.types[0] == 'country') { this.order.Country = component.short_name; }
            });
        }
        else {
            this.order.Address = this.user.Address;
            this.order.District = this.user.District;
            this.order.City = this.user.City;
            this.order.Country = this.user.Country;
        }

        this.CheckOut(this.order);
    }

    CheckOut(order: any) {
        let distance: any = {};
        this.distenaceSrv.Distance(this.order.Address, "Ha%20Noi").subscribe((res: any) => {
            distance = res;
        }, err => {
            console.log(err);
            });
        if (this.flag == true && this.phone != undefined) {
            this.order.Note = this.note + "Phone:" + this.phone;
        } else {
            this.order.Note = this.note + "Phone:" + this.user.Phone;
        }
        
        this.orderService.CheckOut(order).subscribe((res: any) => {
            if (res == true) {
                localStorage.removeItem(CART);
                this.SetMessage("Checked out successfully!");
                this.gloSrv.SetLoadPage();
                setTimeout(() => {
                    this.router.navigate(['/']);
                }, 3000);

            }
            else {
                this.SetErrMess("Checked out failed!");
            }
        }, err => {
            console.log(err);
        });
    }

    SetMessage(mess: string) {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#succMess');
        inputel.removeAttribute('hidden');
        this.message = mess;
        setTimeout(() => {
            inputel.hidden = true;
        }, 1000);
    }

    SetErrMess(mess: string) {
        let inputel: HTMLInputElement = this.el.nativeElement.querySelector('#errMess');
        this.message = mess;
        inputel.removeAttribute('hidden');
        setTimeout(() => {
            inputel.hidden = true;
        }, 5000);
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}