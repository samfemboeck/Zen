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

        public BuddhaBatcher(GraphicsDevice device)
        {
            _device = device;
            _vertexBufferManager = new VertexBufferManager(device);
            _defaultShader = new BasicEffect(device);
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
            _defaultShader.World = Matrix.CreateTranslation(-Screen.Width * 0.5f, -Screen.Height * 0.5f, 0);
            _defaultShader.View = Matrix.CreateLookAt(new Vector3(0, 0, -1), Vector3.Zero, Vector3.Down);
            _defaultShader.Projection = Matrix.CreateOrthographic(Screen.Width, Screen.Height, 0, 1);
            _defaultShader.CurrentTechnique.Passes[0].Apply();

            _vertexBufferManager.Flush();
        }

        public void PushQuad(Texture2D texture, RectangleF uvRectNormalized, Vector2 leftTop, Vector2 rightTop, Vector2 rightBottom, Vector2 leftBottom, Color color)
        {
            var spriteData = new VertexBufferManager.VertexPositionColorTexture4();

            spriteData.Position0 = new Vector3(leftTop, 0);
            spriteData.Position1 = new Vector3(rightTop, 0);
            spriteData.Position2 = new Vector3(rightBottom, 0);
            spriteData.Position3 = new Vector3(leftBottom, 0);

            spriteData.TextureCoordinate0 = new Vector2(uvRectNormalized.X, uvRectNormalized.Y);
            spriteData.TextureCoordinate1 = new Vector2(uvRectNormalized.Max.X, uvRectNormalized.Y);
            spriteData.TextureCoordinate2 = new Vector2(uvRectNormalized.Max.X, uvRectNormalized.Max.Y);
            spriteData.TextureCoordinate3 = new Vector2(uvRectNormalized.X, uvRectNormalized.Max.Y);

            spriteData.Color0 = color;
            spriteData.Color1 = color;
            spriteData.Color2 = color;
            spriteData.Color3 = color;

            _vertexBufferManager.PushSprite(spriteData, texture);
        }

        public void PushQuad(Texture2D texture, RectangleF uvRectNormalized, RectangleF targetRectangle, Matrix transformMatrix, Color color)
        {
            var spriteData = new VertexBufferManager.VertexPositionColorTexture4();

            spriteData.Position0 = Vector3.Transform(new Vector3(targetRectangle.Left, targetRectangle.Top, 0), transformMatrix);
            spriteData.Position1 = Vector3.Transform(new Vector3(targetRectangle.Right, targetRectangle.Top, 0), transformMatrix);
            spriteData.Position2 = Vector3.Transform(new Vector3(targetRectangle.Right, targetRectangle.Bottom, 0), transformMatrix);
            spriteData.Position3 = Vector3.Transform(new Vector3(targetRectangle.Left, targetRectangle.Bottom, 0), transformMatrix);

            spriteData.TextureCoordinate0 = new Vector2(uvRectNormalized.X, uvRectNormalized.Y);
            spriteData.TextureCoordinate1 = new Vector2(uvRectNormalized.Max.X, uvRectNormalized.Y);
            spriteData.TextureCoordinate2 = new Vector2(uvRectNormalized.Max.X, uvRectNormalized.Max.Y);
            spriteData.TextureCoordinate3 = new Vector2(uvRectNormalized.X, uvRectNormalized.Max.Y);

            spriteData.Color0 = color;
            spriteData.Color1 = color;
            spriteData.Color2 = color;
            spriteData.Color3 = color;

            _vertexBufferManager.PushSprite(spriteData, texture);
        }

        public void PushQuad(Texture2D texture, RectangleF uvRectNormalized, RectangleF targetRectangle, Vector2 position, Vector2 origin, float rotation, float scale, Flip flip, Color color)
        {
            Matrix transformMatrix = Matrix.Identity;
            
            if (origin != Vector2.Zero)
                transformMatrix *= Matrix.CreateTranslation(-origin.X, -origin.Y, 0);

            if (scale != 1)
                transformMatrix *= Matrix.CreateScale(scale);

            if (flip != Flip.None)
                transformMatrix *= new Matrix(
                    flip == Flip.X || flip == Flip.XY ? -1 : 1, 0, 0, 0,
                    0, flip == Flip.Y || flip == Flip.XY ? -1 : 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                );

            if (!Mathf.WithinEpsilon(rotation))
                transformMatrix *= Matrix.CreateRotationZ(rotation);

            if (position != Vector2.Zero)
                transformMatrix *= Matrix.CreateTranslation(position.X, position.Y, 0);

            PushQuad(texture, uvRectNormalized, targetRectangle, transformMatrix, color);
        }
    }
}