import { Component, Input, OnInit } from '@angular/core';
import { ColumnSchema } from '../../models/column-schema.model';

@Component({
  selector: 'app-editable-mat-table',
  templateUrl: './editable-mat-table.component.html',
  styleUrls: ['./editable-mat-table.component.scss']
})
export class EditableMatTableComponent implements OnInit {
  @Input() columnsSchema!: ColumnSchema[];
  @Input() dataSource!: any[];
  displayedColumns: string[] = [];

  checkboxEelements!: any[];

  ngOnInit(): void {
    if (this.columnsSchema !== undefined) {
      this.displayedColumns = this.columnsSchema.map((col) => col.key);
    }
  }

  addRow() {
    const newRow = this.columnsSchema[0];
    this.dataSource = [newRow, ...this.dataSource];
  }

  removeRow(id: number) {
    this.dataSource = this.dataSource.filter(u => u.id !== id);
  }
}
