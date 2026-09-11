# Beyond The Island

*Beyond The Island* is a 3D survival platformer built in Unity and C# as my final year university project. Players gather resources, craft tools and structures, and survive in an open environment with wildlife AI and a dynamic day/night cycle.

## Features

- **Resource gathering** — chop trees, mine stone and harvest fruit from interactable world resources.
- **Crafting system** — a recipe-driven crafting window and crafting table let players turn raw resources into tools, gear and placeables.
- **Building system** — place and preview buildings using a dedicated recipe/preview pipeline.
- **Wildlife AI** — NPCs support passive and aggressive behaviour states, with damage, flee and death logic.
- **Day/night cycle** — affects lighting and wildlife behaviour over time.
- **Player systems** — equip manager for tools and building kits (axe, pickaxe, building kit), an inventory system, and player needs (hunger/health-style survival stats).
- **UI** — inventory item slots, floating damage indicators, and crafting UI.

## Controls

- `W A S D` — Move
- Mouse — Look around
- `Space` — Jump
- `E` — Interact / gather resources
- `I` — Open inventory
- Left mouse button — Use equipped tool

## Project structure

Source code is organised by system under `Scripts/`:

| Folder | Contents |
|---|---|
| `Scripts/Building` | Building placement, recipes and preview logic |
| `Scripts/Enemy` | Hostile/enemy behaviour (e.g. Cactus) |
| `Scripts/Environment` | Day/night cycle and world resources (trees, stone, fruit) |
| `Scripts/Items` | Item database, item objects and interaction manager |
| `Scripts/Menu` | Main menu |
| `Scripts/NPC` | NPC AI (states, damage, death) |
| `Scripts/Placeables` | Bed, campfire, crafting table |
| `Scripts/Player` | Player controller, equip system, inventory, player needs |
| `Scripts/Recipe` | Crafting recipes and crafting window/UI |
| `Scripts/UI` | Item slot UI, damage indicators |

## Tech stack

- **Engine**: Unity
- **Language**: C#
- Tools used during development: Blender, Photoshop, ZapSplat (sound effects)

## Requirements

- Unity 2020.3 or later
- Windows or macOS

## Note

This repository contains the game's C# source scripts. It is a snapshot of the scripting layer of the project rather than a full importable Unity project (scenes, assets and prefabs are not included).
