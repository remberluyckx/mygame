using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    class Vijand : BeweegbaarObject
    {
        
        private Rectangle visibleRectangle;
        public Rectangle sight;
        private int delay;
        public string direction;
        private int startPoint;
        private int endPoint;
        public bool dead;
        public Vijand(Surface video, Point position, int startPoint, int endPoint):base(video)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.position = position;
            xVelocity = 1;            
            image_right = new Surface("Enemy_right.gif");
            image_left = new Surface("Enemy_left.gif");
            colRectangle = new Rectangle(position.X, position.Y, 69, 57);
            direction = "right";            
            visibleRectangle = new Rectangle(5, 0, 69, 57);
            delay = 0;            
            dead = false;
        }
        
        public override void Draw()
        {
            if (direction == "right")
                video.Blit(image_right, position, visibleRectangle); 
            if (direction == "left")
                video.Blit(image_left, position, visibleRectangle); 
        }
        public override void Update()
        {           
            delay++;
            if (delay == 8)
            {
                visibleRectangle.X += 70;              
                delay = 0;
            }
            if (visibleRectangle.X >= 210)
            {
                if (direction == "right")
                visibleRectangle.X = 5;
                if (direction == "left")
                    visibleRectangle.X = 0;
            }
                

            if (position.X == endPoint)
            {
                direction = "left";
                visibleRectangle.X = 0;
            }

            if (position.X == startPoint)
            {
                direction = "right";
                visibleRectangle.X = 5;
            }
                
        
            if (direction == "right")
                position.X++;
            if (direction == "left")
                position.X--;

            colRectangle.X = position.X;
            colRectangle.Y = position.Y;

            if (direction == "right")
                sight = new Rectangle(position.X, position.Y, 300, 57);
            if (direction == "left")
                sight = new Rectangle(position.X - 300, position.Y, 300, 57);

        }
        
    
    }
}
