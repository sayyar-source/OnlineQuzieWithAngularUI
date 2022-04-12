

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment} from '../../environments/environment'

import { User } from '../models/user';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<User[]>(`${environment.apiUrl}/users`);
  }

  register(user: User) {
    var url = `${environment.apiUrl}/api/accounts/register`;
    alert("register")
    console.log(url)
    return this.http.post(url, user);
  }
  login(user: User) {
    var url = `${environment.apiUrl}/api/accounts/login`;
    
    return this.http.post(url, user);
  }

  delete(id: number) {
    return this.http.delete(`${environment.apiUrl}/users/${id}`);
  }
}
