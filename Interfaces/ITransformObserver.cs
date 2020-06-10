using Microsoft.Xna.Framework;

namespace Zen
{
    public interface ITransformObserver
    {
        void TransformChanged(Transform transform);
    }
}