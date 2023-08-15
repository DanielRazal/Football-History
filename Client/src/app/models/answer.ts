import { Question } from "./question";

export class Answer {

    id: number = -1;
    text: string = "";
    isCorrect: boolean = false;
    questionId: number = -1;
    question!:Question;
}