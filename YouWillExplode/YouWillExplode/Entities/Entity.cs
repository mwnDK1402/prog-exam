namespace YouWillExplode
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Entity
    {
        protected Entity()
        {
        }

        protected SpriteBatch SpriteBatch { get; private set; }

        public abstract void Draw(GameTime gameTime);

        public virtual void Initialize(SpriteBatch spriteBatch) =>
            this.SpriteBatch = spriteBatch;

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}