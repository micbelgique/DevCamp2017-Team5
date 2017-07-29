import { Component, OnInit } from '@angular/core';
import { PersonService } from "../../services/PersonService";
import { Person } from "../../models/Person.model";
import { EventInfoService } from "app/services/EventInfoService";

@Component({
    templateUrl: 'persons.component.html'
})
export class PersonsComponent implements OnInit {

    private persons: Person[];
    private buttonLabel = 'Test Label';

    constructor(private personService: PersonService,
        private eventService: EventInfoService
    ) { }

    
    ngOnInit(): void {
        this.personService
            .getPersons()
            .subscribe(p => this.persons = p);
        this.eventService.getEvents();
    }
    
}