import { Component } from '@angular/core';
import { ColumnSchema } from '../common/models/column-schema.model';
import { EditableMatTableComponent } from '../common/components/editable-mat-table/editable-mat-table.component';

const COLUMNS_SCHEMA: ColumnSchema[] = [
  {
    key: "name",
    type: "text",
    label: "Full Name"
  },
  {
    key: "occupation",
    type: "text",
    label: "Occupation"
  },
  {
    key: "age",
    type: "number",
    label: "Age"
  },
  {
    key: "nationality",
    type: "text",
    label: "Nationality"
  },
  {
    key: "vdate",
    type: "date",
    label: "vDate"
  },
  {
    key: "isOk",
    type: "combobox",
    label: "isOk"
  },
  {
    key: "isEdit",
    type: "isEdit",
    label: ""
  }
]

const USER_DATA = [
  { "id": 1, "name": "John Smith", "occupation": "Advisor", "age": 36, "nationality": "GER", "vdate": "1984-05-05" },
  { "id": 2, "name": "Muhi Masri", "occupation": "Developer", "age": 28, "nationality": "HR", "vdate": "1984-05-05" },
  { "id": 3, "name": "Peter Adams", "occupation": "HR", "age": 20, "nationality": "CZ", "vdate": "1984-05-05" },
  { "id": 4, "name": "Lora Bay", "occupation": "Marketing", "age": 43, "nationality": "HU", "vdate": "1984-05-05" }
];

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent extends EditableMatTableComponent {
  override displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  override dataSource = USER_DATA;
  columns: ColumnSchema[] = COLUMNS_SCHEMA;
}
