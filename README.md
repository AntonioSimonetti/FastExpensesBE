<b>Expense Tracker Backend - FastExpenses</b>
<br>
<br>
This is the backend repository for the Expense Tracker application. The app was created as an exercise to practice developing full-stack applications and to explore new technologies.
It is built using ASP.NET Core .NET 8 and hosted on Azure. The backend interfaces with an SQL database, also hosted on Azure, to manage and store expense data.

You can test the app live here: https://expensesfe-a3gqgngsfxgsexg8.eastus-01.azurewebsites.net/
<br>
or
<br>
You can test the controllers here via swagger: https://expensesbe.azurewebsites.net/Swagger/index.html

<b>Features</b>
<br>
<br>
<i>ASP.NET Core .NET 8:</i> The backend is developed using the latest version of ASP.NET Core.
<br>
<i>Entity Framework (EF):</i> Used for data access and ORM (Object-Relational Mapping).
<br>
<i>Identity:</i> Authentication and authorization are handled using ASP.NET Identity.
<br>
<i>Azure Hosting:</i> The application and database are hosted on Azure.

<b>Controllers</b>
<br>
<br>
<i>Expenses Controller:</i> Manages CRUD operations for expenses.
<br>
<i>Statistics Controller:</i> Returns statistical data for the frontend's statistics page.
<br>
<i>User Info Controller:</i> Provides information about the logged-in user. Since authentication is delegated to Identity, this controller handles additional requests to retrieve user information.

<b>Frontend Repository</b>
<br>
<br>
You can visit the frontend repository for this project here: https://github.com/AntonioSimonetti/FastExpensesFE
