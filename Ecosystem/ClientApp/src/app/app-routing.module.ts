import { VisualizationComponent } from './comp/visualization.component';
import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './comp/user.component';
import { MessageComponent } from './comp/message.component';
import { NetworkComponent } from './comp/network.component';
import { ServiceComponent } from './comp/service.component';
import { ContainerComponent } from './comp/container.component';
import {PublishComponent } from './comp/publish.component';
import { InsertionComponent } from './comp/insertion.component';
import { LoginComponent } from './comp/login.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'menu',
    pathMatch: 'full'
  },
  { path: 'container', component:ContainerComponent },
  { path: 'user', component: UserComponent },
  { path: 'service', component: ServiceComponent },
  { path: 'network', component: NetworkComponent },
  { path: 'message', component:MessageComponent },
  { path: 'visualization', component:VisualizationComponent },
  { path: 'publish', component:PublishComponent },
  { path: 'insertion', component:InsertionComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
    ]
})
export class AppRoutingModule { }
