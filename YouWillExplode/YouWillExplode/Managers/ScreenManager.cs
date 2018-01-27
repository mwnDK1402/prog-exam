namespace YouWillExplode.Utility
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class ScreenManager
    {
        private GraphicsDevice graphicsDevice;

        public ScreenManager(YouWillExplode game)
        {
            this.graphicsDevice = game.GraphicsDevice;
            this.Viewport = this.graphicsDevice.Viewport;
        }

        public event Action<Viewport> ViewportChanged;

        public int ScreenHeight
        {
            get { return this.Viewport.Height; }
        }

        public int ScreenWidth
        {
            get { return this.Viewport.Width; }
        }

        public Viewport Viewport { get; private set; }

        public void Update(GameTime gameTime)
        {
            var newViewport = this.graphicsDevice.Viewport;
            if (this.Viewport.Equals(newViewport))
            {
                this.Viewport = newViewport;
                this.ViewportChanged?.Invoke(this.Viewport);
            }
        }
    }
}