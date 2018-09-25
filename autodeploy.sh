#!/bin/bash


# Pull the repo and act on the boolean value created by grep
git -C /home/gold/GoldenTicket fetch
if git -C /home/gold/GoldenTicket pull | grep -Fxq 'Already up-to-date.'
  then
      date +"[%Y %b %d %T] Already up-to-date."
  else
    # Build bianary
    dotnet publish /home/gold/GoldenTicket/src &&
    # move to working dir
    sudo cp -rf /home/gold/GoldenTicket/src/bin/Debug/netcoreapp2.0/publish/* /var/aspnetcore/GoldenTicket/ &&
    # restart service
    sudo systemctl restart kestrel-golden-ticket &&
   # Logging
	date +"[%Y %b %d %T] Redeploy Finished"
fi

