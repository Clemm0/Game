using System;
using Game.Core.Items;

namespace Game.Core.Crafting;

public class ForgingMinigame
{
    private readonly ForgingSettings _settings;

    public ForgingMinigame(ForgingSettings settings)
    {
        _settings = settings;
    }

    public (float Quality, ItemModifier Modifier) Play(float hitPosition, float targetPosition)
    {
        float distance = Math.Abs(hitPosition - targetPosition);
        
        float quality;
        ItemModifier modifier;

        if (distance <= _settings.SweetSpotSize / 2)
        {
            quality = 0.85f + (float)Random.Shared.NextDouble() * 0.15f; 
            modifier = ItemModifier.Fabled;
        }
        else if (distance <= _settings.GraceZoneSize / 2)
        {
            quality = 0.50f + (float)Random.Shared.NextDouble() * 0.30f; 
            modifier = ItemModifier.Sturdy;
        }
        else
        {
            quality = 0.10f + (float)Random.Shared.NextDouble() * 0.20f;
            modifier = ItemModifier.Broken;
        }

        return (quality, modifier);
    }
}