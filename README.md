## Budget Tracker

### *In progress!*

### Introduction

This is a simple budget tracking application with
- the backend written in C#, and
- the frontend written in Typescript.

The intent of this project is to let me keep 
track of my expenses in a more categorical manner
rather than using Excel spreadsheets. Plus, it's
good practice for software development. The progress is admittedly rather slow so far, but the final goal is to create a full-stack site following the correct software practices.

Despite the simple nature of the app, the 
architecture of the backend is deliberately 
"over-engineered" for me to learn the Domain Driven
Design. A great example is the 
[eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)
sample project from the Microsoft team.

### Getting Started

To run the app locally, follow these steps 
after cloning the project:

1. In the root folder, run 
```bash
docker compose -f .\docker-compose-dev.yml up -d
```

2. To check the database, visit http://localhost:8080
```
- System: PostgreSQL
- Server: db
- Username: test_user
- Password: 1234
- Database: budget_dev
```

3. The API is hosted at http://localhost:8073/.
A swagger page is available at http://localhost:8073/swagger/
