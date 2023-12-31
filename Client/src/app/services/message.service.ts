import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import Message from '../models/message';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private baseUrl = environment.baseUrl;
  private api = environment.messageApi;
  constructor(private http: HttpClient) { }

  private headers() {
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return httpOptions;
  }

  getAllMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(this.baseUrl + this.api);
  }

  addMessage(senderId: number, receiverId: number, message: Message): Observable<Message> {
    return this.http.post<Message>
    (this.baseUrl + this.api + '/' + senderId + '/' + receiverId, message, this.headers());
  }
}
