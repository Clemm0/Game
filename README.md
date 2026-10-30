# Project TODO

## 🚨 Immediate Next Steps (Core Systems)
- [ ] **Character Classes:** Create base `CharacterClass` implementations (Tank, Warrior, Mage, Healer, Cleric, Necromant, Guardian, Rogue, Ranger, Balanced) in `Core/Rpg/Classes/`.
- [ ] **Character Races:** Port the remaining GameA races (Human, Dwarf, Elf, Beastkin, Lizardfolk, Undead) to `Core/Rpg/Races/`. Apply their stat modifiers.
- [ ] **Godot Adapters:** Create `PlayerNode.cs` or `CharacterNode.cs` that inherits from `Godot.Node` to actually run the C# logic in a scene.
- [ ] **Basic UI:** Set up a simple Godot scene to test inventory/equipping armor pieces.

## 🛠️ Items & Crafting
- [ ] **Material Registry:** Create a static dictionary or factory to look up materials by name/ID (e.g., `MaterialRegistry.Get("Iron")`).
- [ ] **Weapon Factories:** Extend `DaggerFactory` to create factories for Swords, Axes, Bows, and Staves, combining parts + materials.
- [ ] **Crafting Minigames:** Stub out the base `CraftingMinigame` logic (Blacksmithing, Arcane Weaving, Tailoring) based on the GameA docs.
- [ ] **Settlements/Cities:** Create data structures for cities (The Verdant Cradle, Obsidian Peaks, etc.) including their defensive mechanics.

## 🌍 World & Dungeons
- [ ] **Dungeons:** Build out the `Dungeon` base class and the 3 WIP dungeons (The Sunken Temple, Obsidian Mines, Whispering Woods).
- [ ] **Remaining Biomes:** Implement Snowy Tundra, Scorched Desert, and The Medaws. Add their passive effects (e.g., heat damage, dehydration).
- [ ] **Skill System:** Implement concrete abilities for the classes (e.g., Taunting Aura, Cleave, Arcane Missile) using the base `Skill` class.

## ⚙️ Engine & Polish
- [ ] **Combat Loop:** Decide on Turn-Based vs. Real-Time and implement the combat tick/update loop.
- [ ] **Loot Tables:** Implement loot generation using `RarityTable` and `ItemModifier` (Broken, Pristine, etc.).
- [ ] **Inventory System:** Create a container to hold equipped armor sets (making use of `ArmorSlot.IsComplete()`).
- [ ] **Save/Load:** Implement JSON serialization/deserialization for Characters and Items.