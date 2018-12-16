using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planet_Conquest
{
    public class Build_Move
    {
        // Unit build costs
        public readonly int MINE_COST;
        public readonly int SSM_COST;

        public readonly int FRIGATE_COST;
        public readonly int BATTLESHIP_COST;
        public readonly int CAPITALSHIP_COST;

        // Total cost added up on the build menu
        public int totalCost;

        // Number of items requested to build
        public int minesToBuild;
        public int SSMsToBuild;
        public int frigatesToBuild;
        public int battlesToBuild;
        public int capitalsToBuild;

        // Mine harvest amount
        public readonly int HARVEST_AMOUNT;
        

        // Constructor
        public Build_Move()
        {
            MINE_COST = 1500;
            SSM_COST = 1000;

            FRIGATE_COST = 750;
            BATTLESHIP_COST = 2700;
            CAPITALSHIP_COST = 9720;

            HARVEST_AMOUNT = 750;
        }

        // Purge the number of requested items to build to 0
        public void ResetItemAmounts()
        {
            minesToBuild = 0;
            SSMsToBuild = 0;

            frigatesToBuild = 0;
            battlesToBuild = 0;
            capitalsToBuild = 0;

            totalCost = 0;
        }
    }
}
