﻿using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    public class Platform : NietBeweegbaarObject
    {
        public Platform (Surface video, Point position):base(video){
            this.position = position;
            this.video = video;            
            image = new Surface("platform.png");           
        }

        public override void Draw()
        {
            video.Blit(image, position);
        }
    }
}
