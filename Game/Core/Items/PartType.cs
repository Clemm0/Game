namespace Game.Core.Items;

public enum PartType : byte
{
    None = 0x00,
    Blade = 0x01,
    Guard = 0x02,
    Hilt = 0x03,
    Plate = 0x04,
    Notebook = 0x05
}

public enum MaterialType : byte
{
    None = 0x00,
    Iron = 0xA1,
    Steel = 0xA2,
    Obsidian = 0xA3
}