using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KimFinalProject
{
    /// <summary>
    /// This class is a game component that implements IUpdateable.
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont nomalFont, strongFont;
        private List<string> menuItems;
        private int selectedIndex = 0;


        /// <summary>
        /// get and set the selected index of menu items
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
            }
        }

        private Vector2 position;
        private KeyboardState oldKey;

        /// <summary>
        /// main constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code.</param>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings</param>
        /// <param name="menus">string array of its menus</param>
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont nomalFont, SpriteFont strongFont, string[] menus)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;

            this.spriteBatch = spriteBatch;
            this.nomalFont = nomalFont;
            this.strongFont = strongFont;

            menuItems = new List<string>();
            for (int i = 0; i < menus.Length; i++)
            {
                menuItems.Add(menus[i]);
            }

            position = new Vector2(Game1.SCREEN_WIDTH/2, Game1.SCREEN_HEIGHT - 180);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState curKey = Keyboard.GetState();

            if (curKey.IsKeyDown(Keys.Down) && oldKey.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (curKey.IsKeyDown(Keys.Up) && oldKey.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }

            oldKey = curKey;

            base.Update(gameTime);
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. 
        /// Override this method with component-specific drawing code
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 menuPos = position;
            spriteBatch.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(strongFont, menuItems[i], menuPos, Color.Yellow);
                    menuPos.Y += nomalFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(nomalFont, menuItems[i], menuPos, Color.White);
                    menuPos.Y += nomalFont.LineSpacing;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
