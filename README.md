### Project overview
Asynchrounous project for processing orders. It includes good coding and architectural practices.

### How to run
***Make sure to have .NET SDK 8 installed on your machine***

Run locally:
- Go to root folder of the project.
- Run command:
  - ```dotnet run -p .\OrderProcessing\ ```

Run through docker:
- Go to root folder of the project.
- Build image with command: 
  - ``` docker build -t order-processing -f OrderProcessing/Dockerfile .```
- Run program with command: 
  - ``` docker run -i order-processing```

### Architecture diagram (text or mermaid)
Clean Architecture: 

Abstractions & Core -> Infrastructure -> Application -> Order Processing (Console)


### List of completed bonus tasks
- [X] Asynchronous Processing
- [x] Add Order (CRUD)
- [x] IOrderValidator
- [x] Unit Tests
- [x] Configuration via appsettings.json
- [x] Notification Service