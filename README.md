# cs426_Final_Group_Project
Lab simulator:
  By: Dylan Bolanos, Elmerson Tedja, Joonyoung Ma

You are a aspiring chemist with a questionable degree preforming experiments to make more money and upgrade the lab to repeat the cycle

Target audience:
  Relaxing experience 
  Interested in chem without needed a college degree
  Fun and non competitive game

Players:  
  How many players:
    Objective is to scale the game to multiplayer, but can be played by only one player.
  Roles for each player:
    Each player will have the same privileges of other players allowing them to corporate as they see fit
  Interaction Pattern:
    Cooperative play as they will work together towards a common goal

Objectives:
  Main objective:
    Work in the laboratory and complete experiments to earn more cash!
  Side objective:
    Completing experiments will give you money to buy more equipment, thus allowing more experiments!
  Categorization of Objectives
    Using both: Alignment and construction 

Procedures:
  Actions/how:
    The player will be able to interact with their equipment that they buy to run the experiments. 
  Who:
    All players will be able to interact with all equipment, and collision of players while holding glassware will cause them to drop breaking what they had
  Where:
    Your lab that you can customize
  When: 
    No time limit, only earning more money by completing in a quick manner

Rules:
  Each day will have a different assignment for the lab to complete
  They will have you use specific equipment and thus the rules of the game will be to complete the experiment
  If you fail to many experiments you will inevitably run out of money to fund the lab and lose the game

Resorces:
  Equipment:
    Each day will allow the player to purchase more equipment allowing for more experiments allowing the player to earn more money
  Chemicals:
    Each day will cost the player an amount of money to keep the lab open. As well the experiment will also cost money to do, making the player pay to play
  Objective:
    To keep the lab running, and play as long as possible 
  Time: 
    Each day you will have a speed bonus time allowing the player to earn extra money if you complete by a certain time

Conflict:
  Keeping the lab running:
    This will be the main conflict of the game
    Running experiments, and earning money will be the core gameplay look

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











Citation
1) https://youtu.be/xGnRamvfNK0?si=IzAOrfDXBYd0P_rW    // figure out the distance between AI and random destination, basic example of Finite State Machine
2) https://youtu.be/OMc3-Q2N4zc?si=g2keFPkZF_jupCOR    // AI rotation& Changing Animation for each work
3) https://youtu.be/nnrOhb5UdRc?si=IFt736Tpi44s47d6    // finding new destination in specific range
4) https://www.youtube.com/watch?v=u2EQtrdgfNs&t=356s  // AI chase tutorial
5) https://www.youtube.com/watch?v=Vsj_UpnLFF8&t=246s  // Quick refresh on mechanims.
