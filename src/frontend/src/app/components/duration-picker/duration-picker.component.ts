import { Component, OnInit, Input, OnChanges, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'duration-picker',
  templateUrl: './duration-picker.component.html',
  styleUrls: ['./duration-picker.component.scss']
})
export class DurationPickerComponent implements OnInit, OnChanges {

  @Input()
  duration: number;
  @Output()
  durationChange = new EventEmitter<number>();

  hours: number;
  minutes: number;

  constructor() { }

  ngOnChanges(): void {

    if (this.duration) {
      this.hours = Math.floor(this.duration / 3600);
      this.minutes = Math.floor((this.duration - this.hours * 3600) / 60);
    }
    else {
      this.hours = 0;
      this.minutes = 0;
    }
  }
  async onDurationChanged() {
    this.durationChange.emit((this.hours || 0) * 3600 + (this.minutes || 0) * 60);
  }

  ngOnInit(): void {

  }

}
