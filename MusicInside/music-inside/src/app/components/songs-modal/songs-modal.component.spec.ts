import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SongsModalComponent } from './songs-modal.component';

describe('SongsModalComponent', () => {
  let component: SongsModalComponent;
  let fixture: ComponentFixture<SongsModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SongsModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SongsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
