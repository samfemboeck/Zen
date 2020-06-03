namespace Zen.Components
{
    public abstract class Component
    {
        public Entity Entity;
        protected T GetComponent<T>() where T : Component => Entity.GetComponent<T>();
        public Transform Transform { get => Entity.Transform; }

        public virtual void Register(Entity entity) 
        {
            Entity = entity;
        }

        public virtual void Destroy() {}
    }
}