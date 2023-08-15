import { Answer } from "./answer";
import { QuestionType } from "./questionType";
import { Quiz } from "./quiz";

export class Question {

    id: number = -1;
    title: string = "";
    isCorrect: boolean = false;
    questionTypeId: number = -1;
    quizId: number = -1;
    answers: Array<Answer> = [];
    questionType!:QuestionType;
    quiz!:Quiz;
}