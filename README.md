# eCart Discount Shop: An eCommerce .NET and Angular Project

This is a full-stack eCommerce application built using **.NET Core** for the backend and **Angular** for the frontend. The application allows users to browse products, add items to the cart, and make purchases.

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
  - [Backend Setup](#backend-setup)
  - [Frontend Setup](#frontend-setup)
- [Usage](#usage)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Project Overview

This eCommerce platform allows customers to browse products, add them to the shopping cart, and proceed to checkout. The application is split into two parts:

- **Backend**: A RESTful API built using **.NET 9.0** that handles product management, user authentication, and order processing.
- **Frontend**: A single-page application built with **Angular** that communicates with the backend API to display products, manage the cart, and handle user interactions.

## Features

- User registration and login
- Product listing and search functionality
- Add products to the shopping cart
- Checkout and order management
- Admin panel to manage products and orders (upcoming by 15th Jan, 2025)

## Tech Stack

- **Backend**: 
  - .NET 9.0 (ASP.NET Core Web API)
  - Entity Framework Core
  - Redis for Caching as Performance Optimization
  - SQLite (for database storage)
  - Stripe for Payment Processing
  - Package Categorization

  | **Category**       | **Packages**                                                                                 |
  |---------------------|---------------------------------------------------------------------------------------------|
  | **Frameworks**      | 1. Microsoft.AspNetCore.Identity  <br> 2. Microsoft.AspNetCore.Identity.EntityFrameworkCore  <br> 3. Microsoft.EntityFrameworkCore.Design  <br> 4. Microsoft.EntityFrameworkCore.Sqlite  <br> 5. Npgsql.EntityFrameworkCore.PostgreSQL  <br> 6. Microsoft.AspNetCore.Authentication.JwtBearer |
  | **Tools**           | 1. Swashbuckle.AspNetCore  <br> 2. AutoMapper.Extensions.Microsoft.DependencyInjection       |
  | **Libraries**       | 1. AutoMapper  <br> 2. EPPlus  <br> 3. Microsoft.IdentityModel.Tokens  <br> 4. System.IdentityModel.Tokens.Jwt  <br> 5. StackExchange.Redis  <br> 6. SendGrid  <br> 7. SendWithBrevo  <br> 8. sib_api_v3_sdk  <br> 9. Stripe.net |
  | **Hybrid**          | 1. Microsoft.AspNetCore.OpenApi                                                             |
      
- **Frontend**:
  - Angular 19
  - Angular Material for UI components
  - RxJS for reactive programming
  - Package Categorization

  | **Category**       | **Packages**                                                                                 |
  |---------------------|---------------------------------------------------------------------------------------------|
  | **Frameworks**      | 1. @angular/animations <br> 2. @angular/cdk <br> 3. @angular/cli <br> 4. @angular/common <br> 5. @angular/compiler <br> 6. @angular/compiler-cli <br> 7. @angular/core <br> 8. @angular/forms <br> 9. @angular/platform-browser <br> 10. @angular/platform-browser-dynamic <br> 11. @angular/router |
  | **Tools**           | 1. @angular-devkit/build-angular <br> 2. @types/jasmine <br> 3. jasmine-core <br> 4. karma <br> 5. karma-chrome-launcher <br> 6. karma-coverage <br> 7. karma-jasmine <br> 8. karma-jasmine-html-reporter <br> 9. typescript <br> 10. zone.js |
  | **Libraries**       | 1. @stripe/stripe-js <br> 2. bootstrap <br> 3. bootswatch <br> 4. cuid <br> 5. font-awesome <br> 6. ngx-bootstrap <br> 7. ngx-spinner <br> 8. ngx-toastr <br> 9. rxjs <br> 10. tslib <br> 11. xlsx <br> 12. xng-breadcrumb |
  
- **Authentication**:
  - JWT (JSON Web Tokens) for user authentication

## Installation

### Backend Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/HaseebAhmed49/eCart
   cd eCart

2. Navigate to the backend folder and restore the dependencies:
   ``` 
   cd eCart.API/eCart.API
   dotnet restore
   ```

3. Set up the database and secret Keys for Email Notification and Stripe Payment in appsettings.json 

  **(SQLite path "eCart.API/eCart.API/Data/Database"):**

   ![image](https://github.com/user-attachments/assets/d334ae2b-871f-4877-97b6-3f1c4c5526ed)

  **Secret Keys**

   ![image](https://github.com/user-attachments/assets/ba01e03f-4337-4e11-9b9e-81f4d7a8e44f)


5. Configure **Redis** for caching on your local machine as localhost as shown in the above image

6. Run the backend:

   ```
   dotnet run
   ```

7. The backend will be available at https://localhost:7167.
   Swagger Link: https://localhost:7167/swagger.html

### Front Setup

1. Navigate to the frontend folder:

   ```
   cd eCart.SPA
   ```
   
2. Install the dependencies:

   ```
   npm install
   ```

3. Run the frontend:

   ```
   ng serve -o
   ```

The frontend will be available at https://localhost:4200.

## Usage
Once both the backend and frontend are running, you can:
1. Open the frontend application in your browser (http://localhost:4200).
2. Register a new account or log in with an existing account.
3. Browse the product catalog, add items to the shopping cart, and proceed to checkout.
4. Admin users can log in to manage products and orders.

## API Documentation

  ### Controller: Account
  1. | GET | paths['/api/Account']
  2. | GET | paths['/api/Account/email']
  3. | POST | paths['/api/Account/login']
  4. | POST | paths['/api/Account/register']
  5. | GET | paths['/api/Account/ConfirmEmail']
  6. | GET | paths['/api/Account/address']
  7. | PUT | paths['/api/Account/address']
  8. | GET | paths['/api/Account/GetAllUsers']
  
  ### Controller: Basket
  1. | GET | paths['/api/Basket']
  2. | POST | paths['/api/Basket']
  3. | DELETE | paths['/api/Basket']
  
  ### Controller: Excel
  1. | POST | paths['/api/Excel/export']
  
  ### Controller: Orders
  1. | POST | paths['/api/Orders']
  2. | GET | paths['/api/Orders']
  3. | GET | paths['/api/Orders/{id}']
  4. | GET | paths['/api/Orders/delivery']
  
  ### Controller: Payments
  1. | POST | paths['/api/Payments/{basketId}']
  2. | POST | paths['/api/Payments/webHook']
  
  ### Controller: Products
  1. | GET | paths['/api/Products/get-products-without-pagination']
  2. | GET | paths['/api/Products']
  3. | GET | paths['/api/Products/{id}']
  4. | GET | paths['/api/Products/brands']
  5. | GET | paths['/api/Products/types']

## Contributing

We welcome contributions! If you would like to contribute to the project, please fork the repository and submit a pull request. Make sure to follow the code style and add documentation for any new features or bug fixes.

## License
This project is free to use for learning purposes.
