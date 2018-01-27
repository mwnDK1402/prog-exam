﻿namespace MonoGame.Layout
{
    using Collections;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Utility;

    internal sealed partial class VerticalLayout : ILayoutElement
    {
        public readonly MonitoredList<ILayoutElement> Items = new MonitoredList<ILayoutElement>();

        private Rectangle bounds;

        private int spacing;

        public VerticalLayout(ScreenManager screenManager)
        {
            screenManager.ViewportChanged += this.OnViewportChanged;
            this.Items.Changed += this.OnItemsChanged;
            this.Alignment = LayoutAlignment.Middle;
        }

        public LayoutAlignment Alignment { get; set; }

        Rectangle ILayoutElement.Bounds => this.bounds;

        public Point LeftPosition
        {
            get => this.bounds.GetLeftPosition();

            set
            {
                RectangleUtility.SetLeftPosition(ref this.bounds, value);
                this.RecalculateBounds();
            }
        }

        public Point MiddlePosition
        {
            get => this.bounds.Center;

            set
            {
                RectangleUtility.SetMiddlePosition(ref this.bounds, value);
                this.RecalculateBounds();
            }
        }

        public Point RightPosition
        {
            get => this.bounds.GetRightPosition();

            set
            {
                RectangleUtility.SetRightPosition(ref this.bounds, value);
                this.RecalculateBounds();
            }
        }

        public int Spacing
        {
            get => this.spacing;

            set
            {
                this.spacing = value;
                this.RecalculateBounds();
            }
        }

        public void Initialize()
        {
            this.RecalculateBounds();
        }

        private void OnItemsChanged()
        {
            this.RecalculateBounds();
        }

        private void OnViewportChanged(Viewport viewport)
        {
        }

        /// <summary>
        /// Make sure the bounds match the width of the greatest element
        /// and the aggregate height of every element, and the space between them,
        /// without changing the fixed position of the layout.
        /// </summary>
        private void RecalculateBounds()
        {
            var newBounds = this.bounds;
            Point fixedPosition = Point.Zero;

            switch (this.Alignment)
            {
                case LayoutAlignment.Left:
                    fixedPosition = this.bounds.GetLeftPosition();
                    break;

                case LayoutAlignment.Middle:
                    fixedPosition = this.bounds.Center;
                    break;

                case LayoutAlignment.Right:
                    fixedPosition = this.bounds.GetRightPosition();
                    break;
            }

            if (this.Items.Count == 0)
            {
                newBounds.Size = Point.Zero;
                return;
            }

            int greatestWidth = 0;
            int aggregateheight = 0;
            for (int i = 0; i < this.Items.Count; ++i)
            {
                var bounds = this.Items[i].Bounds;
                if (bounds.Width > greatestWidth)
                {
                    greatestWidth = bounds.Width;
                }

                aggregateheight += bounds.Height;
                aggregateheight += this.Spacing;
            }

            // There will be redundant spacing after the last item
            aggregateheight -= this.Spacing;

            newBounds.Size = new Point(greatestWidth, aggregateheight);

            switch (this.Alignment)
            {
                case LayoutAlignment.Left:
                    RectangleUtility.SetLeftPosition(ref newBounds, fixedPosition);
                    break;

                case LayoutAlignment.Middle:
                    RectangleUtility.SetMiddlePosition(ref newBounds, fixedPosition);
                    break;

                case LayoutAlignment.Right:
                    RectangleUtility.SetRightPosition(ref newBounds, fixedPosition);
                    break;
            }

            this.bounds = newBounds;

            this.UpdateItemPositions();
        }

        private void UpdateItemPositions()
        {
            int aggregateY = this.bounds.Y;
            switch (this.Alignment)
            {
                case LayoutAlignment.Left:
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        var item = this.Items[i].Bounds;
                        this.Items[i].LeftPosition = new Point(this.bounds.X, aggregateY + (item.Height / 2));
                        aggregateY += item.Height + this.Spacing;
                    }

                    break;

                case LayoutAlignment.Middle:
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        var item = this.Items[i].Bounds;
                        this.Items[i].MiddlePosition = new Point(this.bounds.Center.X, aggregateY + (item.Height / 2));
                        aggregateY += item.Height + this.Spacing;
                    }

                    break;

                case LayoutAlignment.Right:
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        var item = this.Items[i].Bounds;
                        this.Items[i].RightPosition = new Point(this.bounds.X + this.bounds.Width, aggregateY + (item.Height / 2));
                        aggregateY += item.Height + this.Spacing;
                    }

                    break;
            }
        }
    }
}