import { Answer } from "./answer";
import { Question } from "./question";

export class Quiz {

    id: number = -1;
    name: string = "";
    questions: Array<Question> = [];
}