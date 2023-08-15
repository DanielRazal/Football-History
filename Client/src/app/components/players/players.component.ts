import { Component, OnInit } from '@angular/core';
import { NationalitySelection } from 'src/app/enums/nationalitySelection';
import { PositionSelection } from 'src/app/enums/positionSelection';
import { Player } from 'src/app/models/player';
import { PlayerService } from 'src/app/services/player.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {

  constructor(private playerService: PlayerService, private cookieService: CookieService
    , private router: Router) { }

  players: Array<Player> = [];
  searchTerm:string = "";
  spinnerIcon = faSpinner;
  loading = false;

  ngOnInit(): void {
    setTimeout(() => {
      this.loading = true;
      this.getAllPlayers();
    }, 500); 
  }

  getAllPlayers() {
    this.playerService.getAllPlayers().subscribe((players) => {
      this.players = players;
    })
  }

  getNationalityName(nationality: NationalitySelection): string {
    return this.playerService.getNationalityName(nationality)
  }

  getPositionName(position: PositionSelection): string {
    return this.playerService.getPositionName(position)
  }

  nav(player: Player, route: string) {
    this.cookieService.set('player', JSON.stringify(player));
    this.router.navigate([`${route}`]);
  }
}
