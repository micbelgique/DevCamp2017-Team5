import { Component, OnInit } from '@angular/core';
import { PersonService } from "../../services/PersonService";
import { Person } from "../../models/Person.model";
import { EventService } from "app/services/EventService";

@Component({
    templateUrl: 'persons.component.html'
})
export class PersonsComponent implements OnInit {

    private persons: Person[];

    constructor(private personService: PersonService,
        private eventService: EventService
    ) { }

    
    ngOnInit(): void {
        this.personService
            .getPersons()
            .subscribe(p => this.persons = p);
        this.eventService.getEvents();
    }
    
}