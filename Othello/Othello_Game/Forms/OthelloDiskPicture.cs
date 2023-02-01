using System.Windows.Forms;
using Othello.Logic;

namespace Othello.Forms
{
    internal class OthelloDiskPicture : PictureBox
    {
        // Members
        private readonly DiskPosition r_DiskPosition;

        // Constractors
        internal OthelloDiskPicture(int i_Row, int i_Col)
        {
            this.r_DiskPosition = new DiskPosition(i_Row, i_Col);
        }

        // Properties
        internal DiskPosition Position
        {
            get
            {
                return this.r_DiskPosition;
            }
        }
    }
}
