namespace MonoGame
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Utility;

    internal sealed class Button : Entity, IPosition
    {
        private Rectangle drawRect, inputRect;
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
            this.inputRect = new Rectangle(
                this.drawRect.Location + new Point(margin, margin),
                this.drawRect.Size - new Point(margin * 2, margin * 2));
        }

        Vector2 IPosition.Position
        {
            get
            {
                return this.drawRect.Location.ToVector2();
            }

            set
            {
                var newRect = this.drawRect;
                newRect.Location = value.ToPoint();
                this.drawRect = newRect;
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
            if (this.GetMouseIsOnButton())
            {
                // Activate button
                this.pressedAction();
            }

            this.state = ButtonState.Released;
        }
    }
}