![Some used cars](readme.jpg)

This is the README for the finished DriveIT System. 
This document will describe how to run the system and what is included.

Web Client
==============
- The DriveIT Web Client is available on the website at
[http://driveit.azurewebsites.net](http://driveit.azurewebsites.net).
- Opening the URL in a modern browser will launch the client.

Windows Client
==============
- Included in this .zip file is the compiled binary for the DriveIT Windows Client. 
- Running “DriveIT.WindowsClient.exe” will launch the client. 
- The login for the DriveIT Windows Client is: **awis@itu.dk** with the password: **4dmin_Password**.

Compiling Yourself
==============
If you wish to compile the either client yourself, the complete source code for the entire DriveIT system has been attached as well.
 
- Double clicking on “DriveIT.sln” will open the solution in Visual Studio, where the DriveIT Windows Client and DriveIT Web can be set as the Startup Project and compiled.
- Compiling the DriveIT Windows Client in Debug Mode will make the DriveIT Windows Client run on a database hosted locally. 
- Compiling the DriveIT Windows Client in Release Mode will make the DriveIT Windows Client run on the production database hosted on Microsoft Azure (the same on that the .exe is running on).
- Running the DriveIT Web Project using Visual Studio will make the web client run locally.	