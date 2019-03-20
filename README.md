# VR Test

Daydream Shooter game prototype

## Scripts Documentation for parent classes
###### Classes Names 

1. Player Class :
	- simple class for main player behavior .
2. Enemy Class :
	- Core feature and properties for Enemy .
3. EnemySpawner Class :
	- An Object pool and manging enemy spawning in the scene .
4. Weapon Class :
	- Core features and properties for weapons  
5. BossMinion Class : 
	- Inherit from Enemy Class
	- Ading some custom behavior for this type of enemy
6. MP5 Class : 
	- Inherit from Weapon Class
	- Adding some custom properties for MP5 weapon itself.
7. GettingGun :
	- Inherit from Weapon Class
	- Adding some custom properties for GettingGunu weapon itself.
8. PlayerHUDController :
	- It update the UI when any changes take place.
9. UISetter :
	- It assign the UI sprites to HUD .
10. UITheme : 
	- Inherit from ScriptableObject
	- Easily to manage any change in the theme by assigning the desired sprite for each HUD element .
11. ScoreManager Class :
	- A singletone class for managing score.
12. HelperClass :
	- It's created to hold as much useful helper function as we need
	- Currently has only one function which pick a random point in range of a Circle.
13. ShowFPS Class :
	- A Debug Class to show FPS in the View .


###### How to try the game?

1. Download the project
2. Open it with unity
3. Press play and follow the emulator instruction which is :
	- Alt+ moving mouse you can rotate your head
	- Shit + Moving Mouse getting the controller itself "quite confusing"
	- Ctrl + Moving Mouse benting your head left and right.
4. Or Connect your deveice and build the game . " Not Tested

###### Game Rules :

1. You have to weapons active and firing at same time .
2. There is Spawned enemies will walk toward you and you must shoot them before they hit you .
3. Enemies will become harder with time , each 10 seconds they will become tougher , you can check their Eye color .
4. Guns will be upgraded automatically every 30 seconds .
5. If you die the game will restart in short time .

###### Well Known Issue:

If you encounter any error with regard to GoogleVR SDK during first time loading the project inside unity,
just reimport the SDK package .


