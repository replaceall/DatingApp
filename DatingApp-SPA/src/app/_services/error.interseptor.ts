import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error.status === 401) {
          return throwError(error.statusText);
        }
        if (error instanceof HttpErrorResponse){
          const applicationError = error.headers.get('Application-Error');
          if(applicationError) {
            return throwError(applicationError);
          }
          const serverError = error.error;
          let modelStattErrors = '';
          if(serverError && typeof serverError === 'object') {
            for(const key in serverError) {
              if(serverError[key]) {
                modelStattErrors += serverError[key] + '\n';
              }
            }
          }
          return throwError(modelStattErrors || serverError || 'Server Error');
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  userClass: ErrorInterceptor,
  multi: true
}
