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
    class Endscreen : Level
    {
        private Surface thanksMessage, exitMessage;
        private Point thanksPosition, exitPosition;
        SdlDotNet.Input.Key escape;
        public bool keypress;
        SdlDotNet.Graphics.Font fontBig = new SdlDotNet.Graphics.Font(@"Xcelsion Italic.ttf", 35);
        SdlDotNet.Graphics.Font fontSmall = new SdlDotNet.Graphics.Font(@"Xcelsion Italic.ttf", 20);

        public Endscreen(Surface video) : base(video)
        {
            CreateWorld();
            keypress = false;
            thanksMessage = fontBig.Render("thanks for playing", Color.CornflowerBlue);
            exitMessage = fontSmall.Render("press escape to exit the game", Color.CornflowerBlue);
            escape = SdlDotNet.Input.Key.Escape;
            Events.KeyboardDown += Events_KeyboardDown;
        }

        void Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (e.Key == escape)
            keypress = true;            
        }
        protected override void CreateWorld()
        {
            thanksPosition = new Point(50, 150);
            exitPosition = new Point(250, 250);           
        }
        public override void DrawWorld()
        {
            video.Fill(Color.DarkBlue);

            video.Blit(thanksMessage, thanksPosition);

            video.Blit(exitMessage, exitPosition);           
        }
    }
}
