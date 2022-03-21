import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MessageComponent } from './comp/message.component';
import { MenuComponent } from './comp/menu.component';
import { NetworkComponent } from './comp/network.component';
import { UserComponent } from './comp/user.component';
import { ServiceComponent } from './comp/service.component';
import { ContainerComponent } from './comp/container.component';
import { VisualizationComponent } from './comp/visualization.component';
import { EcosystemService } from './ecosystem.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
// import { InterceptorService } from './interceptor.service';
import {HttpClientModule} from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InterceptorService } from './interceptor.service';
import { PublishComponent } from './comp/publish.component';
import { InsertionComponent } from './comp/insertion.component';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { LoginComponent } from './comp/login.component';

@NgModule({
  declarations: [
    AppComponent,
    MessageComponent,
    MenuComponent,
    NetworkComponent,
    UserComponent,
    ServiceComponent,
    ContainerComponent,
    VisualizationComponent,
    PublishComponent,
    InsertionComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    DragDropModule
  ],
  providers: [EcosystemService,{ provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
