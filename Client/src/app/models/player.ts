import { ClubPlayer } from "./clubPlayer";
import Nationality from "./nationality";
import Position from "./position";

export class Player {

    id: number = -1;
    photo: string = "";
    fullName: string = "";
    goals: number = -1;
    assists: number = -1;
    goalContributions: number = -1;
    titles: number = -1;
    appearances: number = -1;
    dateOfBirth: string = "";
    goalsRatio: number = -1;
    assistsRatio: number = -1;
    information: string = "";
    score: number = -1;
    positionId: number = -1;
    nationalityId: number = -1;
    position!:Position;
    nationality!:Nationality;
    clubPlayers: Array<ClubPlayer> = [];


}