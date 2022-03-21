import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { EcosystemService } from '../ecosystem.service';
import { Page } from '../entity/page';
import { Service } from '../Model/service';
import * as JsonFile from '../Model/datamodel.json';

@Component({
  selector: 'app-insertion',
  templateUrl: './insertion.component.html',
  styleUrls: ['./insertion.component.css']
})
export class InsertionComponent implements OnInit {
  serviceList; //Service.SERVICEMODE2;
  response: Array<any>;
  visible = false;
  visiblerow;
  servicerowadded = new Array<number>();
  name: string;
  idenv: string;
  type: string;
  typename: string;
  namecriteria;
  typecriteria;
  servicetypecriteria;
  serviceSelected;
  formService: FormGroup;
  index = 0;
  keys: Array<string>;
  responseItem = new Array<Array<string>>();
  resultModal;
  selected: any;
  testForm: FormGroup;
  pageList = new Array<Page>();
  report = 'nn';
  result='';
  
  constructor(public service: EcosystemService) { }

  @ViewChild("serviceref") serviceref: ElementRef;

  ngOnInit() {
    this.serviceList=JsonFile["default"];
    this.setSettings();
  }

  setSettings() {
    let form = {};
    for (let item of this.serviceList) {
      for (let subitem of item.field) {
        form[subitem] = new FormControl(null);
      }
    }
    this.formService = new FormGroup(form);
  }

  itemSelected(event) {
    this.visible = false;
    this.serviceSelected = event.target.value;
    this.selected = this.serviceList.find((k) => { return k.service == event.target.value });
    this.index = this.selected.index;
    this.serviceref.nativeElement.value = '';
  }

  getSelection() {
    // this.element.nativeElement.ElementRef("#status").value="ciao";
    var json = "{\"model\":\"" + this.serviceSelected + "\",\"data\":{";
    var keys = Object.keys(this.formService.controls);
    var count = 0;
    var isEmpty = Object.values(this.formService.controls).filter(k => { return k.value != null }).length;
    if (isEmpty == 0)
      return;
    for (let item of keys) {
      if (this.formService.controls[item].value != "" && this.formService.controls[item].value != null) {
        if (count > 0)
          json += ",";
        json += "\"" + item + "\": \"" + this.formService.controls[item].value + "\"";
        count++;
      }
    }
    json += "}}";

    this.service.setKey("datapublish", json)
      .then((k:string) => {
        var t=JSON.parse(k);
        this.result="Riga: "+t.id+' inserita - Stato: '+t.status;
        this.report = "yes";
      })
      .catch(k => {
        this.result="Response: "+k['message']+' - Stato: '+k['statusText'];
        this.report = "no"
      });
    for (let key in this.formService.controls) {
      this.formService.controls[key].setValue(null) ;
    }
  }

}


