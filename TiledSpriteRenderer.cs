using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.EC;
using Zen.Graphics;

namespace Zen.EC
{
	public class TiledSpriteRenderer : SpriteRenderer
	{
		public int ScrollX
		{
			get => _sourceRect.X;
			set => _sourceRect.X = value;
		}

		public int ScrollY
		{
			get => _sourceRect.Y;
			set => _sourceRect.Y = value;
		}

        public Vector2 LocalOffset;

		public int Width
		{
			get => _sourceRect.Width;
			set
			{
				_sourceRect.Width = value;
			}
		}

		public int Height
		{
			get => _sourceRect.Height;
			set
			{
				_sourceRect.Height = value;
			}
		}


		/// <summary>
		/// we keep a copy of the sourceRect so that we dont change the Sprite in case it is used elsewhere
		/// </summary>
		protected Rectangle _sourceRect;

		public TiledSpriteRenderer()
		{
		}

		public TiledSpriteRenderer(Sprite sprite) : base(sprite)
		{
			_sourceRect = sprite.SourceRect;

			Material = new Material
			{
				SamplerState = SamplerState.LinearWrap
			};
		}

		public TiledSpriteRenderer(Texture2D texture) : this(new Sprite(texture))
		{
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_sprite.Texture2D, Entity.Transform.Position + LocalOffset, new Rectangle(_sourceRect.X, _sourceRect.Y, Width, Height), Color, Entity.Transform.Rotation, Origin, Entity.Transform.Scale, SpriteEffects, LayerDepth);
		}
	}
}