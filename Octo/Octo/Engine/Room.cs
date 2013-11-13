using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octo.Engine
{
    class Room
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public List<Entity> Entities { get; private set; }
        public Entity Player { get; set; }

        public Room(int width, int height)
        {
            Width = width;
            Height = height;

            Entities = new List<Entity>();
        }
        
        public virtual void Update(float elapsed)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(elapsed);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Util.Texture, new Rectangle(0, 0, Width * 32, Height * 32), Color.White);

            foreach (Entity e in Entities)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
