import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

export interface Employee {
  Id: number;
  FirstName: string;
  LastName: string;
  Email: string;
  Position: string;
  Salary: number;
}

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private apiUrl = 'https://localhost:7115/api/v1/employee';

  constructor(private http: HttpClient) {}

  getEmployees(page: number, pageSize: number): Observable<any> {
    return this.http.get<Employee[]>(
      `${this.apiUrl}/get-all-employees?page=${page}&pageSize=${pageSize}`
    );
  }

  addEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(`${this.apiUrl}/add-employee`, employee);
  }

  updateEmployee(id: number, employee: Employee): Observable<any> {
    return this.http.put<Employee>(
      `${this.apiUrl}/update-employee?id=${id}`,
      employee
    );
  }

  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete-employee?id=${id}`);
  }

  getEmployeeById(id: number): Observable<any> {
    return this.http.get<Employee>(
      `${this.apiUrl}/get-employee-by-id?id=${id}`
    );
  }

  getEmployeeByPosition(position: string, pageSize: number): Observable<any> {
    return this.http.get<Employee>(
      `${this.apiUrl}/get-employees-by-position?position=${position}&pageSize=${pageSize}`
    );
  }
}
