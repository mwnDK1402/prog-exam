namespace YouWillExplode
{
    using Microsoft.Xna.Framework;

    internal abstract class Scene
    {
        protected Scene(YouWillExplode game)
        {
            this.Game = game;
        }

        protected YouWillExplode Game { get; private set; }

        public abstract void Draw(GameTime gameTime);

        public abstract void Load();

        public abstract void Unload();

        public abstract void Update(GameTime gameTime);
    }
}