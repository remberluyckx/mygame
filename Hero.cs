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
   public class Hero : BeweegbaarObject
    {               
        private Rectangle visibleRectangle;
        private int yVelocity;
        public bool left;
        public bool right;
        public bool down;
        public bool jump;
        public bool shoot;
        public int health;
        public bool immune;
        public bool dead;
        public Point lvl2Position;
        SdlDotNet.Input.Key links, rechts, spatie;
        SdlDotNet.Input.MouseButton leftClick;
        public string facingDirection = "right";
        public int force = 20;
        private int delay = 0;
        public Hero(Surface video, SdlDotNet.Input.Key links, SdlDotNet.Input.Key rechts, SdlDotNet.Input.Key spatie, SdlDotNet.Input.MouseButton leftClick):base(video) //constructor van superklasse aanspreken met base
        {
            image_right = new Surface("robot_walk_right.png");
            image_left = new Surface("robot_walk_left.png");            
            visibleRectangle = new Rectangle(position.X, position.Y, 46, 68);
            position = new Point(5, 398);
            colRectangle = new Rectangle(position.X, position.Y, 46, 70);
            xVelocity = 4;
            yVelocity = 5;
            lvl2Position = new Point(0, 100);
            this.links = links;
            this.rechts = rechts;
            this.spatie = spatie;
            this.leftClick = leftClick;
            down = true;
            health = 3;
            immune = false;
            dead = false;
            Events.KeyboardDown += Events_KeyboardDown;
            Events.KeyboardUp += Events_KeyboardUp;
            Events.MouseButtonDown += Events_MouseButtonDown;
            Events.MouseButtonUp += Events_MouseButtonUp;
        }

        void Events_MouseButtonUp(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
        {
            shoot = false;
        }

        void Events_MouseButtonDown(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonPressed)
            {                
                shoot = true;             
            }
        }

        private void Events_KeyboardUp(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (jump)
            {
                if (e.Key == links)
                    left = false;
                if (e.Key == rechts)
                    right = false;
            }           
            else
            {
                left = right = false;
            }               
        }

        private void Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (e.Key == links)
            {
                left = true;
                facingDirection = "left";
            }                
            if (e.Key == rechts)
            {
                right = true;
                facingDirection = "right";
            }                   
            if (e.Key == spatie && down == false && jump == false) 
            {
                jump = true;
                
            }           
        }

        public override void Update() {
            ScreenBorderCollision();

            if (left) 
                position.X -= xVelocity;
            if (right)
                position.X += xVelocity;            
            if (down)
            {
                visibleRectangle.X = 0;
                position.Y += yVelocity;
            }
            if (jump)
            {               
                    if (force >= 0)
                    {
                        position.Y -= force;
                        force--;
                    }
                    else
                    {                       
                        jump = false;
                        force = 20;
                    }         
            }
            
            if (left || right)
            {
                if (down || jump) // to prevent walking animation in air (when falling/jumping)
                {
                    //do nothing
                }
                else // if left || right
                {
                    delay++;
                    if (delay == 5)
                    {
                        visibleRectangle.X += 50;
                        delay = 0;
                    }
                }                               
                if (visibleRectangle.X >= 250)
                    visibleRectangle.X = 0;                
            }

            colRectangle.X = position.X;
            colRectangle.Y = position.Y;
        }

        public override void Draw() {
            if (facingDirection == "right") 
                video.Blit(image_right, position, visibleRectangle);
            if (facingDirection == "left")
                video.Blit(image_left, position, visibleRectangle);                 
        }
                       
        public void ScreenBorderCollision()
        {
            if (colRectangle.Top < 0)
            {
                jump = false;
                force = 20;
            }
            if (colRectangle.Left < 0)
                left = false;
            if (colRectangle.Bottom > 494)
                down = false;
            if (colRectangle.Right > 792)
                right = false;
        }
        
    }
}
