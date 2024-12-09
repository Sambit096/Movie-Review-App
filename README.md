<a id="readme-top"></a>
<br />
<div align="center">
<h3 align="center">Movie-Review-App CSCE 547</h3>

  <p align="center">
    design and implement a .NET API for a basic movie review experience application
    <br />
    <br />
  </p>
</div>

## Team

* Georgia Towne - Scrum Analyst
*	Adams Keefer – Technical Design Analyst
*	Andy Davison – Test Analyst
*	Cole Reiss – Developer Analyst
*	Mark Johnson – Developer Analyst
*	Sambit Mukherjee – Developer Analyst

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## About The Project

![Product Name Screen Shot][product-screenshot]

Our application consists of 2 main experiences. Users can 1) browse and create movie reviews and 2) purchase movie tickets. Alongside these features, users can create accounts, manage their personal preferences including notifications, and admins can manage movie, showtime, and ticket inventory.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

* Frontend - React + Vite
* Backend - C# (Dotnet)
* Database Management - Entity Core Framework & SQL Server

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

How to setup this project locally:

### Prerequisites

Please have these installed on your computer to run our project.
* npm
  ```sh
  npm install npm@latest -g
  ```
* dotnet
  ```sh
  https://dotnet.microsoft.com/en-us/download/dotnet
  ```

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/Sambit096/Movie-Review-App.git
   ```
2. Setup Backend - Navigate to backend folder
3. Update Database (edit appsettings.json file if needed)
   ```sh
   dotnet ef database update
   ```
4. Run Backend
   ```sh
   dotnet run
   ```
5. Setup Frontend - Navigate to frontend/Movie-ReviewFE folder
6. Install npm Packages
   ```sh
   npm install
   ```
7. Run Frontend
   ```sh
   npm run dev
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>


## Testing Instructions

Enter Testing Instructions here

<p align="right">(<a href="#readme-top">back to top</a>)</p>


## AI/Copilot Use

AI and Copilot were used minimally throughout this project. We used AI to help us create sample data for our database (can be found in the dbContext file) as well as debugging and designing our frontend.

<p align="right">(<a href="#readme-top">back to top</a>)</p>






[product-screenshot]: ReadmeInfo/project.png
