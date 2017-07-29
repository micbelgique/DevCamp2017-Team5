import { Component, OnInit } from '@angular/core';
import { PersonService } from "./services/PersonService";
import { EventInfoService } from "app/services/EventInfoService";
import { UserService } from "app/services/UserService";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app works!';

  constructor(private personService: PersonService,
      private eventInfoService: EventInfoService,
      private userService: UserService){}

  ngOnInit() {
      this.personService.getPersons().subscribe(p => console.log(p));
      this.eventInfoService.getEvents().subscribe(p => console.log(p));
      this.userService.getUsers().subscribe(p => console.log(p));
  }
}
