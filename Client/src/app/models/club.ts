import { ClubSelection } from "../enums/clubSelection";
import { ClubPlayer } from "./clubPlayer";

export class Club {

    id: number = -1;
    clubName: ClubSelection = ClubSelection.None;
    clubPlayers: Array<ClubPlayer> = [];
}