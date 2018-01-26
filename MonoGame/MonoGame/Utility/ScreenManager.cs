namespace MonoGame.Utility
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class ScreenManager
    {
        private GraphicsDevice graphicsDevice;

        public ScreenManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public int ScreenHeight
        {
            get { return this.graphicsDevice.Viewport.Height; }
        }

        public Point ScreenSize
        {
            get { return new Point(this.graphicsDevice.Viewport.Width, this.graphicsDevice.Viewport.Height); }
        }

        public int ScreenWidth
        {
            get { return this.graphicsDevice.Viewport.Width; }
        }
    }
}