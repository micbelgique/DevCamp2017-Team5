import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Person } from '../../models/Person.model';

@Component({
  selector: 'appPersonCard',
  templateUrl: 'personCard.component.html',
  styleUrls: ['personCard.component.css'],
})
export class PersonCardComponent implements OnChanges {
  @Input() person: Person;
  imgUrl: SafeUrl;
  constructor(private domSanitizer: DomSanitizer) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['person'] && this.person) {
      this.imgUrl = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + this.person.avatar);
    }
  }
}
