using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ConversationEditor.UI
{
	public partial class UITree
	{
		private class UIRootElement : UIElement
		{

			Texture2D blank;

			public UIRootElement(GraphicsDevice device, SpriteBatch spriteBatch, UITree tree) : base(0,
				                                                                                        0,
				                                                                                        device.Viewport.Width,
				                                                                                        device.Viewport.Height)
			{
				target = new RenderTarget2D(device, Bounds.Width, Bounds.Height);
				this.spriteBatch = spriteBatch;
				this.tree = tree;
				Parent = null;
				local = new SpriteBatch(device);
				blank = new Texture2D(device, 1, 1);
				blank.SetData(new Color[]{ Color.White });
			}

			public override bool Update()
			{
				return false;
			}

			public override void Draw(SpriteBatch sb)
			{
				sb.Draw(blank, sb.GraphicsDevice.Viewport.Bounds, Color.LightGray);
			}
		}
	}
}

