using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    class Lasers
    {
        List<Laser> laserList = new List<Laser>();

        public void Add(Laser laser)
        {
            laserList.Add(laser);
        }
        public void Remove(Laser laser)
        {
            laserList.Remove(laser);
        }
        public void CheckHitPlatform(Rectangle platform)
        {
            for (int i = 0; i < laserList.Count; i++)
            {
                laserList[i].CheckHitPlatform(platform);
            }
        }
        public void CheckHitEnemy(Vijand enemy)
        {
            for (int i = 0; i < laserList.Count; i++)
            {
                laserList[i].CheckHitEnemy(enemy);
            }
        }
        public void CheckHitSpikes(Rectangle spikes)
        {
            for (int i = 0; i < laserList.Count; i++)
            {
                laserList[i].CheckHitSpikes(spikes);
            }
        }
        public void Draw()
        {
            for (int i = 0; i < laserList.Count; i++)
            {
                laserList[i].Draw();
            }
        }
        public void UpdateRight()
        {
            for (int i = 0; i < laserList.Count; i++)
            {
                laserList[i].UpdateRight();
            }
        }
        public void UpdateLeft()
        {
            for (int i = 0; i < laserList.Count; i++)
            {
                laserList[i].UpdateLeft();
            }
        }
    }
}
