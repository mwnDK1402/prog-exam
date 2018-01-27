namespace MonoGame.Utility
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Extension methods for <see cref="Rectangle"/> class.
    /// </summary>
    internal static class RectangleUtility
    {
        public static Point GetLeftPosition(this Rectangle rect)
        {
            return new Point(rect.Left, rect.Center.Y);
        }

        public static Point GetRightPosition(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Center.Y);
        }

        public static void SetLeftPosition(ref Rectangle rect, Point value)
        {
            rect.Location = value - new Point(0, rect.Height / 2);
        }

        public static void SetMiddlePosition(ref Rectangle rect, Point value)
        {
            rect.Location = value - new Point(rect.Width / 2, rect.Height / 2);
        }

        public static void SetRightPosition(ref Rectangle rect, Point value)
        {
            rect.Location = value - new Point(rect.Width, rect.Height / 2);
        }
    }
}