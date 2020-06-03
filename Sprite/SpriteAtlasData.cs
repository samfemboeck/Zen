using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen
{
    public class SpriteAtlasData
	{
		public List<string> Names = new List<string>();
		public List<Rectangle> SourceRects = new List<Rectangle>();
		public List<Vector2> Origins = new List<Vector2>();
		public List<string> AnimationNames = new List<string>();
		public List<int> AnimationFps = new List<int>();
		public List<List<int>> AnimationFrames = new List<List<int>>();

		public SpriteAtlas AsSpriteAtlas(Texture2D texture)
		{
			var atlas = new SpriteAtlas();
			atlas.Names = Names.ToArray();
			atlas.Sprites = new Sprite[atlas.Names.Length];

			for (var i = 0; i < atlas.Sprites.Length; i++)
				atlas.Sprites[i] = new Sprite(texture, SourceRects[i], Origins[i]);

			atlas.AnimationNames = AnimationNames.ToArray();
			atlas.SpriteAnimations = new SpriteAnimation[atlas.AnimationNames.Length];
			for (var i = 0; i < atlas.SpriteAnimations.Length; i++)
			{
				var sprites = new Sprite[AnimationFrames[i].Count];
				for (var j = 0; j < sprites.Length; j++)
					sprites[j] = atlas.Sprites[AnimationFrames[i][j]];
				atlas.SpriteAnimations[i] = new SpriteAnimation(sprites, (float)AnimationFps[i]);
			}

			return atlas;
		}
	}
}