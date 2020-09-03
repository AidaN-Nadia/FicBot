# FicBot
A Discord Bot set to read and display information from fics and series from the website AO3. User's type an identifier and an address into the discord server, and the bot returns a list of information from the html the link leads to.


All framework aside from the BasicCommands.Module.cs file is taken wholesale from this tutorial:
https://dev.to/bizzycola/creating-a-discord-bot-with-c-net-core-and-dsharpplus-1obg



uses two libraries:
https://dsharpplus.emzi0767.com/articles/first_bot.html is the basic c# to discord connecter.

https://html-agility-pack.net/documentation This is used to retreive the HTML of the site, and to parse out the text we want.


In the BasicCommands Module, there are currently 3 itteritive implimentations of the same program, seperated by large chunks of whitespace. The most current version is near the top, begining with     [Command("#")]
