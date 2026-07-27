# \#Lucky Heroes

\## Game Overview

Heroes with a bit of luck must fight hordes of monsters! Heroes Lucky is a top-down arena roguelite arena where you control a hero, your choices allowing your powers to grow exponentially, and a bit of luck might help.



\## How to Run

\### Requirements
- Windows 10/11
- No additional installation required.

\### Steps
1. Extract the submitted ZIP file.
2. Run the `.exe` file.
3. Click **Play** from the main menu.
4. Survive through the waves by defeating enemies, collecting resources, and choosing upgrades.
5. The run ends when the player is defeated or all stages are completed.
6. Select **Restart** to begin a new run or **Return** back to main menu.

\## Technical Decisions

\### Architecture
- Built using Unity.
- Uses a Bootstrap scene to initialize core systems.
- Service Locator is used to provide access to shared services while reducing direct dependencies.
- Gameplay is separated into managers to keep responsibilities modular.
- Uses a StateMachine to control character and enemy behaviour
- Object Pooling use to reduce runtime allocations and improve performance, as well as the use of pooling for enemies and dropped/picked up items.

\### Data-Driven Design
- This allows balancing without modifying code.
- Manage and maintain data
- ScriptableObjects are used to store configuration data such as: 
    - Character stats
    - Enemy data
    - Upgrade definitions
    - Wave settings

\### Gameplay Systems
- Procedural wave generation using weighted enemy spawning.
- Runtime stat system for applying upgrades and temporary stat changes.
- Level-up system presents multiple upgrade choices, making each run different.
- Procedural upgrade selection using weights.
- Resource tracking includes Health, Mana, Stamina, and Gold.

\## What I Would Do With More Time

Feature and Systems Prioritize
* Character Control
* Enemy AI
* Gameplay System
  * Wave Mode 
* Roguelite System
* Stats Ability
* Upgrade Ability

Extra
* Trade-off System
* Shop/Merchat System
* Item Ability
* New Resources
* New Enemy
* Skill Character and Enemy
* Secondary Weapon System
* Weapon Ability
* New Gameplay Mode



\## Known Issues

* Gameplay is still unbalanced
* Upgrade choices are only available when leveling up.
* Resource management is focused on player stats rather than economy.
* **Gold** resources already exist but have not been used, gold can be used to reroll upgrades, buy items in the shop and so on.
* **Mana** will be used for skills on character/enemy.
* **Stamina** is used by characters when running so that it cannot be used continuously.
* **Luck** is displayed as a player stat and can be increased through upgrades, but it currently has no gameplay effect.
* The original design planned for Luck to influence systems such as reward quality, rare events, and random drops, but this was not implemented within the prototype's scope.
* Some scripts are still maintainable.

Development priorities focused on gameplay stability, procedural progression, combat, and upgrade selection.

