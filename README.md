
````markdown
# ASP.NET SQL Server Employee CRUD

This project is a simple ASP.NET WebForms application that demonstrates how to connect to a SQL Server database and perform CRUD operations (Create, Read, Update, Delete) on an `employee` table. Each employee belongs to a department via a foreign key relationship.

---

## 📌 Features
- Add a new employee with department selection
- Display all employees
- Update employee details
- Delete employee by ID
- SQL Server integration using `SqlConnection`, `SqlCommand`, and parameterized queries
- Foreign key relationship between `employee` and `department`

---

## 🗂️ Database Schema

### Database
```sql
CREATE DATABASE sms;
USE sms;
````

### Department Table

```sql
CREATE TABLE department (
  did INT PRIMARY KEY,
  dname VARCHAR(50) NOT NULL
);
```

### Employee Table

```sql
CREATE TABLE employee (
  eid INT CONSTRAINT eid_pk PRIMARY KEY IDENTITY(101,1),
  ename VARCHAR(50) NOT NULL,
  job VARCHAR(50) NOT NULL,
  salary MONEY NOT NULL,
  did INT CONSTRAINT did_fk REFERENCES department(did)
);
```

---

## 💻 Technologies Used

* ASP.NET WebForms
* C# (Code-behind)
* SQL Server
* ADO.NET (`SqlConnection`, `SqlCommand`)

---



## 🚀 Example Usage

* Insert Employee:

  ```csharp
  SqlCommand cmd = new SqlCommand(
      "INSERT INTO employee (ename, job, salary, did) VALUES (@ename, @job, @salary, @did)", con);
  ```
* Update Employee:

  ```csharp
  SqlCommand cmd = new SqlCommand(
      "UPDATE employee SET ename=@ename, job=@job, salary=@salary, did=@did WHERE eid=@eid", con);
  ```
* Delete Employee:

  ```csharp
  SqlCommand cmd = new SqlCommand(
      "DELETE FROM employee WHERE eid=@eid", con);
  ```

---
<img width="1920" height="1079" alt="1" src="https://github.com/user-attachments/assets/d5ce3d7a-7cce-4ab7-a384-c67ec71c4ec4" />

<img width="1920" height="972" alt="2" src="https://github.com/user-attachments/assets/4adca603-f402-4e44-b375-6e079da9fc89" />

<img width="1920" height="1079" alt="3" src="https://github.com/user-attachments/assets/7551b9b2-9ad2-4e6f-9155-24f941fd1ef8" />




