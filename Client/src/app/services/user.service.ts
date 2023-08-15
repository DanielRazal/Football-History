import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import Login from '../models/login';
import User from '../models/user';
import UserDto from '../models/userDTO';

export interface ApiResponse {
  result: any;
  item1: User;
  item2: User;
}

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private baseUrl = environment.baseUrl;
  private api = environment.userApi;
  private loginUrl = environment.login;
  private registration = environment.registration;
  private detailsUrl = environment.details;

  constructor(private http: HttpClient) { }

  private headers() {
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      withCredentials: true
    };
    return httpOptions;
  }

  login(user: Login): Observable<Login> {
    return this.http.post<Login>(this.baseUrl + this.api + this.loginUrl, user, this.headers())
  }

  sendDetailsToEmail(email: string): Observable<any> {
    return this.http.post<any>(this.baseUrl + this.api + this.detailsUrl + '?email=' + encodeURIComponent(email), { email }, this.headers());
  }

  register(formData: FormData): Observable<any> {

    return this.http.post<any>(this.baseUrl + this.api + this.registration, formData);
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + this.api + '/' + id);
  }

  deleteUser(id: number): Observable<User> {
    return this.http.delete<User>(this.baseUrl + this.api + '/' + id);
  }


  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + this.api);
  }

  getPhotoUrl(photoUrl: string): string {
    const serverBaseUrl = environment.baseUrl;
    if (photoUrl.startsWith('http') || photoUrl.startsWith('https')) {
      return photoUrl;
    }
    else {
      return `${serverBaseUrl}${photoUrl}`;
    }
  }


  get2IdsByUsers(firstId: number, secondId: number): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(this.baseUrl + this.api + '/' + firstId + '/' + secondId)
  }

  fromData(userDto: UserDto, selectedPhoto: File | undefined): FormData {
    const formData = new FormData();
    formData.append('UserDTO.FirstName', userDto.firstName);
    formData.append('UserDTO.LastName', userDto.lastName);
    formData.append('UserDTO.UserName', userDto.userName);
    formData.append('UserDTO.Password', userDto.password);
    formData.append('UserDTO.Email', userDto.email);
    formData.append('UserDTO.AcceptedTerms', userDto.acceptedTerms.toString());
    formData.append('Photo', selectedPhoto!);
    return formData;
  }

}