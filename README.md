# Lime Odyssey Server Emulator
### Built for the Korean CBT3 Client of the game (rev 211)
_There are some references to the AriaGames client in the code, but that will not work at the moment with this emulator_

This is build in C# .NET 9, when you run the server the first time it will generate a default config.json file listening on localhost as default.
`"Crypto": true` need to be set to work with the Korean CBT3 client, this option is here as we disabled it when experimenting with the AriaGames client.

In order to connect to the server, you need the Korean CBT3 client (rev 211) which can be found here from kaitodomoto:
https://forum.ragezone.com/threads/lime-odyssey-obt-english-client.1160226/post-8951757

In the db\xmls folder you can find the ServerList.xml, copy that straight into the clients data folder, open it and set the values as you want them.
Next, make a shortcut to `LimeOdyssey.exe` and add `-localhost` as a start arguement, making the target look like this `<path>\LimeOdysseyOnline\LimeOdyssey.exe -localhost`.

That should hopefully be all that is required.
