#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace ConversationEditor
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class ConversationEditor : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont sf;
		UI.UITree uitree;
		UI.UIBox ui, ui2, ui3, ui4;

		Texture2D blank;

		public ConversationEditor()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;
			IsMouseVisible = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			base.Initialize();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			sf = Content.Load<SpriteFont>("Arial_14");
			blank = new Texture2D(GraphicsDevice, 1, 1);
			blank.SetData(new Color[]{ Color.White });
			uitree = new UI.UITree(GraphicsDevice, spriteBatch);
			ui = new UI.UIBox(5, 5, 20, 20, blank);
			ui2 = new UI.UIBox(50, 50, 100, 100, blank);
			ui3 = new UI.UIBox(100, 100, 50, 50, blank);
			ui4 = new UI.UIBox(125, 125, 200, 200, blank);
			uitree.Root.AddChild(ui);
			uitree.Root.AddChild(ui2);
			ui2.AddChild(ui3);
			ui3.AddChild(ui4);

			//TODO: use this.Content to load your game content here 
		}

		protected override void UnloadContent()
		{
			uitree.ShutDown();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			   Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}
			#endif
			uitree.Update();
			// TODO: Add your update logic here			
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			uitree.Draw();
			spriteBatch.Draw(blank, new Rectangle(Mouse.GetState().X - 1, Mouse.GetState().Y - 1, 2, 2), Color.Blue);
			spriteBatch.End();
            
			base.Draw(gameTime);
		}
	}
}

