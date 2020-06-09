using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Util;

namespace Zen
{
    public class BuddhaBatcher
    {
        GraphicsDevice _device;
        readonly BasicEffect _defaultShader;
        Material _currentMaterial;
        readonly VertexBufferManager _vertexBufferManager;

        Matrix _worldMatrix = Matrix.CreateTranslation(-Screen.Width * 0.5f, -Screen.Height * 0.5f, 0);
        Matrix _viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, -1), Vector3.Zero, Vector3.Down);
        Matrix _projectionMatrix = Matrix.CreateOrthographic(Screen.Width, Screen.Height, 0, 1);

        public BuddhaBatcher(GraphicsDevice device)
        {
            _device = device;
            _vertexBufferManager = new VertexBufferManager(device);
            _defaultShader = new BasicEffect(device);
            _defaultShader.World = _worldMatrix;
            _defaultShader.View = _viewMatrix;
            _defaultShader.Projection = _projectionMatrix;
            _defaultShader.TextureEnabled = true;
            _defaultShader.VertexColorEnabled = true;
        }

        void SetMaterial(Material material)
        {
            _device.SamplerStates[0] = material.SamplerState;
            _device.RasterizerState = material.RasterizerState;
            _device.BlendState = material.BlendState;
            _device.DepthStencilState = material.DepthStencilState;
            _currentMaterial = material;
        }

        public void Begin(Material material = null) => SetMaterial(material ?? Material.Default);

        public void End()
        {
            _defaultShader.CurrentTechnique.Passes[0].Apply();
            _vertexBufferManager.Flush();
        }

        public void PushQuad(Texture2D texture, RectangleF uvRectangle, Vertex4 vertices, Color color)
        {
            var spriteData = new VertexBufferManager.VertexPositionColorTexture4();

            spriteData.Position0 = vertices.LeftTop;
            spriteData.Position1 = vertices.RightTop;
            spriteData.Position2 = vertices.RightBottom;
            spriteData.Position3 = vertices.LeftBottom;

            spriteData.TextureCoordinate0 = new Vector2(uvRectangle.X, uvRectangle.Y);
            spriteData.TextureCoordinate1 = new Vector2(uvRectangle.Max.X, uvRectangle.Y);
            spriteData.TextureCoordinate2 = new Vector2(uvRectangle.Max.X, uvRectangle.Max.Y);
            spriteData.TextureCoordinate3 = new Vector2(uvRectangle.X, uvRectangle.Max.Y);

            spriteData.Color0 = color;
            spriteData.Color1 = color;
            spriteData.Color2 = color;
            spriteData.Color3 = color;

            _vertexBufferManager.PushSprite(spriteData, texture);
        }
    }
}