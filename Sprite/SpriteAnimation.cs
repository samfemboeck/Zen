namespace Zen
{
    public class SpriteAnimation
	{
		public readonly Sprite[] Sprites;
		public readonly float FrameRate;
		public int CurrentFrame;

		public SpriteAnimation(Sprite[] sprites, float frameRate)
		{
			Sprites = sprites;
			FrameRate = frameRate;
		}
	}
}