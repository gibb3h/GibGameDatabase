# GibGameDatabase

With the upcoming release of the Steam Deck and the fact that I will probably be using mine as primarily an emulation machine, I decided I'd like a way to choose a random game to play.

After some research I found IGDB (https://www.igdb.com/) which seems to be the best option for an online games database and a fairly simple API with the help of IGDB by Kamran Ayub (https://github.com/kamranayub/igdb-dotnet)

Unfortunatly, their Amiga collection seems to be missing a few entries, so for that platform in particular it looked like HOL (https://hol.abime.net/) would be a better bet.

So, we have a .net 6 web app using entity framework and SqlLite as a backend which scrapes HOL for all entries and polls the IGDB API for entries from a defined list of platforms

The front end uses Kendo to display the dataset, purely as i've used it before and it made my life easier.

the PSOne theme is from https://github.com/micah5/PSone.css 

Usage :-

To use the HOL scraper you will need to add a file to the root directory called "HolCookies.json", this should contain all cookies from your hall of light login (I used the EditThisCookie plugin for Chrome to get these), these are needed as "Adult" games are not displayed on HOL without a login

you will also need to add the following to appsettings.json 

"IGDBSettings": {
    "TwitchClient": "yourTwitchClientId",
    "TwitchSecret": "yourTwitchSecret"
    "Platforms" : [] // array of platform ids from IGDB, you can get a full list from https://api.igdb.com/v4/platforms 
  }
  
this is for the IGDB API and details can be seen here https://api-docs.igdb.com/#authentication

To build the database go to /Home/BuildHol and /Home/RebuildIgdb (this process takes a while!)

Once the database has been built you can view one of the following :-
  A table of all games in the database.
  A table with only games that were released on multiple platforms.
  A table with only games that were platform exclusives.
  
You can click the "Choose Game" button to either pick a game at random from all platforms, or from the specific platform selected in the dropdown list.
You can also exclude games from this randomiser by checking the as played in the table
n.b. that the randomiser currently tries to only include games in English (more on this in the issues section)

Issues :-

While HOL is pretty good about release languages, IGDB has no data visible to it's API to determine what language a game is in, therefore the scraper just assumes that all games are in english (obviously not the case)
Amiga games were often released multiple times on different chipsets (OCS, AGA etc), I have tried to collate them as best as I can so that there are not duplicates, but some may slip through (re-releases for the CD32 for example)

