# Simple RESTful Web Service implemented with .NET Core and NancyFx
This is a simple lightweight RESTful web service implemented with .NET Core and NancyFx. 
The example web service exposes a number of operations that can be used to handle hero objects stored in memory. 
The web service is self hosted and can run on any Windows, Linux or macOS computer (even a raspberry pi).

## Requirements

[.NET Core 2.1](https://www.microsoft.com/net/learn/get-started/)

## Getting Started

 ```
 dotnet build --configuration Release
 dotnet bin\Release\netcoreapp2.0\SimpleNancyExample.dll
 ```

When the web service is started, navigate to http://localhost:8210/ in a web browser for further details.

## Swagger Documentation
The web service documentation is generated with Nancy.Swagger and can be viewed with Swagger-ui like this:
1. Run the application
2. Go to [http://petstore.swagger.io/](http://petstore.swagger.io/)
3. Type http://localhost:8210/api-docs
4. Click "Explore"
5. Enjoy the Swagger documentation for the web service

Play around with the running web service with curl or Postman.
e.g. `curl -X PUT http://localhost:8210/hero -H 'Content-Type: application/json' -v -d '{ "name": "Robin", "strength": 1 }'`
