import { Injectable, OnInit } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, catchError, of, tap } from 'rxjs';
import { CompletedQuizService } from './completed-quiz.service';
import User from '../models/user';
import { CookieService } from 'ngx-cookie-service';
import { SwalService } from './swal.service';

@Injectable({
  providedIn: 'root'
})
export class CompletedQuizAuthGuardService implements CanActivate, OnInit {

  constructor(private completedQuizService: CompletedQuizService, private cookieService: CookieService
    , private router: Router, private swalService: SwalService) { }

  user!: User
  userId: number = -1;
  quizId: number = 1;

  ngOnInit(): void {
    this.user = JSON.parse(this.cookieService.get('user'));
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    const user: User = JSON.parse(this.cookieService.get('user'));

    return this.completedQuizService.hasCompletedQuiz(user.id, this.quizId).pipe(
      catchError((error) => {
        if (error.status === 400) {
          this.swalService.error("Error", error.error.message, error.status);
          return of(false);
        } else {
          return of(true);
        }
      }),
      tap((canActivate) => {
        if (!canActivate) {
          this.router.navigate(['/players']);
        }
      })
    );
  }
}