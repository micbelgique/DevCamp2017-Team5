import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/primeng';

import { AppComponent } from './app.component';
import { PersonService } from './services/PersonService';

import 'rxjs/Rx';
import {
  ConfigurationService,
  ConfigurationServiceProd,
  ConfigurationServiceLocal,
} from './services/ConfigurationService';
import { PersonsComponent } from 'app/views/persons/persons.component';
import { EventInfoService } from 'app/services/EventInfoService';
import { EventsInfoComponent } from 'app/views/events/eventsInfo.component';
import { PersonCardComponent } from 'app/views/personCard/personCard.component';

@NgModule({
  declarations: [AppComponent, PersonsComponent, EventsInfoComponent, PersonCardComponent],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    ButtonModule,
    RouterModule.forRoot([
      { path: '', component: PersonsComponent },
      { path: 'events/:personId', component: EventsInfoComponent },
    ]),
  ],
  providers: [PersonService, EventInfoService, { provide: ConfigurationService, useClass: ConfigurationServiceProd }],
  bootstrap: [AppComponent],
})
export class AppModule {}
