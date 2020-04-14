import { Component, OnInit, Input } from '@angular/core';
import { Photo } from 'src/app/models/Photo';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() photos: Photo[]
  constructor() { }

  ngOnInit() {
  }

}
