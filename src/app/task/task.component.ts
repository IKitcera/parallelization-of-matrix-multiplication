import { Component, OnInit } from '@angular/core';
import {MatrixMultiplication} from '../shared/MatrixMultiplication.model';
import {TaskService} from '../shared/task.service';
import { InputParametrs } from '../shared/InputParametrs.model';
@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css'],
  providers: [TaskService]
})
export class TaskComponent implements OnInit {
  constructor(public service: TaskService) { }

  matrices: MatrixMultiplication[];
  showMatrixResult = false;
  inputFormValid = false;
  ngOnInit(): void {
    this.service.inp = new InputParametrs();
  }

  onSubmit(){
    document.getElementById('result').hidden = false;
    this.service.runTask().subscribe(res => {
      this.matrices = res;
    });
  }
  counter(i: number){
    return new Array(i);
  }

  changeMatrixResultVisibility(){
    if (this.showMatrixResult){
      this.showMatrixResult = false;
    }else{
      this.showMatrixResult = true;
    }
  }

}
