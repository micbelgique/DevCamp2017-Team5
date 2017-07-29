import { Component, OnInit, Input } from '@angular/core';
import { Person } from '../../models/Person.model';

@Component({
  selector: 'appPersonCard',
  templateUrl: 'personCard.component.html',
  styleUrls: ['personCard.component.css'],
})
export class PersonCardComponent {
  @Input() person: Person;
}
