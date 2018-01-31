﻿namespace YouWillExplode
{
    using System;
    using Microsoft.Xna.Framework;

    internal sealed class RemoveableButton : Entity, ILayoutElement
    {
        private readonly Button button, removeButton;
        private Scene scene;
        private Point spacing;

        public RemoveableButton(Button button, Button.Resources resources)
            : this(button)
        {
            this.removeButton = new Button(
                new Rectangle(),
                "X",
                () => this.scene.Destroy(this),
                resources);

            this.UpdateRemoveButtonPosition();
        }

        public RemoveableButton(Button button, Action removeAction, Button.Resources resources)
            : this(button)
        {
            this.removeButton = new Button(
                new Rectangle(),
                "X",
                () =>
                {
                    removeAction();
                    this.scene.Destroy(this);
                },
                resources);

            this.UpdateRemoveButtonPosition();
        }

        private RemoveableButton(Button button)
        {
            this.button = button;
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

        protected override void OnInitialized(Scene scene)
        {
            this.scene = scene;

            scene.Manage(this.button);
            scene.Manage(this.removeButton);
        }

        protected override void OnTerminated()
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