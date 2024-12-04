# Lime Odyssey Server Emulator
### Built for the Korean CBT3 Client of the game (rev 211)
_There are some references to the AriaGames client in the code, but that will not work at the moment with this emulator_

This is built in C# .NET 9, when you run the server the first time it will generate a default config.json file listening on localhost as default.
If you are running the server on the same PC as you are playing on, then you shouldn't need to make any changes to the config file.
The `"Crypto": true` part in the config need to be set in order to work with the Korean CBT3 client, this option is here only because we disabled it when experimenting with the AriaGames client.

### Get the Lime Odyssey CBT3 client
This server emulator will currently only work with the Korean CBT3 client (rev 211) which can be found here from kaitodomoto: \
https://forum.ragezone.com/threads/lime-odyssey-obt-english-client.1160226/post-8951757

### Required mods
_In order to properly connect to the server, you'll need the unpacked data ref files (as things are buggy otherwise) and our modified LimeOdyssey.exe, you can find them here:_ \
\
**Mega link:** https://mega.nz/file/IBVQ2aBK#BjVEykVzCsPR-CtawmM-XRoOr8oQKKurJZBES068Qs0 \
**Google Drive link:** https://drive.google.com/file/d/1zxn8gLw53bkdXV4e6fDIi9I-nEfFXQSi/view


Finally, make a shortcut to `LimeOdyssey.exe` and add `-localhost` as a start arguement, making the target look like this `<path>\LimeOdysseyOnline\LimeOdyssey.exe -localhost`.

That should hopefully be all that is required.
