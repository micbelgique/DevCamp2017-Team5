import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import { ConfigurationService } from "./ConfigurationService";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { User } from "../models/User.model";

@Injectable()
export class UserService {
    constructor(private http: Http,
        private config: ConfigurationService
    ) {}

    private Users: BehaviorSubject<User[]>;

    public getUsers(): Observable<User[]>{
        if (!this.Users) {
            this.Users = new BehaviorSubject<User[]>([]);

            Observable.interval(this.config.pollingMs).subscribe(_ =>{
            this.http.get(this.config.serverBaseUrl + 'user'/*?ZUMO-API-VERSION=2.0.0'*/)
                .map(p => p.json())
                .subscribe(p => this.Users.next(p));
            });
        }
        return this.Users;
    }


}