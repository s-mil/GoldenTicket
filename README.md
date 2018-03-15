# Golden Ticket

## About

Wow, what a great ticketing system!

The Golden Ticket system consists of two services:

- GoldenTicket.WebApi
- GoldenTicket.Website

You can build and run both of the services in the same way from within their respective folders.

## Build And Run

If using Visual Studio Code, the built in debugger will take care of building and running for you.

### Required Tools

- [.NET Core SDK](https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.4-windows-x64-installer)

### Using The CLI

1. `dotnet build`
1. `dotnet GoldenTicket.Web[site|Api]/bin/Debug/netcoreapp2.0/GoldenTicket.[site|Api].dll`

### Using Visual Studio Code

1. Open debugging pane
1. Select project from launch drop down
1. Hit `f5` or click the play button