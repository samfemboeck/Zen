using Microsoft.Xna.Framework;

namespace Zen
{
    public interface ITransformObserver
    {
        void TransformChanged(Matrix transformMatrix);
    }
}