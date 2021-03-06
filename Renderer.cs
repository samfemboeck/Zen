using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zen.Util;

namespace Zen
{
    public class DefaultRenderer : IRenderer
    {
        Material _curMaterial = Material.Default;

        public void Draw()
        {
            Core.Batcher.Begin();

            foreach (Entity entity in EntityManager.Entities)
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
        }

        public void Init() {}
    }
}