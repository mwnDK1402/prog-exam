namespace YouWillExplode
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal abstract class Scene
    {
        private List<IManageable> destructibles = new List<IManageable>();
        private List<IDrawable> drawables = new List<IDrawable>();
        private List<IUpdateable> updateables = new List<IUpdateable>();

        protected Scene(YouWillExplode game)
        {
            this.Game = game ?? throw new System.ArgumentNullException(nameof(game));
        }

        public YouWillExplode Game { get; private set; }

        public void Destroy(IManageable sceneObject)
        {
            if (sceneObject == null)
            {
                throw new System.ArgumentNullException(nameof(sceneObject));
            }

            this.Unregister(sceneObject);

            sceneObject.Terminate();
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IDrawable sceneObject in this.drawables)
            {
                sceneObject.Draw(gameTime);
            }
        }

        public void Load() => this.OnLoad();

        public void Manage(IManageable sceneObject)
        {
            if (sceneObject == null)
            {
                throw new System.ArgumentNullException(nameof(sceneObject));
            }

            this.Register(sceneObject);

            sceneObject.Initialize(this);
        }

        public void Unload()
        {
            for (int i = 0; i < this.destructibles.Count; i++)
            {
                this.destructibles[i].Terminate();
            }

            this.destructibles.Clear();

            this.drawables.Clear();

            this.updateables.Clear();

            this.OnUnload();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IUpdateable sceneObject in this.updateables)
            {
                sceneObject.Update(gameTime);
            }

            this.OnUpdated(gameTime);
        }

        protected virtual void OnLoad()
        {
        }

        protected virtual void OnUnload()
        {
        }

        protected virtual void OnUpdated(GameTime gameTime)
        {
        }

        private void Register(IManageable sceneObject)
        {
            this.destructibles.Add(sceneObject);

            if (sceneObject is IDrawable drawable)
            {
                this.drawables.Add(drawable);
            }

            if (sceneObject is IUpdateable updateable)
            {
                this.updateables.Add(updateable);
            }
        }

        private void Unregister(IManageable sceneObject)
        {
            this.destructibles.Remove(sceneObject);

            if (sceneObject is IDrawable drawable)
            {
                this.drawables.Remove(drawable);
            }

            if (sceneObject is IUpdateable updateable)
            {
                this.updateables.Remove(updateable);
            }
        }
    }
}