using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGGameLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGGameLibrary
{
    public interface ICollidable
    {
        public Shape Shape { get; }

        public bool CollidesWith(ICollidable other, ref Vector2 collisionNormal);
    }
}
