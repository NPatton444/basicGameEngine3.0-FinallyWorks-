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
    public partial class GameOver : UserControl
    {
        public GameOver()
        {
            InitializeComponent();
            closeButton.Location = new Point(this.Width / 2 - closeButton.Width / 2, this.Height / 2 + closeButton.Height / 2);
            restartButton.Location = new Point(this.Width / 2 - restartButton.Width / 2, this.Height / 2 - restartButton.Height / 2);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();
            f.Controls.Remove(this);
            MainScreen ms = new MainScreen();
            f.Controls.Add(ms);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
