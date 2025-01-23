# BurgerCraft Management System

## Table of Contents
1. [Introduction](#introduction)
2. [Features](#features)
3. [Technologies Used](#technologies-used)
4. [System Architecture](#system-architecture)
5. [Setup and Installation](#setup-and-installation)
6. [Usage](#usage)


---

## Introduction
The BurgerCraft Management System is designed to streamline burger shop operations, improve customer ordering experiences, and provide administrators with tools for efficient management of products and orders.

---

## Features
- **Dynamic Pricing:** Real-time pricing updates based on ingredient selections.
- **Customizable Orders:** Customers can create personalized burgers with detailed descriptions and images.
- **Admin Dashboard:** Comprehensive CRUD operations for managing burgers,burger types, ingredients, and orders.
- **Search and Filter Options:** Easily search and filter burgers by name or type.
- **Promotions:** Time-sensitive offers to boost sales and customer engagement.
- **Responsive Design:** Optimized for all devices using Bootstrap.

---

## Technologies Used
- **Backend:** .NET MVC (C#) with Razor Views
- **Frontend:** Bootstrap for responsive design and interactive components
- **Database:** PostgreSQL (v17.0) with Entity Framework Core for data interactions
- **Version Control:** Git and GitHub for collaboration and source control
- **Communication:** Slack for team discussions
- **Mockups:** Figma for UI/UX designs

---

## System Architecture
The system consists of the following components:
- **Frontend:** ASP.NET MVC with Razor Views and Bootstrap
- **Backend:** Handles business logic and interactions with the database
- **Database:** Stores data for burgers, ingredients, orders, and analytics

---

## Setup and Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/mb30942/NewRepo.git
2.Configure the PostgreSQL database connection in the appsettings.json file.
3.Apply migrations to set up the database:
	dotnet ef database update or update-database

---


## Usage
	Admin Panel:
		Access burger, ingredient,burger types, and order management through the admin dashboard.
		Perform CRUD operations on burgers and ingredients.
	Customer Interface:
		Customize burgers, view prices dynamically, and place orders.
		Search for and filter burgers based on preferences.

