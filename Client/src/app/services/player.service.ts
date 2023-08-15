import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Player } from '../models/player';
import { NationalitySelection } from '../enums/nationalitySelection';
import { PositionSelection } from '../enums/positionSelection';
import { ClubSelection } from '../enums/clubSelection';
import { ComparePlayersResultDTO } from '../models/comparePlayersResultDTO';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private http: HttpClient) { }

  private baseUrl = environment.baseUrl;
  private api = environment.playerApi;

  getAllPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(this.baseUrl + this.api);
  }

  getPlayerById(id: number): Observable<Player> {
    return this.http.get<Player>(this.baseUrl + this.api + '/' + id);
  }

  comparePlayers(firstId: number, secondId: number) : Observable<ComparePlayersResultDTO>{
    return this.http.get<ComparePlayersResultDTO>(this.baseUrl + this.api + '/' + firstId + '/' + secondId);
  }

  getNationalityName(nationality: NationalitySelection): string {
    return NationalitySelection[nationality];
  }

  getPositionName(position: PositionSelection): string {
    return PositionSelection[position];
  }

  getClubName(club: ClubSelection): string {
    const enumName = ClubSelection[club];
    const displayName = enumName.replace(/_/g, ' ');
    return displayName;
  }

}
