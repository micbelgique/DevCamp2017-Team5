import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Person } from "app/models/Person.model";
import { PersonService } from "app/services/PersonService";
import { EventInfoService } from "app/services/EventInfoService";
import { EventInfo } from "app/models/EventInfo.model";
import { DomSanitizer, SafeUrl } from "@angular/platform-browser";
import { UserService } from "app/services/UserService";
import { User } from "app/models/User.model";
import { Appointment } from "app/models/Appointment.viewmodel";

@Component({
    templateUrl: 'eventsInfo.component.html'
})
export class EventsInfoComponent implements OnInit {

    private personId: string;
    private person: Person;
    private eventInfos: EventInfo[];
    private users: User[];
    private appointments: Appointment[];
    private imgUrl: SafeUrl;
    

    constructor(private route: ActivatedRoute,
        private personService: PersonService,
        private eventInfoService: EventInfoService,
        private userService: UserService,
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
            this.userService.getUsers()
                .subscribe(users => {
                    this.users = users;
                    this.mapToAppointmentViewModel();
                });
        });
    }

    private mapToAppointmentViewModel(): void {
        this.appointments = [];
        this.eventInfos = this.eventInfos.sort((eA, eB) => (eA.date > eB.date) ? -1 : 1);

        for (let i = 0; i < this.eventInfos.length; i++) {
            if (this.eventInfos[i].type === 'CheckOut') {

                this.appointments.push(<Appointment>{
                    checkinDate: null,
                    checkoutDate: this.eventInfos[i].date,
                    comment: this.eventInfos[i].comment,
                    userAvatar: this.getUser(this.eventInfos[i].userId).avatar,
                    userLogin: this.getUser(this.eventInfos[i].userId).login
                });
            } else { // we are in 'CheckIn'
                let previousCheckOuts = this.appointments
                    .filter(p => !p.checkinDate && p.userLogin === this.getUser(this.eventInfos[i].userId).login);
                    //.pop();

                if (previousCheckOuts && previousCheckOuts.length) {
                    let previousCheckout = previousCheckOuts.pop();
                    previousCheckout.checkinDate = this.eventInfos[i].date;
                } else {
                    this.appointments.push({
                        checkinDate: this.eventInfos[i].date,
                        checkoutDate: null,
                        comment: this.eventInfos[i].comment,
                        userAvatar: this.getUser(this.eventInfos[i].userId).avatar,
                        userLogin: this.getUser(this.eventInfos[i].userId).login
                    });
                }
                
            }
        }
    }

    private getUser(userId: string): User {
        return this.users.find(user => user.id === userId);
    }

    private buildSafeImgUrl(base64: string): SafeUrl {
        return this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + base64);
    }
}