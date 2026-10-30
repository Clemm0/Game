using System.Collections.Generic;

namespace Game.Core.Rpg;

public enum PrimaryStat { STR, DEX, CON, INT, WIS, CHA }

public class StatBlock
{
    public Dictionary<PrimaryStat, GenericStat> Primary { get; } = new()
    {
        [PrimaryStat.STR] = new GenericStat(),
        [PrimaryStat.DEX] = new GenericStat(),
        [PrimaryStat.CON] = new GenericStat(),
        [PrimaryStat.INT] = new GenericStat(),
        [PrimaryStat.WIS] = new GenericStat(),
        [PrimaryStat.CHA] = new GenericStat(),
    };

    public GenericStat Health        { get; } = new();
    public GenericStat Stamina       { get; } = new();
    public GenericStat Mana          { get; } = new();
    public GenericStat Defense       { get; } = new();
    public GenericStat AttackPower   { get; } = new();
    public GenericStat SpellPower    { get; } = new();
    public GenericStat MoveSpeed     { get; } = new();
    public GenericStat CarryCapacity { get; } = new();
    public GenericStat CritChance    { get; } = new();
    public GenericStat MagicResist   { get; } = new();
    public GenericStat SightDistance { get; } = new();
}