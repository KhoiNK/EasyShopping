import { Component } from '@angular/core';

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
    
}
