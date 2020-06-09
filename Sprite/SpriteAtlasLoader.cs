using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Util;

namespace Zen
{
    public static class SpriteAtlasLoader
	{
		public static SpriteAtlas ParseSpriteAtlas(string dataFile, bool premultiplyAlpha = false)
		{
			var spriteAtlas = ParseSpriteAtlasData(dataFile);
			using (var stream = TitleContainer.OpenStream(dataFile.Replace(".atlas", ".png")))
				return spriteAtlas.AsSpriteAtlas(premultiplyAlpha ? Util.TextureUtils.TextureFromStreamPreMultiplied(stream) : Texture2D.FromStream(Core.GraphicsDevice, stream));
		}

		internal static SpriteAtlasData ParseSpriteAtlasData(string dataFile, bool leaveOriginsRelative = false)
		{
			var spriteAtlas = new SpriteAtlasData();

			var parsingSprites = true;
			var commaSplitter = new char[] { ',' };

			string line = null;
			using (var streamFile = File.OpenRead(dataFile))
			{
				using (var stream = new StreamReader(streamFile))
				{
					while ((line = stream.ReadLine()) != null)
					{
						// once we hit an empty line we are done parsing sprites so we move on to parsing animations
						if (parsingSprites && string.IsNullOrWhiteSpace(line))
						{
							parsingSprites = false;
							continue;
						}

						if (parsingSprites)
						{
							spriteAtlas.Names.Add(line);

							// source rect
							line = stream.ReadLine();
							var lineParts = line.Split(commaSplitter, StringSplitOptions.RemoveEmptyEntries);
							var rect = new RectangleF(int.Parse(lineParts[0]), int.Parse(lineParts[1]), int.Parse(lineParts[2]), int.Parse(lineParts[3]));
							spriteAtlas.SourceRects.Add(rect);

							// origin
							line = stream.ReadLine();
							lineParts = line.Split(commaSplitter, StringSplitOptions.RemoveEmptyEntries);
							var origin = new Vector2(float.Parse(lineParts[0], System.Globalization.CultureInfo.InvariantCulture), float.Parse(lineParts[1], System.Globalization.CultureInfo.InvariantCulture));

							if (leaveOriginsRelative)
								spriteAtlas.Origins.Add(origin);
							else
								spriteAtlas.Origins.Add(origin * new Vector2(rect.Width, rect.Height));
						}
						else
						{
							// catch the case of a newline at the end of the file
							if (string.IsNullOrWhiteSpace(line))
								break;

							spriteAtlas.AnimationNames.Add(line);

							// animation fps
							line = stream.ReadLine();
							spriteAtlas.AnimationFps.Add(int.Parse(line));

							// animation frames
							line = stream.ReadLine();
							var frames = new List<int>();
							spriteAtlas.AnimationFrames.Add(frames);
							var lineParts = line.Split(commaSplitter, StringSplitOptions.RemoveEmptyEntries);

							foreach (var part in lineParts)
								frames.Add(int.Parse(part));
						}
					}
				}
			}
			return spriteAtlas;
		}
	}
}