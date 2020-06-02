namespace Zen.EC
{
    public class Component
    {
        public Entity Entity;
        public virtual void Awake() {}
        public Transform Transform => Entity.Transform;
        public virtual void OnDestroy() { }
    }
}