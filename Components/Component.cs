namespace Zen.Components
{
    public abstract class Component
    {
        public Entity Entity;
        protected T GetComponent<T>() where T : Component => Entity.GetComponent<T>();

        public virtual void Register(Entity entity) 
        {
            Entity = entity;
        }

        public virtual void PreMount() {}

        public virtual void Mount() {}

        public virtual void Destroy() {}
    }
}