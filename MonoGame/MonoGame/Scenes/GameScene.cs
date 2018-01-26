namespace MonoGame
{
    using Microsoft.Xna.Framework;

    internal sealed class GameScene : Scene
    {
        private Player player;

        public GameScene()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            this.player.Draw(gameTime);
        }

        public override void Load()
        {
            this.player = new Player(this.Content, this.SpriteBatch, new Vector2(200f, 200f));
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.player.Update(gameTime);
        }
    }
}