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
  
- **Frontend**:
  - Angular 19
  - Angular Material for UI components
  - RxJS for reactive programming
  
- **Authentication**:
  - JWT (JSON Web Tokens) for user authentication

## Installation

### Backend Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/HaseebAhmed49/eCart
   cd eCart

2. Navigate to the backend folder and restore the dependencies:
   ``` bash
   cd eCart.API/eCart.API
   dotnet restore

3. Set up the database (SQLite path "eCart.API/eCart.API/Data/Database"):
   ![image](https://github.com/user-attachments/assets/d334ae2b-871f-4877-97b6-3f1c4c5526ed)

4. Configure redis on your local machine as localhost as shown in above image
4. Run the backend:

   ``` bash
   dotnet run

   The backend will be available at https://localhost:7167.
   Swagger Link: https://localhost:7167/swagger.html

### Front Setup

1. Navigate to the frontend folder:

   ```bash
   cd eCart.SPA

2. Install the dependencies:

   ```bash
   npm install

3. Run the frontend:

  ``` bash
  ng serve -o

The frontend will be available at https://localhost:4200.

## Usage
Once both the backend and frontend are running, you can:
	1.	Open the frontend application in your browser (http://localhost:4200).
	2.	Register a new account or log in with an existing account.
	3.	Browse the product catalog, add items to the shopping cart, and proceed to checkout.
	4.	Admin users can log in to manage products and orders.

## API Documentation

## Contributing

We welcome contributions! If you would like to contribute to the project, please fork the repository and submit a pull request. Make sure to follow the code style and add documentation for any new features or bug fixes.

## License
This project is free to use for learning purposes.
