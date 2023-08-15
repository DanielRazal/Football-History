import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import Message from 'src/app/models/message';
import User from 'src/app/models/user';
import { MessageService } from 'src/app/services/message.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import { ApiResponse, UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  usersFilter: User[] = [];
  users: User[] = [];
  // sender
  user!: User;
  // recived
  selectedUser!: User | null;
  searchTerm: string = "";
  messageForm!: FormGroup;
  chatMessages: { user: User; content: string; messageId: number }[] = [];
  message = '';
  lastSelectedUserId: number | null = null;
  lastRecipientId: number | null = null;

  constructor(private userService: UserService, private cookieService: CookieService,
    private messageService: MessageService, private formBuilder: FormBuilder
    , private signalRService: SignalRService) {
  }

  ngOnInit(): void {

    this.messageForm = this.formBuilder.group({
      content: [''],
    });

    this.user = JSON.parse(this.cookieService.get('user'));

    this.getAllUsersFilter();
    this.getAllUsers();

    this.signalRService.addReceiveMessageListener((user, content) => {
      this.chatMessages.push({ user, content, messageId: 1 });
    });

    this.messageForm.get('content')!.valueChanges.subscribe(value => {
      this.message = value;
    });
  }

  sendMessage(user: User, message: string) {
    if (message && message.trim() !== '') {
      this.signalRService.sendMessage(user, message);
      this.addMessage();
      this.loadChatMessages(this.user.id, this.selectedUser!.id);
      this.message = '';
    }
    else {
      alert('Error');
    }
  }


  getAllUsersFilter() {
    this.userService.getAllUsers().subscribe((users) => {
      this.usersFilter = users.filter(user => user.id !== this.user.id);
    });
  }

  openChatWindow(user: User) {
    this.selectedUser = user;
    this.lastSelectedUserId = user.id;

    this.loadChatMessages(this.user.id, user.id);
  }

  getAllUsers() {
    this.userService.getAllUsers().subscribe((users) => {
      this.users = users;
    });
  }

  addMessage() {
    const message: Message = this.messageForm.value;
    if (message.content && this.selectedUser) {
      this.messageService.addMessage(this.user.id, this.selectedUser.id, message).subscribe(() => {
        this.messageForm.reset();
        this.loadChatMessages(this.user.id, this.selectedUser!.id);
      });
    }
  }



  getPhotoUrl(photoUrl: string): string {
    return this.userService.getPhotoUrl(photoUrl);
  }

  loadChatMessages(userId1: number, userId2: number) {
    console.log("User 1:", userId1);
    console.log("User 2:", userId2);

    // Check if both user IDs are valid
    if (userId1 === null || userId2 === null || userId1 === undefined || userId2 === undefined) {
      console.error("One or both user IDs are null or undefined.");
      return;
    }

    this.userService.get2IdsByUsers(userId1, userId2).subscribe(
      (response: ApiResponse) => {
        console.log("API Response:", response);

        const { item1, item2 } = response;
        console.log("User1:", item1);
        console.log("User2:", item2);

        if (!item1 || !item2) {
          console.error("User1 or User2 is not found in the API response.");
          return;
        }

        // Merge the messages of the two users
        if (!item1.messages || !item2.messages) {
          console.error("Messages are missing for user1 or user2.");
          return;
        }

        const chatMessages = this.mergeMessages(item1.messages, item2.messages, item1, item2);
        this.chatMessages = chatMessages;
      },
      (error) => {
        console.error("Error occurred while fetching data:", error);
      }
    );
  }

  mergeMessages(
    messages1: Message[],
    messages2: Message[],
    sender: User,
    receiver: User
  ): { user: User; content: string; messageId: number }[] {
    // Combine messages from both users and sort based on their IDs
    const allMessages: Message[] = [...messages1, ...messages2];
    allMessages.sort((a, b) => a.id - b.id);

    // Transform the Message objects into the expected structure
    const transformedMessages = allMessages.map((message) => {
      // Determine the sender and receiver based on the message user ID
      const user = message.userId === sender.id ? sender : receiver;

      return {
        user,
        content: message.content,
        messageId: message.id,
      };
    });

    return transformedMessages;
  }
}

