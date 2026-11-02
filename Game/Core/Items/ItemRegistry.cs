using System;
using System.Collections.Generic;

namespace Game.Core.Items;

public static class ItemRegistry
{
    private static readonly Dictionary<string, Func<Item>> _templates = new();


    public static void Register(string templateId, Func<Item> factory)
    {
        _templates[templateId.ToLower()] = factory;
    }

    public static Item Create(string templateId)
    {
        if (_templates.TryGetValue(templateId.ToLower(), out var factory))
        {
            var item = factory();
            item.TemplateId = templateId.ToLower();
            return item;
        }
        
        throw new ArgumentException($"Item template '{templateId}' not found in registry.");
    }
}