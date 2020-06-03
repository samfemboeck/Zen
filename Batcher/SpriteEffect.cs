using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen.Batching
{
    // Get Monogames default Vertex Shader for the batcher
    public class SpriteEffect : Effect
	{
		EffectParameter _matrixTransformParam;

        static byte[] GetMonoGameEmbeddedResourceBytes(string name)
        {
            var assembly = typeof(MathHelper).Assembly;

            // MG 3.8 decided to change the location of Effecs...sigh.
            if (Array.IndexOf(assembly.GetManifestResourceNames(), name) == -1)
                name = name.Replace(".Framework", ".Framework.Platform");

            using (var stream = assembly.GetManifestResourceStream(name))
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        static byte[] SpriteEffectBytes = GetMonoGameEmbeddedResourceBytes("Microsoft.Xna.Framework.Graphics.Effect.Resources.SpriteEffect.ogl.mgfxo");

		public SpriteEffect() : base(Core.GraphicsDevice, SpriteEffectBytes)
		{
			_matrixTransformParam = Parameters["MatrixTransform"];
		}


		public void SetMatrixTransform(ref Matrix matrixTransform)
		{
			_matrixTransformParam.SetValue(matrixTransform);
		}
	}
}