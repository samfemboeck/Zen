using Microsoft.Xna.Framework.Graphics;
using Zen.EC;
using Zen.Graphics;

namespace Zen.Graphics
{
    public class Renderer
    {
        public bool BeginCalled = false;
        public SpriteBatch SpriteBatch;
        Material _curMaterial;

        public void Begin()
        {
            SpriteBatch.Begin();
            BeginCalled = true;
        }
        
        public void Draw(IDrawable drawable)
        {
            if (drawable.Material != _curMaterial)
            {
                _curMaterial = drawable.Material;
                Flush();
            }

            drawable.Draw(SpriteBatch);
        }

        public void Flush()
        {
            SpriteBatch.End();
            SpriteBatch.Begin(samplerState: _curMaterial.SamplerState);
        }

        public void End()
        {
            SpriteBatch.End();
            BeginCalled = false;
        }
    }
}