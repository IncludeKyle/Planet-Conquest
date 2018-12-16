using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Planet_Conquest
{
    public class Game
    {
        // 8 planet array
        public Planet[] planets;
        public int IDofCurrentPlanet;

        // Variables for both players
        public string player1Name, player2Name;
        public Color player1Color, player2Color;
        public int player1Resources, player2Resources;
        public int player1Mines, player2Mines;

        // ID of whose turn it is
        public int WhichPlayersTurn { get; set; }

        public Game()
        {
            // Get all of the user input data out of Form2
            using (var form2 = new Form2())
            {
                var result = form2.ShowDialog();

                // Values from form are preserved after close
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Get usernames from form
                    player1Name = form2.Username1;            
                    player2Name = form2.Username2;

                    // Get starting resources
                    player1Resources = form2.StartingResources;
                    player2Resources = form2.StartingResources;

                    // Get player colors
                    player1Color = form2.player1color;
                    player2Color = form2.player2color;
                }
            }

            // Loads each planet into the planet array
            LoadPlanetsArray();

            WhichPlayersTurn = 1; // Default starting turn to player #1
        }

        // Advances game from current players turn to next players turn, reverses currentID 
        public void SetWhoseTurn(int currentTurnID)
        {
            if (currentTurnID == 1)
            {
                WhichPlayersTurn = 2;
            }
            else
            {
                WhichPlayersTurn = 1;
            }
        }

        // Load each planet into the planet array with a unique random seed (0-7)
        private void LoadPlanetsArray()
        {
            var generatedNumbers = new List<int>();
            bool stillGenerating = true;

            // Loops until 8 unique random seed numbers have been generated to set each planet spawn point
            while (stillGenerating)
            {
                int randomInt = RandomIntRange(0, 7);      // Generate a random number

                if (!generatedNumbers.Contains(randomInt)) // If list does not already contain that number
                {
                    generatedNumbers.Add(randomInt);       // Then add that number to the list
                }
                // Once list has 8 elements, break from the loop
                if (generatedNumbers.Count == 8) 
                {
                    stillGenerating = false;
                }
            }

            // Convert list to array
            int[] seed = generatedNumbers.ToArray();

            // Create 8 random numbers of mining sites between 1-4, max limit of 2 occurances
            var generatedMines = new List<int>();
            stillGenerating = true;
            int Mine1 = 0;
            int Mine2 = 0;
            int Mine3 = 0;
            int Mine4 = 0;
            bool Mine1Limit = false;
            bool Mine2Limit = false;
            bool Mine3Limit = false;
            bool Mine4Limit = false;

            while (stillGenerating)
            {
                int randomInt = RandomIntRange(1, 4); // Generate a random number

                // Add number to list if that number hasn't already occured twice
                if (randomInt == 1 && !Mine1Limit)
                    generatedMines.Add(randomInt);
                if (randomInt == 2 && !Mine2Limit)
                    generatedMines.Add(randomInt);
                if (randomInt == 3 && !Mine3Limit)
                    generatedMines.Add(randomInt);
                if (randomInt == 4 && !Mine4Limit)
                    generatedMines.Add(randomInt);

                // Filter the random int number to see if it should be added
                if (randomInt == 1)
                {
                    Mine1++;
                    if (Mine1 == 2)
                    {
                        Mine1Limit = true;
                    }
                }
                if (randomInt == 2)
                {
                    Mine2++;
                    if (Mine2 == 2)
                    {
                        Mine2Limit = true;
                    }
                }
                if (randomInt == 3)
                {
                    Mine3++;
                    if (Mine3 == 2)
                    {
                        Mine3Limit = true;
                    }
                }
                if (randomInt == 4)
                {
                    Mine4++;
                    if (Mine4 == 2)
                    {
                        Mine4Limit = true;
                    }
                }

                // Once list has 8 elements, break from the loop
                if (generatedMines.Count == 8)
                {
                    stillGenerating = false;
                }
            }

            // Convert list to array
            int[] mines = generatedMines.ToArray();

            // Construct all of the planets
            Planet planet1 = new Planet(1, 0, seed[0], mines[0]);
            Planet planet2 = new Planet(2, 1, seed[1], mines[7]);
            Planet planet3 = new Planet(3, 2, seed[2], mines[2]);
            Planet planet4 = new Planet(4, 3, seed[3], mines[6]);
            Planet planet5 = new Planet(5, 4, seed[4], mines[4]);
            Planet planet6 = new Planet(6, 3, seed[5], mines[1]);
            Planet planet7 = new Planet(7, 2, seed[6], mines[3]);
            Planet planet8 = new Planet(8, 1, seed[7], mines[5]);

            // Make an array to hold them
            planets = new Planet[8];

            // Load them into the array
            planets[0] = planet1;
            planets[1] = planet2;
            planets[2] = planet3;
            planets[3] = planet4;
            planets[4] = planet5;
            planets[5] = planet6;
            planets[6] = planet7;
            planets[7] = planet8;
        }

        // Returns a random int between int min (inclusive) and int max (inclusive)
        private int RandomIntRange(int min, int max)
        {
            max += 1; // Makes the max inclusive
            Random randomNumber = new Random();
            int randomInt = randomNumber.Next(min, max);
            return randomInt;
        }
    }
}
