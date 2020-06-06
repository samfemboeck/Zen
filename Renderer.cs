using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zen.Util;

namespace Zen
{
    public class Renderer
    {
        public static SamplerState DefaultSamplerState = SamplerState.LinearClamp;
        Material _curMaterial = Material.Default;
        //PrimitiveRectangleRenderer _rectangle = new PrimitiveRectangleRenderer(Core.GraphicsDevice, new RectangleF(0, 0, 100, 100), Color.Green);
        //PrimitiveCircleBorder _circle = new PrimitiveCircleBorder(Core.GraphicsDevice, Vector2.Zero, 100, 40, 5, Color.Red);
        public static PrimitiveRectangleTexture RectTexture;

        public Renderer()
        {
            Core.GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        public void Draw(HashSet<Entity> entities)
        {

            Core.Batcher.Begin();

            foreach (Entity entity in entities)
            {
                foreach (IDrawable drawable in entity.Drawables)
                {
                    if (drawable.Material != _curMaterial)
                    {
                        Core.Batcher.End();
                        Core.Batcher.Begin(drawable.Material);
                    }

                    drawable.Draw();
                }
            }
            Core.Batcher.End();

            //rectangle.Draw();
            //RectTexture.Draw();
        }
    }
}