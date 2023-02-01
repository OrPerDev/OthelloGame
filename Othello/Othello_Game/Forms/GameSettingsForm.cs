using System;
using System.Windows.Forms;

namespace Othello.Forms
{
    public partial class GameSettingsForm : Form
    {
        // consts
        private const int k_MinBoardSize = 6;
        private const int k_MaxBoardSize = 12;

        // Members
        private int m_BoardSize;
        private bool m_IsAI;

        // Constractors
        public GameSettingsForm()
        {
            this.m_BoardSize = k_MinBoardSize;
            InitializeComponent();
        }

        // Properties
        internal int BoardSize
        {
            get
            {
                return this.m_BoardSize;
            }
        }

        internal bool GameType
        {
            get
            {
                return this.m_IsAI;
            }
        }

        // Methods
        private void boardSizeChanger()
        {
            if (this.m_BoardSize == k_MaxBoardSize)
            {
                this.m_BoardSize = k_MinBoardSize;
            }
            else
            {
                this.m_BoardSize += 2;
            }
        }

        private void m_BoardSizeButton_Click(object sender, EventArgs e)
        {
            this.boardSizeChanger();
            Button boardSize = sender as Button;
            string message = this.m_BoardSize == k_MaxBoardSize ?
                $"Board size: {this.m_BoardSize}x{this.m_BoardSize} (Press this button to reset board size)":
                $"Board size: {this.m_BoardSize}x{this.m_BoardSize} (Press this button to increase board size)";
            boardSize.Text = message;
        }

        private void m_PlayAgainstComputerButton_Click(object sender, EventArgs e)
        {
            this.m_IsAI = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_PlayAgainstPlayerButton_Click(object sender, EventArgs e)
        {
            this.m_IsAI = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
