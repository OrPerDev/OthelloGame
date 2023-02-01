using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;


namespace Othello.Forms
{
    public partial class GameBoardForm : Form
    {
        // Events
        public event EventHandler MovePlayedEventHandler;

        // Util members
        private readonly int r_PictureBoxSize = 60;
        private readonly int r_BoardTopStartingPosition = 40;
        private readonly int r_BoardLeftStartingPosition = 15;
        private readonly Bitmap r_BlackCoin = new Bitmap(Properties.Resources.BlackCoin, new Size(55, 55));
        private readonly Bitmap r_FadedBlackCoin = new Bitmap(Properties.Resources.FadedBlackCoin, new Size(55, 55));
        private readonly Bitmap r_WhiteCoin = new Bitmap(Properties.Resources.WhiteCoin, new Size(55, 55));
        private readonly Bitmap r_FadedWhiteCoin = new Bitmap(Properties.Resources.FadedWhiteCoin, new Size(55, 55));
        private readonly SoundPlayer r_PlacementSoundPlayer = new SoundPlayer(Properties.Resources.Placement);

        // Members
        private readonly OthelloDiskPicture[,] r_BoardDisksPictureBox;
        private readonly Label r_ScoreLabel;

        // Constractors
        public GameBoardForm(int io_BoardSize, string o_FirstPlayerName, string o_SecondPlayerName)
        {
            InitializeComponent();
            this.ClientSize = new Size(
                (io_BoardSize * r_PictureBoxSize) + (2 * r_BoardLeftStartingPosition) + io_BoardSize,
                (io_BoardSize * r_PictureBoxSize) + (2 * r_BoardTopStartingPosition));

            this.r_ScoreLabel = new Label();
            this.initBoardLabel(o_FirstPlayerName, o_SecondPlayerName);

            this.r_BoardDisksPictureBox = new OthelloDiskPicture[io_BoardSize, io_BoardSize];
            this.initBoard(io_BoardSize);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Properties.Resources.ParquetBackground;
        }

        // Properties
        internal OthelloDiskPicture[,] BoardPictures
        {
            get
            {
                return this.r_BoardDisksPictureBox;
            }
        }

        internal Bitmap BlackCoin
        {
            get
            {
                return this.r_BlackCoin;
            }
        }

        internal Bitmap FadedBlackCoin
        {
            get
            {
                return this.r_FadedBlackCoin;
            }
        }

        internal Bitmap WhiteCoin
        {
            get
            {
                return this.r_WhiteCoin;
            }
        }

        internal Bitmap FadedWhiteCoin
        {
            get
            {
                return this.r_FadedWhiteCoin;
            }
        }

        internal SoundPlayer PlacementSound
        {
            get
            {
                return this.r_PlacementSoundPlayer;
            }
        }

        internal Label ScoreBoard
        {
            get
            {
                return this.r_ScoreLabel;
            }
        }

        // Methods
        private void initBoard(int i_BoardSize)
        {
            Point currentPicturePoint = new Point(
                this.Location.X + this.r_BoardLeftStartingPosition,
                this.Location.Y + this.r_BoardTopStartingPosition);
            OthelloDiskPicture[,] boardPictures = this.BoardPictures;
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    boardPictures[i, j] = new OthelloDiskPicture(i, j);
                    boardPictures[i, j].Size = new Size(this.r_PictureBoxSize, this.r_PictureBoxSize);
                    boardPictures[i, j].BackColor = Color.FromArgb(210, 180, 140);
                    boardPictures[i, j].Location = currentPicturePoint;
                    boardPictures[i, j].Enabled = false;
                    boardPictures[i, j].BorderStyle = BorderStyle.Fixed3D;
                    boardPictures[i, j].Click += OthelloPcitureBox_Click;
                    currentPicturePoint.X += this.r_PictureBoxSize + 1;
                    this.Controls.Add(boardPictures[i, j]);
                }

                currentPicturePoint.X = this.Location.X + this.r_BoardLeftStartingPosition;
                currentPicturePoint.Y += this.r_PictureBoxSize + 1;
            }
        }

        private void initBoardLabel(string i_FirstPlayerName, string i_SecondPlayerName)
        {
            this.r_ScoreLabel.Text = @$"Score :
{i_FirstPlayerName} {i_SecondPlayerName}";
            this.r_ScoreLabel.AutoSize = true;
            this.r_ScoreLabel.Location = new Point(this.Location.X + (this.Size.Width / 2) - this.r_ScoreLabel.Width, this.Location.Y);
            this.r_ScoreLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.r_ScoreLabel.BackColor = Color.Transparent;
            this.r_ScoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            this.Controls.Add(r_ScoreLabel);
        }

        private void OthelloPcitureBox_Click(object sender, EventArgs e)
        {
            this.MovePlayedEventHandler?.Invoke(sender, e);
        }
    }
}
