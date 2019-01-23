# Overview
Te project consists of 5 projects all starting with *BackendTest*. The description of each project is given below separately.

### BackendTest.Common
The project includes some common features, such as logging and utilities. The log managements tool deployed in the project is [NLog](https://nlog-project.org/). The corresponding config file is stored in [nlog.config](BackendTest.Api/nlog.config).

### BackendTest.Domain
This project consists of the entities, DTOs and enums. Also the DbContext (*[BackendTestDbContext](BackendTest.Domain/BackendTestDbContext.cs)*) is placed in this project. Making a migration in this project will create the corresponding tables.

### BackendTest.Repository
This project is designed to get conncted to the database. The ORM deployed in this project is EF Core. The generic repository pattern is employed in this project. With this in mind, the generic interface and its implementation are included and other interfaces and services use them for connection to the database. Each entity (which represents a table) owns its own interface and service. The logic of each entity is also included in its service repository. For instance, the checkings for having firtname and last name for a student is included in *[StudentRepository](BackendTest.Repository/StudentRepository.cs)*.

### BackendTest.Api
This project includes the necessary implementations of the RESTful APIs. There are two main controllers, *StudentController* and *ClassController*, which handle the CRUD for students, master and detail classes.
In this project, firstly master classes can be created and then a detail class can be added to each master class. Next, students must be created. Finally, students can be added to each detail class with the logic that any pair of students in a single detail class cannot have similar surnames.
DI, in addition to logging, is also added in the project.

### BackendTest.Test
Finally, The test project is deployed to test each method of controllers and the repositories. The unit tests cover 100% of api methods and repositories.
