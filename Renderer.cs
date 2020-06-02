using Microsoft.Xna.Framework.Graphics;
using Zen.EC;
using Zen.Graphics;

namespace Zen
{
    public class Renderer
    {
        private readonly SpriteBatch _spriteBatch;
        private Material _curMaterial = Material.Default;

        public Renderer(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Begin()
        {
            _spriteBatch.Begin(samplerState: _curMaterial.SamplerState, blendState: _curMaterial.BlendState, rasterizerState: _curMaterial.RasterizerState, depthStencilState: _curMaterial.DepthStencilState);
        }
        
        public void Draw(IDrawable drawable)
        {
            if (drawable.Material != _curMaterial)
            {
                _curMaterial = drawable.Material;
                End();
                Begin();
            }

            drawable.Draw(_spriteBatch);
        }

        public void End()
        {
            _spriteBatch.End();
        }
    }
}