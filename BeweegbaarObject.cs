using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    public abstract class BeweegbaarObject
    {
        public Point position;
        protected int xVelocity;
        protected Surface video;
        protected Surface image_right;
        protected Surface image_left;
        public Rectangle colRectangle;

        public BeweegbaarObject(Surface video)
        {
            this.video = video;
        }

        public abstract void Update();
        public abstract void Draw();

    }
}
