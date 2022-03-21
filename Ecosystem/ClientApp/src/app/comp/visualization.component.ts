// import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
// import { service } from '../entity/service';
// import { EcosystemService } from '../ecosystem.service';
// import { FormControl, FormGroup, Validators } from '@angular/forms';
// import { Page } from '../entity/page';
// import { Service } from '../Model/service';
// import { DataModel } from '../Model/querymodel';
// import * as JsonFile from '../Model/datamodel.json';

// @Component({
//   selector: 'app-visualization',
//   templateUrl: './visualization.component.html',
//   styleUrls: ['./visualization.component.css']
// })

// export class VisualizationComponent implements OnInit {
//   response: Array<any>;
//   visible = false;
//   visiblerow;
//   servicerowadded = new Array<number>();
//   name: string;
//   idenv: string;
//   type: string;
//   typename: string;
//   namecriteria;
//   typecriteria;
//   servicetypecriteria;
//   serviceSelected;
//   formService: FormGroup;
//   index = 0;
//   keys: Array<string>;
//   responseItem = new Array<Array<string>>();
//   resultModal;
//   selected: any;
//   testForm: FormGroup;
//   pageList = new Array<Page>();

//   constructor(public service: EcosystemService) { }

//   @ViewChild("serviceref") serviceref: ElementRef;

//   ngOnInit() {
//     // var node = JsonFile['default'].filter(k => { return this.service.servicemodel.find(kk => { return k.service != kk.service }) });
//     // node.forEach(k => {
//     //   this.service.servicemodel.push(k);
//     // })
//     this.setSettings();
//   }

//   setSettings() {
//     var ss = new Array<string>();
//     for (let item of this.service.servicemodel) {
//       for (let subitem of item.field) {
//         ss.push(subitem);
//       }
//     }

//     let form = {};
//     for (let ll of ss) {
//       form[ll] = new FormControl();
//     }
//     this.formService = new FormGroup(form);
//   }

//   itemSelected(event) {
//     this.visible = false;
//     var count = 0;
//     this.serviceSelected = event.target.value;
//     this.selected = this.service.servicemodel.find((k) => { return k.service == event.target.value });
//     this.index = this.selected.index;
//     this.serviceref.nativeElement.value = '';
//     // this.getPage();

//   }

//   addRow() {
//     this.servicerowadded.push(1);
//     if (this.servicerowadded.length > 1) {
//       let item: object = { Id: '', Name: this.name, IdEnv: this.idenv, Type: this.type, typename: this.typename };
//       this.response.push(item);
//       this.servicerowadded.pop();
//     }
//   }


//   doCheck() {
//     let result = this.response.filter((xx) => { return this.response.find((kk) => { return kk.id.trim() == xx.name.trim() && kk.name.trim() == xx.tls.trim() }) });
//     let result1 = this.response.filter((k => { return k.ip.trim() == '' || k.name.trim() == '' }))

//     if (result.length > 0 || result1.length > 0) {

//       return false;
//     }
//     return true;
//   }

//   submit(event = null) {

//     var page = event != null ? parseInt(event.target.text) : 1;

//     var count = 0;
//     //    var formdata = Object.keys(this.selected.field);
//     var formdata = (this.selected.field);
//     var jsonDataComplete = "{\"model\":\"" + this.serviceSelected + "\",\"page\":" + page + ",\"data\":{";
//     for (const key of formdata) {
//       count += 1;
//       if (key.indexOf('_') != -1)
//         jsonDataComplete += "\"" + key.replace("_", ".") + "\":\"" + this.formService.get(key).value + "\"";
//       else
//         jsonDataComplete += "\"" + this.serviceSelected + "." + key + "\":\"" + this.formService.get(key).value + "\"";
//       if (formdata.length > count)
//         jsonDataComplete += ",";
//     }
//     jsonDataComplete += "}}";

//     this.service.getDataCriteria(jsonDataComplete)
//       .then((x) => {
//         this.extractorData(x);
//         this.getPage(page);
//       })
//       .catch((k) => {
//         var t = k
//       });
//     this.namecriteria = '';
//     this.typecriteria = '';
//     this.servicetypecriteria = '';
//     this.responseItem = new Array<Array<string>>();

//     for (const key of formdata) {
//       this.formService.get(key).setValue('');
//     }
//   }

//   //let obj:Object={};
//   extractorData(data) {
//     var count = 0;
//     var itemsList: String;
//     this.response = data;
//     this.keys = Object.keys(this.response[0]);
//     this.response.forEach((k) => {
//       var obj: Array<any> = Object.values(k);
//       var objnew = obj.map(kk => { return (typeof (kk) == "object") ? Object.keys(kk).length == 0 ? '' : kk : kk });
//       this.responseItem[count++] = objnew;
//     });
//     if (data.length > 0) {
//       this.visible = true;
//     }
//   }

