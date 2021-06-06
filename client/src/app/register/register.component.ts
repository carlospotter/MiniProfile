import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { RegisterUser } from '../_models/registerUser';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelReg = new EventEmitter();
  newUser: any = {};
  
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.newUser).subscribe(response => {
      console.log(response);
      this.cancel();
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    this.cancelReg.emit(false);
  }

}
