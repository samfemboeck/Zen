namespace Zen.Components
{
    public abstract class Component
    {
        public Entity Entity;
        protected T GetComponent<T>() where T : Component => Entity.GetComponent<T>();

        protected T AddComponent<T>() where T : Component, new() => Entity.AddComponent<T>();

        protected void AddComponent(Component component) => Entity.AddComponent(component);

        public virtual void Register(Entity entity) 
        {
            Entity = entity;
        }

        public virtual void LoadComponents() {}

        public virtual void Mount() {}

        public virtual void Destroy() {}
    }
}