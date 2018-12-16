using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Planet_Conquest
{
    public class Planet
    {
        // Variables intended to change during gameplay
        private int ownedByUserID;      // User ID of player who currently owns this planet
        public int OwnedByUserID
        {
            get { return ownedByUserID; }
            set { ownedByUserID = value; }
        }

        private int minesActive = 0;    // Mines actively gathering resources, can't exceed minesAvailable
        public int MinesActive
        {
            get { return minesActive; }
            set { minesActive = value; }
        }

        private int ssmSite = 0;        // Surface to space missile sites owned by player on this planet
        public int SSMSite
        {
            get { return ssmSite; }
            set { ssmSite = value; }
        }

        private int combatFrigate = 0;  // Combat frigates owned by player on this planet
        public int CombatFrigate
        {
            get { return combatFrigate; }
            set { combatFrigate = value; }
        }

        private int battleShip = 0;     // Battle ships owned by player on this planet
        public int BattleShip
        {
            get { return battleShip; }
            set { battleShip = value; }
        }

        private int capitalShip = 0;    // Captial ships owned by player on this planet
        public int CapitalShip
        {
            get { return capitalShip; }
            set { capitalShip = value; }
        }

        // Read only variables - never change
        public readonly string planetName;
        public readonly int minesAvailable;
        public readonly int position;
        private readonly int relativePosition; // --- Useless, delete this variable and its dependancies later
        
        // Image array for the planet images
        private Image[] planetImages = new Image[8];
        public Image planetImage;

        // Corrosponding name array for the respective planets
        private readonly string[] planetNames = { "Blueball", "Ol' Dusty", "Tierra", "Wookland", "Reach",
                                                  "Cheese Wheel", "Lil' Beanland", "Rusty Moon"};


        // Constructor
        public Planet(int setPosition, int setRelativePosition, int load, int setMines)
        {
            position = setPosition;                 // Position acts as a unique planet ID
            relativePosition = setRelativePosition; // Sets how many planets away from the first planet [Planet 0] another planet is
            LoadImageArray();                       // Loads the 8 images in parallel to the name array 
            minesAvailable = setMines;  // (Assigns a planet between 1-4 mines)

            if (setPosition != 1 && setPosition != 5)
                OwnedByUserID = 0;          // Set planet to "Neutral" ID of 0
            else if (setPosition == 1)      // Player #1
                OwnedByUserID = 1;
            else if (setPosition == 5)      // Player #2
                OwnedByUserID = 2;

            planetImage = planetImages[load];
            planetName = planetNames[load];
        }
        

        // Loads the image array with a picture for each planet
        private void LoadImageArray()
        {
            planetImages[0] = Properties.Resources.blue;
            planetImages[1] = Properties.Resources.dusty;
            planetImages[2] = Properties.Resources.earth;
            planetImages[3] = Properties.Resources.jupiter;
            planetImages[4] = Properties.Resources.mercury;
            planetImages[5] = Properties.Resources.moon;
            planetImages[6] = Properties.Resources.red;
            planetImages[7] = Properties.Resources.rusty;
        }
    }
}
