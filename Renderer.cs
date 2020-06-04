using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Zen
{
    public class Renderer
    {
        public static SamplerState DefaultSamplerState = SamplerState.LinearClamp;
        Material _curMaterial = Material.Default;

        public void Draw(HashSet<Entity> entities)
        {
            Core.Batcher.Begin();

            foreach (Entity entity in entities)
            {
                foreach (IDrawable drawable in entity.ToDraw)
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
        }
    }
}