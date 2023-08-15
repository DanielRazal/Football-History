import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderOutsideComponent } from './header-outside.component';

describe('HeaderOutsideComponent', () => {
  let component: HeaderOutsideComponent;
  let fixture: ComponentFixture<HeaderOutsideComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HeaderOutsideComponent]
    });
    fixture = TestBed.createComponent(HeaderOutsideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
