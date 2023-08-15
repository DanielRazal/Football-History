import { Pipe, PipeTransform } from '@angular/core';
import { Player } from '../models/player';
import { PlayerService } from '../services/player.service';

@Pipe({
  name: 'searchFilterPlayer'
})
export class SearchFilterPlayerPipe implements PipeTransform {

  constructor(private playerService: PlayerService) { }


  transform(players: Player[], searchTerm: string): Player[] {
    if (!players || !searchTerm) {
      return players;
    }

    searchTerm = searchTerm.toLowerCase();
    return players.filter(player => {
      return (
        player.fullName.toLowerCase().includes(searchTerm) ||
        player.appearances.toString().includes(searchTerm) ||
        player.goals.toString().includes(searchTerm) ||
        player.assists.toString().includes(searchTerm) || 
        this.playerService.getPositionName(player.position.positionName).toLowerCase().includes(searchTerm) ||
        this.playerService.getNationalityName(player.nationality.nationalityName).toLowerCase().includes(searchTerm)
      );
    });
  }
}
