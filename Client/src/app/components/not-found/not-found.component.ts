import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.css']
})
export class NotFoundComponent {

  token: string = "";
  constructor(private cookieService: CookieService, private router: Router) { }

  ngOnInit(): void {
    this.token = this.cookieService.get('token');
  }

  nav(router: string): void {
    this.router.navigate([router]);
  }

}
