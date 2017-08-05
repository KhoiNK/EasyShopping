import { Component, OnInit, ElementRef, OnDestroy, NgZone, ViewChild } from '@angular/core';
import { StoreServices } from './store.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UploadService } from '../upload/upload-image.service';
import { NgForm, FormControl } from '@angular/forms';
import { Base64EncodeService } from '../upload/base64Encode.service';
import { AgmCoreModule, MapsAPILoader, SebmGoogleMapMarker } from 'angular2-google-maps/core';

@Component({
    selector: 'store-edit',
    templateUrl: 'Store/EditStore',
    providers: [StoreServices, UploadService, Base64EncodeService]
})

export class StoreEditComponent implements OnInit {
    public store: any;
    public countries: any[];
    public districts: any[];
    public cities: any[];
    //public location: any;
    public position: any[];
    public address: string;
    public searchControl: FormControl;
    public zoom: number;
    public latitude: number;
    public longitude: number;
    public thumbailImg: string;
    public subscription: Subscription;
    public storeId: number;
    public message: string = "";

    constructor(private storeService: StoreServices
        , private uploadService: UploadService
        , private el: ElementRef
        , private b64: Base64EncodeService
        , private router: Router
        , private mapsAPILoader: MapsAPILoader
        , private ngZone: NgZone
        , private activateRoute: ActivatedRoute
    ) {
        this.store = {};
        //this.location = {};
    }
    @ViewChild("search")
    public searchElementRef: ElementRef;
    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.storeId = params['id'];
        });
        this.storeService.GetStoreById(this.storeId).subscribe((res: any) => {
            this.store = res;
        }, err => {
            console.log(err);
        });

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
                    this.address = place.formatted_address;
                    let places = place.address_components;
                    this.store.Address = "";
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
        this.position = place;
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
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#previewImg');
        let imgEl: HTMLInputElement = this.el.nativeElement.querySelector('#storeThumbailImg');
        imgEl.hidden = true;
        inputEl.setAttribute('src', "data:image/jpeg;base64," + btoa(binaryString));
        this.thumbailImg = btoa(binaryString);
    }


    SaveChange() {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        let file: File = inputEl.files[0];
        if (this.address != undefined || this.address != "") {
            let locationResult: any;
            this.store.Address = this.address;
            this.store.LatX = this.latitude;
            this.store.LatY = this.longitude;
            this.position.forEach((component) => {
                if (component.types[0] == 'administrative_area_level_2') { this.store.District = component.long_name; }
                if (component.types[0] == 'administrative_area_level_1') { this.store.City = component.long_name; }
                if (component.types[0] == 'country') { this.store.Country = component.short_name; }
            });
        }
        if (file == null) {
            this.EditStore(this.store);
        }

        if (file != null || file != undefined) {
            this.uploadService.UploadImage(this.thumbailImg).subscribe((res: any) => {
                this.store.ImgLink = res.data.link;
                this.EditStore(this.store);
            }, err => {
                console.log(err);
            });
        }
    }

    EditStore(store: any) {
        this.storeService.EditStore(store).subscribe(
            (res: any) => {
                if (res == true) {
                    this.store = res;
                    this.SetMessage("Updated successfully", "storeMess_success");
                    setTimeout(() => {
                        this.router.navigate(['/stores/store-detail/' + this.storeId]);
                    }, 2000);
                }
                else {
                    this.SetMessage("Updated failed", "storeMess_error");
                }
            }, err => {
                this.SetMessage("Updated failed", "storeMess_error");
                console.log(err);
            });
    }

    SetMessage(mess: string, id: string) {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#' + id);
        this.message = mess;
        inputEl.removeAttribute('hidden');
    }
}