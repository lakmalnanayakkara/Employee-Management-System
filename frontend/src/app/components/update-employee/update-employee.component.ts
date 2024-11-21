import { Component, OnInit } from '@angular/core';
import { Employee, EmployeeService } from '../../service/employee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-update-employee',
  templateUrl: './update-employee.component.html',
  styleUrl: './update-employee.component.css',
})
export class UpdateEmployeeComponent implements OnInit {
  employee: Employee = {
    Id: 0,
    FirstName: '',
    LastName: '',
    Email: '',
    Position: '',
    Salary: 0,
  };

  employeeId: number;
  emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  private subscriptions: Subscription = new Subscription();

  errorMessage: string = '';
  showErrorPopup: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.employeeId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.employeeId) {
      const sub = this.employeeService
        .getEmployeeById(this.employeeId)
        .subscribe(
          (data) => {
            this.employee = data.data;
          },
          (error) => {
            this.showErrorDialog(`Error fetching employee details`);
          }
        );
      this.subscriptions.add(sub);
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  updateEmployee() {
    if (this.employee) {
      const sub = this.employeeService
        .updateEmployee(this.employeeId, this.employee)
        .subscribe(
          () => {
            this.router.navigate(['']);
          },
          (error) => {
            this.showErrorDialog(`Error updating employee`);
          }
        );
      this.subscriptions.add(sub);
    }
  }

  showErrorDialog(message: string) {
    this.errorMessage = message;
    this.showErrorPopup = true;
  }

  closeErrorPopup() {
    this.showErrorPopup = false;
  }
}
