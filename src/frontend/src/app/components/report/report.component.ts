import { Component, OnInit } from '@angular/core';
import { ReportByMonth, Taetigkeit, ReportDetail, ReportGroupMonth } from 'src/app/models/graphql-models';
import { GraphqlService } from 'src/app/services/graphql.service';
import { group } from '@angular/animations';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  report: ReportByMonth;
  displayedColumns: string[] = ['taetigkeit', 'einsaetze', 'dauer', 'helfer', 'einsaetze2', 'dauer2'];

  constructor(private grapqlService: GraphqlService) {
    this.report = {
      helferInnenGesamt: 0,
      helferInnenEingesetzt: 0,
      groups: []
    };
  }

  ngOnInit(): void {
    var reportSub = this.grapqlService.getReportByMonth()
      .subscribe(report => {
        this.report = report;
        reportSub.unsubscribe();
      });
  }

  totalDauer(group: ReportGroupMonth): number {
    return group.details.reduce((acc, cur) => {
      return acc + cur.dauer;
    }, 0) / 3600;
  }

  totalEinsaetze(group: ReportGroupMonth): number {
    return group.details.reduce((acc, cur) => {
      return acc + cur.einsaetze;
    }, 0);
  }

  dataSource(group: ReportGroupMonth): ReportDetail[] {
    return group.details.sort((a, b) => {
      return a.taetigkeit.localeCompare(b.taetigkeit);
    });
  }

  reportTotalEinsaetze() {
    return this.report.groups.reduce((acc,cur) => {
      return acc + this.totalEinsaetze(cur);
    }, 0)
  }

  reportTotalDauer() {
    return this.report.groups.reduce((acc,cur) => {
      return acc + this.totalDauer(cur);
    }, 0)
  }

  nameOfMonth(month: number): string {
    switch (month) {
      case 1: return "Jänner";
      case 2: return "Februar";
      case 3: return "März";
      case 4: return "April";
      case 5: return "Mai";
      case 6: return "Juni";
      case 7: return "Juli";
      case 8: return "August";
      case 9: return "September";
      case 10: return "Oktober";
      case 11: return "November";
      case 12: return "Dezember";
    }
  }
}
