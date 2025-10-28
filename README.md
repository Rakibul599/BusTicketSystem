# Bus Ticket System

A full-stack web application built using **ASP.NET 9 Web API** and **Angular 18 (Standalone)** with **Tailwind CSS**.  
This system allows users to search, book, and manage bus tickets with boarding and dropping point options.

---

## Features

- Search available buses by route and date  
- View seat status: 🟩 Available | 🟧 Booked | 🟥 Sold  
- Book and confirm seats  
- Manage boarding & dropping points  
- Built with Clean Architecture: Domain, Application, Infrastructure, WebAPI  
- Integrated PostgreSQL Database

---

##  Tech Stack

| Layer | Technology |
|-------|-------------|
| Backend | ASP.NET 9 Web API |
| Frontend | Angular 18 |
| Database | PostgreSQL |
| Styling | Tailwind CSS |
| ORM | Entity Framework Core |

---

## ⚙️ Installation & Setup

###  Prerequisites
Before running the project, make sure you have installed:
- [.NET SDK 9.0+](https://dotnet.microsoft.com/en-us/download)
- [Node.js (v18+)](https://nodejs.org/)
- [Angular CLI](https://angular.dev/guide/setup-local)
- [PostgreSQL](https://www.postgresql.org/)

---

## 🧱 Backend Setup (.NET)

1. **Clone the repository**
   ```bash
   git clone https://github.com/Rakibul599/BusTicketSystem.git


2. **Backend setup**
   ```bash
    dotnet ef migrations add InitialCreate -p Infrastructure -s WebApi
    dotnet ef database update -p src/Infrastructure -s src/WebApi
    cd BusTicketSystem/src/WebApi
    dotnet build
    dotnet run

3. **Frontend setup**
   ```bash
   cd BusTicketSystem/src/ClientApp
   npm install
   ng serve

