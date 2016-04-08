///Author: Noah Patton
///Last Modified: 04/08/16
///Description: This program is a simple game engine to be used in later projects.
///A small game was made on top of it where the objective is to shoot a monster and 
///not get hit by the monster.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicGameEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MainScreen ms = new MainScreen();
            this.Controls.Add(ms);
        }
    }
}
