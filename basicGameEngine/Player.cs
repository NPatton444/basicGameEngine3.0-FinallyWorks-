using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace basicGameEngine
{
    class Player
    {
        public int x, y, width, height, speed;
        public Image[] image = new Image[4];

        public Player(int _x, int _y, int _width, int _height, int _speed, Image[] _image)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = _speed;
            image = _image;
        }

        public void move(Player p, string direction)
        {
            if (direction == "left")
            {
                p.x -= p.speed;
            }
            else if(direction == "right")
            {
                p.x += p.speed;
            }
            else if(direction == "down")
            {
                p.y += p.speed;
            }
            else if(direction == "up")
            {
                p.y -= p.speed;
            }
        }

        public bool collision(Player p, Monster m)
        {
            Rectangle pRec = new Rectangle(p.x, p.y, p.width, p.height);
            Rectangle mRec = new Rectangle(m.x, m.y, m.size, m.size);

            if (pRec.IntersectsWith(mRec))
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
