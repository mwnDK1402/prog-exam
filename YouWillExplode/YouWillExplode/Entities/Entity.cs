namespace YouWillExplode
{
    using System;

    internal abstract class Entity : IManageable
    {
        public event Action Terminated;

        void IManageable.Initialize(Scene scene)
        {
            this.OnInitialized(scene);
        }

        void IManageable.Terminate()
        {
            this.Terminated?.Invoke();
            this.OnTerminated();
        }

        protected virtual void OnInitialized(Scene scene)
        {
        }

        protected virtual void OnTerminated()
        {
        }
    }
}