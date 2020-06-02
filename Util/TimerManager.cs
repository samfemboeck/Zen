/*
    Studiengang MultimediaTechnology, FH Salzburg
    Multimediaprojekt 1
    Author: Samuel Femböck
*/

using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class TimerManager : GameComponent
{
    List<Timer> toRemove = new List<Timer>();
    List<Timer> timers = new List<Timer>();
    public static TimerManager Instance;

    public TimerManager(Game game) : base(game)
    {
        Instance = this;
    }

    public static void Add(Timer Timer) { Instance.timers.Add(Timer); }
    public static void Remove(Timer Timer) { Instance.timers.Remove(Timer); }

    public override void Update(GameTime gametime)
    {
        for (int i = timers.Count - 1; i >= 0; i--)
            timers[i].Update();
    }
}
