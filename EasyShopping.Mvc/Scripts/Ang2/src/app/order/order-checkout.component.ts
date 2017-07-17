import { Component, OnInit, OnDestroy, ElementRef, ViewChild, NgZone } from '@angular/core';
import { OrderServices } from './order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NgForm, FormControl } from '@angular/forms';
import { AgmCoreModule, MapsAPILoader, SebmGoogleMapMarker } from 'angular2-google-maps/core';

@Component({
    selector: 'order-checkout',
    templateUrl: 'Order/CheckOut',
    providers: [OrderServices]
})

export class CheckOutComponent implements OnInit, OnDestroy {
    public id: number;
    public order: any;
    public subscription: Subscription;
    public location: any[];
    public address: string;
    public searchControl: FormControl;
    public zoom: number;
    public latitude: number;
    public longitude: number;
    constructor(private orderService: OrderServices
        , private activateRoute: ActivatedRoute
        , private el: ElementRef
        , private router: Router
        , private mapsAPILoader: MapsAPILoader
        , private ngZone: NgZone) {
        this.order = {};
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
            //load Places Autocomplete
            this.searchElementRef.nativeElement.value = res.Address;
        }, err => {
            console.log(err);
        });
    }

    SaveChange() {
        this.order.Address = this.address;
        let tempAdress: any;
        this.location.forEach((component) => {
            if (component.types[0] == 'administrative_area_level_2') { this.order.District = component.long_name; }
            if (component.types[0] == 'administrative_area_level_1') { this.order.City = component.long_name; }
            if (component.types[0] == 'country') { this.order.Country = component.long_name; }
        });

        this.CheckOut(this.order);
    }

    CheckOut(order: any) {
        this.orderService.CheckOut(order).subscribe((res: any) => {
            if (res == true) {
                alert("Checkout Successfully!");
            }
            else {
                alert("Checkout Failed!");
            }
        }, err => {
            console.log(err);
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}