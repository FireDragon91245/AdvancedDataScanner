# (Probably) Discountainued (as of 2022)
  i have no further intents to update this project as well as the Installer i will probebly remove these repoos soon 
  but i will (Propeply) create a new repo where i rewrite this programm as libary


# AdvancedDataScanner
Scanner that scanns entire folder trees and colects information about files.

The scanner scans form the start folder to infinite depth until the entire folder structure was read. The Scanner takes the path of every file and categorises it
in file extensions. You can aply seaches to filter out files. You can view detailed information about every scanned file.
The programm runs without crashes or lag, its slower then the windows counter but colects therefor more information. It should be relativ memory save, not more then 1GB
at maximum is neaded.
View all commands and a short explaination with "help".

V1.0.0 Feature Aditions:
- add         > add a new folder path to scann in
- addfast     > add a new folder path to scann in but using a faster but les detailed scanner
- list        > list all scanns
- get         > detailed readout of a scann
- info        > readout of a scann only with esential information
- terminate   > delete a scann
- type        > show file types, file lists of a type, and view precise information about a file
- exit        > close the programm after deleting all scanns (free up memory)
- help        > show help and usage of every command
- search      > aply a search to a scann to filter it
- copy        > copy a scann
- recount     > recount the files that are collectet by the scann
- save        > save results of a scann in diferent detail to a text file (human readable)

V1.0.1 Feature Aditions:
- diskSave    > save a scann to your loacal drive for later loading (Computer Readable (.json))
- diskLoad    > load a file from your loacal drive to a scann, works like a normal scann
- diskList    > displays all scanns that are saved with propertys of thair scanns
- diskDelete  > delete a saved scann
