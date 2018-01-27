using Microsoft.Xna.Framework;

namespace MonoGame
{
    internal interface ILayoutElement
    {
        Rectangle Bounds { get; }

        Point LeftPosition { get; set; }

        Point MiddlePosition { get; set; }

        Point RightPosition { get; set; }
    }
}