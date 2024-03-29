import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
 @Input() totalCount: number;
 @Input() pageSize: number;
 @Output() pageChanged = new  EventEmitter<number>(); // It is important to import it from the angular/core
  
  constructor() { }

  ngOnInit(): void {
  }


  onPagerChanged(event: any){
    this.pageChanged.emit(event.page)
  }
}
