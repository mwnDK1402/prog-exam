namespace YouWillExplode
{
    using System.Collections.Generic;
    using Layout;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class SettingsMenuScene : Scene
    {
        private Button backButton;
        private List<RemoveableButton> list;
        private VerticalLayout removeableButtonLayout;

        public SettingsMenuScene(YouWillExplode game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            this.backButton.Draw(gameTime);
            foreach (RemoveableButton button in this.list)
            {
                button.Draw(gameTime);
            }
        }

        public override void Load()
        {
            var resources = new Button.Resources()
            {
                PressedTexture = this.Game.Content.Load<Texture2D>("ButtonPressed"),
                ReleasedTexture = this.Game.Content.Load<Texture2D>("ButtonReleased"),
                Font = this.Game.Content.Load<SpriteFont>("ButtonFont")
            };

            this.backButton = new Button(
                new Rectangle(8, this.Game.ScreenManager.ScreenHeight - 40, 92, 32),
                "Back",
                () => this.Game.SceneManager.ActiveScene = new MainMenuScene(this.Game),
                resources,
                this.Game.InputManager);

            this.backButton.Initialize(this.Game.SpriteBatch);

            this.list = new List<RemoveableButton>();

            this.removeableButtonLayout = new VerticalLayout(this.Game.ScreenManager);

            for (int i = 0; i < 5; ++i)
            {
                RemoveableButton button = this.GetNewButton(resources);
                this.list.Add(button);
                this.removeableButtonLayout.Items.Add(button);
            }

            this.removeableButtonLayout.Initialize();

            this.removeableButtonLayout.MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center;
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.backButton.Update(gameTime);
            foreach (RemoveableButton button in this.list)
            {
                button.Update(gameTime);
            }
        }

        private RemoveableButton GetNewButton(Button.Resources resources)
        {
            var newButton = new RemoveableButton(
                            new Button(
                                new Rectangle(8, 8, 92, 32),
                                "Test",
                                () => { },
                                resources,
                                this.Game.InputManager),
                            resources,
                            this.Game.InputManager);

            newButton.Initialize(this.Game.SpriteBatch);

            return newButton;
        }
    }
}