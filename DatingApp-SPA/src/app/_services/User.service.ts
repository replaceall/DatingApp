import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/User';
import { PaginatedResult } from '../models/Pagination';
import { map } from 'rxjs/operators';

// const httpOptions = {
//   headers: new HttpHeaders({
//     'Authorization': 'Bearer ' + localStorage.getItem('token')
//   })
// };

@Injectable({
  providedIn: 'root'
})
export class UserService {
baseURL = environment.apiUrl;

constructor(private http: HttpClient) { }

  getUsers(page?, pageSize?, userParams?, likesParam?): Observable<PaginatedResult<User[]>> {
    const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
    let params = new HttpParams();
    if (page != null && pageSize != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', pageSize);
    }
    if (userParams != null) {
      params = params.append('minAge', userParams.minAge);
      params = params.append('maxAge', userParams.maxAge);
      params = params.append('gender', userParams.gender);
      params = params.append('orderBy', userParams.orderBy);
    }

    if (likesParam === 'Likers') {
      params = params.append('Likers', 'true');
    }

    if (likesParam === 'Likees') {
      params = params.append('Likees', 'true');
    }
    return this.http
    .get<User[]>(this.baseURL + 'users', { observe: 'response', params })
    .pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(
            response.headers.get('Pagination')
          );
        }
        return paginatedResult;
      })
    );
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseURL + 'users/' + id);
  }
  updateUser(id: number, user: User) {
    return this.http.put(this.baseURL + 'users/' + id, user);
  }
  setMainPhoto(userId: number, id: number) {
    return this.http.post(
      this.baseURL + 'users/' + userId + '/photos/' + id + '/setMain',
      {}
    );
  }

  deletePhoto(userId: number, id: number) {
    return this.http.delete(this.baseURL + 'users/' + userId + '/photos/' + id);
  }
}
