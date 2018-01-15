# OpenYandere

[Click here to join the community discord server.](https://discord.gg/ctrAA7).

## What is OpenYandere?

OpenYandere is an open-source community rewrite of the game "Yandere Simulator" in C#.

## Download

The first download will be available when all the core features have been implemented, however you can build the game yourself.

## Building

To build OpenYandere you'll need Unity 2018.1.0b2 and NET 4.6 framework.

## Core Features

1. NPC
	- [ ] Talk
		- [x] Show dialogue box.
		- [x] Show the dialogue choices
			- [x] Compliment
				- [x] Increase player's reputation by one point.
			- [ ] Gossip
				- [x] Design user interface ([concept](https://i.imgur.com/4dVf6Md.png), [final]()).
				- [ ] Select a secret.
				- [ ] The NPC spreads it to anyone they are friends with.
			- [ ] Tasks
			- [ ] Favor
				- [ ] Follow
				- [ ] Distract
				- [ ] Leave
			- [ ] Bye
		
	- [ ] Attack
		- [ ] Become a ragdoll.
		- [ ] Produce blood pools.
	- [ ] Drag
	- [ ] Follow a routine.
	
2. Localization
	- [ ] Created the system
	- [ ] Update NPC interactions to use this system.
	- [ ] Updated dialogue to use this system.

3. Inventory
	- [ ] Design the user interface.
	- [ ] Equip item.
	- [ ] Drop item.
	
4. Heads-up Display
	- [ ] Update the information in the right hand corner.
	
5. School Environment
	- [ ] Create a basic building using cubes.
	- [ ] Have NPCs go to points designated as classes.
	
6 Create "senpai".
	- [ ] Senpai's aura. When the player enters their screen goes pink.
	- [ ] Comments on the behavior of the player.
		- [ ] Reacts to the player holding a weapon.

## After core features

1. Objectives system that was discussed with [Zombie](https://github.com/DaZombieKiller).

2. Determine the traits all rivals share.
	- [ ] Create a base rival class.
	- [ ] Do unit-testing by using the base rival class to create "Kokona Haruka".
	
3. Add saving and loading using XML serialization.
	- [ ] Save the game.
	- [ ] Load the game.
	- [ ] Add user interface.
	
4. Add conversational topics.
	- Conversational topics are a game mechanic that determines what types of conversations the player can have with other students. 