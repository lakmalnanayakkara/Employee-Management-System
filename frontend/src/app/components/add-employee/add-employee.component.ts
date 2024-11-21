import { Component } from '@angular/core';
import { Employee, EmployeeService } from '../../service/employee.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.css',
})
export class AddEmployeeComponent {
  employee: Employee = {
    Id: 0,
    FirstName: '',
    LastName: '',
    Email: '',
    Position: '',
    Salary: undefined,
  };

  emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  private subscriptions: Subscription = new Subscription();

  errorMessage: string = '';
  showErrorPopup: boolean = false;

  constructor(
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  addEmployee() {
    const sub = this.employeeService.addEmployee(this.employee).subscribe(
      (data) => {
        this.employee = {
          Id: 0,
          FirstName: '',
          LastName: '',
          Email: '',
          Position: '',
          Salary: 0,
        };

        this.router.navigate(['']);
      },
      (error) => {
        this.showErrorDialog(`Error adding employee`);
      }
    );
    this.subscriptions.add(sub);
  }

  showErrorDialog(message: string) {
    this.errorMessage = message;
    this.showErrorPopup = true;
  }

  closeErrorPopup() {
    this.showErrorPopup = false;
  }
}
