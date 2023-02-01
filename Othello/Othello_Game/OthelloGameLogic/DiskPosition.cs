namespace Othello.Logic
{
    internal class DiskPosition
    {
        private int m_Row;
        private int m_Col;

        // Constractors
        internal DiskPosition(int i_Row = 0, int i_Col = 0)
        {
            this.m_Col = i_Col;
            this.m_Row = i_Row;
        }

        // Overrides and operators
        public override bool Equals(object i_DifferentObject)
        {
            bool areEquals;
            if (i_DifferentObject == null)
            {
                areEquals = false;
            }

            DiskPosition position = (DiskPosition)i_DifferentObject;
            areEquals = (this.Col == position.Col) && (this.Row == position.Row);

            return areEquals;
        }

        public static bool operator ==(DiskPosition i_Position, DiskPosition i_OtherPosition)
        {
            return i_Position.Equals(i_OtherPosition);
        }

        public static bool operator !=(DiskPosition i_Position, DiskPosition i_OtherPosition)
        {
            return !i_Position.Equals(i_OtherPosition);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // Properties
        internal int Row
        {
            get
            {
                return this.m_Row;
            }

            set
            {
                this.m_Row = value;
            }
        }

        internal int Col
        {
            get
            {
                return this.m_Col;
            }

            set
            {
                this.m_Col = value;
            }
        }
    }
}
