import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = {};

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {

  }

  login(form: NgForm) {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/profile');
    }, error => {
      console.log(error);
      this.toastr.error(error.error);
    })
    form.resetForm();
    
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
