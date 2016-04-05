using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace basicGameEngine
{
    class Monster
    {
        public int x, y, size, speed;
        public Image[] image = new Image[4];

        public Monster(int _x, int _y, int _size, int _speed, Image[] _image)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            image = _image;
        }

        public void move(Monster m, string direction)
        {
            if (direction == "left")
            {
                m.x -= m.speed;
            }
            else if (direction == "right")
            {
                m.x += m.speed;
            }
            else if (direction == "down")
            {
                m.y += m.speed;
            }
            else if (direction == "up")
            {
                m.y -= m.speed;
            }
        }

        public bool collision(Monster m, Bullets b)
        {
            Rectangle mRec = new Rectangle(m.x, m.y, m.size, m.size);
            Rectangle bRec = new Rectangle(b.x, b.y, b.size, b.size);

            if (mRec.IntersectsWith(bRec))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}

