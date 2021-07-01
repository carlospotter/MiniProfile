import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AccountService } from '../_services/account.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {

  constructor(public bsModalRef: BsModalRef,
              private userService: UserService,
              private accountService: AccountService,
              private router: Router) { }

  ngOnInit(): void {
  }

  deleteUser() {
    this.userService.deleteUser().subscribe(() => {
      this.bsModalRef.hide();
      this.accountService.logout();
      this.router.navigateByUrl('/');
    })
  }

}
