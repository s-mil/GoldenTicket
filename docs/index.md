# Golden Ticket

## About

Wow, what a great ticketing system!

The Golden Ticket system consists of one ASP.Net Core MVC App:

- GoldenTicket

You can build and run the app from the "src" folder.

## Build And Run

If using Visual Studio Code, the built in debugger will take care of building and running for you.

### Required Tools

- [.NET Core SDK](https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.4-windows-x64-installer)

### Using The CLI

1. `dotnet build`
1. `dotnet GoldenTicket/bin/Debug/netcoreapp2.0/GoldenTicket.[site|Api].dll`

### Using Visual Studio Code

1. Open debugging pane
1. Select project from launch drop down
1. Hit `f5` or click the play button


### Hosting the application

1. dotnet publish within the src directory
1. the autodeploy script can handle this for your you can also set it to run as a cron job so that the process is automatic
1. the service file will allow you to have the applicaiton run as a service on your linux os of choice, just copy it to /etc/systemd/system/ and enable with (sudo) systemctl enable kestrel-golden-ticket.service

the application by default listens on port 5000, apache or nginx can be used as a reverse proxy in order to have the application on port 80 and 443.

I would sugest using certbot(uses LetsEncrypt) in order to give your webaddress an SSL cert in order to enable https
