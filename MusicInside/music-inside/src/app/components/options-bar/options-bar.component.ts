import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PageEvent } from '@angular/material';
import { IOptionsBar, IOptionsEvent } from 'src/app/components/options-bar/options-bar.component.types';

@Component({
  selector: 'app-options-bar',
  templateUrl: './options-bar.component.html',
  styleUrls: ['./options-bar.component.scss']
})
export class OptionsBarComponent implements OnInit {

  @Input() searchPlaceholder: string;
  @Input() length: number;
  @Input() options: IOptionsBar;

  @Output() onModelChanged: EventEmitter<string>;
  @Output() onSearchClick: EventEmitter<IOptionsEvent>;
  @Output() onPageChanged: EventEmitter<IOptionsEvent>;

  public searchParameter: string;

  private _index: number;

  constructor() {
    this.onModelChanged = new EventEmitter();
    this.onSearchClick = new EventEmitter();
    this.onPageChanged = new EventEmitter();
    this.searchParameter = '';
    this._index = 1;
  }

  ngOnInit() { }

  public search(): void {
    this.onSearchClick.emit({
      pageIndex: this._index,
      pageSize: this.options.pageSize,
      searchParameter: this.searchParameter
    });
  }

  public page(e: PageEvent): void {
    this.onPageChanged.emit({
      pageIndex: e.pageIndex + 1,
      pageSize: e.pageSize,
      searchParameter: this.searchParameter
    });
  }

}

