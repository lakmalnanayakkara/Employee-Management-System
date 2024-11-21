import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Employee, EmployeeService } from '../../service/employee.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-list-employee',
  templateUrl: './list-employee.component.html',
  styleUrl: './list-employee.component.css',
})
export class ListEmployeeComponent implements OnInit {
  employees: Employee[] = [];
  currentPage: number = 1;
  pageSize: number = 5;
  totalEmployees: number = 0;

  private subscriptions: Subscription = new Subscription();
  errorMessage: string = '';
  showErrorPopup: boolean = false;

  positionFilter: string = '';
  isSearch: boolean = false;
  isLoading: boolean = false;

  constructor(
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadEmployees(this.currentPage, this.pageSize);
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  loadEmployees(page: number, pageSize: number): void {
    this.isLoading = true;
    const sub = this.employeeService.getEmployees(page, pageSize).subscribe(
      (data) => {
        this.employees = data.data.employeeDTOs;
        this.totalEmployees = data.data.count;
        this.isLoading = false;
      },
      (error) => {
        this.showErrorDialog(`There was an error fetching the employees!`);
      }
    );
    this.subscriptions.add(sub);
  }

  editEmployee(id: number) {
    this.router.navigate([`/update-employee/${id}`]);
  }

  deleteEmployee(id: number) {
    const confirmDelete = window.confirm(
      'Are you sure you want to delete this employee?'
    );
    if (confirmDelete) {
      const sub = this.employeeService.deleteEmployee(id).subscribe(
        () => {
          this.employees = this.employees.filter((emp) => emp.Id !== id);
          window.location.reload();
        },
        (error) => {
          this.showErrorDialog(`There was an error deleting the employee!`);
        }
      );
      this.subscriptions.add(sub);
    }
  }

  loadMoreEmployees() {
    if (this.pageSize >= 5 && this.pageSize <= this.totalEmployees) {
      this.loadEmployees(this.currentPage, this.pageSize);
    } else {
      this.showErrorDialog(
        `Please enter a number greater than 5 and less than or equal to ${this.totalEmployees}.`
      );
    }
  }

  showErrorDialog(message: string) {
    this.errorMessage = message;
    this.showErrorPopup = true;
  }

  closeErrorPopup() {
    this.showErrorPopup = false;
  }

  searchByPosition() {
    this.isSearch = true;
    const sub = this.employeeService
      .getEmployeeByPosition(this.positionFilter, this.pageSize)
      .subscribe(
        (data) => {
          this.employees = data.data.employeeDTOs;
          this.totalEmployees = data.data.count;
        },
        (error) => {
          this.showErrorDialog(`There was an error searching the employees!`);
        }
      );
    this.subscriptions.add(sub);
  }

  resetSearch() {
    this.positionFilter = '';
    this.isSearch = false;
    this.loadEmployees(1, 5);
  }
}
