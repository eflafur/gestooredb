import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthToken } from './entity/authtoken';
import { Page } from './entity/page';
import { User } from './entity/user';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { Subject } from 'rxjs';
import { Service } from './Model/service';
import { DataModel } from './Model/querymodel';


const URL = "http://localhost:3333/api/UserAuth/";
const URLPOSTGRES = "http://localhost:4444/api/Scalar/getuser?id_corenetwork=1&id=3";
const URLPOSTGRESUSER = "http://localhost:4444/api/Device/";
const URLPOSTGREDATA = "http://localhost:4444/api/Data/";


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

const headerDict = {
  'Content-Type': 'application/json',
  'Accept': 'application/json',
  'Access-Control-Allow-Headers': 'Content-Type',
};
@Injectable({
  providedIn: 'root'
})
export class EcosystemService {
  showmenu$ = new Subject<number>();
  result: AuthToken;
  servicemodel;
  table = {};
  isAuthentcate=false;

  constructor(private httpclient: HttpClient) {
  }

  Authenticate(Value: any) {
    return this.httpclient.post<AuthToken>(URL + 'Auth', { "UserName": Value.username, "Password": Value.password }).toPromise();
  }

  postService(value: any = null) {
    return this.httpclient.post<any>(URLPOSTGRESUSER + 'postservice', value).toPromise();
  }

  postContainerSp(Value: string = null) {
    const headers = { 'content-type': 'application/json' }
    return this.httpclient.post<any>(URLPOSTGRESUSER + 'postcontainersp', Value, { headers: headerDict }).toPromise();
  }

  getDataCriteria(Value: string = null) {
    const headers = { 'content-type': 'application/json' }
    return this.httpclient.post<Array<any>>(URLPOSTGRESUSER + 'getdatacriteria', Value, { headers: headerDict }).toPromise();
  }

  GetData(model: string) {
    const headers1 = { 'content-type': 'application/json' };
    const extension = 'getuser?id_corenetwork=1&id=3';
    return this.httpclient.get<Array<any>>(URLPOSTGRESUSER + 'public/get/' + model).toPromise();
  }

  setuser2(): Promise<any> {
    return this.httpclient.get<any>(URL + "setuser").toPromise();
  }


  getPage(model, id: number) {
    return this.httpclient.get<Array<Page>>(URLPOSTGREDATA + model + '/' + id).toPromise();

  }
  setKey(model, Value) {
    return this.httpclient.post<string>(URLPOSTGREDATA + "setkey", Value, { headers: headerDict }).toPromise();
  }

  setDataRelation( Value) {
    return this.httpclient.post<string>(URLPOSTGREDATA + "createtable", Value, { headers: headerDict }).toPromise();
  }

  saveJsonFile( Value) {
    return this.httpclient.post<string>(URLPOSTGREDATA + "savejsonfile", Value, { headers: headerDict }).toPromise();
  }
  saveForeignTable( Value) {
    return this.httpclient.post<string>(URLPOSTGREDATA + "saveforeigntable", Value, { headers: headerDict }).toPromise();
  }

  setModel(model, service, table = null, temp = null) {
    this.table[model] = table;
    this.servicemodel.map(K => { if(K.service==model) K.field = [] });
      this.servicemodel.forEach(k => {
        if (k.service == model) {
          service.forEach(kk => {
            k.field.push(kk);
          })
        }
      })
    if (temp == null) {
      var user = this.result.user;
      user.preference = JSON.stringify(this.servicemodel);
      return this.httpclient.post<string>(URL + "inserttable", user, { headers: headerDict }).toPromise();
    }
    return new Promise((resolve, reject) => {
      resolve("modello aggiornato");
    })
  }
}