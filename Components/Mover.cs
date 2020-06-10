using Microsoft.Xna.Framework;
using Zen;
using Zen.Components;

public class Mover : Component, IUpdatable
{       
    public Vector2 Velocity = Vector2.Zero;
    Transform _transform;

    public override void Mount()
    {
        _transform = GetComponent<Transform>();
    }

    public void Update()
    {
        _transform.Position = _transform.Position + Velocity * Time.DeltaTime;
    }
}