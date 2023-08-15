import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderInsideComponent } from './header-inside.component';

describe('HeaderInsideComponent', () => {
  let component: HeaderInsideComponent;
  let fixture: ComponentFixture<HeaderInsideComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HeaderInsideComponent]
    });
    fixture = TestBed.createComponent(HeaderInsideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
