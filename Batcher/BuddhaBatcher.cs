using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen
{
    public class BuddhaBatcher
    {
        GraphicsDevice _device;
        readonly BasicEffect _defaultShader;
        Material _currentMaterial;
        VertexBufferManager _vertexBufferManager;
        Texture2D[] _textureBuffer;

        const int MaxSprites = 100;
        const int MaxVertices = MaxSprites * 4;
        const int MaxIndices = MaxSprites * 6;
        int _numSprites;

        Matrix _worldMatrix = Matrix.Identity;
        Matrix _viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 1), Vector3.Zero, Vector3.Down);
        Matrix _projectionMatrix = Matrix.CreateOrthographic(Screen.Width, Screen.Height, 0, 1);

        public BuddhaBatcher(GraphicsDevice device)
        {
            _device = device;
            _vertexBufferManager = new VertexBufferManager(device);
        }

        void SetMaterial(Material material)
        {
            _device.SamplerStates[0] = material.SamplerState;
            _device.RasterizerState = material.RasterizerState;
            _device.BlendState = material.BlendState;
            _device.DepthStencilState = material.DepthStencilState;
        }

        public void Draw(Texture2D texture, RectangleF sourceRectangle, RectangleF targetRectangle, float rotation, float scale)
        {
            _textureBuffer[_numSprites++] = texture;
            
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct VertexPositionColorTexture4 : IVertexType
        {
            public const int RealStride = 96;

            VertexDeclaration IVertexType.VertexDeclaration => throw new NotImplementedException();

            public Vector3 Position0;
            public Color Color0;
            public Vector2 TextureCoordinate0;
            public Vector3 Position1;
            public Color Color1;
            public Vector2 TextureCoordinate1;
            public Vector3 Position2;
            public Color Color2;
            public Vector2 TextureCoordinate2;
            public Vector3 Position3;
            public Color Color3;
            public Vector2 TextureCoordinate3;
        }
    }
}