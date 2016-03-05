using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ConversationEditor.UI
{
	public class UIBox : UIElement
	{
		Texture2D background;

		public UIBox(int x, int y, int width, int height, Texture2D background) : base(x, y, width, height)
		{
			this.background = background;
		}

		public override bool Update()
		{
			return false;
		}

		public override void Draw(SpriteBatch sb)
		{
			sb.Draw(background, sb.GraphicsDevice.Viewport.Bounds, Color.White);
		}
	}
}

