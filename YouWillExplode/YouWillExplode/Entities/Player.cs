namespace YouWillExplode
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    internal sealed class Player : IManaged, IUpdateable, IDrawable
    {
        private const float NameElevation = 5f, Speed = 100f;
        private static readonly string CharacterName = "Player";
        private static readonly Color NameColor = Color.Red;
        private static readonly Point Size = new Point(200, 200);
        private SpriteFont nameFont;
        private Vector2 rawPosition, velocity;
        private Rectangle rect;
        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public Player(Vector2 position)
        {
            this.rawPosition = position;
            this.rect.Location = position.ToPoint();
            this.rect.Size = Size;
        }

        void IDrawable.Draw(GameTime gamemTime)
        {
            this.UpdateRect();
            this.spriteBatch.Draw(this.texture, this.rect, Color.White);

            Vector2 fontSize = this.nameFont.MeasureString(CharacterName);
            Vector2 textPosition = this.rect.Center.ToVector2()
                + new Vector2(
                    -fontSize.X * 0.5f,
                    (-this.rect.Height * 0.5f) - fontSize.Y - NameElevation);
            this.spriteBatch.DrawString(this.nameFont, CharacterName, textPosition, NameColor);
        }

        void IManaged.Initialize(Scene scene)
        {
            this.texture = scene.Game.Content.Load<Texture2D>("Char");
            this.spriteBatch = scene.Game.SpriteBatch;
            this.nameFont = scene.Game.Content.Load<SpriteFont>("NameFont");
        }

        void IManaged.Terminate()
        {
        }

        void IUpdateable.Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W))
            {
                this.velocity.Y = -Speed;
            }
            else if (keyboard.IsKeyDown(Keys.S))
            {
                this.velocity.Y = Speed;
            }
            else if (keyboard.IsKeyUp(Keys.W) || keyboard.IsKeyUp(Keys.S))
            {
                this.velocity.Y = 0f;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                this.velocity.X = Speed;
            }
            else if (keyboard.IsKeyDown(Keys.A))
            {
                this.velocity.X = -Speed;
            }
            else if (keyboard.IsKeyUp(Keys.D) || keyboard.IsKeyUp(Keys.A))
            {
                this.velocity.X = 0f;
            }

            this.rawPosition += this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void UpdateRect() =>
            this.rect.Location = this.rawPosition.ToPoint();
    }
}