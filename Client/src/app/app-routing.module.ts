import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlayersComponent } from './components/players/players.component';
import { AboutPlayerComponent } from './components/about-player/about-player.component';
import { ComparePlayersComponent } from './components/compare-players/compare-players.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuardService } from './services/auth-guard.service';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { CompletedQuizAuthGuardService } from './services/completed-quiz-auth-guard.service';
import { ChatComponent } from './components/chat/chat.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'players', component: PlayersComponent, canActivate: [AuthGuardService] },
  { path: 'about-player', component: AboutPlayerComponent, canActivate: [AuthGuardService] },
  { path: 'compare-players', component: ComparePlayersComponent, canActivate: [AuthGuardService] },
  { path: 'quiz', component: QuizComponent, canActivate: [AuthGuardService, CompletedQuizAuthGuardService] },
  // { path: 'chat', component: ChatComponent, canActivate: [AuthGuardService] },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
