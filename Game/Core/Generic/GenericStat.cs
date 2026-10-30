namespace Game.Core.Generic;

public class GenericStat
{
    public float BaseValue { get; set; }
    public float FlatBonus { get; set; }
    public float PercentBonus { get; set; }

    public float Effective => (BaseValue + FlatBonus) * (1f + PercentBonus);
}