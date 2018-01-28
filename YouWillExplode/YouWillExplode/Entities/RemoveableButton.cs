namespace YouWillExplode
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Utility;

    internal sealed class RemoveableButton : Entity, ILayoutElement
    {
        private readonly Button button, removeButton;
        private bool enabled;
        private Point spacing;

        public RemoveableButton(Button button, Button.Resources resources, InputManager inputManager)
        {
            this.enabled = true;
            this.button = button;
            this.removeButton = new Button(
                new Rectangle(),
                "X",
                this.Remove,
                resources,
                inputManager);

            this.UpdateRemoveButtonPosition();
        }

        public Rectangle Bounds => this.button.Bounds;

        public Point LeftPosition
        {
            get => this.button.LeftPosition;

            set
            {
                this.button.LeftPosition = value;
                this.UpdateRemoveButtonPosition();
            }
        }

        public Point MiddlePosition
        {
            get => this.button.MiddlePosition;

            set
            {
                Point halfSize = this.Size;
                halfSize.X /= 2;

                this.button.LeftPosition = value - halfSize;
                this.removeButton.RightPosition = value + halfSize;
            }
        }

        public Point RightPosition
        {
            get => this.button.RightPosition;

            set
            {
                this.removeButton.RightPosition = value;
                this.UpdateButtonPosition();
            }
        }

        private Point Size =>
                    this.button.Bounds.Size + this.spacing + this.removeButton.Bounds.Size;

        public int Spacing
        {
            get => this.spacing.X;
            set => this.spacing = new Point(value, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.enabled)
            {
                this.button.Draw(gameTime);
                this.removeButton.Draw(gameTime);
            }
        }

        public override void Initialize(SpriteBatch spriteBatch)
        {
            base.Initialize(spriteBatch);
            this.button.Initialize(spriteBatch);
            this.removeButton.Initialize(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.enabled)
            {
                this.button.Update(gameTime);
                this.removeButton.Update(gameTime);
            }
        }

        private void Remove() => this.enabled = false;

        private void UpdateButtonPosition() =>
            this.button.RightPosition = this.removeButton.LeftPosition - this.spacing;

        private void UpdateRemoveButtonPosition() =>
            this.removeButton.LeftPosition = this.button.RightPosition + this.spacing;
    }
}