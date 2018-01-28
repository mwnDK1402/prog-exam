namespace YouWillExplode.Layout
{
    using System;
    using Collections;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Utility;

    /// <summary>
    /// Controls and makes it easy to align the position of <see cref="ILayoutElement"/>s.
    /// </summary>
    /// <remarks>Add screen alignment.</remarks>
    internal sealed class VerticalLayout : ILayoutElement
    {
        public readonly MonitoredList<ILayoutElement> Items = new MonitoredList<ILayoutElement>();

        private Rectangle bounds;

        private int spacing;

        public VerticalLayout(ScreenManager screenManager)
        {
            screenManager.ViewportChanged += this.OnViewportChanged;
            this.Items.Changed += this.OnItemsChanged;
            this.Alignment = Alignment.Middle;
        }

        public Alignment Alignment { get; set; }

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

        private Point GetFixedPosition()
        {
            switch (this.Alignment)
            {
                case Alignment.Left:
                    return this.bounds.GetLeftPosition();

                case Alignment.Middle:
                    return this.bounds.Center;

                case Alignment.Right:
                    return this.bounds.GetRightPosition();

                default:
                    throw new NotImplementedException();
            }
        }

        private Point GetRecalculatedSize()
        {
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

            return new Point(greatestWidth, aggregateheight);
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
            if (this.Items.Count == 0)
            {
                this.bounds.Size = Point.Zero;
                return;
            }

            Point fixedPosition = this.GetFixedPosition();
            this.bounds.Size = this.GetRecalculatedSize();
            this.SetFixedPosition(ref this.bounds, fixedPosition);

            this.UpdateItemPositions();
        }

        private void SetFixedPosition(ref Rectangle newBounds, Point fixedPosition)
        {
            switch (this.Alignment)
            {
                case Alignment.Left:
                    RectangleUtility.SetLeftPosition(ref newBounds, fixedPosition);
                    break;

                case Alignment.Middle:
                    RectangleUtility.SetMiddlePosition(ref newBounds, fixedPosition);
                    break;

                case Alignment.Right:
                    RectangleUtility.SetRightPosition(ref newBounds, fixedPosition);
                    break;
            }
        }

        private void UpdateItemPositions()
        {
            int aggregateY = this.bounds.Y;
            switch (this.Alignment)
            {
                case Alignment.Left:
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        var item = this.Items[i].Bounds;
                        this.Items[i].LeftPosition = new Point(this.bounds.X, aggregateY + (item.Height / 2));
                        aggregateY += item.Height + this.Spacing;
                    }

                    break;

                case Alignment.Middle:
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        var item = this.Items[i].Bounds;
                        this.Items[i].MiddlePosition = new Point(this.bounds.Center.X, aggregateY + (item.Height / 2));
                        aggregateY += item.Height + this.Spacing;
                    }

                    break;

                case Alignment.Right:
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