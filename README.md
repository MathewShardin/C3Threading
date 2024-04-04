# C3Threading
Final assignment for the Threading in C# class of NHL Stenden
The purpose of this application is to demonstrate threading abiltiies within C#, the app is a city destination and hotel picker. You can make a trip through the netherlands, select the cities and will be given options for hotels and data showing their prices.

## INSTALL REQUIREMENTS
* Preferably Windows OS
* Visual Studio 2019 or higher
* C# .NET and Winui3 Installed
* The project solution

## PROJECT LIMITATIONS
* This project was done in the time-span of 8 weeks.
* We have learned threading at the same time as working on the projects, the code may not be perfectly optimised.
* The program (likely due to a threading reason) has a small chance to crash on launch. (relaunch the application until it runs)
* Winui3's ui is difficult to use, the scaling is a little weird. (recommended to view in a small window instead of fullscreen)

## USAGE INSTRUCTIONS

When The application there will not be much displaying. The first task is to press the __Add New Stop__ Button. This will add a new drop down menu. The drop down menu displays different cities that can be visited. When a city is clicked on the right side of the screen will show a list of hotels, these hotels can also be clicked on to link them to the city. After that selecting the city it also shows the graph on the bottom of the screen displaying the prices of the hotels.

After binding a hotel to the city you can add a new city by clicking the __Add New Stop__ button again, you can only press this button when the previous city has a hotel selected for it.
You can delete a specific city by clicking the __Delete__ button.
If you want to change the hotel of a previous city, you can click around the middle of the area around the hotel name, this will select the city and allow you to reselect a city and hotel.
The Hotels can be sorted using the __Sort__ menu on the top-left of the screen. It can be sorted alphabetical, reverse-alphabetical, and price increasing and decreasing.

When you are satisfied with the cities and hotels for the trip, you can save the selection by clicking on the __File__ menu, where you can choose to save the work. This will store a JSON file in the __Saves__ folder. If you wish to re-edit the save or open it again at a different time you can choose to reload it by pressing the __load from file__ option.

### AUTHORS
Dimitri Vastenhout (DimoTheWizard)	
Teodor Folea (micriteo)	
Mathew Shardin (MathewShardin)	
Corvin Wittmaack (Mr-Iddle)	