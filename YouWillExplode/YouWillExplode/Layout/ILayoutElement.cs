namespace YouWillExplode
{
    using Microsoft.Xna.Framework;

    internal interface ILayoutElement : IManageable
    {
        Rectangle Bounds { get; }

        Point LeftPosition { get; set; }

        Point MiddlePosition { get; set; }

        Point RightPosition { get; set; }
    }
}