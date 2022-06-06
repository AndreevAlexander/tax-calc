# Short project overview
Simple application for tax calculation using ASP.NET Core and Blazor

This application demonstrates architectural solutions, clean and reusable code.
For example CQRS and Validation components being used by both Front-end and Back-end.

Application can calculate taxes in different currencies (UAH, PLN, USD, EUR).
Each currency has it's own immutable id and setting up in db as a part of migration
For the development interactions there is IIdentifierService which contains list
of currencies. For the conversion there is a special converter

For this project reach domain model was selected, this means that all domain logic 
contains inside of the models and Query/Command handlers serve to orchestrate it 

For validation there is custom ValidationEngine class which executes validation 
rules. There are some predefined rules, "Required", "Min/Max Length" and etc,
also it's possible to handle more complex validation scenarios with custom
rules (for example to validate one field, you need to get data from db).
The validation configuration goes in the "Fluent" manner with Validation profiles

# Description of layers:

# Application
This layer contains Query/Command handlers and validation profiles

# Domain
This layer contains core logic

# Persistance
Serves as an abstraction from the EntityFramework and Db
and introduces contracts for interaction with data source.
Also it contains Repository contracts

# Data
Contains EntityFramework and implementations of the Persistance
contracts

# Infrastructure
Contains decorator for the build-in asp.net core MemoryCache
and AutoMapper decorator and Mapping builder

# Contracts
Contains shared contracts for mapper and cache 
