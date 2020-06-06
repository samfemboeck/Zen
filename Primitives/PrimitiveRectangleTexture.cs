using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zen.Util;

namespace Zen
{
    public class PrimitiveRectangleTexture : PrimitiveShape
    {
        private float _curScale = 1;
        protected override int NumPrimitives => 2;
        Texture2D _texture;

        public PrimitiveRectangleTexture(GraphicsDevice device, Texture2D texture, RectangleF rectangle) : base(device)
        {
            _texture = texture;

            VertexPositionTexture[] vertexPositionTextures = new VertexPositionTexture[4];
            vertexPositionTextures[0].Position = new Vector3(rectangle.Left, rectangle.Top, 0);
            vertexPositionTextures[0].TextureCoordinate = new Vector2(0, 1);
            vertexPositionTextures[1].Position = new Vector3(rectangle.Right, rectangle.Top, 0);
            vertexPositionTextures[1].TextureCoordinate = new Vector2(1, 1);
            vertexPositionTextures[2].Position = new Vector3(rectangle.Right, rectangle.Bottom, 0);
            vertexPositionTextures[2].TextureCoordinate = new Vector2(1, 0);
            vertexPositionTextures[3].Position = new Vector3(rectangle.Left, rectangle.Bottom, 0);
            vertexPositionTextures[3].TextureCoordinate = new Vector2(0, 0);

            short[] indices = new short[] { 0, 1, 2, 0, 2, 3 };

            SetVertexPositionTexture(texture, vertexPositionTextures, indices);

            CustomEffect = new BasicEffect(Core.GraphicsDevice);
        }

        public override void Draw()
        {
            KeyboardState kb = Keyboard.GetState();
        
            if (kb.IsKeyDown(Keys.Up))
                _curScale += Time.DeltaTime * 10;

            if (kb.IsKeyDown(Keys.Down))
                _curScale -= Time.DeltaTime * 10;

            CustomEffect.View = DefaultEffect.View;
            CustomEffect.Projection = DefaultEffect.Projection;
            CustomEffect.World = Matrix.CreateScale(_curScale);
            CustomEffect.TextureEnabled = true;
            CustomEffect.Texture = _texture;

            base.Draw();
        }
    }
}