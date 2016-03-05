using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace ConversationEditor.UI
{
	public abstract class UIElement
	{
		public UIElement Parent;
		protected UITree tree;
		public List<UIElement> Children;
		protected RenderTarget2D target;
		protected SpriteBatch spriteBatch;
		Rectangle bounds, dest, src;
		Color color = Color.White;
		protected SpriteBatch local;

		public Rectangle Bounds
		{
			get
			{
				return bounds;
			}
		}

		public UIElement(int x, int y, int width, int height)
		{
			Children = new List<UIElement>();
			bounds = new Rectangle(x, y, width, height);
		}

		public abstract bool Update();

		public abstract void Draw(SpriteBatch sb);

		public void DoDraw()
		{
			if(target != null && local != null)
			{
				target.GraphicsDevice.SetRenderTarget(target);
				target.GraphicsDevice.Clear(Color.Transparent);
				local.Begin();
				Draw(local);
				local.End();
				target.GraphicsDevice.SetRenderTarget(null);
				if(Parent != null)
				{
					dest = Rectangle.Intersect(bounds, Parent.Bounds);
					src = new Rectangle(dest.X - bounds.X, dest.Y - bounds.Y, dest.Width, dest.Height);
					spriteBatch.Draw(target, dest, src, color);
				}
				else
				{
					spriteBatch.Draw(target, bounds, color);
				}
			}
		}


		public void AddChild(UIElement child)
		{
			Children.Add(child);
			child.tree = tree;
			child.spriteBatch = spriteBatch;
			child.local = local;
			child.target = new RenderTarget2D(target.GraphicsDevice, child.Bounds.Width, child.Bounds.Height);
			child.Parent = this;
		}

		public bool TriggerUpdate()
		{
			return Update();
		}
	}
}

