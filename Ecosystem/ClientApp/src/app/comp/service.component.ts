import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { service } from '../entity/service'
import { EcosystemService } from '../ecosystem.service';
import { Service } from '../Model/service';
import { container } from '../entity/container';
import { Route } from '../entity/route';
import { User } from '../entity/user';
import { DataModel } from '../Model/querymodel';

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.css']
})

export class ServiceComponent implements OnInit {
  openInsert = false;
  formService: FormGroup;
  formContainer: FormGroup;
  formUser: FormGroup;
  response: Array<container>;
  serviceid: any;
  serviceList = DataModel.SERVICEMODE1;
  containerview = false;
  serviceview = false;
  routeview = false;
  userview = false;
  view = false;
  matchItem: string='';
  target: string=null;


  @ViewChild("serviceref") serviceref: ElementRef;
  @ViewChild("servicetyperef") servicetyperef: ElementRef;

  constructor(public service: EcosystemService) {
    this.formService = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      //  ip: new FormControl(null, [Validators.required, Validators.pattern("^((25[0-5]|(2[0-4]|1[0-9]|[1-9]|)[0-9])(\.(?!$)|$)){4}$")]),
      id$servicetype: new FormControl(null),
      type: new FormControl(null, Validators.required),
      //   idenv: new FormControl({ value: null, disabled: true })
      idenv: new FormControl(null),
      service: new FormControl(null),
      type_id: new FormControl(null, Validators.required),
      firstname: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      lastname: new FormControl(null),
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      id: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      routeto: new FormControl(null),
      routefrom: new FormControl(null, Validators.required),
    });
  }

  ngOnInit(): void {
    this.service.GetData("servicetype")
      .then((x: Array<container>) => {
        this.serviceid = x;
      })
      .catch((k) => {
        var error = k;
      });
  }

  onSubmitSp() {
    var jsonData = new Array<string>();
    var idservice;
    var formdata = Object.keys(this.formService.controls);
    for (const key of formdata) {
      var field = this.formService.get(key).value;
      if (field!=null && field!=undefined  && field != '') {
        if (key == 'id_servicetype') {
          field = (this.serviceid.filter((k) => { return k.name == field }).map((kk) => { return kk.id }));
          jsonData.push("\"" + key + "\":" + parseInt(field));
          continue;
        }
        jsonData.push("\"" + key + "\":\"" + field + "\"");
      }
    }
    var jsonDataComplete = "{\"model\":\"" + this.target + "\",\"data\":{" + jsonData.join(',') + "}}";
    this.service.postContainerSp(jsonDataComplete);
    for (const key of formdata) {
      this.formService.get(key).setValue('');
    }
  }

  postContainerSp(data: string) {
    this.service.postContainerSp(data)
      .then((x: any) => {
        this.response = x.data;
        var t = x.data[0]["service"];
      })
      .catch((er: any) => {
      });
  }

  itemSelected(event) {
    this.target = event.target.value;
    this.matchItem = this.serviceList.find((x) => {
      return x.service == this.target;
    })
    this.serviceList.map((x) => { return x.view = false; })
    this.matchItem['view'] = true;
    this.view = true;
  }

  resetText(mode) {
    if (mode == "service")
      this.serviceref.nativeElement.value = '';
    else if (mode == "type")
      this.servicetyperef.nativeElement.value = '';
  }

}
