import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EcosystemService } from './ecosystem.service';
import { AuthToken } from './entity/authtoken';
import { User } from './entity/user';
import { ContainerComponent } from './comp/container.component';
import { LoginComponent } from './comp/login.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  visible = 0;
  // @ViewChild(FlussiComponent, { static: false }) flussi: FlussiComponent;
  //@ViewChild("registro", { static: false }) reg;
  mytext: string = "";
  messageresult = new Array<User>();
  user: User;
  title: string;
  isAuthenticate= false;

  constructor(private service: EcosystemService, private element: ElementRef, public router: Router) {
    this.user = new User()
    this.user.id = 1;
    this.user.username = "dbovan";
    this.user.password = "ceccolozzopozzo";
  }

  ngOnInit(): void {
  }
  // @HostListener("dblclick", ['$event'])
  // showregistro(event) {
  //   if (event.target.tagName == "INPUT") {
  //     this.visible = 5;
  //     this.mytext = event.target.value;
  //   }
  // }

  getAuthenticate(event) {
    this.isAuthenticate = event;
  }

  Start2(event) {
    this.service.GetData("service")
      .then((x: Array<User>) => {
        this.messageresult = x;
        const link = [event];
        this.router.navigate(link);
      })
      .catch((er: any) => {
        this.messageresult = er;
      });
  }

  postMessage(event, message: string = '') {
    // this.service.postData()
    // .then((x:any) => {
    const link = [event];
    this.router.navigate(link);
  }

  SwitchPage(event) {
    this.title = event;
    const link = [event];
    this.router.navigate(link);
  }

  // showregistro1(menu: number) {
  //   this.element.nativeElement.querySelector("#reg").style.display = "block";

  //   this.visible = 2;
  //   this.service.getLottis("menu").subscribe((x) => {
  //     x.map((k) => {
  //       k.Status = (k.Status * 10);
  //       k.DataCarico = moment(k.DataCarico).format('MM/DD/YYYY', 'LL');
  //     })
  //     this.dati = x;
  //   });
  // }

}
