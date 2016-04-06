using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicGameEngine
{
    public partial class GameScreen : UserControl
    {
        bool leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, shot;
        int playerDrawX, playerDrawY, direction, shotOk;

        Image[] heroImage = new Image[4];

        Player p;

        List<Monster> monsterList = new List<Monster>();
        int monsterX, monsterY, monsterSpeed, monsterSpawn;//monsterSpawn counts the number of shots to spawn monsters.
        Random randGen = new Random();

        Image[] monsterImage = new Image[4];

        List<Bullets> bulletsList = new List<Bullets>();
        SolidBrush bulletBrush = new SolidBrush(Color.White);

        public GameScreen()
        {
            InitializeComponent();

            //Create elements of image array for player
            heroImage[0] = Properties.Resources.RedGuyDown;
            heroImage[1] = Properties.Resources.RedGuyUp;
            heroImage[2] = Properties.Resources.RedGuyLeft;
            heroImage[3] = Properties.Resources.RedGuyRight;

            //Image array for monster
            monsterImage[0] = Properties.Resources.monsterDown;
            monsterImage[1] = Properties.Resources.monsterUp;
            monsterImage[2] = Properties.Resources.monsterLeft;
            monsterImage[3] = Properties.Resources.monsterRight;

            //start the timer when the program starts
            gameTimer.Enabled = true;
            gameTimer.Start();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            shotOk = 20;
            playerDrawX = this.Width / 2;
            playerDrawY = this.Height / 2;
            p = new Player(playerDrawX, playerDrawY, Properties.Resources.RedGuyDown.Width, Properties.Resources.RedGuyDown.Height, 5, heroImage);

            monsterX = randGen.Next(0, this.Width - Properties.Resources.monsterDown.Width - 20);
            monsterY = randGen.Next(0, this.Height - Properties.Resources.monsterDown.Height - 20);
            monsterSpeed = 2;
            Monster m = new Monster(monsterX, monsterY, Properties.Resources.monsterUp.Width, Properties.Resources.monsterUp.Height, monsterSpeed, monsterImage);
            monsterList.Add(m);
            monsterSpawn = 20;

            this.Focus();
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //check to see if a key is pressed and set is KeyDown value to true if it has
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Space:
                    shot = true;
                    break;
                default:
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //check to see if a key has been released and set its KeyDown value to false if it has
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Space:
                    shot = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            shotOk--;

            //checks to see if any keys have been pressed and adjusts the X or Y value
            //for the Player appropriately
            if (leftArrowDown)
            {
                if (p.x >= 0)
                {
                    p.move(p, "left");
                    direction = 2;
                }
            }
            else if (downArrowDown)
            {
                if (p.y <= this.Height - Properties.Resources.RedGuyDown.Height)
                {
                    p.move(p, "down");
                    direction = 0;
                }
            }
            else if (rightArrowDown)
            {
                if (p.x <= this.Width - Properties.Resources.RedGuyDown.Width)
                {
                    p.move(p, "right");
                    direction = 3;
                }
            }
            else if (upArrowDown)
            {
                if (p.y >= 0)
                {
                    p.move(p, "up");
                    direction = 1;
                }
            }

            if (shotOk == 0)
            {
                if (shot)
                {
                    if (direction == 0)
                    {
                        Bullets b = new Bullets(p.x + Properties.Resources.RedGuyDown.Width / 2, p.y + Properties.Resources.RedGuyDown.Height, 10, 10, "down");
                    }
                }

            }
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(p.image[direction], p.x, p.y);
            foreach(Bullets b in bulletsList)
            {
                e.Graphics.FillRectangle(bulletBrush, b.x, b.y, b.size, b.size);
            }
        }
    }
}
