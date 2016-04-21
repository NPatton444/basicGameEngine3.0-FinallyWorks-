using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicGameEngine
{
    class Bullets
    {
        public int x, y, size, speed;
        public string direction;

        public Bullets(int _x, int _y, int _size, int _speed, string _direction)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            direction = _direction;
        }

        /// <summary>
        /// Bullet Movement
        /// </summary>
        /// <param name="b">Bullet Object</param>
        public void move(Bullets b)
        {
            if (b.direction == "left")
            {
                b.x -= b.speed;
            }
            else if (b.direction == "right")
            {
                b.x += b.speed;
            }
            else if (b.direction == "down")
            {
                b.y += b.speed;
            }
            else if (b.direction == "up")
            {
                b.y -= b.speed;
            }
        }
    }
}
