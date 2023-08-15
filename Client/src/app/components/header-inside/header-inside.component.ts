import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Player } from 'src/app/models/player';
import { PlayerService } from 'src/app/services/player.service';
import { CookieService } from 'ngx-cookie-service';
import { SwalService } from 'src/app/services/swal.service';
import User from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { CompletedQuizService } from 'src/app/services/completed-quiz.service';
import { CompletedQuiz } from 'src/app/models/completedQuiz';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-header-inside',
  templateUrl: './header-inside.component.html',
  styleUrls: ['./header-inside.component.css']
})
export class HeaderInsideComponent implements OnInit {

  players: Array<Player> = [];
  showDropdown: boolean = false;
  selectionMade: boolean = false;
  user!: User;
  completedQuizzes: CompletedQuiz[] = [];
  quizId: number = 1;
  quizCompleted = false;

  constructor(private router: Router, private playerService: PlayerService
    , private cookieService: CookieService, private swalService: SwalService,
    private userService: UserService, private completedQuizService: CompletedQuizService) { }

  ngOnInit(): void {
    this.getAllPlayers();
    this.getAllCompletedQuizzes();
    this.user = JSON.parse(this.cookieService.get('user'));

    this.completedQuizService.hasCompletedQuiz(this.user.id, this.quizId)
    .subscribe(hasCompleted => {
      this.quizCompleted = hasCompleted;
    });

  }

  nav(player: Player, route: string) {
    if (!this.selectionMade) {
      this.cookieService.set('player', JSON.stringify(player));
      this.router.navigate([`${route}`])
        .then(() => {
          window.location.reload();
        });
    }
  }


  getAllPlayers() {
    this.playerService.getAllPlayers().subscribe((players) => {
      this.players = players;
    })
  }

  selectPlayer(player: Player) {
    this.selectionMade = true;
    console.log('Selected player:', player.fullName);
  }

  LogOut() {
    this.router.navigate(['login']);
    this.cookieService.deleteAll();
  }

  deleteUser() {
    this.swalService.delete().then((result) => {
      if (result.isConfirmed) {
        if (this.user) {
          this.userService.deleteUser(this.user.id).subscribe(() => {
            this.router.navigate(['login']);
            this.cookieService.deleteAll();
          });
        }
      }
    })
  }

  getPhotoUrl(photoUrl: string): string {
    return this.userService.getPhotoUrl(photoUrl);
  }

  getAllCompletedQuizzes() {
    this.completedQuizService.getAllCompletedQuizzes().subscribe((completedQuizzes) => {
      this.completedQuizzes = completedQuizzes;
    })
  }

  topScore() {
    var quizData = this.completedQuizzes.map((quiz, index) => {
      return `<tr class="border"><td class="border px-4 py-2">${index + 1}</td><td class="border px-4 py-2">${quiz.user.firstName} ${quiz.user.lastName}</td><td class="border px-4 py-2">${quiz.score}</td></tr>`;
    });

    var tableHtml = `<table class="table-auto w-full"><thead><tr><th class="border px-4 py-2">Rank</th><th class="border px-4 py-2">Name</th><th class="border px-4 py-2">Score</th></tr></thead><tbody>${quizData.join(
      ''
    )}</tbody></table>`;

    this.swalService.topScoreAlert('Top Scores', tableHtml);
  }
}
