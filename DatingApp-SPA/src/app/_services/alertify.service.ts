import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }
  confirm(message: string, okcallback: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okcallback();
      } else {}
  });
  }

  error(message: string) {
    alertify.error(message);
  }

  warning(message: string) {
    alertify.warning(message);
  }

  success(message: string) {
    alertify.success(message);
  }

  message(message: string) {
    alertify.message(message);
  }

}
