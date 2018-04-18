#!/bin/bash

timestamp=$(date +%s)


# Pull the repo and act on the boolean value created by grep
git -C /home/gold/GoldenTicket fetch
if git -C /home/gold/GoldenTicket pull | grep -Fxq 'Already up-to-date.'
  then
      echo timestamp+"Already up-to-date."
  else
    dotnet publish /home/gold/GoldenTicket/src &&

    sudo cp -rf /home/gold/GoldenTicket/src/bin/Debug/netcoreapp2.0/publish/* /var/aspnetcore/GoldenTicket/ &&

    sudo systemctl restart kestrel-golden-ticket &&
    
    echo timestamp+"Redeploy Finished"
fi

