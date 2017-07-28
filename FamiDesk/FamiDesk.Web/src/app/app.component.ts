import { Component, OnInit } from '@angular/core';
import { PersonService } from "./services/PersonService";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app works!';

  constructor(private personService: PersonService){}

  ngOnInit() {
    this.personService.getPersons().subscribe(p => {
      console.log(p);
    });
  }
}
