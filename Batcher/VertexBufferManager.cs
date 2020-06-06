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
        VertexPositionColorTexture4[] _preVertexBuffer;
        IndexBuffer _indexBuffer;
        readonly short[] Indices = CreateIndices();

        const int MaxSprites = 100;
        const int MaxVertices = MaxSprites * 4;
        const int MaxIndices = MaxSprites * 6;
        int _preVertexBufferSize;

        public VertexBufferManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;

            _vertexBuffer = new DynamicVertexBuffer(graphicsDevice, typeof(VertexPositionColorTexture), MaxSprites * 4, BufferUsage.WriteOnly);
            _preVertexBuffer = new VertexPositionColorTexture4[MaxSprites];
            _indexBuffer = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, MaxIndices, BufferUsage.WriteOnly);
            _indexBuffer.SetData(Indices);
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

        public void Push(VertexPositionColorTexture4 vertexPositionColorTexture) => _preVertexBuffer[_preVertexBufferSize++] = vertexPositionColorTexture;

        public void Clear() => _preVertexBufferSize = 0;

        void SetBufferData() => _vertexBuffer.SetData(0, _preVertexBuffer, 0, _preVertexBufferSize, VertexPositionColorTexture4.RealStride, SetDataOptions.None);

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