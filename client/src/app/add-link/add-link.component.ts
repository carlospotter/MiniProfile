import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Links } from '../_models/links';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-add-link',
  templateUrl: './add-link.component.html',
  styleUrls: ['./add-link.component.css']
})
export class AddLinkComponent implements OnInit {
  newLink: any = {}; //Links = {linkName: "", linkUrl: "", id: null};
  userId: number;

  constructor(public bsModalRef: BsModalRef, private userService: UserService) { }

  ngOnInit(): void {
  }

  saveNewLink() {
    this.userService.addNewLink(this.newLink).subscribe(() => {
      this.bsModalRef.hide();
    });
  }

}
