import { Club } from "./club";
import { Player } from "./player";

export class ClubPlayer {

    id: number = -1;
    clubId: number = -1;
    playerId: number = - 1;
    player!: Player;
    club!: Club;
}