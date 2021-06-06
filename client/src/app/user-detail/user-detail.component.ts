import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Profile } from '../_models/profile';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  username: string;
  userDetails: Profile;

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit(): void {
    this.username = this.route.snapshot.paramMap.get('username');
    this.getUserDetail();
  }

  getUserDetail() {
    this.userService.getUser(this.username).subscribe(res => {
      this.userDetails = res;
    });
  }

}
