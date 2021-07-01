import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AddLinkComponent } from '../add-link/add-link.component';
import { DeleteUserComponent } from '../delete-user/delete-user.component';
import { Links } from '../_models/links';
import { Profile } from '../_models/profile';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-profile-update',
  templateUrl: './profile-update.component.html',
  styleUrls: ['./profile-update.component.css']
})
export class ProfileUpdateComponent implements OnInit {
  user: User;
  profile: Profile;
  linksRefresh: Links[];
  bsModalRef: BsModalRef;

  constructor(private userService: UserService, private toastr: ToastrService, private modalService: BsModalService, private router: Router) { }

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile() {
    this.userService.getProfile().subscribe(profile => {
      this.profile = profile;
    });
  }

  updateProfile() {
    this.userService.updateProfile(this.profile).subscribe(() => {
      this.toastr.success("Profile updated!");
    });    
    this.reloadComponent();
  }

  openNewLink() {
    this.bsModalRef = this.modalService.show(AddLinkComponent);
    this.bsModalRef.onHide.subscribe(() => {
      this.updateProfile();
    })

  }

  deleteLink(linkId: number) {
    console.log(linkId);
    this.userService.deleteLink(linkId).subscribe(() => {
      this.toastr.success("Link deleted!");
      this.updateProfile();
    });
  }

  loadLinks() {
    this.userService.getProfile().subscribe(profile => {
      this.linksRefresh = profile.links;
    });
  }

  reloadComponent() {
    let currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
  }

  openDeleteUser(){
    this.bsModalRef = this.modalService.show(DeleteUserComponent);
  }
}
