namespace MonoGame
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Utility;

    internal sealed class Button : Entity, ILayoutElement
    {
        private Rectangle drawRect, inputRect;
        private int margin = 0;
        private Action pressedAction;
        private Texture2D pressedTexture, releasedTexture;
        private ButtonState state;

        public Button(Rectangle rect, Action pressedAction, Texture2D pressedTexture, Texture2D releasedTexture, InputManager inputManager)
        {
            this.pressedAction = pressedAction;
            this.drawRect = this.inputRect = rect;
            this.state = ButtonState.Released;
            this.pressedTexture = pressedTexture;
            this.releasedTexture = releasedTexture;

            inputManager.LeftMousePressed += this.OnLeftMousePressed;
            inputManager.LeftMouseReleased += this.OnLeftMouseReleased;
        }

        public Button(Rectangle rect, int margin, Action pressedAction, Texture2D pressedTexture, Texture2D releasedTexture, InputManager inputManager)
            : this(rect, pressedAction, pressedTexture, releasedTexture, inputManager)
        {
            this.margin = margin;
            this.RecalculateInputRect();
        }

        Rectangle ILayoutElement.Bounds => this.drawRect;

        Point ILayoutElement.LeftPosition
        {
            get => this.drawRect.GetLeftPosition();

            set
            {
                RectangleUtility.SetLeftPosition(ref this.drawRect, value);
                this.RecalculateInputRect();
            }
        }

        Point ILayoutElement.MiddlePosition
        {
            get => this.drawRect.Center;

            set
            {
                RectangleUtility.SetMiddlePosition(ref this.drawRect, value);
                this.RecalculateInputRect();
            }
        }

        Point ILayoutElement.RightPosition
        {
            get => this.drawRect.GetRightPosition();

            set
            {
                RectangleUtility.SetRightPosition(ref this.drawRect, value);
                this.RecalculateInputRect();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            switch (this.state)
            {
                case ButtonState.Released:
                    this.SpriteBatch.Draw(this.releasedTexture, this.drawRect, Color.White);
                    break;

                case ButtonState.Pressed:
                    this.SpriteBatch.Draw(this.pressedTexture, this.drawRect, Color.White);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.GetMouseIsOnButton())
            {
                this.state = ButtonState.Released;
            }
        }

        private bool GetMouseIsOnButton()
        {
            return this.inputRect.Contains(Mouse.GetState().Position);
        }

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
                this.drawRect.Location + new Point(this.margin, this.margin),
                this.drawRect.Size - new Point(this.margin * 2, this.margin * 2));
        }
    }
}