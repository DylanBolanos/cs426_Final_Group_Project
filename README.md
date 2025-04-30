# cs426_Final_Group_Project
Lab simulator:
  By: Dylan Bolanos, Elmerson Tedja, Joonyoung Ma

How to play:
  W-A-S-D to move the character around the map.
  Use the mouse to rotate the POV of the player around.
  Use F to interact with objects or pick up flasks.
  Use G to drop flasks.
  Shift with W-A-S-D to run the character.

You are a aspiring chemist with a questionable degree preforming experiments to make more money and upgrade the lab to repeat the cycle

Target audience:
  Relaxing experience 
  Interested in chem without needed a college degree
  Fun and non competitive game

Players:  
  How many players:
    In this game, only single player can play game.
  Roles for each player:
    The player need to make specific chemical what the game required.
  Interaction Pattern:
    There is no any time limits, so Player is free to self organize based on their preferred tasks.
    Interact with all available lab equipment and chemical zone.
    Share responsibilties, such as retreving Flask, handing experiments.
    Player can pick up and carry flask, fill it with chemicals, and perform reactions using available stations.

Objectives:
  Main objective:
    Work in the laboratory and work for satisfying the game requirement.
  Side objective:
    Completing experiments will give you money to buy more equipment, thus allowing more experiments!
  Categorization of Objectives
    Using both: Alignment and construction 

Procedures:
  Actions/how:
    The player can interact with all lab equipment to conduct experiments. This includes picking up glassware, filling it with chemicals, and placing it into reaction         stations to perform chemical transformations.
  Who:
    As a single-player experience, the player has full control over all actions and interactions in the lab. There are no other characters or roles.
  Where:
    All interactions take place within a compact, customizable laboratory space where tools and stations are physically arranged.
  When: 
    There is no time limit or progression system. The player can experiment freely at their own pace, focusing on careful handling and successful execution.

Rules:
  The player can freely perform experiments using the availale lab tools and chemicals
  There are no day-by-day assignment with deadline, the player can choose what to experiment on.
  Some experiments have specific requirements or conditions
  Flask is fragile, if the player ran or misusedm it will break and leave shards.
  Chemical sources have limited capacity and cannot be used infinitely - planning is essential.
  For the realistic, the game encourges careful experimentation, and random trial and error.

Resorces:
  Equipment:
    Each day will allow the player to purchase more equipment allowing for more experiments allowing the player to earn more money
  Chemicals:
    Each day will cost the player an amount of money to keep the lab open. As well the experiment will also cost money to do, making the player pay to play
  Objective:
    To keep the lab running, and play as long as possible 
  Flask(Glassware): 
  Used to contain and transport chemicals, if broken,  it must be replaced using the empty flask and dispenser, again.
  Equipment:
  A set of pre-placed station includes heating and mixing chemicals
    

Conflict:
  Keeping the lab running:
    This will be the main conflict of the game
    Running experiments, and earning money will be the core gameplay look
    Handling fragile glassware carefully
    Performing experiments with correct conditions and timing
    Avoiding resource waste, as chemicals and materials are limited
    Coordinating actions (even in solo play) to optimize the use of available tools and avoid accidental failures

Google Design Document
https://docs.google.com/document/d/1hdoo0mKjeMYYgq0EDBBi-C8dSeENMVNMY_13-_xXsoE/edit?usp=sharing 



Assignment 6
Description
https://docs.google.com/document/d/1y0-kU-7dbGl9N9CIEbYwioqkXDLTmf0fkhl3eEU8Z2U/edit?tab=t.0 


Explanation about Work in Assignment 6
JoonYoung Ma:
1) Refill_Patrol Robot implemented by a finite statemachine AI, that is wandering in Main Stage boundary
   if there is lack in Chemical zone's capacity, robot will head to Chemical zone, after robot arrived zone, it will act    working animation, and then it will be return to Walking.
   In this AI Mecanim, Walking Animation and Working Animation are applied with skin.
   
