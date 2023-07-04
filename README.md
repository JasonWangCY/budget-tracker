## Budget Tracker

### *In progress!*

### Introduction

This is a budget tracking application written 
in C#. This repo only contains the backend side
so far, most notably the WebApi as the entry point.

The intent of this project is to let me keep 
track of my expenses in a more categorical manner
rather than using Excel spreadsheets. Plus, it's
good practice for software development. The progress
is admittedly rather slow so far, but the final goal
is to create a full-stack deployed site.

Despite the simple nature of the app, the 
architecture of the backend is deliberately 
"over-engineered" for me to learn the Domain Driven
Design. A great example is the 
[eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)
sample project from the Microsoft team.

### Getting Started

To run the app locally, follow these steps 
after cloning the project:

1. Start the local PostgreSQL server.
```bash
docker-compose -f docker-compose.yml up -d
```

2. Run the Web Api
- will provide DockerFile later

3. To check the database, visit localhost:8080
- System: PostgreSQL
- Server: db
- Username: test_user
- Password: 1234
- Database: bduget_dev

4. To view the swagger page for the API,
visit https://localhost:7073/ 
