using Microsoft.Xna.Framework.Graphics;

namespace Zen
{
    public class Material
    {
        public static Material Default = new Material();

        public static Material LinearWrap = new Material() { SamplerState = SamplerState.LinearWrap };
        
        public SamplerState SamplerState;
        public DepthStencilState DepthStencilState;
        public BlendState BlendState;
        public Effect Effect;
    }
}