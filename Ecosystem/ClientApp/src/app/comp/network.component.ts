import { Component, OnInit } from '@angular/core';
//import {CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import { CdkDragDrop, moveItemInArray, transferArrayItem, CdkDrag } from '@angular/cdk/drag-drop';
// import * as data from '../Model/example.json';
import * as data from '../Model/datamodel.json';

@Component({
  selector: 'app-network',
  templateUrl: './network.component.html',
  styleUrls: ['./network.component.css']
})

export class NetworkComponent {
  model = [];
  tenant = ['name', 'site', 'area'];
  servicecategory = ['aa', 'bb', 'tenant', "cc"];
  container = ["name", "service"];
  service = [];

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }

  testjson() {
    var t = JSON.stringify( data);
  }
}