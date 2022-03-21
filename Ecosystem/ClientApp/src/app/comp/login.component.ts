import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { EcosystemService } from '../ecosystem.service';
import { AuthToken } from '../entity/authtoken';
import { User } from '../entity/user';
import { DataModel } from '../Model/querymodel';
import { Service } from '../Model/service';
import * as jsonfile from '../Model/datamodel.json';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username;
  password;
  messageresult = new Array<User>();
  jsondatamodel;

  @Output() authenticate = new EventEmitter();

  constructor(private service: EcosystemService) { }

  ngOnInit(): void {
    this.jsondatamodel = jsonfile['default'].map(k => Object.assign({}, k));
    this.jsondatamodel.map(k => { return jsonfile['default'].map(kk => { if (k.service == kk.service) k.field = kk.field.slice(0) }) });
    this.jsondatamodel.map(k => { return jsonfile['default'].map(kk => { if (k.service == kk.service) k.tablefield = kk.slice(0) }) });
  }

  submit() {
    var userprofile = {};
    userprofile["username"] = this.username;
    userprofile["password"] = this.password;
    this.Authenticate(userprofile);
  }

  Authenticate(userprofile) {
    this.service.Authenticate(userprofile)
      .then((x: AuthToken) => {
        this.service.result = x;
        this.authenticate.emit(true);

        if (this.service.result.user.preference == '') {
          this.service.servicemodel = this.jsondatamodel;
          for (let item of this.service.servicemodel) {
            this.service.table[item.service] = item.tablefield.filter(k => { return !item.field.includes(k) });
          }
        }
        else {
          this.service.servicemodel = JSON.parse(this.service.result.user.preference);
          var nodes = this.jsondatamodel;

          for (let item of this.service.servicemodel) {
            var node = nodes.find(k => { return k.service == item.service });
            this.service.table[item.service] = node.tablefield.filter(k => { return !item.field.includes(k) });
          }
        }
        var node = jsonfile['default'].filter(k => { return !this.service.servicemodel.find(kk => { return k.service == kk.service }) });
        node.forEach(k => {
          this.service.servicemodel.push(k);
        })
      })
      .catch((er: any) => {
        this.messageresult = er;
      });
  }
}
