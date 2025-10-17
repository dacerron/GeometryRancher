# GeometryRancher
A game built in Unity, incorporating genealogy concepts into a tongue-in-cheek gacha game.

## Concept

This game is based on biology's [Punnet square](https://en.wikipedia.org/wiki/Punnett_square) into an interactive shape sandbox where polyhedrons have different weighted rarities. It is an educational, tongue-in-cheek play on popular farming simulation and gacha games encouraging users to find rare combinations of geometric features.

As a Geometry Rancher, your goal is to breed a Legendary version of each of the polyhedron, relying on combining the right shapes together and hitting the mutation chance. 

## Features

### Inventory
This inventory is generated from the inventory.json file, colocated with the executable. This file stores InventoryItem information, which gets used to generate inventory slots and fill them with the correct shapes. Changing this json file will change the starting inventory, thus changing the difficulty of the game. Note that the shape data and rarity data are case sensitive and must be spelled correctly. Here are the shapes: "Sphere", "Pyramid", "Ico", Dod", "Cube" and here are the rarities: "Common", "Uncommon", "Magic", "Rare", "Legendary"


### Drag and Drop
The shapes can be dragged and dropped using Unity UI EventSystem events. Dragging a shape uses a raycast into the scene to dictate the shape's position. Raycasting onto an inventory slot locks the shape there, and updates the inventory system accordingly. Doing this also writes to inventory.json, saving the inventory to a path that persists between sessions. The same happens when a shape is removed from the inventory

### Generated shapes
Each shape is created using two variables, their model and their rarity. this dictaes what they look like, models giving different shapes and rarities giving the shape their color. Each rarity is weighted differently, going from Common to Legendary.

### Combining shapes
In the bottom right, you can combine two shapes to create a new one, inheriting aspects from both inputs. Here the rarity weights are used such that combining two shapes of similar rarity gives a better chance of having the higher rarity than if you combine a Common with a Legendary, for example.
There is also a mutation chance, where rarity can be upgraded by one rank with 5% probability. The rarity weights go from 0.4 for Common to 0.05 for Legendary.

### Physics sandbox
I made the shapes have rigidbodies when dropped in the center, so that it would feel fun to generate shapes and throw them in the middle, and after some time of gameplay they interact with each other more, giving the user a colorful landscape of shapes. The shapes also maintain velocity when thrown around, interacting with other physics-enabled shapes in the center.


## Me as a developer
With this game I wanted to showcase my level of attention to detail when it comes to ease of use. I dedicated time to ensure that the clicking and dragging of the shapes felt good, even when you put shapes on top of each other in the inventory system. This required careful handling of the rigidbodies and raycasting layers between the middle pen and the UI. I also care about creating systems that provide emergent behaviour, which is why I took care to make the physics between shapes in the middle was satisfying, and I thought through the weights of the different shapes and the feedback you get for mutations and generating rare shapes. Finally, it is important to me that code is legible and intuitive, so I made sure to make critical functionality in the game reusable and make the places where there is business logic clear.
