import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { routing } from './store.routing';
import { StoreListComponent } from './store-list.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        routing,
    ],
    declarations: [
        StoreListComponent,
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy }
    ]
})

export class StoreModule { }