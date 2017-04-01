import { Component } from '@angular/core';



@Component({
  selector: 'my-app',
  template: `
    <my-header></my-header>
    <div class="container body-content">
        <user-list></user-list>                
    </div>
    <my-footer></my-footer>
  `,
})
export class AppComponent  {
    
}
