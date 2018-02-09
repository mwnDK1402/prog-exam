namespace YouWillExplode
{
    using System;
    using DatabaseContract;
    using Microsoft.Xna.Framework;

    internal sealed class ProfileEditor : Entity, IDrawable
    {
        public Profile Profile { get; private set; }

        void IDrawable.Draw(GameTime gameTime)
        {
        }

        public void EditProfile(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}