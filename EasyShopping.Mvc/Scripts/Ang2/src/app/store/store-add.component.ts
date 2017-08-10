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
    public position: any[];
    public address: string;
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
    @ViewChild("storename")
    public storeName: ElementRef;
    @ViewChild("bankaccount")
    public bankaccount: ElementRef;
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
                    this.address = place.formatted_address;
                    this.position = place.address_components;
                    
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
        inputEl.setAttribute('src', "data:image/jpeg;base64,"+ btoa(binaryString));
        this.thumbailImg = btoa(binaryString);
        //console.log(btoa(binaryString));
    }


    SaveChange() {
        let inputEl: HTMLInputElement;
        if (this.CheckValue() == true) {
            inputEl = this.el.nativeElement.querySelector('#photo');
            let file: File = inputEl.files[0];
            if (this.address != undefined) {
                this.store.Address = this.address;
                this.store.LatX = this.latitude;
                this.store.LatY = this.longitude;
                this.position.forEach((component) => {
                    if (component.types[0] == 'administrative_area_level_2') { this.store.District = component.long_name; }
                    if (component.types[0] == 'administrative_area_level_1') { this.store.City = component.long_name; }
                    if (component.types[0] == 'country') { this.store.Country = component.short_name; }
                });
            }
            else {
                inputEl = this.el.nativeElement.querySelector('#storeMess');
                inputEl.removeAttribute('hidden');
                setTimeout(() => {
                    inputEl.hidden = true;
                }, 2000);
            }

            if (file == null) {
                this.CreateStore(this.store);
            }
            if (file != null) {
                this.uploadService.UploadImage(this.thumbailImg).subscribe((res: any) => {
                    this.store.ImgLink = res.data.link;
                    this.CreateStore(this.store);
                }, err => {
                    console.log(err);
                });
            }
        }
        else {
            inputEl = this.el.nativeElement.querySelector('#storeMess');
            inputEl.removeAttribute('hidden');
            setTimeout(() => {
                inputEl.hidden = true;
            }, 2000);
        }
    }

    Validate(value: string, id: string) {
        let input: HTMLElement = this.el.nativeElement.querySelector('#' + id);;
        if (value == "" || value == null) {
            input.style.border = "1px solid red";
            event.preventDefault;
        } else {
            input.removeAttribute("style");
        }
    }

    CheckValue(): boolean {
        if (this.store.Name == "") {
            return false;
        }
        if (this.store.Description == "") {
            return false;
        }
        if (this.store.BankAccount == "") {
            return false;
        }
        if (this.store.Address == "") {
            return false;
        }
        if (this.store.RequiredDeposit == "") {
            return false;
        }
        return true;
    }

    CreateStore(store: any) {
        this.storeService.CreateStore(store).subscribe(
            (res: any) => {
                if (res == null) {
                    let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#storeMess');
                    inputEl.removeAttribute('hidden');
                    setTimeout(() => {
                        inputEl.hidden == true;
                    }, 1000);
                }
                if (res.ID) {
                    alert("Added Successfully!");
                    this.store = res;
                    let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#modalClick');
                    inputEl.click();
                    //setTimeout(() => {
                    //    this.router.navigate[''];
                    //}, 20000);
                }
            }, err => {
                console.log(err);
            });
    }
}