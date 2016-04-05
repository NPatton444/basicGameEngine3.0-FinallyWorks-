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
    public partial class MainScreen : UserControl
    {
        public MainScreen()
        {
            InitializeComponent();
            startButton.Location = new Point(this.Width / 2 - startButton.Width / 2, this.Height / 2 - startButton.Height);
            closeButton.Location = new Point(this.Width / 2 - closeButton.Width / 2, this.Height / 2 + closeButton.Height);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            GameScreen gs = new GameScreen();
            Form f = this.FindForm();
            f.Controls.Add(gs);
            f.Controls.Remove(this);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
