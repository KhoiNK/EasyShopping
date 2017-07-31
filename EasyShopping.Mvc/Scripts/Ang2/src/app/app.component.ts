import { Component, ViewChild } from '@angular/core';
import { GlobalService } from './global-observable.service';

@Component({
    selector: 'my-app',
    template: `
    <my-header></my-header>

    <div class="container body-content">
        
        <router-outlet></router-outlet>
    </div>
    <my-footer></my-footer>
  `,
})
export class AppComponent {
    //<my-slider></my-slider>
        //<my-sidebar > </my-sidebar>
}
