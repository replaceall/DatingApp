import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  constructor() { }

  ngOnInit() {
  }

  RegisterToggle() {
    this.registerMode = true;
  }

  cancelregisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
    console.log(this.registerMode);
  }

}
