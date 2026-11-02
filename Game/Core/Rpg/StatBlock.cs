using System.Collections.Generic;
using Game.Core.Generic;

namespace Game.Core.Rpg;

public class StatBlock
{
    public Dictionary<PrimaryStat, GenericStat> Primary { get; } = new()
    {
        [PrimaryStat.STR] = new(),
        [PrimaryStat.PER] = new(),
        [PrimaryStat.END] = new(),
        [PrimaryStat.CHA] = new(),
        [PrimaryStat.INT] = new(),
        [PrimaryStat.AGL] = new(),
        [PrimaryStat.LUC] = new()
    };

    public Dictionary<string, int> Skills { get; } = new();

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