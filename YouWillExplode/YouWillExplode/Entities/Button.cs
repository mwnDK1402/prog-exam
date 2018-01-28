namespace YouWillExplode
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Utility;

    internal sealed class Button : ILayoutElement, IManaged, IDrawable, IUpdateable
    {
        private static readonly Vector2 TextPadding = new Vector2(10, 2);
        private readonly Action pressedAction;
        private readonly Resources resources;
        private readonly string text;
        private readonly Vector2 textSize;
        private Rectangle drawRect, inputRect;
        private int margin;
        private Scene scene;
        private SpriteBatch spriteBatch;
        private ButtonState state;

        public Button(Rectangle rect, string text, Action pressedAction, Resources resources)
            : this(text, pressedAction, resources)
        {
            this.drawRect = this.inputRect = rect;
            this.TryResize(text);
        }

        // It may be gross to use a Vector2 here, but I don't see any other way as of now
        // I'd rather not expose the Rectangle of the Button
        public Button(Vector2 size, string text, Action pressedAction, Resources resources)
            : this(text, pressedAction, resources)
        {
            this.drawRect.Size = this.inputRect.Size = size.ToPoint();
            this.TryResize(text);
        }

        public Button(Point position, string text, Action pressedAction, Resources resources)
            : this(text, pressedAction, resources)
        {
            this.drawRect.Location = this.inputRect.Location = position;
            this.TryResize(text);
        }

        private Button(string text, Action pressedAction, Resources resources)
        {
            this.pressedAction = pressedAction;
            this.resources = resources;

            this.text = text;
            this.textSize = resources.Font.MeasureString(text);

            this.state = ButtonState.Released;
        }

        public Rectangle Bounds => this.drawRect;

        public Point LeftPosition
        {
            get => this.drawRect.GetLeftPosition();

            set
            {
                RectangleUtility.SetLeftPosition(ref this.drawRect, value);
                this.RecalculateInputRect();
            }
        }

        public int Margin
        {
            get => this.margin;

            set
            {
                this.margin = value;
                this.RecalculateInputRect();
            }
        }

        public Point MiddlePosition
        {
            get => this.drawRect.Center;

            set
            {
                RectangleUtility.SetMiddlePosition(ref this.drawRect, value);
                this.RecalculateInputRect();
            }
        }

        public Point RightPosition
        {
            get => this.drawRect.GetRightPosition();

            set
            {
                RectangleUtility.SetRightPosition(ref this.drawRect, value);
                this.RecalculateInputRect();
            }
        }

        public Color TextColor { get; set; } = Color.White;

        // May be completely useless
        private Rectangle DrawRect
        {
            get => this.drawRect;

            set
            {
                this.drawRect = value;
                this.RecalculateInputRect();
            }
        }

        private Vector2 TextBounds
        {
            get => this.textSize + (2 * TextPadding);
        }

        void IDrawable.Draw(GameTime gameTime)
        {
            switch (this.state)
            {
                case ButtonState.Released:
                    this.spriteBatch.Draw(this.resources.ReleasedTexture, this.drawRect, Color.White);
                    break;

                case ButtonState.Pressed:
                    this.spriteBatch.Draw(this.resources.PressedTexture, this.drawRect, Color.White);
                    break;
            }

            this.spriteBatch.DrawString(
                this.resources.Font,
                this.text,
                this.drawRect.Center.ToVector2() - (new Vector2(this.textSize.X, this.textSize.Y) * 0.5f),
                this.TextColor);
        }

        void IManaged.Initialize(Scene scene)
        {
            this.scene = scene;
            this.spriteBatch = scene.Game.SpriteBatch;

            scene.Game.InputManager.LeftMousePressed += this.OnLeftMousePressed;
            scene.Game.InputManager.LeftMouseReleased += this.OnLeftMouseReleased;
        }

        void IManaged.Terminate()
        {
            this.scene.Game.InputManager.LeftMousePressed -= this.OnLeftMousePressed;
            this.scene.Game.InputManager.LeftMouseReleased -= this.OnLeftMouseReleased;
        }

        void IUpdateable.Update(GameTime gameTime)
        {
            if (!this.GetMouseIsOnButton())
            {
                this.state = ButtonState.Released;
            }
        }

        private bool GetMouseIsOnButton() =>
            this.inputRect.Contains(Mouse.GetState().Position);

        private void OnLeftMousePressed()
        {
            if (this.GetMouseIsOnButton())
            {
                // Hold down button
                this.state = ButtonState.Pressed;
            }
        }

        private void OnLeftMouseReleased()
        {
            if (this.state == ButtonState.Pressed && this.GetMouseIsOnButton())
            {
                // Activate button
                this.pressedAction();
            }

            this.state = ButtonState.Released;
        }

        private void RecalculateInputRect()
        {
            this.inputRect = new Rectangle(
                this.drawRect.Location + new Point(this.Margin, this.Margin),
                this.drawRect.Size - new Point(this.Margin * 2, this.Margin * 2));
        }

        private void TryResize(string text)
        {
            Point fixedPosition = this.MiddlePosition;

            Vector2 textSize = this.TextBounds;
            Vector2 difference = this.drawRect.Size.ToVector2() - textSize;
            if (difference.X < 0)
            {
                this.drawRect.Width = (int)Math.Ceiling(textSize.X);
            }

            if (difference.Y < 0)
            {
                this.drawRect.Height = (int)Math.Ceiling(textSize.Y);
            }

            this.MiddlePosition = fixedPosition;

            this.RecalculateInputRect();
        }

        public struct Resources
        {
            public SpriteFont Font;
            public Texture2D PressedTexture, ReleasedTexture;
        }
    }
}