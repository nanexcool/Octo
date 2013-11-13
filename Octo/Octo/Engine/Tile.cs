using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octo.Engine
{
    public enum TileType
	{
        Empty
	}

    class Tile
    {
        public TileType Type { get; set; }
    }
}
