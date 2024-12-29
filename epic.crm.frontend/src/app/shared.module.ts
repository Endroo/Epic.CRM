import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppConfig } from './common/services/app-config.service';
import { MaterialModule } from './material.module';
import { EditableMatTableComponent } from './common/components/editable-mat-table/editable-mat-table.component';

@NgModule({
  declarations: [
    EditableMatTableComponent
  ],
  imports: [
    MaterialModule,
    ReactiveFormsModule,
    CommonModule,
    FormsModule
  ],
  exports: [
    EditableMatTableComponent,
    MaterialModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: loadSettings,
      deps: [AppConfig],
      multi: true
    },
  ]
})
export class SharedModule { }

export function loadSettings(appConfig: AppConfig) {
  return () => appConfig.load();
}
