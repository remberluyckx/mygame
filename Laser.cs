using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    class Laser : BeweegbaarObject
    {
        public string direction;
        private Lasers lasers;
        public Laser (Surface video, Point position, Lasers lasers, string direction):base(video){
            this.position = position;
            this.video = video;
            this.direction = direction;
            image_right = new Surface("laserRay.png");
            xVelocity = 10;
            this.lasers = lasers;
            lasers.Add(this);            
            colRectangle = new Rectangle(position.X, position.Y, 15, 5);
        }

        public override void Draw()
        {
            video.Blit(image_right, position);
        }
        public override void Update()
        {
            
        }

        public void UpdateLeft()
        {
            if (direction == "right")
            {
                position.X += xVelocity;
                colRectangle.X = position.X;
            }           
        }
        public void UpdateRight()
        {
            if (direction == "left")
            {
                position.X -= xVelocity;
                colRectangle.X = position.X;
            }            
        }
        public void CheckHitPlatform(Rectangle platform)
        {
            if (colRectangle.IntersectsWith(platform))
            {
                lasers.Remove(this);                
            }
        }
        public void CheckHitEnemy(Vijand enemy)
        {                        
                if (colRectangle.IntersectsWith(enemy.colRectangle))
                {
                    lasers.Remove(this);
                    enemy.dead = true;
                }                           
        }
        public void CheckHitSpikes(Rectangle spikes)
        {
            if (colRectangle.IntersectsWith(spikes))
            {
                lasers.Remove(this);
            }
        }

    }
}
