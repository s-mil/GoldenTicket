#!/bin/bash
# Pull the repo and act on the boolean value created by grep
 if git pull | grep -Fxq 'Already up to date'
  then
      echo "Already up to date"
  else
    dotnet publish

    sudo cp -rf /home/gold/GoldenTicketi/src/bin/Debug/netcoreapp2.0/publish/* /var/aspnetcore/GoldenTicket/

    sudo systemctl restart kestrel-golden-ticket
fi
