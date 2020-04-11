import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavigationPresitenceService {

  private lastHelfer: string;
  constructor() { }

  storeLastHelfer(id: string) {
    this.lastHelfer = id;
  }

  getLastHelfer() {
    return this.lastHelfer;
  }
  
  clearLastHelfer() {
    this.lastHelfer = null;
  }
}
