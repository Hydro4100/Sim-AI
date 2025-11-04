using Microsoft.Xna.Framework;

namespace MGGameLibrary
{
    public interface ISteerable
    {
        Vector2 Position { get; }
        Vector2 Velocity { get; }
        float MaxSpeed { get; }
        float Heading { get; }
    }
}
