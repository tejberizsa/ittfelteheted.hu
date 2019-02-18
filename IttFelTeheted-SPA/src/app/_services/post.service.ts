import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Post } from '../_models/post';
import { Observable } from 'rxjs';
import { Topic } from '../_models/topic';
import { Answer } from '../_models/answer';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

  getPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(this.baseUrl + 'post/');
  }

  getTopics(): Observable<Topic[]> {
    return this.http.get<Topic[]>(this.baseUrl + 'topic/');
  }

  getPost(id: number): Observable<Post> {
    return this.http.get<Post>(this.baseUrl + 'post/' + id);
  }

  getTrendingPosts(id: number): Observable<Post[]> {
    return this.http.get<Post[]>(this.baseUrl + 'post/trending/' + id);
  }

  sendAnswer(postId: number, answer: Answer) {
    return this.http.post(this.baseUrl + 'post/' + postId + '/addanswer', answer);
  }
}
