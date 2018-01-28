namespace YouWillExplode
{
    using Microsoft.Xna.Framework;

    internal sealed class RemoveableButton : ILayoutElement, IManaged
    {
        private readonly Button button, removeButton;
        private Scene scene;
        private Point spacing;

        public RemoveableButton(Button button, Button.Resources resources)
        {
            this.button = button;
            this.removeButton = new Button(
                new Rectangle(),
                "X",
                () => this.scene.Destroy(this),
                resources);

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
                int halfWidth = this.Size.X;
                halfWidth /= 2;

                this.button.LeftPosition = new Point(value.X - halfWidth, value.Y);
                this.removeButton.RightPosition = new Point(value.X + halfWidth, value.Y);
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

        public int Spacing
        {
            get => this.spacing.X;
            set => this.spacing = new Point(value, 0);
        }

        private Point Size
        {
            get => this.button.Bounds.Size + this.spacing + this.removeButton.Bounds.Size;
        }

        void IManaged.Initialize(Scene scene)
        {
            this.scene = scene;

            scene.Manage(this.button);
            scene.Manage(this.removeButton);
        }

        void IManaged.Terminate()
        {
            this.scene.Destroy(this.button);
            this.scene.Destroy(this.removeButton);
        }

        private void UpdateButtonPosition() =>
            this.button.RightPosition = this.removeButton.LeftPosition - this.spacing;

        private void UpdateRemoveButtonPosition() =>
            this.removeButton.LeftPosition = this.button.RightPosition + this.spacing;
    }
}