using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    public abstract class Level
    {
        protected Surface video;
        public Level(Surface video)
        {
            this.video = video;
        }

        protected int[,] intTileArray;

        protected Ground[,] groundTileArray;
        protected Platform[,] platformTileArray;
        protected Spikes[,] spikesTileArray;

        protected abstract void CreateWorld();      
    
        virtual public void DrawWorld()
        {          
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    if (groundTileArray[i, j] != null)
                    {
                        groundTileArray[i, j].Draw();
                    }
                    if (platformTileArray[i, j] != null)
                    {
                        platformTileArray[i, j].Draw();
                    }
                    if (spikesTileArray[i, j] != null)
                    {
                        spikesTileArray[i, j].Draw();
                    }     
                }
            }
        }
    }
}
