import { Component, OnInit } from '@angular/core';
import { EcosystemService } from '../ecosystem.service';
import * as JsonFile from '../Model/datamodel.json';

@Component({
  selector: 'app-publish',
  templateUrl: './publish.component.html',
  styleUrls: ['./publish.component.css']
})

export class PublishComponent {
  key: string
  fillColor = 'rgb(255, 0, 0)';
  datamodel;

  constructor(public service: EcosystemService) { }

  ngOnInit() {
    this.datamodel = JsonFile['default'];
  }

  changeColor() {
    const r = Math.floor(Math.random() * 256);
    const g = Math.floor(Math.random() * 256);
    const b = Math.floor(Math.random() * 256);
    this.fillColor = `rgb(${r}, ${g}, ${b})`;
  }

}
