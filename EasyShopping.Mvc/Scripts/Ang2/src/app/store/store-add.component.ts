import { Component, OnInit, ElementRef, OnDestroy, NgZone, ViewChild } from '@angular/core';
import { StoreServices } from './store.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UploadService } from '../upload/upload-image.service';
import { NgForm, FormControl } from '@angular/forms';
import { Base64EncodeService } from '../upload/base64Encode.service';
import { AgmCoreModule, MapsAPILoader, SebmGoogleMapMarker } from 'angular2-google-maps/core';

@Component({
    selector: 'store-add',
    templateUrl: 'Store/AddStore',
    providers: [StoreServices, UploadService, Base64EncodeService]
})

export class StoreAddComponent implements OnInit {
    public store: any;
    public countries: any[];
    public districts: any[];
    public cities: any[];
    //public location: any;
    public address: any[];
    public searchControl: FormControl;
    public zoom: number;
    public latitude: number;
    public longitude: number;
    public thumbailImg: string;

    constructor(private storeService: StoreServices
        , private uploadService: UploadService
        , private el: ElementRef
        , private b64: Base64EncodeService
        , private router: Router
        , private mapsAPILoader: MapsAPILoader
        , private ngZone: NgZone

    ) {
        this.store = {};
        //this.location = {};
    }
    @ViewChild("search")
    public searchElementRef: ElementRef;
    ngOnInit() {
        //set google maps defaults
        this.zoom = 4;
        this.latitude = 39.8282;
        this.longitude = -98.5795;

        //create search FormControl
        this.searchControl = new FormControl();

        //set current position
        this.setCurrentPosition();

        //load Places Autocomplete
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
                    this.latitude = place.geometry.location.lat();
                    this.longitude = place.geometry.location.lng();
                    let places = place.address_components;
                    console.log(places);
                    this.setLatLng(place.geometry.location.lat(), place.geometry.location.lng(), place.address_components);
                    this.zoom = 12;
                });
            });
        });
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

    private setLatLng(lat: any, lng: any, place: any[]) {
        this.latitude = lat;
        this.longitude = lng;
        this.address = place;
    }

    setThumbailImg() {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        let file: File = inputEl.files[0];
        var reader = new FileReader();
        reader.onload = this._handleReaderLoaded.bind(this);
        reader.readAsBinaryString(file);
    }

    _handleReaderLoaded(readerEvt: any) {
        var binaryString = readerEvt.target.result;
        this.thumbailImg = btoa(binaryString);
        //console.log(btoa(binaryString));
    }


    SaveChange() {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        let file: File = inputEl.files[0];
        let locationResult: any;
        this.store.Address = this.address;
        this.store.LatX = this.latitude;
        this.store.LatY = this.longitude;
        let tempAdress: any;
        this.address.forEach((component) => {
            if (component.types[0] == 'street_number') { tempAdress = component.long_name; }
            if (component.types[0] == 'route') { this.store.Address = tempAdress + " " + component.long_name }
            if (component.types[0] == 'administrative_area_level_2') { this.store.District = component.long_name; }
            if (component.types[0] == 'administrative_area_level_1') { this.store.City = component.long_name; }
            if (component.types[0] == 'country') { this.store.Country = component.long_name; }
        });

        if (file == null) {
            this.CreateStore(this.store);
        }

        this.uploadService.UploadImage(this.thumbailImg).subscribe((res: any) => {
            this.store.ImgLink = res.data.link;
            this.CreateStore(this.store);
        }, err => {
            console.log(err);
        });
    }

    CreateStore(store: any) {
        this.storeService.CreateStore(store).subscribe(
            (res: any) => {
                if (res.ID) {
                    alert("Added Successfully!");
                    this.router.navigate[''];
                }
            }, err => {
                console.log(err);
            });
    }
}