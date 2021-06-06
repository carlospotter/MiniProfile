import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Links } from '../_models/links';
import { Profile } from '../_models/profile';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUser(username: string) {
    return this.http.get<Profile>(this.baseUrl + 'users/' + username);
  }

  getProfile() {
    return this.http.get<Profile>(this.baseUrl + 'dashboard/');
  }

  updateProfile(profile: Profile) {
    return this.http.put(this.baseUrl + 'dashboard', profile);
  }

  addNewLink(link: Links) {
    return this.http.post(this.baseUrl + 'dashboard', link);
  }

  deleteLink(linkId: number) {
    return this.http.delete(this.baseUrl + 'dashboard/link/' + String(linkId));
  }
}
