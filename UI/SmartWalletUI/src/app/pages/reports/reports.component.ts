import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Chart, registerables, ChartData, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { ReportService, ReportSummary } from '../../services/reports.service';
import * as XLSX from 'xlsx';
import * as FileSaver from 'file-saver';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

// 🔥 VERY IMPORTANT
Chart.register(...registerables);

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  userId = 1;

  incomeReports: ReportSummary[] = [];
  expenseReports: ReportSummary[] = [];

  totalIncome = 0;
  totalExpense = 0;
  balance = 0;
  
  showIncomeChart = false;
showExpenseChart = false;

  chartType: ChartType = 'pie';

  incomeChartData: ChartData<'pie'> = {
    labels: [],
    datasets: [{
      data: [],
      backgroundColor: ['#2ecc71', '#27ae60', '#1abc9c', '#16a085']
    }]
  };

  expenseChartData: ChartData<'pie'> = {
    labels: [],
    datasets: [{
      data: [],
      backgroundColor: ['#e74c3c', '#c0392b', '#ff6b6b', '#ff7675']
    }]
  };

  constructor(private reportService: ReportService) {}

  ngOnInit(): void {
    this.loadReports();
  }

  loadReports(): void {
    this.reportService.getIncomeReport(this.userId).subscribe(res => {
      this.incomeReports = res;
      this.calculateSummary();
      this.updateCharts();
    });

    this.reportService.getExpenseReport(this.userId).subscribe(res => {
      this.expenseReports = res;
      this.calculateSummary();
      this.updateCharts();
    });
  }
   
    downloadExcel(): void {

  const incomeSheet = XLSX.utils.json_to_sheet(this.incomeReports);
  const expenseSheet = XLSX.utils.json_to_sheet(this.expenseReports);

  const workbook: XLSX.WorkBook = {
    Sheets: {
      Income: incomeSheet,
      Expense: expenseSheet
    },
    SheetNames: ['Income', 'Expense']
  };

  const excelBuffer = XLSX.write(workbook, {
    bookType: 'xlsx',
    type: 'array'
  });

  const data: Blob = new Blob([excelBuffer], {
    type: 'application/octet-stream'
  });

  FileSaver.saveAs(data, 'SmartWallet_Report.xlsx');
}

downloadPDF(): void {
  const doc = new jsPDF();

  // Title
  doc.setFontSize(16);
  doc.text('SmartWallet Report', 14, 15);

  // Summary
  doc.setFontSize(11);
  doc.text(`Total Income: ₹ ${this.totalIncome}`, 14, 25);
  doc.text(`Total Expense: ₹ ${this.totalExpense}`, 14, 32);
  doc.text(`Balance: ₹ ${this.balance}`, 14, 39);

  // Income Table
  autoTable(doc, {
    startY: 48,
    head: [['Income Category', 'Amount (₹)']],
    body: this.incomeReports.map(i => [i.category, i.totalAmount]),
    theme: 'grid',
    headStyles: { fillColor: [46, 204, 113] }
  });

  // Expense Table
  autoTable(doc, {
    startY: (doc as any).lastAutoTable.finalY + 10,
    head: [['Expense Category', 'Amount (₹)']],
    body: this.expenseReports.map(e => [e.category, e.totalAmount]),
    theme: 'grid',
    headStyles: { fillColor: [231, 76, 60] }
  });

  doc.save('SmartWallet_Report.pdf');
}

  calculateSummary(): void {
    this.totalIncome = this.incomeReports.reduce((s, i) => s + i.totalAmount, 0);
    this.totalExpense = this.expenseReports.reduce((s, e) => s + e.totalAmount, 0);
    this.balance = this.totalIncome - this.totalExpense;
  }

 updateCharts(): void {

  // 🔴 reset first (important)
  this.showIncomeChart = false;
  this.showExpenseChart = false;

  // ✅ IMMUTABLE assignment (KEY FIX)
  this.incomeChartData = {
    labels: this.incomeReports.map(i => i.category),
    datasets: [{
      data: this.incomeReports.map(i => i.totalAmount),
      backgroundColor: ['#2ecc71', '#27ae60', '#1abc9c', '#16a085']
    }]
  };

  this.expenseChartData = {
    labels: this.expenseReports.map(e => e.category),
    datasets: [{
      data: this.expenseReports.map(e => e.totalAmount),
      backgroundColor: ['#e74c3c', '#c0392b', '#ff6b6b', '#ff7675']
    }]
  };

  // 🔥 render AFTER data ready
  setTimeout(() => {
    this.showIncomeChart = true;
    this.showExpenseChart = true;
  });



}
}