import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportsService } from '../../services/reports.service';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  reports: any[] = [];
  userId = 1;

  constructor(private reportsService: ReportsService) {}

  ngOnInit(): void {
    this.reportsService.getMonthlyReport(this.userId)
      .subscribe(data => this.reports = data);
  }
}
