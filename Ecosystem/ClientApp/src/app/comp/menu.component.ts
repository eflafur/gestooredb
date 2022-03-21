import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Service } from '../Model/service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  isSelected = false;
  mode = Service.SERVICEMODE;
  @Output() $setMenu: EventEmitter<any> = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }

  Emit(item: string) {
    // var t = Object.keys(this.mode);
    // for (var obj of t) {
    //   if (obj == item)
    //     continue;
    //   this.mode[obj].view = false;
    // }
    // this.mode[item].view = this.mode[item].view == false ? true : false;
    // this.isSelected = this.isSelected == true ? false : true;
    this.$setMenu.emit(item);
  }
}
