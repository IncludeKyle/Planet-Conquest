using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planet_Conquest
{
    public partial class Form3 : Form
    {
        // Constructor
        public Form3(string actionContent, string invaderMessage, string defenderMessage, Color colorAttack, Color colorDefense)
        {
            InitializeComponent();

            // Set the action textBox's string contents
            textBoxAction.Text = actionContent;

            // Set offense colors
            textBoxOffense.BackColor = colorAttack;
            textBoxOffenseTitle.BackColor = colorAttack;

            // Set defense colors
            textBoxDefense.BackColor = colorDefense;
            textBoxDefenseTitle.BackColor = colorDefense;

            // Invader and defender messages set to respective textboxes after each round of combat
            textBoxOffense.Text = invaderMessage;
            textBoxDefense.Text = defenderMessage;

            // Set the invader and defender titles -----> This will eventually need to be set to Player #1 and Player #2 names'
            textBoxOffenseTitle.Text = "Invader";
            textBoxDefenseTitle.Text = "Defender";
        }

        // User event - Clicked continue button
        private void buttonContinue_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // User event - Clicked retreat button
        private void buttonRetreat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
