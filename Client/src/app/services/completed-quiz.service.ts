import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SelectedAnswerDTO } from '../models/selectedAnswerDTO';
import { CompletedQuiz } from '../models/completedQuiz';

@Injectable({
  providedIn: 'root'
})
export class CompletedQuizService {

  constructor(private http: HttpClient) { }

  private baseUrl = environment.baseUrl;
  private api = environment.completedQuiz;
  private isCompletedQuiz = environment.hasCompletedQuiz;
  private userId = environment.userId;
  private quizId = environment.quizId;

  addCompletedQuiz(userId: number, selectedAnswerDTO: Array<SelectedAnswerDTO>): Observable<CompletedQuiz> {
    return this.http.post<CompletedQuiz>(this.baseUrl + this.api + this.userId + userId, selectedAnswerDTO);
  }

  hasCompletedQuiz(userId: number, quizId: number): Observable<boolean> {
    return this.http.get<boolean>
      (this.baseUrl + this.api + this.isCompletedQuiz + this.userId + userId + this.quizId + quizId);
  }

  getAllCompletedQuizzes(): Observable<CompletedQuiz[]> {
    return this.http.get<CompletedQuiz[]>(this.baseUrl + this.api);
  }

}
