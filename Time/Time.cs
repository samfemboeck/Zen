namespace Zen
{
	public static class Time
	{
		public static float TotalTime;

		public static float DeltaTime;

		public static float TimeScale = 1f;

		internal static void Update(float dt)
		{
			TotalTime += dt;
			DeltaTime = dt * TimeScale;
		}
	}
}