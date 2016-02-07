using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    class HeartContainer : NietBeweegbaarObject
    {
        public HeartContainer (Surface video, Point position):base(video){
            this.position = position;
            this.video = video;            
            image = new Surface("flyingheart.png");
            colRectangle = new Rectangle(position.X, position.Y, 60, 42);
        }

        public override void Draw()
        {
            video.Blit(image, position);
        }
        
    }
}
