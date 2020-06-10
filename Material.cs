using Microsoft.Xna.Framework.Graphics;

namespace Zen
{
    public class Material
    {
        public static Material Default = new Material();

        public static Material LinearWrap = new Material() { SamplerState = SamplerState.LinearWrap };
        
        public SamplerState SamplerState = SamplerState.LinearClamp;
        public DepthStencilState DepthStencilState = DepthStencilState.Default;
        public BlendState BlendState = BlendState.AlphaBlend;
        public RasterizerState RasterizerState = RasterizerState.CullNone;
    }
}