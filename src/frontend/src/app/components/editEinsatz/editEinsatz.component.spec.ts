import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEinsatzComponent } from './editEinsatz.component';

describe('CreateHelferComponent', () => {
  let component: EditEinsatzComponent;
  let fixture: ComponentFixture<EditEinsatzComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditEinsatzComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditEinsatzComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
