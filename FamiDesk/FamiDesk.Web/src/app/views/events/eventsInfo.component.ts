import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Person } from "app/models/Person.model";
import { PersonService } from "app/services/PersonService";
import { EventInfoService } from "app/services/EventInfoService";
import { EventInfo } from "app/models/EventInfo.model";
import { DomSanitizer, SafeUrl } from "@angular/platform-browser";

@Component({
    templateUrl: 'eventsInfo.component.html'
})
export class EventsInfoComponent implements OnInit {

    private personId: string;
    private person: Person;
    private eventInfos: EventInfo[];
    private imgUrl: SafeUrl;
    

    constructor(private route: ActivatedRoute,
        private personService: PersonService,
        private eventInfoService: EventInfoService,
         private domSanitizer: DomSanitizer,
    ) {}



    ngOnInit(): void {
        this.route.params.subscribe(p => {
            this.personId = p['personId'];
            console.log(this.personId);
            this.person = null;
            this.personService.getPersons()
                .subscribe(persons => {
                    this.person = persons.find(person => person.id === this.personId);
                    console.log(this.person);
                    this.imgUrl = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + this.person.avatar);
                        });

            this.eventInfos = [];
            this.eventInfoService.getEvents()
                .subscribe(p => this.eventInfos = p.filter(eventInfo => eventInfo.personId === this.personId));
        });
    }


}