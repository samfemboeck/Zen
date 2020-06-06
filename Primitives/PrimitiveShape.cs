using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    public abstract class PrimitiveShape
    {
        public VertexBuffer VertexBuffer { get; protected set; }
        public IndexBuffer IndexBuffer { get; protected set; }
        public BasicEffect DefaultEffect { get; protected set; }
        public GraphicsDevice Device;
        protected abstract int NumPrimitives { get; }
        protected BasicEffect CustomEffect { get; set; } = null;

        public PrimitiveShape(GraphicsDevice device)
        {
            Device = device;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.FillMode = FillMode.Solid;
            rasterizerState.CullMode = CullMode.None;
            device.RasterizerState = rasterizerState;

            DefaultEffect = new BasicEffect(device);
            DefaultEffect.VertexColorEnabled = true;
            DefaultEffect.Projection = Matrix.CreateOrthographic(Screen.Width, Screen.Height, 0, 1);
        }

        protected void SetVertexPositionColor(Color[] colors, Vector3[] vertices, short[] indices)
        {
            VertexBuffer = new VertexBuffer(Device, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            IndexBuffer = new IndexBuffer(Device, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
            VertexPositionColor[] vertexPostionColors = new VertexPositionColor[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                vertexPostionColors[i] = new VertexPositionColor(new Vector3(vertices[i].X, vertices[i].Y, 0), colors[i]);
            }

            VertexBuffer.SetData<VertexPositionColor>(vertexPostionColors);
            IndexBuffer.SetData<short>(indices);
            Device.SetVertexBuffer(VertexBuffer);
            Device.Indices = IndexBuffer;
        }

        protected void SetVertexPositionTexture(Texture2D texture, VertexPositionTexture[] vertexPositionTextures, short[] indices)
        {
            VertexBuffer = new VertexBuffer(Device, typeof(VertexPositionTexture), vertexPositionTextures.Length, BufferUsage.WriteOnly);
            IndexBuffer = new IndexBuffer(Device, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
            
            VertexBuffer.SetData<VertexPositionTexture>(vertexPositionTextures);
            Device.SetVertexBuffer(VertexBuffer);
            IndexBuffer.SetData<short>(indices);
            Device.Indices = IndexBuffer;
            DefaultEffect.TextureEnabled = true;
            DefaultEffect.VertexColorEnabled = false;
            DefaultEffect.Texture = texture;
        }

        public virtual void Draw()
        {
            Vector3 cameraPos = new Vector3(0, 0, -1);
            DefaultEffect.View = Matrix.CreateLookAt(cameraPos, Vector3.Zero, Vector3.Down);

            BasicEffect effect = CustomEffect ?? DefaultEffect;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, NumPrimitives);
            }
        }
    }
}