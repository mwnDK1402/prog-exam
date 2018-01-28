namespace YouWillExplode
{
    using Microsoft.Xna.Framework;

    internal sealed class GameScene : Scene
    {
        public GameScene(YouWillExplode game) : base(game)
        {
        }

        protected override void OnLoad()
        {
            this.Manage(new Player(new Vector2(200f, 200f)));
        }
    }
}