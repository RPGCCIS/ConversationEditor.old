using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace ConversationEditor.UI
{
	public partial class UITree
	{
		List<UIElement> DrawList;
		List<UIElement> NextDrawList;
		List<UIElement> UpdateChildList;
		UIElement ShouldUpdate;
		Thread UIThread;
		bool running = true;
		MouseState mouseState;

		UIElement root;

		public UIElement Root
		{
			get
			{
				return root;
			}
		}

		public UITree(GraphicsDevice device, SpriteBatch sb)
		{
			root = new UIRootElement(device, sb, this);
			DrawList = new List<UIElement>();
			UpdateChildList = new List<UIElement>();
			UIThread = new Thread(ThreadUpdate);
			UIThread.Start();
		}


		public void Draw()
		{
			for(DrawList.Add(root); DrawList.Count != 0;)
			{
				NextDrawList = new List<UIElement>();
				foreach(UIElement e in DrawList)
				{
					e.DoDraw();
					NextDrawList.AddRange(e.Children);
				}
				DrawList = NextDrawList;
			}
		}

		public void Update()
		{
			lock(this)
			{
				mouseState = Mouse.GetState();
			}
		}

		private void ThreadUpdate()
		{
			while(running)
			{
				Thread.Sleep(1);
				UpdateChildList = new List<UIElement>();
				UpdateChildList.Add(root);
				ShouldUpdate = root;
				bool done = false;
				while(mouseState != null && !done && UpdateChildList.Count != 0)
				{
					for(int i = 0; i < UpdateChildList.Count; ++i)
					{
						lock(this)
						{
							done = true;
							if(UpdateChildList[i].Bounds.Contains(mouseState.Position))
							{
								ShouldUpdate = UpdateChildList[i];
								i = UpdateChildList.Count;
								UpdateChildList = ShouldUpdate.Children;
								done = false;
							}
						}
					}
				}
				for(UIElement e = ShouldUpdate; e != null; e = e.Parent)
				{
					if(e.TriggerUpdate())
					{
						e = root;
					}
				}
			}
		}

		public void ShutDown()
		{
			running = false;
			UIThread.Join();
		}

	}
}

