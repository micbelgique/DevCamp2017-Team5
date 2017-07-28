import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import { ConfigurationService } from "./ConfigurationService";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { EventInfo } from "app/models/Event.model";

@Injectable()
export class EventService {
    constructor(private http: Http,
        private config: ConfigurationService
    ) {}

    private events: BehaviorSubject<EventInfo[]>;

    public getEvents(): Observable<EventInfo[]>{
        if (!this.events) {
            this.events = new BehaviorSubject<EventInfo[]>([]);

            Observable.interval(this.config.pollingMs).subscribe(_ =>{
            this.http.get(this.config.serverBaseUrl + 'event'/*?ZUMO-API-VERSION=2.0.0'*/)
                .map(p => p.json())
                .subscribe(p => this.events.next(p));
            });
        }
        return this.events;
    }


}