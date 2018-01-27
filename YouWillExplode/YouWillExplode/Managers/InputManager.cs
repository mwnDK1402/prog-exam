namespace YouWillExplode.Utility
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal sealed class InputManager
    {
        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;

        public event Action LeftMousePressed, LeftMouseReleased,
                            MiddleMousePressed, MiddleMouseReleased,
                            RightMousePressed, RightMouseReleased;

        public void Update(GameTime gameTime)
        {
            var currentMouseState = Mouse.GetState();

            this.TryRaiseMouseStateEvent(
                this.previousMouseState.LeftButton,
                currentMouseState.LeftButton,
                ref this.LeftMousePressed,
                ref this.LeftMouseReleased);

            this.TryRaiseMouseStateEvent(
                this.previousMouseState.MiddleButton,
                currentMouseState.MiddleButton,
                ref this.MiddleMousePressed,
                ref this.MiddleMouseReleased);

            this.TryRaiseMouseStateEvent(
                this.previousMouseState.RightButton,
                currentMouseState.RightButton,
                ref this.RightMousePressed,
                ref this.RightMouseReleased);

            this.previousKeyboardState = Keyboard.GetState();
            this.previousMouseState = currentMouseState;
        }

        private void TryRaiseMouseStateEvent(ButtonState previous, ButtonState current, ref Action pressedEvent, ref Action releasedEvent)
        {
            if (previous != current)
            {
                switch (current)
                {
                    case ButtonState.Released:
                        releasedEvent?.Invoke();
                        break;

                    case ButtonState.Pressed:
                        pressedEvent?.Invoke();
                        break;
                }
            }
        }
    }
}