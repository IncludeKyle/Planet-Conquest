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
    public partial class Form1 : Form
    {
        // Make the game object variable
        Game game;
        Build_Move build_move;

        // Array of planet flags
        TextBox[] flagBoxes;

        // Constructor
        public Form1()
        {
            InitializeComponent(); // Display form1
            game = new Game();     // Create the game object which contains all info for 8 planets and player info
            build_move = new Build_Move(); // Make the build and move object 
            LoadImages();          // Set up all 8 planet images and names
            LoadFlagBoxes();       // Create array of flag textboxes
            textBoxPlayerTurn.BackColor = game.player1Color;       // Load palyer #1's team color
            textBoxPlayerTurn.Text = game.player1Name + "'s Turn"; // Load the resource amount for player #1 into the textbox
            textBoxResources.Text = "$" + game.player1Resources;   // Load player #1 username into the textbox
            ComboBoxMoveToSetUp();   // Set up move option comboBox
            UpdatePlanetStatsBox(0); // Set to player #1 home planet

            // Set player #1 and #2 flag colors
            textBoxFlag1.BackColor = game.player1Color;
            textBoxFlag5.BackColor = game.player2Color;
        }

        // Load array of flag boxes
        void LoadFlagBoxes()
        {
            flagBoxes = new TextBox[8];
            flagBoxes[0] = textBoxFlag1;
            flagBoxes[1] = textBoxFlag2;
            flagBoxes[2] = textBoxFlag3;
            flagBoxes[3] = textBoxFlag4;
            flagBoxes[4] = textBoxFlag5;
            flagBoxes[5] = textBoxFlag6;
            flagBoxes[6] = textBoxFlag7;
            flagBoxes[7] = textBoxFlag8;
        }

        // Sets up the combobox list on the GUI
        void ComboBoxMoveToSetUp()
        {
            var resourceList = new List<ResourceComboBox>(); // ResourceComboBox is a class declared in Form2.cs
                                                             // Build a list of ResourceComboBox objects 
            resourceList.Add(new ResourceComboBox() { Name = game.planets[1].planetName, Value = 1 });
            resourceList.Add(new ResourceComboBox() { Name = game.planets[7].planetName, Value = 2 });

            // Set up data binding
            this.comboBoxMoveTo.DataSource = resourceList;
            this.comboBoxMoveTo.DisplayMember = "Name";
            this.comboBoxMoveTo.ValueMember = "Value";
        }

        // Modify combobox list, verify relative distance difference
        void ComboBoxMoveToModify(int planetPosition)
        {
            int option1, option2;

            // Value needs to equal the planet location ID, not relative ID 

            // If planet #1
            if (planetPosition == 1) 
            {
                option1 = 1; option2 = 7; }
            // If planet #8
            else if (planetPosition == 8) 
            {
                option1 = 0; option2 = 6; }
            else // If not a special condition like positions #1 or #8
            {
                option1 = planetPosition;     // Position 1: higher position
                option2 = planetPosition - 2; // Position 2: lower position
            }

            var resourceList = new List<ResourceComboBox>(); // ResourceComboBox is a class declared in Form2.cs
                                                             // Build a list of ResourceComboBox objects 
            resourceList.Add(new ResourceComboBox() { Name = game.planets[option1].planetName, Value = game.planets[option1].position });  // Move option 1
            resourceList.Add(new ResourceComboBox() { Name = game.planets[option2].planetName, Value = game.planets[option2].position }); // Move option 2

            // Set up data binding
            this.comboBoxMoveTo.DataSource = resourceList;
            this.comboBoxMoveTo.DisplayMember = "Name";
            this.comboBoxMoveTo.ValueMember = "Value";
        }

        // Link all of the GUI picture boxes with each planet's image
        private void LoadImages()
        {
            // Set up all 8 planet images and names
            pictureBox1.BackgroundImage = game.planets[0].planetImage;
            pictureBox2.BackgroundImage = game.planets[1].planetImage;
            pictureBox3.BackgroundImage = game.planets[2].planetImage;
            pictureBox4.BackgroundImage = game.planets[3].planetImage;
            pictureBox5.BackgroundImage = game.planets[4].planetImage;
            pictureBox6.BackgroundImage = game.planets[5].planetImage;
            pictureBox7.BackgroundImage = game.planets[6].planetImage;
            pictureBox8.BackgroundImage = game.planets[7].planetImage;
        }

        // Update a given planet's stats on the GUI display
        private void UpdatePlanetStatsBox(int planetID)
        {
            game.IDofCurrentPlanet = planetID;                      // Keeps track of what planet is selected
            textBoxPlanet.Text = game.planets[planetID].planetName; // Update planet textbox
            int playerID = game.planets[planetID].OwnedByUserID;    // Get planet's owner ID

            // Use ID to determine username
            string player;
            if (playerID == 1)
            {
                player = game.player1Name;
                textBoxPlanetStats.ForeColor = game.player1Color; // Set stat box text color for player #1
                textBoxPlanet.BackColor = game.player1Color;

            }
            else if (playerID == 2)
            {
                player = game.player2Name;
                textBoxPlanetStats.ForeColor = game.player2Color; // Set stat box text color for player #2
                textBoxPlanet.BackColor = game.player2Color;
            }
            else
            {
                player = "Neutral";
                textBoxPlanetStats.ForeColor = Color.FromName("White"); // Set colors to white and black for neutral
                textBoxPlanet.BackColor = Color.FromName("Black"); 
            }

            // Update the planet stats box
            textBoxPlanetStats.Text = "Owned by: " + player + "\r\n";
            textBoxPlanetStats.Text += "\r\nMines Active: " + game.planets[planetID].MinesActive + "\r\n";
            textBoxPlanetStats.Text += "Mining Sites Available: " + game.planets[planetID].minesAvailable + "\r\n\r\n";

            if (game.planets[planetID].CombatFrigate > 0)
                textBoxPlanetStats.Text += "Combat Frigates: " + game.planets[planetID].CombatFrigate + "\r\n";

            if (game.planets[planetID].BattleShip > 0)
                textBoxPlanetStats.Text += "Battle Ships: " + game.planets[planetID].BattleShip + "\r\n";

            if (game.planets[planetID].CapitalShip > 0)
                textBoxPlanetStats.Text += "Capital Ships: " + game.planets[planetID].CapitalShip + "\r\n";

            if (game.planets[planetID].SSMSite > 0)
                textBoxPlanetStats.Text += "SSM Defensive Sites: " + game.planets[planetID].SSMSite + "\r\n";
        }

        // Sets a 2D border around the selected planet on the GUI
        private void SetSelectedPlanetBorder(int selectedPlanet)
        {
            // Create an array for the picture boxes
            PictureBox[] planetPics;
            planetPics = new PictureBox[8];
            planetPics[0] = pictureBox1;
            planetPics[1] = pictureBox2;
            planetPics[2] = pictureBox3;
            planetPics[3] = pictureBox4;
            planetPics[4] = pictureBox5;
            planetPics[5] = pictureBox6;
            planetPics[6] = pictureBox7;
            planetPics[7] = pictureBox8;

            for (int i = 0; i < planetPics.Length; i++)
            {
                // Turn off borders for all other planets
                planetPics[i].BorderStyle = System.Windows.Forms.BorderStyle.None;

                if (i == selectedPlanet) // Once planet is found, add border
                {
                    planetPics[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }
            }
        }

        // ------------------------------------------------------------------- [Clicked planet picture events]
        // User clicked planet #1
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SetSelectedPlanetBorder(0); // Set the border around the selected planet
            UpdatePlanetStatsBox(0);    // Set the textbox with selected planet's info
            ComboBoxMoveToModify(1);    // Update the move options combobox with the two adjacent planets
        }

        // User clicked planet #2
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SetSelectedPlanetBorder(1); // Set the border around the selected planet
            UpdatePlanetStatsBox(1);    // Set the textbox with selected planet's info
            ComboBoxMoveToModify(2);    // Update the move options combobox with the two adjacent planets
        }

        // User clicked planet #3
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SetSelectedPlanetBorder(2); // Set the border around the selected planet
            UpdatePlanetStatsBox(2);
            ComboBoxMoveToModify(3);
        }

        // User clicked planet #4
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SetSelectedPlanetBorder(3); // Set the border around the selected planet
            UpdatePlanetStatsBox(3);
            ComboBoxMoveToModify(4);
        }

        // User clicked planet #5
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SetSelectedPlanetBorder(4); // Set the border around the selected planet
            UpdatePlanetStatsBox(4);
            ComboBoxMoveToModify(5);
        }

        // User clicked planet #6
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            SetSelectedPlanetBorder(5); // Set the border around the selected planet
            UpdatePlanetStatsBox(5);
            ComboBoxMoveToModify(6);
        }

        // User clicked planet #7
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            SetSelectedPlanetBorder(6); // Set the border around the selected planet
            UpdatePlanetStatsBox(6);
            ComboBoxMoveToModify(7);
        }

        // User clicked planet #8
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            SetSelectedPlanetBorder(7); // Set the border around the selected planet
            UpdatePlanetStatsBox(7);
            ComboBoxMoveToModify(8);
        }

        // ------------------------------------------------------------------- [Build menu events]
        // User wants to build a mine
        private void pictureBoxMine_Click(object sender, EventArgs e)
        {
            numericBuildMine.Value++;
            textBoxCost.Text = "$" + getTotalCost();
        }
        private void numericBuildMine_ValueChanged(object sender, EventArgs e)
        {
            textBoxCost.Text = "$" + getTotalCost();
        }

        // User wants to build an SSM
        private void pictureBoxSSM_Click(object sender, EventArgs e)
        {
            numericBuildSSM.Value++;
            textBoxCost.Text = "$" + getTotalCost();
        }
        private void numericBuildSSM_ValueChanged(object sender, EventArgs e)
        {
            textBoxCost.Text = "$" + getTotalCost();
        }

        // User wants to build a Frigate
        private void pictureBoxFrigate_Click(object sender, EventArgs e)
        {
            numericBuildFrigate.Value++;
            textBoxCost.Text = "$" + getTotalCost();
        }
        private void numericBuildFrigate_ValueChanged(object sender, EventArgs e)
        {
            textBoxCost.Text = "$" + getTotalCost();
        }

        // User wants to build a Battleship
        private void pictureBoxBattleship_Click(object sender, EventArgs e)
        {
            numericBuildBattleship.Value++;
            textBoxCost.Text = "$" + getTotalCost();
        }
        private void numericBuildBattleship_ValueChanged(object sender, EventArgs e)
        {
            textBoxCost.Text = "$" + getTotalCost();
        }

        // User wants to build a Capitalship
        private void pictureBoxCapitalship_Click(object sender, EventArgs e)
        {
            numericBuildCapitalship.Value++;
            textBoxCost.Text = "$" + getTotalCost();
        }
        private void numericBuildCapitalship_ValueChanged(object sender, EventArgs e)
        {
            textBoxCost.Text = "$" + getTotalCost();
        }

        // Calculates total cost across all build menu numeric up-downs
        private int getTotalCost()
        {
            // Calculate grand total 
            int totalCost = (int)(numericBuildMine.Value * build_move.MINE_COST);
            totalCost += (int)(numericBuildSSM.Value * build_move.SSM_COST);

            totalCost += (int)(numericBuildFrigate.Value * build_move.FRIGATE_COST);
            totalCost += (int)(numericBuildBattleship.Value * build_move.BATTLESHIP_COST);
            totalCost += (int)(numericBuildCapitalship.Value * build_move.CAPITALSHIP_COST);

            // Save this info in the build-move object
            build_move.minesToBuild = (int)numericBuildMine.Value;
            build_move.SSMsToBuild = (int)numericBuildSSM.Value;
            build_move.frigatesToBuild = (int)numericBuildFrigate.Value;
            build_move.battlesToBuild = (int)numericBuildBattleship.Value;
            build_move.capitalsToBuild = (int)numericBuildCapitalship.Value;
            build_move.totalCost = totalCost;

            // Reset warning flag
            labelWarning.Visible = false;

            return totalCost;
        }

        // User clicked build button
        private void buttonBuild_Click(object sender, EventArgs e)
        {
            // If it's player #1's turn
            if (game.WhichPlayersTurn == 1)
            {
                if (game.player1Resources >= build_move.totalCost               // If player has enough resources
                    && game.planets[game.IDofCurrentPlanet].OwnedByUserID == 1  // Only allow you to build on planets you own
                    && game.planets[game.IDofCurrentPlanet].MinesActive + numericBuildMine.Value <= game.planets[game.IDofCurrentPlanet].minesAvailable) // Can't build past mining site limit
                {
                    // Update the current planet with the purchased items
                    game.planets[game.IDofCurrentPlanet].MinesActive += build_move.minesToBuild;
                    game.planets[game.IDofCurrentPlanet].SSMSite += build_move.SSMsToBuild;
                    game.planets[game.IDofCurrentPlanet].CombatFrigate += build_move.frigatesToBuild;
                    game.planets[game.IDofCurrentPlanet].BattleShip += build_move.battlesToBuild;
                    game.planets[game.IDofCurrentPlanet].CapitalShip += build_move.capitalsToBuild;

                    // Subtract the spent resources
                    game.player1Resources -= build_move.totalCost;
                    labelCashflow.Visible = true;                     // Set outflow label visible
                    labelCashflow.ForeColor = Color.FromName("Red");  // Set color to red for outflows
                    labelCashflow.Text = "-$" + build_move.totalCost; // Update the cash outflow label

                    // Add the built mines to total mine amount
                    game.player1Mines += build_move.minesToBuild;

                    // Update the resources text box and stat box
                    textBoxResources.Text = "$" + game.player1Resources;
                    UpdatePlanetStatsBox(game.IDofCurrentPlanet);
                    ResetBuildAmounts();
                }
                else if (game.WhichPlayersTurn == 1 && game.player1Resources < build_move.totalCost)
                {
                    // Trigger warning label, not enough resources
                    ResetBuildAmounts();
                    labelWarning.Text = "Insufficient resources!";
                    labelWarning.Visible = true;
                }

                // If player tries to build past the mine limit
                if (game.planets[game.IDofCurrentPlanet].MinesActive + numericBuildMine.Value > game.planets[game.IDofCurrentPlanet].minesAvailable)
                {
                    ResetBuildAmounts();
                    labelWarning.Text = "Insufficient mining sites!";
                    labelWarning.Visible = true;
                }

                // If player tries to build on a planet that isn't theirs 
                if (game.planets[game.IDofCurrentPlanet].OwnedByUserID != 1)
                {
                    ResetBuildAmounts();
                    labelWarning.Text = "You can't build there!";
                    labelWarning.Visible = true;
                }
            }


            // If it's player #2's turn
            if (game.WhichPlayersTurn == 2)
            {
                if (game.player2Resources >= build_move.totalCost               // If player has enough resources
                    && game.planets[game.IDofCurrentPlanet].OwnedByUserID == 2  // Only allow you to build on planets you own
                    && game.planets[game.IDofCurrentPlanet].MinesActive + numericBuildMine.Value <= game.planets[game.IDofCurrentPlanet].minesAvailable) // Can't build past mining site limit
                {
                    // Update the current planet with the purchased items
                    game.planets[game.IDofCurrentPlanet].MinesActive += build_move.minesToBuild;
                    game.planets[game.IDofCurrentPlanet].SSMSite += build_move.SSMsToBuild;
                    game.planets[game.IDofCurrentPlanet].CombatFrigate += build_move.frigatesToBuild;
                    game.planets[game.IDofCurrentPlanet].BattleShip += build_move.battlesToBuild;
                    game.planets[game.IDofCurrentPlanet].CapitalShip += build_move.capitalsToBuild;

                    // Subtract the spent resources
                    game.player2Resources -= build_move.totalCost;
                    labelCashflow.Visible = true;                     // Set outflow label visible
                    labelCashflow.ForeColor = Color.FromName("Red");  // Set color to red for outflows
                    labelCashflow.Text = "-$" + build_move.totalCost; // Update the cash outflow label

                    // Add the built mines to total mine amount
                    game.player2Mines += build_move.minesToBuild;

                    // Update the resources text box and stat box
                    textBoxResources.Text = "$" + game.player2Resources;
                    UpdatePlanetStatsBox(game.IDofCurrentPlanet);
                    ResetBuildAmounts();
                }
                else if (game.WhichPlayersTurn == 2 && game.player2Resources < build_move.totalCost)
                {
                    // Trigger warning label, not enough resources
                    ResetBuildAmounts();
                    labelWarning.Text = "Insufficient resources!";
                    labelWarning.Visible = true;
                }

                // If player tries to build past the mine limit
                if (game.planets[game.IDofCurrentPlanet].MinesActive + numericBuildMine.Value > game.planets[game.IDofCurrentPlanet].minesAvailable)
                {
                    ResetBuildAmounts();
                    labelWarning.Text = "Insufficient mining sites!";
                    labelWarning.Visible = true;
                }

                // If player tries to build on a planet that isn't theirs 
                if (game.planets[game.IDofCurrentPlanet].OwnedByUserID != 2)
                {
                    ResetBuildAmounts();
                    labelWarning.Text = "You can't build there!";
                    labelWarning.Visible = true;
                }
            }

            // Clear out and reset the up-downs and textbox
            build_move.ResetItemAmounts();

            // Reset numeric up-downs
            void ResetBuildAmounts()
            {
                numericBuildMine.Value = 0;
                numericBuildSSM.Value = 0;
                numericBuildFrigate.Value = 0;
                numericBuildBattleship.Value = 0;
                numericBuildCapitalship.Value = 0;
            }
        }

        // ------------------------------------------------------------------- [Move menu events]
        // User clicked move capital ship picture box
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            numericMoveCapitalships.Value++;
            labelWarning.Visible = false;
        }
        private void numericMoveCapitalships_ValueChanged(object sender, EventArgs e)
        {
            labelWarning.Visible = false;
        }

        // User clicked move battle ship picture box
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            numericMoveBattleships.Value++;
            labelWarning.Visible = false;
        }
        private void numericMoveBattleships_ValueChanged(object sender, EventArgs e)
        {
            labelWarning.Visible = false;
        }

        // User clicked move frigate picture box
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            numericMoveFrigates.Value++;
            labelWarning.Visible = false;
        }
        private void numericMoveFrigates_ValueChanged(object sender, EventArgs e)
        {
            labelWarning.Visible = false;
        }

        // End and switch turns
        private void EndSwitchTurns()
        {
            ResetBuildAmounts();

            game.SetWhoseTurn(game.WhichPlayersTurn); // Change the ID of whose turn it is
            if (game.WhichPlayersTurn == 1)
            {
                // Reset warning label
                labelWarning.Visible = false;

                // Combo box move options update
                ComboBoxMoveToModify(1);

                // Update resources, resources generate per mine
                game.player1Resources += game.player1Mines * build_move.HARVEST_AMOUNT;

                // Set cashflow label visible and green
                labelCashflow.Visible = true;
                labelCashflow.ForeColor = Color.FromName("Green");
                labelCashflow.Text = "+$" + game.player1Mines * build_move.HARVEST_AMOUNT;

                // Set stat box to home base
                UpdatePlanetStatsBox(0);
                // Update border
                SetSelectedPlanetBorder(0);

                textBoxPlayerTurn.BackColor = game.player1Color;
                textBoxPlayerTurn.Text = game.player1Name + "'s Turn"; // Turn set to player 1
                textBoxResources.BackColor = game.player1Color;
                textBoxResources.Text = "$" + game.player1Resources;
            }
            else
            {
                // Reset warning label
                labelWarning.Visible = false;

                // Combo box move options update
                ComboBoxMoveToModify(5);

                // Update resources, resources generate per mine
                game.player2Resources += game.player2Mines * build_move.HARVEST_AMOUNT;

                // Set cashflow label visible and green
                labelCashflow.Visible = true;
                labelCashflow.ForeColor = Color.FromName("Green");
                labelCashflow.Text = "+$" + game.player2Mines * build_move.HARVEST_AMOUNT;

                // Set stat box to home base
                UpdatePlanetStatsBox(4);
                // Update border
                SetSelectedPlanetBorder(4);

                textBoxPlayerTurn.BackColor = game.player2Color;
                textBoxPlayerTurn.Text = game.player2Name + "'s Turn"; // Turn set to player 2
                textBoxResources.BackColor = game.player2Color;
                textBoxResources.Text = "$" + game.player2Resources;
            }

            // Reset GUI numeric up-downs
            void ResetBuildAmounts()
            {
                numericBuildMine.Value = 0;
                numericBuildSSM.Value = 0;
                numericBuildFrigate.Value = 0;
                numericBuildBattleship.Value = 0;
                numericBuildCapitalship.Value = 0;
            }
        }

        // User clicked to end their turn in the maneuver menu
        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            EndSwitchTurns();
        }

        // User initiated a move 
        private void buttonMove_Click(object sender, EventArgs e)
        {
            // Player 1 is making a move
            if (game.WhichPlayersTurn == 1)
            {
                PerformMove();
            }

            // Player 2 is making a move
            if (game.WhichPlayersTurn == 2)
            {
                PerformMove();
            }

            // Reset numeric up down values
            void ResetUpDowns()
            {
                numericMoveFrigates.Value = 0;
                numericMoveBattleships.Value = 0;
                numericMoveCapitalships.Value = 0;
            }

            // Verify the move can be performed
            bool CanMove()
            {
                bool canMove = false;
                bool allUpDownsZero = false;
                bool tooManyShips = false;

                // If all numeric up-downs are set to 0 and a move is tried
                if (numericMoveFrigates.Value == 0 && numericMoveBattleships.Value == 0 && numericMoveCapitalships.Value == 0)
                {
                    tooManyShips = true;
                    allUpDownsZero = true;
                    labelWarning.Text = "No ships selected!";
                    labelWarning.Visible = true;
                    ResetUpDowns();
                }
                // If theres not enough frigates on current planet to perform move
                if (numericMoveFrigates.Value > game.planets[game.IDofCurrentPlanet].CombatFrigate)
                {
                    tooManyShips = true;
                    ResetUpDowns();
                    labelWarning.Text = "Not enough frigates!";
                    labelWarning.Visible = true;
                }
                // If theres not enough battleships on current planet to perform move
                if (numericMoveBattleships.Value > game.planets[game.IDofCurrentPlanet].BattleShip)
                {
                    tooManyShips = true;
                    ResetUpDowns();
                    labelWarning.Text = "Not enough battleships!";
                    labelWarning.Visible = true;
                }
                // If theres not enough capitalships on current planet to perform move
                if (numericMoveCapitalships.Value > game.planets[game.IDofCurrentPlanet].CapitalShip)
                {
                    tooManyShips = true;
                    ResetUpDowns();
                    labelWarning.Text = "Not enough capital ships!";
                    labelWarning.Visible = true;
                }

                // Check to see if planet is neutral, or if planet ID is same as current user ID, makes sure user selected at least 1 ship
                if (game.planets[(int)comboBoxMoveTo.SelectedValue - 1].OwnedByUserID == 0 && !allUpDownsZero && !tooManyShips
                    || game.WhichPlayersTurn == game.planets[(int)comboBoxMoveTo.SelectedValue - 1].OwnedByUserID && !allUpDownsZero && !tooManyShips)
                {
                    canMove = true; 
                }

                // Otherwise start a battle
                else if (game.planets[(int)comboBoxMoveTo.SelectedValue - 1].OwnedByUserID != 0
                         && game.WhichPlayersTurn != game.planets[(int)comboBoxMoveTo.SelectedValue - 1].OwnedByUserID
                         && !allUpDownsZero && !tooManyShips)
                     {
                        // Assign these shorter variable names to make the constructor easier to understand
                        int attacker_ID = game.WhichPlayersTurn; // ID of the attacking player
                        int attacker_planet_ID = game.IDofCurrentPlanet; // Planet ID of attacking player

                        int attackFrig = (int)numericMoveFrigates.Value; // Attack units
                        int attackBatt = (int)numericMoveBattleships.Value;
                        int attackCapi = (int)numericMoveCapitalships.Value;

                        int defenseSSM = game.planets[(int)comboBoxMoveTo.SelectedValue - 1].SSMSite; // Defense units
                        int defenseFrig = game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CombatFrigate; 
                        int defenseBatt = game.planets[(int)comboBoxMoveTo.SelectedValue - 1].BattleShip;
                        int defenseCapi = game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CapitalShip;

                        // Team colors
                        Color offense, defense;

                        // If player 1 is attacker
                        if (game.WhichPlayersTurn == 1)
                        {
                            offense = game.player1Color;
                            defense = game.player2Color;
                        }
                        else // Player 2 is attacker
                        {
                            offense = game.player2Color;
                            defense = game.player1Color;
                        }

                        // Make a battle object that will maintain the state of both fleets after a skirmish
                        BattleStatus battle = new BattleStatus(attackFrig, attackBatt, attackCapi, defenseSSM, defenseFrig, defenseBatt, defenseCapi);

                        // Create a CombatSim object and simulate the battle, pass in the status of both fleets with object 'battle'
                        CombatSim sim = new CombatSim(attacker_ID, battle, offense, defense); 

                        // Now grab the results of the skirmish out of the CombatSim object
                        // Update the respective planets according to the state found in 'sim'
                        if (sim.DefenderWon)
                        {
                            // --- Update fleet sizes
                            // Defending planet fleet
                            game.planets[(int)comboBoxMoveTo.SelectedValue - 1].SSMSite = sim.DefenseSSM;
                            game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CombatFrigate = sim.DefenseFrig;
                            game.planets[(int)comboBoxMoveTo.SelectedValue - 1].BattleShip = sim.DefenseBatt;
                            game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CapitalShip = sim.DefenseCapi;

                            // Invading planet fleet
                            game.planets[attacker_planet_ID].CombatFrigate = 0;
                            game.planets[attacker_planet_ID].BattleShip = 0;
                            game.planets[attacker_planet_ID].CapitalShip = 0;

                            // Reset up-downs
                            ResetUpDowns();

                            // End turn and switch to next player
                            EndSwitchTurns();

                            // Update current window after move operation
                            UpdatePlanetStatsBox(game.IDofCurrentPlanet);
                        }
                        if (sim.InvaderWon)
                        {
                            // Set the defending planet fleet size equal to the invader's fleet to move ships over
                            game.planets[(int)comboBoxMoveTo.SelectedValue - 1].SSMSite = 0;
                            game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CombatFrigate = sim.AttackFrig; 
                            game.planets[(int)comboBoxMoveTo.SelectedValue - 1].BattleShip = sim.AttackBatt;
                            game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CapitalShip = sim.AttackCapi;

                            // Update the invaders planet to reflect the ships that have left and moved on to the new planet
                            game.planets[attacker_planet_ID].CombatFrigate -= (int)numericMoveFrigates.Value;
                            game.planets[attacker_planet_ID].BattleShip -= (int)numericMoveBattleships.Value;
                            game.planets[attacker_planet_ID].CapitalShip -= (int)numericMoveCapitalships.Value;

                            // Update flag color and owner ID
                            if (game.WhichPlayersTurn == 1)
                            {
                                flagBoxes[(int)comboBoxMoveTo.SelectedValue - 1].BackColor = game.player1Color;       // Change flag color
                                game.planets[(int)comboBoxMoveTo.SelectedValue - 1].OwnedByUserID = 1;                // Change planet owner ID
                                game.player1Mines += game.planets[(int)comboBoxMoveTo.SelectedValue - 1].MinesActive; // Update how many mines are owned
                                game.player2Mines -= game.planets[(int)comboBoxMoveTo.SelectedValue - 1].MinesActive; // Update how many mines are owned
                            }
                            if (game.WhichPlayersTurn == 2)
                            {
                                flagBoxes[(int)comboBoxMoveTo.SelectedValue - 1].BackColor = game.player2Color;       // Change flag color
                                game.planets[(int)comboBoxMoveTo.SelectedValue - 1].OwnedByUserID = 2;                // Change planet owner ID
                                game.player2Mines += game.planets[(int)comboBoxMoveTo.SelectedValue - 1].MinesActive; // Update how many mines are owned
                                game.player1Mines -= game.planets[(int)comboBoxMoveTo.SelectedValue - 1].MinesActive; // Update how many mines are owned
                            }

                            // Reset up-downs
                            ResetUpDowns();

                            // End turn and switch to next player
                            EndSwitchTurns();

                            // Update current window after move operation
                            UpdatePlanetStatsBox(game.IDofCurrentPlanet);
                        }
                        if (sim.RetreatOrTie)
                        {
                             // --- Update fleet sizes
                             // Defending planet fleet
                             game.planets[(int)comboBoxMoveTo.SelectedValue - 1].SSMSite = sim.DefenseSSM; 
                             game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CombatFrigate = sim.DefenseFrig;
                             game.planets[(int)comboBoxMoveTo.SelectedValue - 1].BattleShip = sim.DefenseBatt;
                             game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CapitalShip = sim.DefenseCapi;

                             // Invading planet fleet
                             game.planets[attacker_planet_ID].CombatFrigate = sim.AttackFrig; 
                             game.planets[attacker_planet_ID].BattleShip = sim.AttackBatt;
                             game.planets[attacker_planet_ID].CapitalShip = sim.AttackCapi;

                            // Reset up-downs
                            ResetUpDowns();

                            // End turn and switch to next player
                            EndSwitchTurns();

                            // Update current window after move operation
                            UpdatePlanetStatsBox(game.IDofCurrentPlanet);
                        }

                        // Create a victory condition based on if either player owns 0 of the 8 planets
                        int player1control = 0;
                        int player2control = 0;

                        // Loop through to check all planets
                        for (int x = 0; x<game.planets.Length; x++)
                        {
                            if (game.planets[x].OwnedByUserID == 1)
                                player1control++;

                            else if (game.planets[x].OwnedByUserID == 2)
                                player2control++;
                        }

                        // Now check to see if either player has been defeated
                        if (player1control == 0)
                            MessageBox.Show(game.player2Name + " is victorious!", "You should close the game now!");

                        else if (player2control == 0)
                            MessageBox.Show(game.player1Name + " is victorious!", "You should close the game now!");
                }

                return canMove;
            }

            // Perform the move
            void PerformMove()
            {
                // Check if there are enough ships to perform requested move
                if (CanMove())
                {
                    // Update flag color
                    if (game.WhichPlayersTurn == 1)
                    {
                        flagBoxes[(int)comboBoxMoveTo.SelectedValue - 1].BackColor = game.player1Color;
                        game.planets[(int)comboBoxMoveTo.SelectedValue - 1].OwnedByUserID = 1;
                    }
                    if (game.WhichPlayersTurn == 2)
                    {
                        flagBoxes[(int)comboBoxMoveTo.SelectedValue - 1].BackColor = game.player2Color;
                        game.planets[(int)comboBoxMoveTo.SelectedValue - 1].OwnedByUserID = 2;
                    }

                    // Use the currently selected value of planet in the combobox to access correct planet index
                    // Add frigates to selected planet
                    game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CombatFrigate += (int)numericMoveFrigates.Value;
                    game.planets[game.IDofCurrentPlanet].CombatFrigate -= (int)numericMoveFrigates.Value;

                    // Add battleships to selected planet
                    game.planets[(int)comboBoxMoveTo.SelectedValue - 1].BattleShip += (int)numericMoveBattleships.Value;
                    game.planets[game.IDofCurrentPlanet].BattleShip -= (int)numericMoveBattleships.Value;

                    // Add capital ships to selected planet
                    game.planets[(int)comboBoxMoveTo.SelectedValue - 1].CapitalShip += (int)numericMoveCapitalships.Value;
                    game.planets[game.IDofCurrentPlanet].CapitalShip -= (int)numericMoveCapitalships.Value;

                    // Reset up-downs
                    ResetUpDowns();

                    // End turn and switch to next player
                    EndSwitchTurns();

                    // Update current window after move operation
                    UpdatePlanetStatsBox(game.IDofCurrentPlanet);
                }
            }
        }
    }
}
