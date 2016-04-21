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
        public int x, y, width, height, speed, mDirection;
        public Image[] image = new Image[4];

        public Monster(int _x, int _y, int _width, int _height, int _speed, int _mDirection, Image[] _image)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = _speed;
            mDirection = _mDirection;
            image = _image;
        }

        /// <summary>
        /// Allows monster to move
        /// </summary>
        /// <param name="m">Monster Object</param>
        /// <param name="direction">Direction of Movement</param>
        public void move(Monster m, int direction)
        {
            if (direction == 2)
            {
                m.x -= m.speed;
            }
            else if (direction == 3)
            {
                m.x += m.speed;
            }
            else if (direction == 0)
            {
                m.y += m.speed;
            }
            else if (direction == 1)
            {
                m.y -= m.speed;
            }
        }

        public bool collision(Monster m, Bullets b)
        {
            //Put Bullet and Monster in Box for collision
            Rectangle mRec = new Rectangle(m.x, m.y, m.width, m.height);
            Rectangle bRec = new Rectangle(b.x, b.y, b.size, b.size);

            //Checks collision
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

