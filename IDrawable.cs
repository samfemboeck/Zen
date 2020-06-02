using Microsoft.Xna.Framework.Graphics;
using Zen.Graphics;

namespace Zen.EC
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
        Material Material { get; set; }
    }
}