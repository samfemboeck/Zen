using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen
{
    public class VertexBufferManager
    {
        GraphicsDevice _graphicsDevice;

        DynamicVertexBuffer _vertexBuffer;
        VertexPositionColorTexture4[] _vertexPreBuffer;
        IndexBuffer _indexBuffer;
        readonly short[] Indices = CreateIndices();
        Texture2D[] _textureBuffer;

        const int MaxSprites = 100;
        const int MaxVertices = MaxSprites * 4;
        const int MaxIndices = MaxSprites * 6;
        int _numSprites;

        public VertexBufferManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;

            _vertexBuffer = new DynamicVertexBuffer(graphicsDevice, typeof(VertexPositionColorTexture), MaxSprites * 4, BufferUsage.WriteOnly);
            _vertexPreBuffer = new VertexPositionColorTexture4[MaxSprites];
            _indexBuffer = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, MaxIndices, BufferUsage.WriteOnly);
            _indexBuffer.SetData(Indices);
            _textureBuffer = new Texture2D[MaxSprites];
        }

        static short[] CreateIndices()
        {
            var ret = new short[MaxIndices];

            for (int i = 0, j = 0; i < MaxIndices; i += 6, j += 4)
            {
                ret[i] = (short)(j);
                ret[i + 1] = (short)(j + 1);
                ret[i + 2] = (short)(j + 2);
                ret[i + 3] = (short)(j + 0);
                ret[i + 4] = (short)(j + 2);
                ret[i + 5] = (short)(j + 3);
            }

            return ret;
        }

        public void PushSprite(VertexPositionColorTexture4 vertexPositionColorTexture, Texture2D texture)
        {
            _vertexPreBuffer[_numSprites] = vertexPositionColorTexture;
            _textureBuffer[_numSprites] = texture;
            _numSprites++;
        }

        public void Flush()
        {
            if (_numSprites == 0)
                return;
                
            _vertexBuffer.SetData(0, _vertexPreBuffer, 0, _numSprites, VertexPositionColorTexture4.RealStride, SetDataOptions.None);
            _graphicsDevice.SetVertexBuffer(_vertexBuffer);
            _graphicsDevice.Indices = _indexBuffer;

            for (int i = 0; i < _numSprites; i++)
            {
                _graphicsDevice.Textures[0] = _textureBuffer[i];
                _graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, i * 4, 0, 2);
            }

            _numSprites = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct VertexPositionColorTexture4 : IVertexType
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