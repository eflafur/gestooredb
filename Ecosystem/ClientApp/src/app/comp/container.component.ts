import { Component, Input, OnInit } from '@angular/core';
import { EcosystemService } from '../ecosystem.service';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css']
})
export class ContainerComponent implements OnInit {
  response: any;
  messageresult:any;
  @Input() item = '';
  
  constructor(private service: EcosystemService) { }

  ngOnInit() {
    this.service.postContainerSp()
      .then((x: any) => {
        this.response = x.data;
        var t = x.data[0]["service"];
      })
      .catch((er: any) => {
        this.messageresult = er;
      });
  }

}
