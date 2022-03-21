import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
//import {CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import { CdkDragDrop, moveItemInArray, transferArrayItem, CdkDrag } from '@angular/cdk/drag-drop';
import { Service } from '../Model/service';
import { EcosystemService } from '../ecosystem.service';
import { JsonpClientBackend } from '@angular/common/http';
import * as jsonfile from '../Model/datamodel.json';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  tables: any;
  table = new Array<any>();
  model: string;
  service = [];
  visible = false;
  report = 'nn';
  result = '';
  jsondatamodel = jsonfile['default'];

  constructor(public http: EcosystemService) {
  }

  @ViewChild("select") select: ElementRef;

  ngOnInit(): void {
    this.tables = this.jsondatamodel;
  }

  getTable(event) {
    this.service = new Array<string>();
    this.model = event.target.value;
    this.table = this.http.table[this.model];
    this.service = this.http.servicemodel.find(k => { return k.service == this.model }).field;
    this.select.nativeElement.value = '';
    this.visible = true;
  }

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

  submit(temp = null) {
    this.http.setModel(this.model, this.service, this.table, temp)
      .then((k: string) => {
        var t = JSON.parse(k);
        this.result = "Riga: " + t.id + ' inserita - Stato: ' + t.status;
        this.report = "yes";
      })
      .catch(k => {
        this.result = "Response: " + k['message'] + ' - Stato: ' + k['statusText'];
        this.report = "no"
      });
    this.visible = false;
  }
}