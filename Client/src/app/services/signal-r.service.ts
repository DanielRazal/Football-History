import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import User from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: HubConnection;
  private baseUrl = environment.baseUrl;
  private userhub = environment.chatHub;

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.baseUrl + this.userhub)
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR connection started.'))
      .catch(err => console.error('Error while starting SignalR connection:', err));
  }

  public addReceiveMessageListener(callback: (user: User, content: string) => void) {
    this.hubConnection.on('ReceiveMessage', callback);
  }

  public sendMessage(recipient: User, message: string) {
    this.hubConnection.invoke('SendMessage', recipient, message)
      .catch(err => console.error('Error while sending SignalR message:', err));
  }
}
