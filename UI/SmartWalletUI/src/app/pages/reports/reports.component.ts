import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ReportsService, MonthlyReport } from '../../services/reports.service';
import Chart from 'chart.js/auto';
import jsPDF from 'jspdf';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  monthlyReports: MonthlyReport[] = [];
  selectedReport!: MonthlyReport;

  monthlyChart: any;
  savingsChart: any;

  savingsGoals: { title: string; amount: number }[] = [];
  newGoalTitle: string = '';
  newGoalAmount: number = 0;

  constructor(
    private reportsService: ReportsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const userId = 1;

    this.reportsService.getMonthlyReports(userId)
      .subscribe(data => {
        this.monthlyReports = data;
        if (data.length > 0) {
          this.selectedReport = data[data.length-1];
        }
      });
  }

  generateReport() {
    if (!this.selectedReport) return;

    if (this.monthlyChart) this.monthlyChart.destroy();

    this.monthlyChart = new Chart('monthlyCanvas', {
      type: 'pie',
      data: {
        labels: ['Income', 'Expense', 'Balance'],
        datasets: [{
          data: [
            this.selectedReport.totalIncome,
            this.selectedReport.totalExpense,
            this.selectedReport.netBalance
          ],
          backgroundColor: ['#00c853', '#d50000', '#2962ff']
        }]
      },
      options: {
        animation: {
          animateRotate: true,
          duration: 1500
        }
      }
    });
  }

  addSavingsGoal() {
    if (!this.newGoalTitle || this.newGoalAmount <= 0) return;

    this.savingsGoals.push({
      title: this.newGoalTitle,
      amount: this.newGoalAmount
    });

    this.newGoalTitle = '';
    this.newGoalAmount = 0;

    this.generateSavingsChart();
  }

  generateSavingsChart() {
    if (this.savingsChart) this.savingsChart.destroy();

    this.savingsChart = new Chart('savingsCanvas', {
      type: 'pie',
      data: {
        labels: this.savingsGoals.map(g => g.title),
        datasets: [{
          data: this.savingsGoals.map(g => g.amount),
          backgroundColor: ['#7b2ff7', '#f107a3', '#ff6f00', '#00bfa5']
        }]
      }
    });
  }

  exportPDF() {
    const doc = new jsPDF();

    doc.setFontSize(16);
    doc.text('Monthly Financial Report', 20, 20);

    let y = 35;

    this.monthlyReports.forEach(r => {
      doc.text(
        `Month: ${r.month}/${r.year} | Income: ${r.totalIncome} | Expense: ${r.totalExpense} | Balance: ${r.netBalance}`,
        20,
        y
      );
      y += 10;
    });

    doc.save('Financial_Report.pdf');
  }

  exportExcel() {
    const data = this.monthlyReports.map(r => ({
      Month: `${r.month}/${r.year}`,
      Income: r.totalIncome,
      Expense: r.totalExpense,
      Balance: r.netBalance
    }));

    const ws = XLSX.utils.json_to_sheet(data);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Report');

    XLSX.writeFile(wb, 'Financial_Report.xlsx');
  }

  backToDashboard() {
    this.router.navigate(['/dashboard']);
  }
}