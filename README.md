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
