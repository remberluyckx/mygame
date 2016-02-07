using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    class Slimes
    {
        List<Slime> slimeList = new List<Slime>();

        public void Add(Slime slime)
        {
            slimeList.Add(slime);
        }
        public void Remove(Slime slime)
        {
            slimeList.Remove(slime);
        }
        public void CheckHitPlatform(Rectangle platform)
        {
            for (int i = 0; i < slimeList.Count; i++)
            {
                slimeList[i].CheckHitPlatform(platform);
            }
        }
        public void CheckHitHero(Hero hero)
        {
            for (int i = 0; i < slimeList.Count; i++)
            {
                slimeList[i].CheckHitHero(hero);
            }
        }
        public void CheckHitSpikes(Rectangle spikes)
        {
            for (int i = 0; i < slimeList.Count; i++)
            {
                slimeList[i].CheckHitSpikes(spikes);
            }
        }
        public void Draw()
        {
            for (int i = 0; i < slimeList.Count; i++)
            {
                slimeList[i].Draw();
            }
        }
        public void UpdateRight()
        {
            for (int i = 0; i < slimeList.Count; i++)
            {
                slimeList[i].UpdateRight();
            }
        }
        public void UpdateLeft()
        {
            for (int i = 0; i < slimeList.Count; i++)
            {
                slimeList[i].UpdateLeft();
            }
        }
    }
}
