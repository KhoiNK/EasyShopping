import { Component, OnInit, ElementRef, OnDestroy, NgZone, ViewChild } from '@angular/core';
import { Http } from '@angular/http';
import { StoreServices } from './store.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UploadService } from '../upload/upload-image.service';
import { CountryServices } from '../country/country.service';
import { CityService } from '../city/city.service';
import { DistrictService } from '../district/district.service';
import { NgForm, FormControl } from '@angular/forms';
import { Base64EncodeService } from '../upload/base64Encode.service';
import { LoadLocationService } from '../commonService/load-location.service';
import { AgmCoreModule, MapsAPILoader, SebmGoogleMapMarker } from 'angular2-google-maps/core';

@Component({
    selector: 'store-add',
    templateUrl: 'Store/AddStore',
    providers: [StoreServices, UploadService, CountryServices, DistrictService, Base64EncodeService, CityService, LoadLocationService]
})

export class StoreAddComponent implements OnInit {
    public store: any;
    public countries: any[];
    public districts: any[];
    public cities: any[];
    public location: any;    



    constructor(private storeService: StoreServices
        , private uploadService: UploadService
        , private countryService: CountryServices
        , private districtService: DistrictService
        , private cityService: CityService
        , private el: ElementRef
        , private b64: Base64EncodeService
        , private router: Router
        , private locationService: LoadLocationService

    ) {
        this.store = {};
        this.location = {};

    }

    ngOnInit() {
        this.countryService.GetCountryList().subscribe(res => this.countries = res);
        this.cityService.GetAll().subscribe(res => this.cities = res);
        this.districtService.GetAll().subscribe(res => this.districts = res);
    }

    OnSelectCountry(id: number) {
        this.countryService.GetSingleCountry(id).subscribe((res: any) => {
            this.location.Country = res.CommonName;
        });
        this.cities.filter((item: any) => {item.CountryId == id;});
    }

    OnSelectCity(id: number) {
        this.cityService.GetById(id).subscribe((res: any) => {
            this.location.City = res.Name
        });
        this.districts.filter((item: any) => item.ProvinceId == id);
    }

    OnSelectDistrict(id: number) {
        this.districtService.GetById(id).subscribe((res: any) => {
            this.location.District = res.Name;
        });
    }

    SaveChange() {
        let inputEl: HTMLInputElement = this.el.nativeElement.querySelector('#photo');
        let file: File = inputEl.files[0];
        let thumbailImg: string = this.b64.GetB64(file);
        let locationResult: any;
        this.locationService.TrackLocation(this.store.Address
            , this.location.District
            , this.location.City
            , this.location.Country).subscribe((res: any) => {
                locationResult = res;
                this.store.LatX = locationResult.results.geometry.location.lat;
                this.store.LatY = locationResult.results.geometry.location.lng;
            });
        
        this.uploadService.UploadImage(thumbailImg).subscribe((res: any) => {
            this.store.ImgLink = res.data.link;
            this.storeService.CreateStore(this.store).subscribe(
                (res: any) => {
                    if (res.Ok) {
                        this.router.navigate[''];
                    }
                }, err => {
                    console.log(err);
                });
        });
    }
}