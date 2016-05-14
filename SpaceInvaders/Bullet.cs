using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Bullet
    {

        public readonly PictureBox Box;
        public readonly string Owner;

        public Point Location
        {
            get
            {
                return Box.Location;
            }

            set
            {
                Box.Location = value;
            }
        }

        public Bullet(PictureBox pbox, string owner)
        {
            this.Box = pbox;
            this.Owner = owner;
        }

        public bool IntersectsWith(Rectangle testBox)
        {
            return this.Box.Bounds.IntersectsWith(testBox);
        }
    }
}
