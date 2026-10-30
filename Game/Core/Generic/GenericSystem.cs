using System.Collections.Generic;

namespace Game.Core.Generic;

public abstract class GenericSystem<T> where T : GenericEntity
{
    protected readonly List<T> Entities = new();

    public virtual void Register(T entity) => Entities.Add(entity);
    public virtual void Unregister(T entity) => Entities.Remove(entity);
    public abstract void Tick(float deltaTime);
}