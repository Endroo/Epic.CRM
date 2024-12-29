import { Component } from '@angular/core';
import { EditableMatTableComponent } from '../common/components/editable-mat-table/editable-mat-table.component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomerComponent extends EditableMatTableComponent {

}
