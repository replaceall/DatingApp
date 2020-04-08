import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../_services/User.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-members-list',
  templateUrl: './members-list.component.html',
  styleUrls: ['./members-list.component.css']
})
export class MembersListComponent implements OnInit {
  users: User[];
  constructor(private userService: UserService, private alertify: AlertifyService, private route: Router) { }

  ngOnInit() {
    this.loadusers();
  }
  loadusers() {
    this.userService.getUsers().subscribe( (users: User[]) => {
      this.users = users;
    }, error => {
      this.alertify.error(error);
    });
  }

}
