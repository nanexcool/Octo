using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octo.Engine
{
    static class Util
    {
        public static SpriteFont Font { get; private set; }
        public static Texture2D Texture { get; private set; }

        public static void Initialize(Game game)
        {
            Texture = new Texture2D(game.GraphicsDevice, 1, 1);
            Texture.SetData<Color>(new Color[] { Color.White });

            Font = game.Content.Load<SpriteFont>("Font");
        }
    }
}
