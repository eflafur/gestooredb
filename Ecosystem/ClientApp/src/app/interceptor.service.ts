import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EcosystemService } from './ecosystem.service';

@Injectable()
export class InterceptorService implements HttpInterceptor {
  response: any;

  constructor(private service: EcosystemService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    var url = request.url;
    if (this.service.result != null) {
      this.response = request.clone({
        setHeaders: { Authorization: this.service.result.token }
      });
    }
    else {
      this.response = request;
    }
    return next.handle(this.response as any);
  }
}
