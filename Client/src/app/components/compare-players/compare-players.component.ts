import { Component } from '@angular/core';
import { ComparePlayersResultDTO } from 'src/app/models/comparePlayersResultDTO';
import { Player } from 'src/app/models/player';
import { PlayerService } from 'src/app/services/player.service';
import { SwalService } from 'src/app/services/swal.service';

@Component({
  selector: 'app-compare-players',
  templateUrl: './compare-players.component.html',
  styleUrls: ['./compare-players.component.css']
})
export class ComparePlayersComponent {

  showFirstDropdown: boolean = false;
  showSecondDropdown: boolean = false;
  selectedFirstPlayer: Player | null = null;
  selectedSecondPlayer: Player | null = null;
  comparePlayersResultDTO: ComparePlayersResultDTO | null = null;
  selectPlayer2 = "Select Player";
  selectPlayer1 = "Select Player";
  players: Array<Player> = [];

  constructor(private playerService: PlayerService, private swalService: SwalService) { }


  ngOnInit(): void {
    this.getAllPlayers();
  }

  getAllPlayers() {
    this.playerService.getAllPlayers().subscribe((players) => {
      this.players = players;
    })
  }

  selectFirstPlayer(player: Player | null) {
    if (player) {
      this.playerService.getPlayerById(player.id).subscribe((selectedPlayer) => {
        this.selectedFirstPlayer = selectedPlayer;
        this.selectPlayer1 = this.selectedFirstPlayer.fullName;
        this.showFirstDropdown = false;
      });
    } else {
      this.selectedFirstPlayer = null;
      this.selectPlayer1 = "Select Player";
      this.showFirstDropdown = false;
    }
  }


  selectSecondPlayer(player: Player | null) {
    if (player) {
      this.playerService.getPlayerById(player.id).subscribe((selectedPlayer) => {
        this.selectedSecondPlayer = selectedPlayer;
        this.selectPlayer2 = this.selectedSecondPlayer.fullName;
        this.showSecondDropdown = false;
      });
    } else {
      this.selectedSecondPlayer = null;
      this.selectPlayer2 = "Select Player";
      this.showSecondDropdown = false;
    }
  }


  comparePlayers(firstPlayer: Player, secondPlayer: Player) {
    if (!firstPlayer || !secondPlayer) {
      this.swalService.error("Error", "Please select both Player 1 and Player 2.", "");
      return;
    }

    this.playerService.comparePlayers(firstPlayer.id, secondPlayer.id).subscribe(
      (res) => {
        this.comparePlayersResultDTO = res;
        this.swalService.message(this.comparePlayersResultDTO.message);
      },
      (error: any) => {
        if (error.error) {
          this.swalService.error('Error', error.error, `Status code: ${error.status}`);
        } else {
          console.error(error);
        }
      }
    );
  }
}