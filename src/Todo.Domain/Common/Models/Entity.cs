using System.Collections;

namespace Todo.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{

    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

#pragma warning disable CS8618
    protected Entity() { }
#pragma warning restore CS8618

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? entity)
    {
        return Equals((object?)entity);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}