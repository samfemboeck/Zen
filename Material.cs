using Microsoft.Xna.Framework.Graphics;

namespace Zen.Graphics
{
    public struct Material
    {
        public SamplerState SamplerState;

        public static bool operator ==(Material m1, Material m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Material m1, Material m2)
        {
            return !m1.Equals(m2);
        }

        public override bool Equals(object other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }

            Material compare = (Material)other;
            return compare.SamplerState == SamplerState;
        }
    }
}