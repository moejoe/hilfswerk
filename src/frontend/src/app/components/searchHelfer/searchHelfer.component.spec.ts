import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchHelferComponent } from './searchHelfer.component';

describe('SearchHelferComponent', () => {
  let component: SearchHelferComponent;
  let fixture: ComponentFixture<SearchHelferComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchHelferComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchHelferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
