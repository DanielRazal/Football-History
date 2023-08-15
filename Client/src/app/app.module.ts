import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { PlayersComponent } from './components/players/players.component';
import { AboutPlayerComponent } from './components/about-player/about-player.component';
import { HeaderInsideComponent } from './components/header-inside/header-inside.component';
import { ComparePlayersComponent } from './components/compare-players/compare-players.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderOutsideComponent } from './components/header-outside/header-outside.component';
import { RegisterComponent } from './components/register/register.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { ChatComponent } from './components/chat/chat.component';
import { SearchFilterUserPipe } from './pipes/search-filter-user.pipe';
import { SearchFilterPlayerPipe } from './pipes/search-filter-player.pipe';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';

library.add(fas);


@NgModule({
  declarations: [
    AppComponent,
    PlayersComponent,
    AboutPlayerComponent,
    HeaderInsideComponent,
    ComparePlayersComponent,
    LoginComponent,
    HeaderOutsideComponent,
    RegisterComponent,
    NotFoundComponent,
    QuizComponent,
    ChatComponent,
    SearchFilterUserPipe,
    SearchFilterPlayerPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
