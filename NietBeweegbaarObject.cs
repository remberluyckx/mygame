using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{
    public abstract class NietBeweegbaarObject
    {
        protected Point position;
        protected Surface video;
        protected Surface image;
        public Rectangle colRectangle;

        public NietBeweegbaarObject(Surface video)
        {
            this.video = video;
        }
        public abstract void Draw();
    }
}
