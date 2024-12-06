## **Backend: ExpenseTrackerApi**
![Hosting Backend API Azure](/HostingBackendAPI.png)
### **Description**

ExpenseTrackerApi is a .NET Core backend API designed to manage expenses and budgets for a financial tracking application. The API interacts with Firestore to store and retrieve data, providing endpoints to manage expenses, set budgets, and monitor budget overruns.

---

### **Features**

- Add, retrieve, and delete expenses.
- Set and retrieve a monthly budget.
- Manage expenses and monitor budget overruns.

---

### **Prerequisites**

- **.NET SDK** (version 6 or later)
- **Google Cloud SDK** (for Firestore)
- A valid Firebase configuration (service account JSON key)
- Firestore set up with the following collections:
  - `expenses`: For storing expenses.
  - `settings`: For storing the budget (`settings/budget`).

---

### **Installation and Configuration**

1. Clone the project:
   ```bash
   git clone https://github.com/your-repo/expense-tracker-api.git
   cd expense-tracker-api
   ```

2. Configure Firestore:
   - Download your Firebase service account JSON key from the Firebase Console.
   - Place the file in the project root and configure the `GOOGLE_APPLICATION_CREDENTIALS` environment variable:
     ```bash
     export GOOGLE_APPLICATION_CREDENTIALS=path/to/your-service-account-key.json
     ```

3. Install dependencies:
   ```bash
   dotnet restore
   ```

4. Run the project:
   ```bash
   dotnet run
   ```

5. The API will be available at:
   ```bash
   http://localhost:5057
   ```

---

### **Main Endpoints**

- **GET `/api/expenses`**: Retrieve all expenses.
- **POST `/api/expenses`**: Add a new expense.
- **DELETE `/api/expenses/{id}`**: Delete an expense.
- **POST `/api/expenses/budget`**: Set the monthly budget.
- **GET `/api/expenses/budget`**: Retrieve the current budget.

---

### **Technologies Used**

- **Backend Framework:** .NET Core
- **Database:** Firestore (Firebase)
- **Language:** C#

### **License**

This project is licensed under the MIT License. You are free to modify and redistribute it under the terms of this license.
