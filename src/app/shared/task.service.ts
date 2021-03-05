import {Injectable} from '@angular/core';
import {MatrixMultiplication} from './MatrixMultiplication.model';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {InputParametrs} from './InputParametrs.model';

@Injectable({
  providedIn: 'root'
})

export class TaskService{
  constructor(private http: HttpClient) {}
  readonly baseUrl = 'http://localhost:61643/api/Task';
  inp: InputParametrs;
  runTask(){
     return this.http.post<MatrixMultiplication[]>(this.baseUrl + '/RunTask', this.inp);
  }

}
