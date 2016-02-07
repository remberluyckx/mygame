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
    class Startscreen : Level
    {
        private Surface Title, Controls1, Controls2, Controls3, Continue;
        private Point titlePosition, controls1Position, controls2Position, controls3Position, continuePosition;
        public bool keypress;
        SdlDotNet.Graphics.Font fontBig = new SdlDotNet.Graphics.Font(@"Xcelsion Italic.ttf", 35);
        SdlDotNet.Graphics.Font fontSmall = new SdlDotNet.Graphics.Font(@"Xcelsion Italic.ttf", 20);
            
        public Startscreen(Surface video) : base(video)
        {
            titlePosition = new Point(50, 150);
            controls1Position = new Point(250, 250);
            controls2Position = new Point(250, 300);
            controls3Position = new Point(250, 350);
            continuePosition = new Point(150, 400);
            keypress = false;
            Title = fontBig.Render("Alien Terrorist Killer", Color.CornflowerBlue);
            Controls1 = fontSmall.Render("left/right: Q/D", Color.CornflowerBlue);
            Controls2 = fontSmall.Render("jump: space", Color.CornflowerBlue);
            Controls3 = fontSmall.Render("shoot: mouse click", Color.CornflowerBlue);
            Continue = fontSmall.Render("PRESS ANY KEY TO CONTINUE", Color.CornflowerBlue);
            Events.KeyboardDown += Events_KeyboardDown;
        }

        void Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            keypress = true;
        }
        protected override void CreateWorld()
        {
            
        }
        public override void DrawWorld()
        {
            video.Fill(Color.DarkBlue);
            
            video.Blit(Title, titlePosition);
            
            video.Blit(Controls1, controls1Position);
            video.Blit(Controls2, controls2Position);
            video.Blit(Controls3, controls3Position);

            video.Blit(Continue, continuePosition);
        }
        
    }
}
