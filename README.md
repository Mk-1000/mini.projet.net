# Mini Project - .NET

## Overview

This project is a mini application built with **ASP.NET Core**, which involves managing and tracking student absences, courses, departments, groups, and instructors. It includes functionality for CRUD operations on various entities such as `Classe`, `Departement`, `Enseignant`, `Etudiant`, and `FicheAbsence`.

## Table of Contents

- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Database Setup](#database-setup)
- [Entities](#entities)
- [Project Structure](#project-structure)
- [License](#license)

## Technologies Used

- **ASP.NET Core** (MVC Architecture)
- **Entity Framework Core** (ORM)
- **SQL Server/MySQL** (Database)
- **Bootstrap 5** (Frontend Framework)
- **jQuery** (Frontend Library)

## Setup and Installation

To get started with this project locally, follow these steps:

1. **Clone the Repository**  
   Clone the repository to your local machine using Git:
   ```bash
   git clone https://github.com/yourusername/mini.projet.net.git
   ```

2. **Restore NuGet Packages**  
   Open the solution in Visual Studio or use the following command to restore the required packages:
   ```bash
   dotnet restore
   ```

3. **Database Configuration**  
   Ensure your database connection is set up properly in the `appsettings.json` file:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "server=localhost;database=yourdb;user=root;password=yourpassword"
   }
   ```

4. **Apply Database Migrations**  
   Run the following commands to create the necessary database tables based on your models:
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**  
   Start the application using:
   ```bash
   dotnet run
   ```

   Open your browser and navigate to `http://localhost:5000` to access the application.

## Database Setup

The project uses **Entity Framework Core** for database management. The entities are defined in `Models` and are linked using foreign key relationships. Run the migrations to generate the necessary tables in your database.

## Entities

The main entities in the project are:

1. **Classe**: Represents a class or course, related to a `Groupe` and `Departement`.
2. **Departement**: Represents a department, which can have multiple classes (`Classe`) and instructors (`Enseignant`).
3. **Enseignant**: Represents an instructor, belonging to a department.
4. **Etudiant**: Represents a student, who is enrolled in a class.
5. **FicheAbsence**: Represents an attendance record for a class.
6. **Matiere**: Represents a subject or topic within a class.

## Project Structure

Here's an overview of the project structure:

```
src
├── esprim
│   ├── bin               # Compiled binaries
│   ├── Controllers       # Controller classes for handling HTTP requests
│   ├── Migrations        # Database migrations
│   ├── Models            # Entity models (Classe, Departement, etc.)
│   ├── obj               # Temporary object files
│   ├── Properties        # Project properties
│   ├── Views             # Views for MVC
│   └── wwwroot           # Static files (CSS, JS, Bootstrap, etc.)
```

### Key Directories and Files:
- `Controllers/`: Contains controller classes like `ClasseController`, `DepartementController`, etc.
- `Models/`: Contains model classes such as `Classe`, `Departement`, `Etudiant`, and `Enseignant`.
- `Views/`: Contains Razor views for different entities (`Classe`, `Departement`, etc.).
- `wwwroot/`: Static files like CSS, JavaScript, and Bootstrap assets.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
