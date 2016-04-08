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
        int monsterSpeed, monsterWidth, monsterHeight, monsterDirection, monsterSpawn;//monsterSpawn counts the number of shots to spawn monsters.
        Random randGen = new Random();
        int mDirectionChange = 100;

        Image[] monsterImage = new Image[4];

        List<Bullets> bulletsList = new List<Bullets>();
        SolidBrush bulletBrush = new SolidBrush(Color.White);

        int score = 0;

        int scoreIncrement = 15;

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
            shotOk = 5;
            playerDrawX = this.Width / 2;
            playerDrawY = this.Height / 2;
            p = new Player(playerDrawX, playerDrawY, Properties.Resources.RedGuyDown.Width, Properties.Resources.RedGuyDown.Height, 5, heroImage);

            monsterSpeed = 2;
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
            #region Player Movement

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
                if (p.y >= 60)
                {
                    p.move(p, "up");
                    direction = 1;
                }
            }

            #endregion

            #region Bullet Movement

            shotOk--;

            if (shot)
            {
                if (shotOk < 0)
                {
                    if (direction == 0)
                    {
                        Bullets b = new Bullets(p.x + p.width / 2, p.y + p.height / 2, 10, 10, "down");
                        bulletsList.Add(b);
                        shotOk = 25;
                        monsterSpawn--;
                    }
                    else if(direction == 1)
                    {
                        Bullets b = new Bullets(p.x + p.width / 2, p.y + p.height / 2, 10, 10, "up");
                        bulletsList.Add(b);
                        shotOk = 25;
                        monsterSpawn--;
                    }
                    else if(direction == 2)
                    {
                        Bullets b = new Bullets(p.x + p.width / 2, p.y + p.height / 2, 10, 10, "left");
                        bulletsList.Add(b);
                        shotOk = 25;
                        monsterSpawn--;
                    }
                    else if(direction == 3)
                    {
                        Bullets b = new Bullets(p.x + p.width / 2, p.y + p.height / 2, 10, 10, "right");
                        bulletsList.Add(b);
                        shotOk = 25;
                        monsterSpawn--;
                    }
                }
            }

            foreach(Bullets b in bulletsList)
            {
                b.move(b);
            }

            //Remove Bullets
            foreach(Bullets b in bulletsList)
            {
                if(b.x + b.size < 0 || b.x + b.size > this.Width)
                {
                    bulletsList.Remove(b);
                    break;
                }
                else if (b.y + b.size < 55 || b.y + b.size > this.Height)
                {
                    bulletsList.Remove(b);
                     break;
                }
             }

            #endregion

            #region Monster Spawning

            if (monsterList.Count() == 0)
            {
                monsterDirection = randGen.Next(0, 4);
                if (monsterDirection == 0 || monsterDirection == 1)
                {
                    monsterWidth = Properties.Resources.monsterDown.Width;
                    monsterHeight = Properties.Resources.monsterDown.Height;
                }
                else if (monsterDirection == 2 || monsterDirection == 3)
                {
                    monsterWidth = Properties.Resources.monsterLeft.Width;
                    monsterHeight = Properties.Resources.monsterLeft.Height;
                }
                int monsterX, monsterY;

                monsterX = randGen.Next(0, this.Width - Properties.Resources.monsterDown.Width - 20);
                monsterY = randGen.Next(60, this.Height - Properties.Resources.monsterDown.Height - 20);

                Monster m = new Monster(monsterX, monsterY, monsterWidth, monsterHeight, monsterSpeed, monsterDirection, monsterImage);
                monsterList.Add(m);
                monsterSpawn = 20;
                monsterSpeed++;
            }

            if (monsterSpawn == 0)
            {
                monsterDirection = randGen.Next(0, 4);
                if (monsterDirection == 0 || monsterDirection == 1)
                {
                    monsterWidth = Properties.Resources.monsterDown.Width;
                    monsterHeight = Properties.Resources.monsterDown.Height;
                }
                else if (monsterDirection == 2 || monsterDirection == 3)
                {
                    monsterWidth = Properties.Resources.monsterLeft.Width;
                    monsterHeight = Properties.Resources.monsterLeft.Height;
                }

                int monsterX, monsterY;

                monsterX = randGen.Next(0, this.Width - Properties.Resources.monsterDown.Width - 20);
                monsterY = randGen.Next(0, this.Height - Properties.Resources.monsterDown.Height - 20);

                Monster m = new Monster(monsterX, monsterY, monsterWidth, monsterHeight, monsterSpeed, monsterDirection, monsterImage);
                monsterList.Add(m);
                monsterSpawn = 20;
                monsterSpeed++;
            }

            #endregion

            #region Monster Move

            mDirectionChange--;

            foreach (Monster m in monsterList)
            {
                if(m.x >= 0 && m.y >= 60 && m.y < this.Height + m.height && m.x < this.Width + m.width)
                {
                    m.move(m, m.mDirection);
                }

                if (m.x < 5 || m.y < 60 || m.x + m.width > this.Width - 5 || m.y + m.height > this.Width - 5)
                {
                    if (m.mDirection == 0)
                    {
                        m.mDirection = randGen.Next(1, 4);
                        m.y -= 10;
                        mDirectionChange = randGen.Next(20, 101);
                        break;
                    }
                    else if (m.mDirection == 1)
                    {
                        m.mDirection = randGen.Next(2, 4);
                        m.y += 10;
                        mDirectionChange = randGen.Next(20, 101);
                        break;
                    }
                    else if (m.mDirection == 2)
                    {
                        m.mDirection = randGen.Next(0, 2);
                        m.x += 10;
                        mDirectionChange = randGen.Next(20, 101);
                        break;
                    }
                    else if (m.mDirection == 3)
                    {
                        m.mDirection = randGen.Next(0, 3);
                        m.x -= 10;
                        mDirectionChange = randGen.Next(20, 101);
                        break;
                    }
                }
                
                if(mDirectionChange == 0)
                {
                    if (m.mDirection == 0)
                    {
                        m.mDirection = randGen.Next(1, 4);
                        m.y -= 10;
                        mDirectionChange = randGen.Next(20, 101);
                        break;
                    }
                    else if (m.mDirection == 1)
                    {
                        m.mDirection = randGen.Next(2, 4);
                        m.y += 10;
                        mDirectionChange = randGen.Next(20, 101);
                        break;
                    }
                    else if (m.mDirection == 2)
                    {
                        m.mDirection = randGen.Next(0, 2);
                        m.x += 10;
                        mDirectionChange = randGen.Next(20, 101);
                        break;
                    }
                    else if (m.mDirection == 3)
                    {
                        m.mDirection = randGen.Next(0, 3);
                        m.x -= 10;
                        mDirectionChange = randGen.Next(20, 101);
                        break;
                    }
                }
            }

            #endregion

            #region Collision

            //Player and Monster Collision
            foreach (Monster m in monsterList)
            {
                if (p.collision(p, m))
                {
                    gameTimer.Stop();

                    monsterList.Remove(m);

                    Form f = this.FindForm();
                    f.Controls.Remove(this);
                    GameOver go = new GameOver();
                    f.Controls.Add(go);

                    break;
                }
            }

            foreach(Monster m in monsterList)
            {
                foreach(Bullets b in bulletsList)
                {
                    if(m.collision(m, b))
                    {
                        monsterList.Remove(m);
                        bulletsList.Remove(b);
                        score += 15;
                        break;
                    }
                }
                break;
            }

            #endregion

            scoreIncrement--;

            if(scoreIncrement == 0)
            {
                score++;
                scoreIncrement = 15;
            }

            scoreLabel.Text = "Score: " + Convert.ToString(score);

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(bulletBrush, 0, 50, this.Width, 5);

            e.Graphics.DrawImage(p.image[direction], p.x, p.y);

            foreach(Bullets b in bulletsList)
            {
                e.Graphics.FillRectangle(bulletBrush, b.x, b.y, b.size, b.size);
            }
            foreach(Monster m in monsterList)
            {
                e.Graphics.DrawImage(m.image[m.mDirection], m.x, m.y);
            }
        }
    }
}
