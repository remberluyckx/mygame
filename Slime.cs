using SdlDotNet.Audio;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    class Slime : BeweegbaarObject
    {
        public string direction;
        private Slimes slimes;
        Sound sndPain = new Sound("pain.wav");
        public Slime (Surface video, Point position, Slimes slimes, string direction):base(video){
            this.position = position;
            this.video = video;
            this.direction = direction;
            image_right = new Surface("slime.png");
            xVelocity = 10;
            this.slimes = slimes;
            slimes.Add(this);            
            colRectangle = new Rectangle(position.X, position.Y, 15, 5);
            sndPain.Volume = 15;
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
            if (direction == "left")
            {
                position.X -= xVelocity;
                colRectangle.X = position.X;
            }           
        }
        public void UpdateRight()
        {
            if (direction == "right")
            {
                position.X += xVelocity;
                colRectangle.X = position.X;
            }            
        }
        public void CheckHitPlatform(Rectangle platform)
        {
            if (colRectangle.IntersectsWith(platform))
            {
                slimes.Remove(this);                
            }
        }
        public void CheckHitSpikes(Rectangle spikes)
        {
            if (colRectangle.IntersectsWith(spikes))
            {
                slimes.Remove(this);
            }
        }
        public void CheckHitHero(Hero hero)
        {                        
                if (colRectangle.IntersectsWith(hero.colRectangle))
                {
                    slimes.Remove(this);
                    if (hero.immune == false)
                    {
                        hero.health--;
                        sndPain.Play();
                    }
                    
                    Console.WriteLine(hero.health);
                    if (hero.health == 0)
                        hero.dead = true;
                    hero.immune = true;
                }                           
        }
    }
}
