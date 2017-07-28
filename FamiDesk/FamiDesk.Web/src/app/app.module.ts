import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { PersonService } from "./services/PersonService";

import 'rxjs/Rx';
import { ConfigurationService, ConfigurationServiceProd, ConfigurationServiceLocal } from "./services/ConfigurationService";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [
    PersonService,
    { provide: ConfigurationService, useClass: ConfigurationServiceLocal }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
