import { Component, OnInit } from '@angular/core'
import { CountryServices } from './country.service'

@Component({
    selector: 'country-list',
    templateUrl: '/Country',
    providers: [CountryServices] 
})
export class GetListCountries implements OnInit {
    public countries: any;
    constructor(private countryservice: CountryServices) {

    }

    ngOnInit() {
        this.countryservice.GetCountryList().subscribe((response: any) => {
            this.countries = response;
        }, error => {
            console.log(error);
        });
    }
}