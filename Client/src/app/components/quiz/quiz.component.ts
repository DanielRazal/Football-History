import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Quiz } from 'src/app/models/quiz';
import { SelectedAnswerDTO } from 'src/app/models/selectedAnswerDTO';
import User from 'src/app/models/user';
import { CompletedQuizService } from 'src/app/services/completed-quiz.service';
import { QuizService } from 'src/app/services/quiz.service';
import { SwalService } from 'src/app/services/swal.service';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {
  
  quizzes: Array<Quiz> = [];
  currentQuestionIndex: number = 0;
  user!: User
  addCompletedQuizForm!: FormGroup;
  selectedAnswers: FormArray[] = [];
  timerSeconds: number = 600;
  formattedTime: string = this.formatTime(this.timerSeconds);
  quizTimer: any;
  

  constructor(private quizService: QuizService,
    private completedQuiz: CompletedQuizService, private cookieService: CookieService,
    private formBuilder: FormBuilder, private router: Router, private swalService: SwalService) { }

  ngOnInit(): void {
    this.addCompletedQuizForm = this.formBuilder.group({
      answerId: this.formBuilder.array([])
    });

    this.user = JSON.parse(this.cookieService.get('user'));
    this.GetAllQuizzes();
    this.resetTimer();
  }

  GetAllQuizzes() {
    this.quizService.GetAllQuizzes().subscribe((quizzes) => {
      this.quizzes = quizzes;
    });
  }

  nextQuestion() {
    if (this.currentQuestionIndex < this.quizzes[0].questions.length - 1) {
      this.currentQuestionIndex++;
    }
  }

  prevQuestion() {
    if (this.currentQuestionIndex > 0) {
      this.currentQuestionIndex--;
    }
  }


  private startTimer() {
    this.quizTimer = setInterval(() => {
      this.tick();
    }, 1000);
  }

  tick() {
    this.timerSeconds--;
    this.formattedTime = this.formatTime(this.timerSeconds);
    if (this.timerSeconds <= 0) {
      this.stopTimer();
    }
  }

  stopTimer() {
    clearInterval(this.quizTimer);
    this.addCompletedQuiz();
  }

  resetTimer() {
    this.timerSeconds = 600;
    this.formattedTime = this.formatTime(this.timerSeconds);
    this.startTimer();
  }

  formatTime(totalSeconds: number): string {
    const minutes: number = Math.floor(totalSeconds / 60);
    const seconds: number = totalSeconds % 60;
    const formattedMinutes: string = minutes < 10 ? `0${minutes}` : `${minutes}`;
    const formattedSeconds: string = seconds < 10 ? `0${seconds}` : `${seconds}`;
    return `${formattedMinutes}:${formattedSeconds}`;
  }

  addCompletedQuiz() {
    const selectedAnswerIds = this.addCompletedQuizForm.value.answerId as number[];
    const selectedAnswerDTOs: SelectedAnswerDTO[] = selectedAnswerIds.map((answerId) => ({ answerId }));

    this.completedQuiz.addCompletedQuiz(this.user.id, selectedAnswerDTOs).subscribe(
      (res) => {
        const message = res.message;
        this.swalService.message(message, () => {
          this.router.navigate(['players']);
        });
      },
      (err) => {
        if (err.status === 400) {
          const errorMessage = err.error.message;
          this.swalService.message(errorMessage, () => {
            this.router.navigate(['players']);
          });
        } else {
          console.error('An error occurred:', err);
        }
      }
    );
  }

  updateSelectedAnswerIds(answerId: number) {
    const answerIdsArray = this.addCompletedQuizForm.get('answerId') as FormArray;

    if (this.currentQuestionIndex >= 0 && this.currentQuestionIndex < answerIdsArray.length) {
      answerIdsArray.at(this.currentQuestionIndex).setValue(answerId);
    } else {
      answerIdsArray.push(this.formBuilder.control(answerId));
    }
  }
}