3) Chemical Zone, Flask zone
   Created Chemical zone implemented by interface, that is having max capacity. When player placed Empty Flask on Chemical zone, zone will charge flask within 5 seconds.
   Created Flask zone, it also has max capacity, when the player contact with button on Flask zone, new flask will appear, and it will be dropped on ground. Flask and button have rigid body and collider each. (Physics)

Elmerson Tedja:
1) Created an AI that chases down the player's position, this robot will act as an obstacle that will distract the player from accomplishing their objectives. This AI contains textures and mechanims.
3) Created Shelves at the back side of the map, and added sliding door physics into it for more interactivity between player and object. Shelves will still need to be opened manually through Unity3D.
4) Added a trash/recycle object between the two shelves, no functionality added to it yet.
5) Added spotlights to the objects that I created to improve visibility for players.

Dylan Bolanos:
1) Created the cleaning robot to find the broken glass object with tag to clean up the lab. The robot will free wander and find points to try and find any potential glasswith walking animation.
2) Created the burner prefab as both a light and a new place for the player to interact with
3) Made a Beaker with "Bubbles" coming out after 5 seconds but can be activated with a bool. This will be used later for interaction with burners
4) Used a Mechanim to walk and clean with the robot from 1

Assignment 8
  Adapted Shader
  JoonYoung Ma:
  From the Feedback 3, I realized I need to some difference between empty glass and filled glass in graphic. To enhance the visual clarity and immersion of the game, I implemented a custom shader for the empty glass object. The new shader gives the glass a transparent, slightly glowing appearance with soft rim lighting, and litle refraction.
  Elmerson Tedja: Created additional shaders, added background music and sound effects (sliding door sounds) to the game.


Response about Alpha Release Feedback
Tester 1)
Testing for 8 minutes
Feedback 1: Robot does not move, it does not move from its position
Tester noticed that the robot character remained stationary and performed walking animation, but it did not move from its position. This broke immersion and made the environment feel static.
Fixed: 

Feedback 2: Player's Jump key is not working
Fixed: Basically, Jump key is not in our plan, We deleted.


Tester 2)
Testing for 10 minutes
Feedback 3: Environment objects(table, chairs) are missing proper colliders
Tester was able to walk through tables and other environmental objects, indicating that proper collides were not set. And Some AI was passing through tables.
Fixed: We adjusted overall collider, those objects are unique, I could not set specific collider. I set right collider boundary.

Feedback 4: Glass charging status is only visible via console logs
Tester asked me how to figure out between charged glass and empty glass, this information was only printed in the console prompt.
Fixed: Previously, Overall Flasks are having filled material, So I added custom shader for empty Flask. That will help Player figure out right Flask.


Tester 3)
Testing for 8 minutes
Feedback 5: Players can pick up multiple glasses at once, when a player tries to pick up while it was already picking up glass.
Fixed: 


Feedback 6: Overall activities that the player can enjoy, is not enough, need to add something.
Overall, the gameplay lacks sufficient interactive activities for the player to enjoy. More mini games or interactive elements should be added.
Fixed: That was Alpha Release version, We will add extra interactive activities.


Assignment 9
  JoonYoung Ma: Added more playable elements into the game for finishing touches.
  Elmerson Tedja: Adjusted AI collision to not be inside the player and block their POV. This should make the game more playable.


Citation
1) https://youtu.be/xGnRamvfNK0?si=IzAOrfDXBYd0P_rW    // figure out the distance between AI and random destination, basic example of Finite State Machine
2) https://youtu.be/OMc3-Q2N4zc?si=g2keFPkZF_jupCOR    // AI rotation& Changing Animation for each work
3) https://youtu.be/nnrOhb5UdRc?si=IFt736Tpi44s47d6    // finding new destination in specific range
4) https://www.youtube.com/watch?v=u2EQtrdgfNs&t=356s  // AI chase tutorial
5) https://www.youtube.com/watch?v=Vsj_UpnLFF8&t=246s  // Quick refresh on mechanims.
