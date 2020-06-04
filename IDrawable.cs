namespace Zen
{
    public interface IDrawable
    {
        Material Material { get; }
        void Draw();
    }
}