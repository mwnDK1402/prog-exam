namespace MonoGame
{
    using Microsoft.Xna.Framework;

    internal abstract class Scene
    {
        protected Scene(MonoGame game)
        {
            this.Game = game;
        }

        protected MonoGame Game { get; private set; }

        public abstract void Draw(GameTime gameTime);

        public abstract void Load();

        public abstract void Unload();

        public abstract void Update(GameTime gameTime);

        internal void Initialize(MonoGame game)
        {
            this.Game = game;
        }
    }
}