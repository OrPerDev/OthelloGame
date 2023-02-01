namespace Othello.Logic
{
    internal class OthelloDisk
    {
        private readonly DiskPosition m_Position;
        private eDiskColor m_Color;

        // Constractors
        internal OthelloDisk(eDiskColor i_DiskColor, int i_Col, int i_Row)
        {
            this.m_Position = new DiskPosition(i_Row, i_Col);
            this.m_Color = i_DiskColor;
        }

        // Properties
        internal eDiskColor DiskColor
        {
            get
            {
                return this.m_Color;
            }

            set
            {
                this.m_Color = value;
            }
        }

        internal DiskPosition Position
        {
            get
            {
                return this.m_Position;
            }
        }
    }
}
