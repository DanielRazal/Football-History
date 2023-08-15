import { Component, OnDestroy, OnInit } from '@angular/core';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { CookieService } from 'ngx-cookie-service';
import { ClubSelection } from 'src/app/enums/clubSelection';
import { NationalitySelection } from 'src/app/enums/nationalitySelection';
import { PositionSelection } from 'src/app/enums/positionSelection';
import { Player } from 'src/app/models/player';
import { PlayerService } from 'src/app/services/player.service';

@Component({
  selector: 'app-about-player',
  templateUrl: './about-player.component.html',
  styleUrls: ['./about-player.component.css']
})
export class AboutPlayerComponent implements OnInit, OnDestroy {

  player!: Player;
  loading = false;
  spinnerIcon = faSpinner;

  constructor(private cookieService: CookieService, private playerService: PlayerService) { }
  
  ngOnDestroy(): void {
    this.cookieService.delete('player')
  }

  ngOnInit(): void {
    setTimeout(() => {
      this.loading = true;
      this.player = JSON.parse(this.cookieService.get('player'));
    }, 500); 
  }

  getNationalityName(nationality: NationalitySelection): string {
    return this.playerService.getNationalityName(nationality)
  }

  getPositionName(position: PositionSelection): string {
    return this.playerService.getPositionName(position)
  }

  getClubName(club: ClubSelection): string {
    return this.playerService.getClubName(club)
  }
}
