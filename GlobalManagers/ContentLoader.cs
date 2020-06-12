using Microsoft.Xna.Framework.Content;

namespace Zen
{
    public static class ContentLoader
    {
        static ContentManager _content;

        public static void Init(ContentManager content) => _content = content;

        public static T Load<T>(string fileName) => _content.Load<T>(fileName);
    }
}