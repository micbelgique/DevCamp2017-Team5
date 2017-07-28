import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { PersonService } from "./services/PersonService";

import 'rxjs/Rx';
import { ConfigurationService, ConfigurationServiceProd, ConfigurationServiceLocal } from "./services/ConfigurationService";
import { PersonsComponent } from "app/views/persons/persons.component";
import { EventInfoService } from "app/services/EventInfoService";

@NgModule({
  declarations: [
    AppComponent,
    PersonsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      {path: '', component: PersonsComponent}
    ])
  ],
  providers: [
      PersonService,
      EventInfoService,
    { provide: ConfigurationService, useClass: ConfigurationServiceProd }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
