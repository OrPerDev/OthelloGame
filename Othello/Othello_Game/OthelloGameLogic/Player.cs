namespace Othello.Logic
{
    internal class Player
    {
        private readonly bool r_IsAI;
        private readonly string r_PlayerName;
        private bool m_IsWinner;
        private bool m_IsPlayerTurn;
        private int m_PlayerScore;
        private eDiskColor m_PlayerColor;
        private int m_PlayerWinsCount;

        // Constractors
        internal Player(
            eDiskColor i_PlayerColor,
            string i_PlayerName,
            bool i_isAI = false,
            bool i_IsWinner = false,
            bool i_IsPlayerTurn = false,
            int i_PlayerScore = 2)
        {
            this.r_IsAI = i_isAI;
            this.r_PlayerName = i_PlayerName;
            this.m_PlayerColor = i_PlayerColor;
            this.m_IsWinner = i_IsWinner;
            this.m_IsPlayerTurn = i_IsPlayerTurn;
            this.m_PlayerScore = i_PlayerScore;
            this.m_PlayerWinsCount = 0;
        }

        // Properties
        internal bool IsAI
        {
            get
            {
                return this.r_IsAI;
            }
        }

        internal string Name
        {
            get
            {
                return this.r_PlayerName;
            }
        }

        internal eDiskColor PlayerColor
        {
            get
            {
                return this.m_PlayerColor;
            }

            set
            {
                this.m_PlayerColor = value;
            }
        }

        internal bool IsWinner
        {
            get
            {
                return this.m_IsWinner;
            }

            set
            {
                this.m_IsWinner = value;
            }
        }

        internal bool IsPlayerTurn
        {
            get
            {
                return this.m_IsPlayerTurn;
            }

            set
            {
                this.m_IsPlayerTurn = value;
            }
        }

        internal int PlayerScore
        {
            get
            {
                return this.m_PlayerScore;
            }

            set
            {
                this.m_PlayerScore = value;
            }
        }

        internal int PlayerWinsCount
        {
            get
            {
                return this.m_PlayerWinsCount;
            }

            set
            {
                this.m_PlayerWinsCount = value;
            }
        }
    }
}
