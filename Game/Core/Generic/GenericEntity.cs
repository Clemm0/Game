using System;

namespace Game.Core.Generic;

public abstract class GenericEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; protected set; } = true;

    public abstract void Update(float deltaTime);
    public virtual void Reset() => IsActive = true;
}