//   getPage(page = null) {
//     this.service.getPage(this.serviceSelected, page)
//       .then((k: Array<Page>) => {
//         this.pageList = k;
//       })
//       .catch(kk => kk);
//   }

// }


// //CAMBIO NOME PARAMETRI
//     // this.selected.field.forEach(element => {
//     //   count++;
//     //   if (this.selected.field.length == count)
//     //     itemsList += element;
//     //   else
//     //     itemsList += element + ','
//     // });

//     // const picked = (({ name, idenv }) => ({ name, idenv }))(this.response[0]);

//     // var newItems = this.response.map((k) => {
//     //       return (({ itemsList }) => ({itemsList }))(k)
//     // })





//OLD
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { service } from '../entity/service';
import { EcosystemService } from '../ecosystem.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Page } from '../entity/page';
import { Service } from '../Model/service';
import { DataModel } from '../Model/querymodel';
import * as JsonFile from '../Model/datamodel.json';

@Component({
  selector: 'app-visualization',
  templateUrl: './visualization.component.html',
  styleUrls: ['./visualization.component.css']
})

export class VisualizationComponent implements OnInit {
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

  constructor(public service: EcosystemService) { }

  @ViewChild("serviceref") serviceref: ElementRef;

  ngOnInit() {
    this.setSettings();
  }

  setSettings() {
    var ss = new Array<string>();
    for (let item of this.service.servicemodel) {
      for (let subitem of item.field) {
        ss.push(subitem);
      }
    }

    let form = {};
    for (let ll of ss) {
      form[ll] = new FormControl();
    }
    this.formService = new FormGroup(form);
  }

  itemSelected(event) {
    this.visible = false;
    var count = 0;
    this.serviceSelected = event.target.value;
    this.selected = this.service.servicemodel.find((k) => { return k.service == event.target.value });
    this.index = this.selected.index;
    this.serviceref.nativeElement.value = '';
    // this.getPage();

  }

  addRow() {
    this.servicerowadded.push(1);
    if (this.servicerowadded.length > 1) {
      let item: object = { Id: '', Name: this.name, IdEnv: this.idenv, Type: this.type, typename: this.typename };
      this.response.push(item);
      this.servicerowadded.pop();
    }
  }


  doCheck() {
    let result = this.response.filter((xx) => { return this.response.find((kk) => { return kk.id.trim() == xx.name.trim() && kk.name.trim() == xx.tls.trim() }) });
    let result1 = this.response.filter((k => { return k.ip.trim() == '' || k.name.trim() == '' }))

    if (result.length > 0 || result1.length > 0) {

      return false;
    }
    return true;
  }

  submit(event = null) {

    var page = event != null ? parseInt(event.target.text) : 1;

    var count = 0;
    //    var formdata = Object.keys(this.selected.field);
    var formdata = (this.selected.field);
    var jsonDataComplete = "{\"model\":\"" + this.serviceSelected + "\",\"page\":" + page + ",\"data\":{";
    for (const key of formdata) {
      count += 1;
      if (key.indexOf('_') != -1)
        jsonDataComplete += "\"" + key.replace("_", ".") + "\":\"" + this.formService.get(key).value + "\"";
      else
        jsonDataComplete += "\"" + this.serviceSelected + "." + key + "\":\"" + this.formService.get(key).value + "\"";
      if (formdata.length > count)
        jsonDataComplete += ",";
    }
    jsonDataComplete += "}}";

    this.service.getDataCriteria(jsonDataComplete)
      .then((x) => {
        this.extractorData(x);
        this.getPage(page);
      })
      .catch((k) => {
        var t = k
      });
    this.namecriteria = '';
    this.typecriteria = '';
    this.servicetypecriteria = '';
    this.responseItem = new Array<Array<string>>();

    for (const key of formdata) {
      this.formService.get(key).setValue('');
    }
  }

  //let obj:Object={};
  extractorData(data) {
    var count = 0;
    var itemsList: String;
    this.response = data;
    this.keys = Object.keys(this.response[0]);
    this.response.forEach((k) => {
      var obj: Array<any> = Object.values(k);
      var objnew = obj.map(kk => { return (typeof (kk) == "object") ? Object.keys(kk).length == 0 ? '' : kk : kk });
      this.responseItem[count++] = objnew;
    });
    if (data.length > 0) {
      this.visible = true;
    }
  }

  getPage(page = null) {
    this.service.getPage(this.serviceSelected, page)
      .then((k: Array<Page>) => {
        this.pageList = k;
      })
      .catch(kk => kk);
  }
}


//CAMBIO NOME PARAMETRI
    // this.selected.field.forEach(element => {
    //   count++;
    //   if (this.selected.field.length == count)
    //     itemsList += element;
    //   else
    //     itemsList += element + ','
    // });

    // const picked = (({ name, idenv }) => ({ name, idenv }))(this.response[0]);

    // var newItems = this.response.map((k) => {
    //       return (({ itemsList }) => ({itemsList }))(k)
    // })
