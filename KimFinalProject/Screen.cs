using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/// <summary>
/// Kookhwan Im
/// PROG2370 Final Project
/// Dec 30, 2017
/// 
/// This program is Snake vs Blocks game.
/// </summary>

namespace KimFinalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class Screen : DrawableGameComponent
    {
        private List<GameComponent> components;

        /// <summary>
        /// make and return the list include all components in this components
        /// </summary>
        public List<GameComponent> Components
        {
            get
            {
                return components;
            }
            set
            {
                components = value;
            }
        }

        /// <summary>
        /// constructor of this class
        /// </summary>
        /// <param name="game">Provides basic graphics device initialization, game logic, and rendering code.</param>
        public Screen(Game game) : base(game)
        {
            components = new List<GameComponent>();
            isVisible();
        }

        //  This Method is able to set to show or hide for each screens
        public virtual void isVisible(bool bVisible = false)
        {
            this.Enabled = bVisible;
            this.Visible = bVisible;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// This method is to update for components in visible screen.
        /// It is similar with Timer or Thread
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This method is to draw for components in visible screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent GameComponent = null;
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    GameComponent = (DrawableGameComponent)item;

                    if (GameComponent.Visible)
                    {
                        // Draw components in the visible component
                        GameComponent.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}
