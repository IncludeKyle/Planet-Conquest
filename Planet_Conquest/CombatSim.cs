using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Planet_Conquest
{

    class CombatSim
    {

        // Public instance variables to return to Form1 with current state of both fleets after a skirmish
        public int AttackFrig { get; set; }
        public int AttackBatt { get; set; }
        public int AttackCapi { get; set; }

        // Defense units (quantity)
        public int DefenseSSM { get; set; }
        public int DefenseFrig { get; set; }
        public int DefenseBatt { get; set; }
        public int DefenseCapi { get; set; }

        // Determines who won the fight, or if there was a stalemate, or retreat
        public bool InvaderWon { get; set; }
        public bool DefenderWon { get; set; }
        public bool RetreatOrTie { get; set; }

        // Class level battle object
        private BattleStatus composed_battle;

        // Updates status of fleets after skirmish
        private void FleetStatus()
        {
            // Update the status of all units
            AttackFrig = composed_battle.AttackFrig;
            AttackBatt = composed_battle.AttackBatt;
            AttackCapi = composed_battle.AttackCapi;

            DefenseSSM = composed_battle.DefenseSSM;
            DefenseFrig = composed_battle.DefenseFrig;
            DefenseBatt = composed_battle.DefenseBatt;
            DefenseCapi = composed_battle.DefenseCapi;

            // Update who won, or stalemate
            InvaderWon = composed_battle.InvaderWon;
            DefenderWon = composed_battle.DefenderWon;
            RetreatOrTie = composed_battle.RetreatOrTie;
        }

        // Constructor
        public CombatSim(int attackerID, BattleStatus battle, Color colorAttack, Color colorDefense)
        {
            // Set for first call to Form3, it will be replaced during the combat loop
            string actionContent = "Press continue if you're sure you'd like to initiate combat!";
            InvaderWon = false;
            DefenderWon = false;
            RetreatOrTie = false;

            // Launch Form3 window
            using (var form3 = new Form3(actionContent, battle.InvaderMessage, battle.DefenderMessage, colorAttack, colorDefense))
            {
                bool fighting = true; // This bool later breaks user from the Form3 combat loop

                // Launch Form3, save the user's button click choice
                var result = form3.ShowDialog();

                // Perform first round of calculations if user clicks Continue button
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Continuosly respawn the combat menu with updated combat info while true
                    while (fighting)
                    {
                        // Run a round of the combat algorithm 
                        battle.SimulateARound();

                        // Report back with a string of the battle results
                        actionContent = battle.CombatMessage;

                        // Display the combat results by relaunching Form3
                        using (var form3relaunch = new Form3(actionContent, battle.InvaderMessage, battle.DefenderMessage, colorAttack, colorDefense))
                        {
                            // Launch and return user button selection again
                            var resultRelaunch = form3relaunch.ShowDialog();

                            // If a victory condition has been tripped
                            if (battle.battleFinished)
                            {
                                fighting = false; // Break from combat loop 

                                // Compose this class level object from the current state of the battle object 
                                composed_battle = battle;

                                FleetStatus(); // Update the state of both fleets
                            }

                            // Break from combat loop if user selected Retreat button
                            if (resultRelaunch == System.Windows.Forms.DialogResult.Cancel)
                            {
                                fighting = false; // Break from combat loop 

                                // Compose this class level object from the current state of the battle object 
                                composed_battle = battle;

                                composed_battle.RetreatOrTie = true; // Toggle retreat to true

                                FleetStatus(); // Update the state of both fleets
                            }
                        }
                    }
                }
            }
        }
        
    }

}
