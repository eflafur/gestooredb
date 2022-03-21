import { Component, OnInit } from '@angular/core';
import { EcosystemService } from '../ecosystem.service';
import { User } from '../entity/user';
import { container } from '../entity/container';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Service } from '../Model/service';
import { isNumber } from '@ng-bootstrap/ng-bootstrap/util/util';
import * as JsonFile from '../Model/datamodel.json';
import { temporaryAllocator } from '@angular/compiler/src/render3/view/util';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {
  messageresult = new Array<container>();
  response: any;
  formCreateTable: FormGroup;
  types = Service.TYPES;
  table;
  tablename = '';
  istablename = false;
  count = 0;
  jsontable = '';
  report = 'nn';
  result = '';
  datamodel = Array<any>();
  isBool = false;
  isInt;

  constructor(private service: EcosystemService) {
    this.formCreateTable = new FormGroup({
      serial: new FormControl(''),
      primary: new FormControl(''),
      name: new FormControl('', [Validators.required, Validators.maxLength(15)]),
      type: new FormControl(''),
      //  nullable: new FormControl({ value: null, disabled: false }),
      nullable: new FormControl(''),
      foreign: new FormControl(''),
      default: new FormControl(''),
      datenow: new FormControl(''),
      vardim: new FormControl(''),
      unique: new FormControl('')
    });
  }

  ngOnInit() {
    this.datamodel = JsonFile["default"];
  }

  submit() {
    var fieldlist = new Array<any>();
    var multiFIeldForeignkey: any = null;
    var json = "{\"segreto\":\"" + this.table + ");\"}";
    this.jsontable += "]}"

    this.datamodel = JsonFile["default"];
    var data = {};
    data['service'] = this.tablename;
    data['field'] = (JSON.parse(this.jsontable))[this.tablename];
    data['view'] = 'false';
    data['index'] = this.datamodel.length > 0 ? this.datamodel[this.datamodel.length - 1].index + 1 : 0;
    data['tablefield'] = (JSON.parse(this.jsontable))[this.tablename];
    //GENERAZIONE TABELLA DEI CAMPI

    var appsettings = {};
    var rs = data['field'].filter(k => { return k.includes('_') });
    if (rs.length > 0) {
      var getForeign = data['field'].map(k => {
        return k.split('_');
      });
      var isForeign = getForeign.filter(k => { return (k.length > 1 && k[k.length - 1] == 'id') });
      var isMultiForeign = isForeign.map(k => { return k.length == 3 ? k[0] + '_' + k[1] : k[0] });

      //generazine model per appsettings.json
      appsettings[this.tablename] = isMultiForeign;
      //generazine model per appsettings.json

      for (let item of this.datamodel) {
        for (let itemin of isMultiForeign) {
          if (itemin == item.service) {
            Array.prototype.push.apply(data['tablefield'], (item.field.map(k => { return item.service + '_' + k })));
            data['tablefield'] = data['tablefield'].filter(function (elem, index, self) {
              return index === self.indexOf(elem);
            });
          }
        }
      }
    }

    this.datamodel.push(data);
    var strdatamodel = JSON.stringify(this.datamodel);
    var foreigntable = JSON.stringify(appsettings);

    this.service.setDataRelation(json)
      .then(x => {
        this.result = x;
        this.report = 'yes';
        this.service.saveJsonFile(JSON.stringify(strdatamodel))
          .then(xx => {
            this.result = xx;
            this.report = 'yes';
            if (Object.keys(appsettings).length > 0)
              this.service.saveForeignTable(foreigntable)
                .then(xxx => { this.result = xxx; this.report = 'yes' })
                .catch(kkk => { this.result = kkk; this.report = 'no'; return 0; })
          })
          .catch(kk => { this.result = kk; this.report = 'no'; return 0; })
      })
      .catch(k => {
        this.result = k; this.report = 'no'; return 0;
      });

    this.table = '';
    this.istablename = false;

    this.tablename = '';

    this.jsontable = '';
    this.count = 0;
  }

  gettablename() {
    this.table = "CREATE TABLE " + this.tablename + ' ( ';
    this.jsontable += "{\"" + this.tablename + "\":[";
    this.istablename = true;
    this.formCreateTable.get('name').setValue('id');
    this.formCreateTable.get('primary').setValue('true');
    this.formCreateTable.get('serial').setValue('true');
    this.resetFormId();
  }

  gettype() {
    this.resetForm();
    if (this.formCreateTable.get('type').value != 'foreign') {
      this.formCreateTable.get('nullable').enable();
      this.formCreateTable.get('unique').enable();
    }
    if (this.formCreateTable.get('type').value == 'date') {
      this.formCreateTable.get('datenow').enable();
    }
    if (this.formCreateTable.get('type').value == 'varchar') {
      this.formCreateTable.get('vardim').enable();
    }
    if (Service.ENABLE.number.includes(this.formCreateTable.get('type').value)) {
      this.formCreateTable.get('default').enable();
    }
    if (Service.ENABLE.int.includes(this.formCreateTable.get('type').value)) {
      this.isInt = true;
    }
    if (this.formCreateTable.get('type').value == 'bool') {
      this.isBool = true;
    }
    if (this.formCreateTable.get('type').value == 'foreign key') {
      this.formCreateTable.get('foreign').enable();
      this.formCreateTable.get('name').disable();
      this.formCreateTable.get('nullable').disable();
      this.formCreateTable.get('unique').disable();
    }
  }

  callAdd() {
    var result = this.add();
    if (result == 0) {
      this.result = "Response: " + ['Campi errati'] + ' - Stato: ' + ['Operazione annullata'];
      this.report = "no";
      return;
    }
    this.resetFormBase();
  }

  add(): number {
    var textdefault = '';
    var defaultValue = this.formCreateTable.get('default').value;
    var type = this.formCreateTable.get('type').value;

    if (this.count > 0) {
      if (this.formCreateTable.get('foreign').value == '') {
        var t = this.jsontable.split('"');
        if (t.includes(this.formCreateTable.get('name').value))
          return 0;
        if (this.formCreateTable.get('type').value == '')
          return 0;
        if (this.formCreateTable.get('name').value == '')
          return 0;
      }
      else {
        this.table += ','
        this.jsontable += ',';

        this.jsontable += ("\"" + this.formCreateTable.get('foreign').value + "_id\"");
        this.table += this.formCreateTable.get('foreign').value != '' ? this.formCreateTable.get('foreign').value + "_id int references "
          + this.formCreateTable.get('foreign').value + "(id)" : '';
        this.datamodel = this.datamodel.filter(k => {
          return k.service != this.formCreateTable.get('foreign').value;
        });
        ++this.count;
        return 1;
      }
      this.table += ','
      this.jsontable += ',';
    }
    this.table += this.formCreateTable.get('name').value + ' ';
    this.jsontable += ("\"" + this.formCreateTable.get('name').value + "\"").slice();
    this.table += this.formCreateTable.get('serial').value ? 'serial ' : '';
    this.table += this.formCreateTable.get('primary').value ? 'PRIMARY KEY ' : '';
    this.table += type == 'date' ? 'timestamptz ' : type + ' ';
    this.table += this.formCreateTable.get('datenow').value ? "default now() " : '';
    this.table += this.formCreateTable.get('vardim').value != '' ? "(" + this.formCreateTable.get('vardim').value + ") " : '';
    this.table += defaultValue == '' ? '' : type == 'varchar' || type == 'bool' ? 'DEFAULT ' + "'" + defaultValue + "' " : ' DEFAULT ' + defaultValue+' ' ;
    this.table += this.formCreateTable.get('nullable').value ? "NULL " : '';
    this.table += this.formCreateTable.get('unique').value ? "UNIQUE " : '';
    ++this.count;
    return 1;
  }

  resetForm() {
    this.isBool = false;
    this.isInt = false;
    for (let key in this.formCreateTable.controls) {
      if (key == 'type' || key == 'name')
        continue;
      this.formCreateTable.get(key).setValue('');
      this.formCreateTable.get(key).disable();
    }
  }

  resetFormId() {
    for (let key in this.formCreateTable.controls) {
      if (key != 'serial')
        this.formCreateTable.get(key).disable();
    }
  }

  resetFormBase() {
    for (let key in this.formCreateTable.controls) {
      this.formCreateTable.get(key).setValue('');
      if (key == 'name' || key == 'type')
        this.formCreateTable.get(key).enable();
      else
        this.formCreateTable.get(key).disable();
    }
  }
}
