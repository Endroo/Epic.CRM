import { ChangeDetectionStrategy, Component, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { EventColor } from 'calendar-utils';
import { isSameDay, isSameMonth } from 'date-fns';
import { Subject } from 'rxjs';
import { WorkService } from '../work/work.service';

import { registerLocaleData } from '@angular/common';
import localeHu from '@angular/common/locales/hu';
import localeEn from '@angular/common/locales/en';
import { LocalStorageService } from '../common/services/local-storage.service';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
registerLocaleData(localeHu);
registerLocaleData(localeEn);


const colors: Record<string, EventColor> = {
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },

};
@Component({
  selector: 'app-work-calendar',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './work-calendar.component.html',
  styleUrls: ['./work-calendar.component.scss']
})

export class WorkCalendarComponent {
  @ViewChild('modalContent', { static: true }) modalContent!: TemplateRef<any>;
  view: CalendarView = CalendarView.Month;
  CalendarView = CalendarView;
  viewDate: Date = new Date();
  modalData!: {
    action: string;
    event: CalendarEvent;
  };
  refresh = new Subject<void>();

  events: CalendarEvent[] = [];

  activeDayIsOpen: boolean = false;

  locale!: string;

  constructor(
    private localStorageService: LocalStorageService,
    private translateService: TranslateService,
    private modal: NgbModal, private workService: WorkService) {
    this.getCalendarData();

    this.translateService.onLangChange.subscribe((event: LangChangeEvent) => {
      if (event.lang) {
        this.locale = event.lang;
      }
    });
  }


  getCalendarData() {

    this.workService.getCalendar().subscribe(result => {
      if (result) {
        result.forEach(item => {
          this.events.push({
            start: new Date(item.date),
            color: colors['blue'],
            title: item.description,
            draggable: false,
            allDay: true
          });
        });
        this.refresh.next();
      }
    });
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
