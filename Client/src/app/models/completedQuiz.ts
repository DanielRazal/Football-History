
import { Quiz } from "./quiz";
import { SelectedAnswer } from "./selectedAnswer";
import User from "./user";

export class CompletedQuiz {
    id: number = -1;
    score: number = -1;
    userId: number = -1;
    quizId: number = -1;
    user!: User;
    quiz!: Quiz;
    selectedAnswers: Array<SelectedAnswer> = [];
    message: string = "";
}