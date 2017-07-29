import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ConfigurationService } from './ConfigurationService';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Person } from '../models/Person.model';

@Injectable()
export class PersonService {
  constructor(private http: Http, private config: ConfigurationService) {}

  private persons: BehaviorSubject<Person[]>;

  public getPersons(): Observable<Person[]> {
    if (!this.persons) {
      this.persons = new BehaviorSubject<Person[]>([]);
      this.fetchPerson();
      Observable.interval(this.config.pollingMs).subscribe(_ => {
        this.fetchPerson();
      });
    }
    return this.persons;
  }

  private fetchPerson(): void {
    this.http.get(this.config.serverBaseUrl + 'person').map(p => p.json()).subscribe(p => this.persons.next(p));
  }
}
