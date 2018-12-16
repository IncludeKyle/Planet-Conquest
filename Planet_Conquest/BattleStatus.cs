using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planet_Conquest
{
    // Create an object of this CombatSim class to send to Form3 so it can display the status of all ships
    public class BattleStatus
    {
        // ----- Public instances -----

        // Attack units (quantity)
        public int AttackFrig { get; set; }
        public int AttackBatt { get; set; }
        public int AttackCapi { get; set; }

        // Defense units (quantity)
        public int DefenseSSM { get; set; }
        public int DefenseFrig { get; set; }
        public int DefenseBatt { get; set; }
        public int DefenseCapi { get; set; }

        // Attack units (HP)
        public int AttackFrigHP { get; set; }
        public int AttackBattHP { get; set; }
        public int AttackCapiHP { get; set; }

        // Defense units (HP)
        public int DefenseSSMHP { get; set; }
        public int DefenseFrigHP { get; set; }
        public int DefenseBattHP { get; set; }
        public int DefenseCapiHP { get; set; }

        // Fleet HP
        public int AttackFleetHP { get; set; }
        public int DefenseFleetHP { get; set; }

        // Victory condition bool
        public bool battleFinished;

        // Determines who won the fight, or if there was a stalemate, or retreat
        public bool InvaderWon { get; set; }
        public bool DefenderWon { get; set; }
        public bool RetreatOrTie { get; set; }

        // ----- Private instances -----

        // Random number object
        private Random randomNumber;

        // HP stats
        private int SSM_HP { get; set; }
        private int Frigate_HP { get; set; }
        private int Battleship_HP { get; set; }
        private int Capitalship_HP { get; set; }

        // DMG lower limit
        private int SSM_DMG_min { get; set; }
        private int Frigate_DMG_min { get; set; }
        private int Battleship_DMG_min { get; set; }
        private int Capitalship_DMG_min { get; set; }

        // DMG upper limit
        private int SSM_DMG_max { get; set; }
        private int Frigate_DMG_max { get; set; }
        private int Battleship_DMG_max { get; set; }
        private int Capitalship_DMG_max { get; set; }

        // String to store result of combat
        public string CombatMessage { get; set; }
        public string InvaderMessage { get; set; }
        public string DefenderMessage { get; set; }

        // Arrays to store both fleets and fleet hp in
        private int[] Invading_Fleet;  // Elements 0-2, frigate, battle, capital
        private int[] Defending_Fleet; // Elements 0-3, frigate, battle, capital, SSM
        private int[] Invading_Fleet_HP; 
        private int[] Defending_Fleet_HP;
        private int[] UnitTypeHPStat; // Stores the amount of HP for each type of unit

        // Constructor
        public BattleStatus(int attackFrigs, int attackBatts, int attackCapis,
                            int defenseSSM, int defenseFrigs, int defenseBatts, int defenseCapis)
        {
            // Initialize victory achieved bool to false
            battleFinished = false;

            // Instantiate the random number object
            randomNumber = new Random();

            // Save the quantity of all attack and defense units
            AttackFrig = attackFrigs;
            AttackBatt = attackBatts;
            AttackCapi = attackCapis;

            DefenseSSM = defenseSSM;
            DefenseFrig = defenseFrigs;
            DefenseBatt = defenseBatts;
            DefenseCapi = defenseCapis;

            // Set HP for each unit type
            SSM_HP = 150;
            Frigate_HP = 100;
            Battleship_HP = 325;
            Capitalship_HP = 1500;

            // Calculate total HP
            AttackFrigHP = AttackFrig * Frigate_HP;
            AttackBattHP = AttackBatt * Battleship_HP;
            AttackCapiHP = AttackCapi * Capitalship_HP;
            DefenseSSMHP = DefenseSSM * SSM_HP;
            DefenseFrigHP = DefenseFrig * Frigate_HP;
            DefenseBattHP = DefenseBatt * Battleship_HP;
            DefenseCapiHP = DefenseCapi * Capitalship_HP;

            // Set fleet total HP
            AttackFleetHP = AttackFrigHP + AttackBattHP + AttackCapiHP;
            DefenseFleetHP = DefenseSSMHP + DefenseFrigHP + DefenseBattHP + DefenseCapiHP;

            // Set damage lower limit for each unit
            SSM_DMG_min = 90;          // 90-135 DMG
            Frigate_DMG_min = 45;      // 45-90 DMG
            Battleship_DMG_min = 200;  // 200-325 DMG
            Capitalship_DMG_min = 400; // 400-1000 DMG

            // Set damage upper limit for each unit
            SSM_DMG_max = 135;
            Frigate_DMG_max = 90;
            Battleship_DMG_max = 325;
            Capitalship_DMG_max = 1000;

            // Load the fleet quantity and HP arrays
            LoadFleetArray();

            // Set the initial invader and defender messages
            SetCombatMessages();
        }

        // Load the fleet arrays
        private void LoadFleetArray()                           
        {
            Invading_Fleet = new int[3];  // Elements 0-2, frigate, battle, capital
            Defending_Fleet = new int[4]; // Elements 0-3, SSM, frigate, battle, capital
            Invading_Fleet_HP = new int[3];
            Defending_Fleet_HP = new int[4];
            UnitTypeHPStat = new int[4];

            // Load in the HP amounts for any given unit type
            UnitTypeHPStat[0] = Frigate_HP;
            UnitTypeHPStat[1] = Battleship_HP;
            UnitTypeHPStat[2] = Capitalship_HP;
            UnitTypeHPStat[3] = SSM_HP;

            // Load in invading unit quantities
            Invading_Fleet[0] = AttackFrig;
            Invading_Fleet[1] = AttackBatt;
            Invading_Fleet[2] = AttackCapi;

            // Load in defending unit quantities
            Defending_Fleet[0] = DefenseFrig;
            Defending_Fleet[1] = DefenseBatt;
            Defending_Fleet[2] = DefenseCapi;
            Defending_Fleet[3] = DefenseSSM;

            // Load in total HP for each invading unit type
            Invading_Fleet_HP[0] = AttackFrigHP; 
            Invading_Fleet_HP[1] = AttackBattHP;
            Invading_Fleet_HP[2] = AttackCapiHP;

            // Load in total HP for each defending unit type
            Defending_Fleet_HP[0] = DefenseFrigHP;
            Defending_Fleet_HP[1] = DefenseBattHP;
            Defending_Fleet_HP[2] = DefenseCapiHP;
            Defending_Fleet_HP[3] = DefenseSSMHP;
        }

        // Update fleet array after combat
        private void UpdateFleetFromArray()
        {
            // Update invading fleet
            AttackFrig = Invading_Fleet[0];
            AttackBatt = Invading_Fleet[1];
            AttackCapi = Invading_Fleet[2];

            // Update defending fleet
            DefenseFrig = Defending_Fleet[0];
            DefenseBatt = Defending_Fleet[1];
            DefenseCapi = Defending_Fleet[2];
            DefenseSSM = Defending_Fleet[3];
        }

        // Returns a random int between int min (inclusive) and int max (inclusive)
        private int RandomIntRange(int min, int max)
        {
            max += 1; // Makes the max inclusive
            int randomInt = randomNumber.Next(min, max);
            return randomInt;
        }

        // Method will invoke one round of a fight if called, altering the values within the respective 'battle' object of this class
        public void SimulateARound()
        {
            // Simulate total damage from offense
            CombatMessage = "Offense deals: \r\n";

            // Calculate total damage
            int Odamage1 = CalculateDamage(Invading_Fleet[0], Frigate_DMG_min, Frigate_DMG_max); 
            int Odamage2 = CalculateDamage(Invading_Fleet[1], Battleship_DMG_min, Battleship_DMG_max);
            int Odamage3 = CalculateDamage(Invading_Fleet[2], Capitalship_DMG_min, Capitalship_DMG_max);
            int Ototal = Odamage1 + Odamage2 + Odamage3;

            // Update the combat message string
            CombatMessage += "Frigate +[" + Odamage1 + "]\r\n";
            CombatMessage += "Battle  +[" + Odamage2 + "]\r\n";
            CombatMessage += "Capital +[" + Odamage3 + "]\r\n";
            CombatMessage += "------------------\r\n";
            CombatMessage += "[>" + Ototal + "<]\r\n\r\n";

            // Simulate total damage from defense
            CombatMessage += "Defense deals: \r\n";

            // Calculate total damage
            int Ddamage0 = CalculateDamage(Defending_Fleet[3], SSM_DMG_min, SSM_DMG_max);
            int Ddamage1 = CalculateDamage(Defending_Fleet[0], Frigate_DMG_min, Frigate_DMG_max);
            int Ddamage2 = CalculateDamage(Defending_Fleet[1], Battleship_DMG_min, Battleship_DMG_max);
            int Ddamage3 = CalculateDamage(Defending_Fleet[2], Capitalship_DMG_min, Capitalship_DMG_max);
            int Dtotal = Ddamage0 + Ddamage1 + Ddamage2 + Ddamage3;

            // Update the combat message string
            CombatMessage += "SSM     +[" + Ddamage0 + "]\r\n";
            CombatMessage += "Frigate +[" + Ddamage1 + "]\r\n";
            CombatMessage += "Battle  +[" + Ddamage2 + "]\r\n";
            CombatMessage += "Capital +[" + Ddamage3 + "]\r\n";
            CombatMessage += "------------------\r\n";
            CombatMessage += "[>" + Dtotal + "<]\r\n";

            // Deliver damage total to defending fleet
            DistributeDamageToDefenders(Ototal, Defending_Fleet, Defending_Fleet_HP);

            // Deliver damage total to invading fleet
            DistributeDamageToInvaders(Dtotal, Invading_Fleet, Invading_Fleet_HP);

            // Compare and update the offense and defense text boxes after casualties have been calculated
            SetCombatMessages();

            // Test for victory conditions
            if (DefenseFleetHP <= 0 && AttackFleetHP > 0) // Invaders won
            {
                battleFinished = true;
                InvaderWon = true;
                UpdateFleetFromArray(); // Update the public instances maintained in this class
            }
            else if (AttackFleetHP <= 0 && DefenseFleetHP > 0) // Defenders won
            {
                battleFinished = true;
                DefenderWon = true;
                UpdateFleetFromArray(); // Update the public instances maintained in this class
            }
            else if (AttackFleetHP <= 0 && DefenseFleetHP <= 0) // Stalemate
            {
                battleFinished = true;
                RetreatOrTie = true;
                UpdateFleetFromArray(); // Update the public instances maintained in this class
            }
        }

        // Updates the offense and defense text boxes after casualties have been calculated
        private void SetCombatMessages()
        {
            string textBoxOffense = ""; // Builds a large string of everything to display in the invader's textBox
            string textBoxDefense = ""; // Builds a large string of everything to display in the defender's textBox

            // Display offense player's units and HP
            if (Invading_Fleet[0] > 0)
            {
                textBoxOffense += "Frigates: " + Invading_Fleet[0] + "\r\n";
                textBoxOffense += "HP [" + Invading_Fleet_HP[0] + "]\r\n";
            }
            if (Invading_Fleet[1] > 0)
            {
                textBoxOffense += "Battleships: " + Invading_Fleet[1] + "\r\n";
                textBoxOffense += "HP [" + Invading_Fleet_HP[1] + "]\r\n";
            }
            if (Invading_Fleet[2] > 0)
            {
                textBoxOffense += "Capitalships: " + Invading_Fleet[2] + "\r\n";
                textBoxOffense += "HP [" + Invading_Fleet_HP[2] + "]\r\n";
            }

            // Display the invading force fleet HP
            textBoxOffense += "----------\r\n";
            textBoxOffense += "Fleet HP = [" + AttackFleetHP + "]\r\n";

            // Create the invading fleet's message after combat
            InvaderMessage = textBoxOffense; // Set the class level instance variable

            // Display defensive player's units and HP
            if (Defending_Fleet[3] > 0)
            {
                textBoxDefense = "SSM Sites: " + Defending_Fleet[3] + "\r\n";
                textBoxDefense += "HP [" + Defending_Fleet_HP[3] + "]\r\n";
            }
            if (Defending_Fleet[0] > 0)
            {
                textBoxDefense += "Frigates: " + Defending_Fleet[0] + "\r\n";
                textBoxDefense += "HP [" + Defending_Fleet_HP[0] + "]\r\n";
            }
            if (Defending_Fleet[1] > 0)
            {
                textBoxDefense += "Battleships: " + Defending_Fleet[1] + "\r\n";
                textBoxDefense += "HP [" + Defending_Fleet_HP[1] + "]\r\n";
            }
            if (Defending_Fleet[2] > 0)
            {
                textBoxDefense += "Capitalships: " + Defending_Fleet[2] + "\r\n";
                textBoxDefense += "HP [" + Defending_Fleet_HP[2] + "]\r\n";
            }

            // Display the defending force fleet HP
            textBoxDefense += "----------\r\n";
            textBoxDefense += "Fleet HP = [" + DefenseFleetHP + "]\r\n";

            // Create the defending fleet's message after combat
            DefenderMessage = textBoxDefense; // Set the class level instance variable
        }

        // Distribute damage and deaths to the defending planet
        private void DistributeDamageToDefenders(int totalDamage, int[] fleet, int[] fleet_HP)
        {
            // Loop through each defensive unit type
            for (int unitType = 0; unitType < fleet.Length; unitType++)
            {
                // Checks to see if damage is larger than that unit's type total HP:
                // if damage total is greater than unit's HP, it kills off all units in that class 
                // and then updates the remaining total damage to pass onto the next unit type
                if (totalDamage >= fleet_HP[unitType])
                {
                    totalDamage -= fleet_HP[unitType]; // Damage remaining is reduced by the HP
                    fleet_HP[unitType] = 0;            // Drop HP of unit type to 0
                    fleet[unitType] = 0;               // Quantity remaning in fleet is set to 0
                }
                else // If the damage total remaining is less than total HP of unit type
                {
                    fleet_HP[unitType] -= totalDamage; // Reduce unit's type HP by however much damage it receives

                    if (fleet_HP[unitType] < 0)
                        fleet_HP[unitType] = 0; // Prevent negative HP

                    totalDamage = 0; // Total damage is fully spent and set to 0

                    // Check to see if there is no remainder during division
                    int damagedShip;
                    if (fleet_HP[unitType] % UnitTypeHPStat[unitType] == 0)
                        damagedShip = 0; // If there is no remainder, there is no damaged ship to account for
                    else
                        damagedShip = 1; // There is a damaged ship of this type that needs to be factored in after the int division

                    // Calculate deaths, update ships remaining of that type in the fleet
                    fleet[unitType] = (int)((double)fleet_HP[unitType] / (double)UnitTypeHPStat[unitType]); // Finds the number of ships remaing of that type after taking damage 
                    fleet[unitType] += damagedShip;                                                         // Acount for a possible damaged ship
                }
            }// The for loop just modifies the local fleet[] and fleet_HP[], not the actual instance variables in the class

            // So now update the respective class instance arrays with the values in fleet[] and fleet_HP[]
            Array.Copy(fleet, Defending_Fleet, 4);
            Array.Copy(fleet_HP, Defending_Fleet_HP, 4); // Copy the local arrays calculated here out to the class variables

            // Reset the fleet HP to 0 before calculating total fleet HP
            DefenseFleetHP = 0;

            // Update total fleet HP 
            foreach (int hp in Defending_Fleet_HP)
                DefenseFleetHP += hp;
        }

        // Distribute damage and deaths to the invaders
        private void DistributeDamageToInvaders(int totalDamage, int[] fleet, int[] fleet_HP)
        {
            // Loop through each invading unit type
            for (int unitType = 0; unitType < fleet.Length; unitType++)
            {
                // Checks to see if damage is larger than that unit's type total HP:
                // if damage total is greater than unit's HP, it kills off all units in that class 
                // and then updates the remaining total damage to pass onto the next unit type
                if (totalDamage >= fleet_HP[unitType])
                {
                    totalDamage -= fleet_HP[unitType]; // Damage remaining is reduced by the HP
                    fleet_HP[unitType] = 0;            // Drop HP of unit type to 0
                    fleet[unitType] = 0;               // Quantity remaning in fleet is set to 0
                }
                else // If the damage total remaining is less than total HP of unit type
                {
                    fleet_HP[unitType] -= totalDamage; // Reduce unit's type HP by however much damage it receives

                    if (fleet_HP[unitType] < 0)
                        fleet_HP[unitType] = 0; // Prevent negative HP

                    totalDamage = 0; // Total damage is fully spent and set to 0

                    // Check to see if there is no remainder during division
                    int damagedShip;
                    if (fleet_HP[unitType] % UnitTypeHPStat[unitType] == 0)
                        damagedShip = 0; // If there is no remainder, there is no damaged ship to account for
                    else
                        damagedShip = 1; // There is a damaged ship of this type that needs to be factored in after the int division

                    // Calculate deaths, update ships remaining of that type in the fleet
                    fleet[unitType] = (int)((double)fleet_HP[unitType] / (double)UnitTypeHPStat[unitType]); // Finds the number of ships remaing of that type after taking damage
                    fleet[unitType] += damagedShip;                                                         // Acount for a possible damaged ship
                }
            } // The for loop just modifies the fleet[] and fleet_HP[], not the actual instance variables in the class

            // So now update the respective class instance arrays with the values in fleet[] and fleet_HP[]
            Array.Copy(fleet, Invading_Fleet, 3);
            Array.Copy(fleet_HP, Invading_Fleet_HP, 3);

            // Reset the fleet HP to 0 before calculating total fleet HP
            AttackFleetHP = 0;

            // Update total fleet HP
            foreach (int hp in Invading_Fleet_HP)
                AttackFleetHP += hp;
        }

        // Returns a damage calculation for a quantity of units, DMG is between (min-max)
        private int CalculateDamage(int unitAmount, int min, int max)
        {
            int damageTotal = 0; // Store total damage output from this class of unit

            // by looping through each unit and generating a damage value
            for (int unit = 0; unit < unitAmount; unit++)
                damageTotal += RandomIntRange(min, max);

            return damageTotal;
        }
    }
}
