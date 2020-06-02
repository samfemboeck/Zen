using Microsoft.Xna.Framework.Graphics;

namespace Zen.Graphics
{
    public class Material
    {
        public static Material Default = new Material();
        public static Material LinearWrap = new Material(SamplerState.LinearWrap);
        
        public SamplerState SamplerState;
        public DepthStencilState DepthStencilState;
        public BlendState BlendState;
        public RasterizerState RasterizerState;

        public Material(
            SamplerState samplerState = null, 
            DepthStencilState depthStencilState = null, 
            BlendState blendState = null, 
            RasterizerState rasterizerState = null)
        {
            SamplerState = samplerState;
            DepthStencilState = depthStencilState;
            BlendState = blendState;
            RasterizerState = rasterizerState;
        }
    }
}