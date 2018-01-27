namespace MonoGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    internal class Player
    {
        private const float NameElevation = 5f, Speed = 100f;
        private static readonly string CharacterName = "Dal";
        private static readonly Color NameColor = Color.Red;
        private static readonly Point Size = new Point(200, 200);
        private SpriteFont nameFont;
        private Vector2 position, velocity;
        private Rectangle rect;
        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public Player(ContentManager content, SpriteBatch spriteBatch, Vector2 position)
        {
            this.texture = content.Load<Texture2D>("Char");
            this.spriteBatch = spriteBatch;
            this.position = position;

            this.rect.Location = position.ToPoint();
            this.rect.Size = Size;

            this.nameFont = content.Load<SpriteFont>("NameFont");
        }

        public void Draw(GameTime gamemTime)
        {
            this.UpdateRect();
            this.spriteBatch.Draw(this.texture, this.rect, Color.White);

            var fontSize = this.nameFont.MeasureString(CharacterName);
            var textPosition = this.rect.Center.ToVector2()
                + new Vector2(
                    -fontSize.X * 0.5f,
                    -this.rect.Height * 0.5f - fontSize.Y - NameElevation);
            this.spriteBatch.DrawString(this.nameFont, CharacterName, textPosition, NameColor);
        }

        public void Update(GameTime gameTime)
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

            this.position += this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void UpdateRect()
        {
            this.rect.Location = this.position.ToPoint();
        }
    }
}