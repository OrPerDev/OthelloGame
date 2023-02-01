using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using Othello.Forms;
using Othello.Logic;

namespace Othello
{
    internal class GameController
    {
        private readonly GameBoardForm r_GameBoardForm;
        private readonly Player[] r_Players;
        private readonly eGameType r_GameType;
        private GameBoard m_GameBoard;

        // Constractors
        internal GameController()
        {
            GameSettingsForm settingsForm = new GameSettingsForm();
            DialogResult settingsResult = settingsForm.ShowDialog();
            if (settingsResult == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }

            int boardSize = settingsForm.BoardSize;
            this.r_GameType = settingsForm.GameType == true ? eGameType.SinglePlayer : eGameType.MultiPlayer;
            this.r_Players = new Player[2];
            this.setPlayers(this.r_GameType);
            this.m_GameBoard = new GameBoard(boardSize);
            this.r_GameBoardForm = new GameBoardForm(boardSize, Players[0].Name, Players[1].Name);
            this.r_GameBoardForm.MovePlayedEventHandler += this.FormDiskPlaced;
            this.updateFormVisualization();
            this.r_GameBoardForm.ShowDialog();
        }

        // Properties
        private GameBoard GameBoard
        {
            get
            {
                return this.m_GameBoard;
            }
        }

        private Player[] Players
        {
            get
            {
                return this.r_Players;
            }
        }

        // Methods
        private void setPlayers(eGameType i_GameType)
        {
            this.r_Players[0] = new Player(eDiskColor.Black, "Black");
            this.r_Players[0].IsPlayerTurn = true;

            if (i_GameType == eGameType.SinglePlayer)
            {
                this.r_Players[1] = new Player(eDiskColor.White, "White", true);
            }
            else
            {
                this.r_Players[1] = new Player(eDiskColor.White, "White");
            }

            this.r_Players[1].IsPlayerTurn = false;
        }

        private void postPlacementRoutine()
        {
            if (this.isGameOver())
            {
                DialogResult userRespond = this.gameOverRoutine();
                if (userRespond == DialogResult.Yes)
                {
                    this.resetGame();
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            Player currentPlayer = this.getCurrentPlayer();
            if (!isPlayerHavePossibleMoves(currentPlayer))
            {
                MessageBox.Show(
                    $@"{currentPlayer.Name}, Has no possible moves!
Press the OK button to switch to the other player",
                    "No Possible Moves",
                    MessageBoxButtons.OK);
                this.switchPlayerTurn();
                currentPlayer = this.getCurrentPlayer();
                this.updateFormVisualization();
            }

            if (currentPlayer.IsAI)
            {
                this.playAINextMove(currentPlayer);
            }
        }

        private DialogResult gameOverRoutine()
        {
            StringBuilder messageToShow = new StringBuilder();
            Player? winner = this.determineWinner();
            if (winner == null)
            {
                messageToShow.AppendLine("The game ended up in a tie!");
            }
            else
            {
                winner.PlayerWinsCount += 1;
                messageToShow.AppendLine($"{winner.Name} is the Winner!");
            }

            messageToShow.AppendLine($@"The score for this round is: ({this.Players[0].PlayerScore}/{this.Players[1].PlayerScore}) to {winner.Name}.
The total wins count are: {this.Players[0].Name} - {this.Players[0].PlayerWinsCount} / {this.Players[1].Name} - {this.Players[1].PlayerWinsCount}");
            messageToShow.Append("Would you like to play another round?");
            return MessageBox.Show(messageToShow.ToString(), "Othello - Game Over", MessageBoxButtons.YesNo);
        }

        private bool isGameOver()
        {
            return this.GameBoard.PossibleMoves.Count == 0;
        }

        private Player? determineWinner()
        {
            return this.Players[0].PlayerScore > this.Players[1].PlayerScore ? Players[0] :
                this.Players[0].PlayerScore < this.Players[1].PlayerScore ? Players[1] :
                null;
        }

        private void playAINextMove(Player io_CurrentPlayer)
        {
            List<OthelloDisk> possibleMovesForAI = this.GameBoard.GetPlayerPossibleMoves(io_CurrentPlayer.PlayerColor);
            this.GameBoard.PlacePlayerDisk(OthelloAI.RandomizeNextPlay(possibleMovesForAI), io_CurrentPlayer.PlayerColor);
            this.updatePlayersScore();
            this.switchPlayerTurn();
            this.updateFormVisualization();
            this.postPlacementRoutine();
        }

        private bool isPlayerHavePossibleMoves(Player o_CurrentPlayer)
        {
            return this.GameBoard.GetPlayerPossibleMoves(o_CurrentPlayer.PlayerColor).Count != 0;
        }

        private Player getCurrentPlayer()
        {
            return this.r_Players[0].IsPlayerTurn == true ? this.r_Players[0] : this.r_Players[1];
        }

        private Player getNextPlayer()
        {
            return this.r_Players[0].IsPlayerTurn == false ? this.r_Players[0] : this.r_Players[1];
        }

        private void switchPlayerTurn()
        {
            Player currentPlayer = getCurrentPlayer();
            Player nextPlayer = getNextPlayer();
            currentPlayer.IsPlayerTurn = !currentPlayer.IsPlayerTurn;
            nextPlayer.IsPlayerTurn = !nextPlayer.IsPlayerTurn;
        }

        private void updatePlayersScore()
        {
            int whiteCounters = 0;
            int blackCounters = 0;
            foreach (eDiskColor? position in this.GameBoard.BoardMatrix)
            {
                if (position == eDiskColor.Black)
                {
                    blackCounters++;
                }
                else if (position == eDiskColor.White)
                {
                    whiteCounters++;
                }
            }

            this.r_Players[0].PlayerScore = r_Players[0].PlayerColor == eDiskColor.Black ? blackCounters : whiteCounters;
            this.r_Players[1].PlayerScore = r_Players[1].PlayerColor == eDiskColor.Black ? blackCounters : whiteCounters;
        }

        private void updateFormVisualization()
        {
            this.updateFormDisksVisualization();
            this.updateFormScoreVisualization();
            this.updateFormPossiblePlayerMovesVisualization(this.getCurrentPlayer());
            this.updateFormTitle(this.getCurrentPlayer());
        }

        private void updateFormDisksVisualization()
        {
            int gameBoardSize = this.GameBoard.BoardSize;
            eDiskColor?[,] boardMatrix = this.GameBoard.BoardMatrix;
            foreach (OthelloDiskPicture diskPicture in this.r_GameBoardForm.Controls.OfType<OthelloDiskPicture>())
            {
                int pictureRow = diskPicture.Position.Row;
                int pictureCol = diskPicture.Position.Col;
                if (boardMatrix[pictureRow, pictureCol] == eDiskColor.Black)
                {
                    diskPicture.Image = this.r_GameBoardForm.BlackCoin;
                }
                else if (boardMatrix[pictureRow, pictureCol] == eDiskColor.White)
                {
                    diskPicture.Image = this.r_GameBoardForm.WhiteCoin;
                }
            }
        }

        private void updateFormScoreVisualization()
        {
            this.r_GameBoardForm.ScoreBoard.Text = $@"Score:
{this.Players[0].Name}: {this.Players[0].PlayerScore}{new string(' ', 25)}{this.Players[1].Name}: {this.Players[1].PlayerScore}";
        }

        private void updateFormPossiblePlayerMovesVisualization(Player i_CurrentPlayer)
        {
            List<OthelloDisk> possibleMoves = this.GameBoard.GetPlayerPossibleMoves(i_CurrentPlayer.PlayerColor);
            foreach (OthelloDiskPicture diskPicture in this.r_GameBoardForm.Controls.OfType<OthelloDiskPicture>())
            {
                diskPicture.Enabled = false;
                if (this.GameBoard.BoardMatrix[diskPicture.Position.Row, diskPicture.Position.Col] != null)
                {
                    continue;
                }
                diskPicture.Image = null;
                if (possibleMoves.Any(possibleMove => possibleMove.Position == diskPicture.Position))
                {
                    diskPicture.Image = i_CurrentPlayer.PlayerColor == eDiskColor.Black ? this.r_GameBoardForm.FadedBlackCoin : this.r_GameBoardForm.FadedWhiteCoin;
                    diskPicture.Enabled = true;
                }
            }
        }

        private void updateFormTitle(Player i_CurrentPlayer)
        {
            this.r_GameBoardForm.Text = $"Othello Game - {i_CurrentPlayer.Name}'s Turn";
        }

        private void resetGame()
        {
            this.m_GameBoard = new GameBoard(this.GameBoard.BoardSize);
            this.resetBoardForm();
            this.updatePlayersScore();
            this.Players[0].IsPlayerTurn = true;
            this.Players[1].IsPlayerTurn = false;
            this.updateFormVisualization();
        }

        private void resetBoardForm()
        {
            foreach (OthelloDiskPicture diskPicture in this.r_GameBoardForm.Controls.OfType<OthelloDiskPicture>())
            {
                diskPicture.Image = null;
                diskPicture.Enabled = false;
            }
        }

        internal void FormDiskPlaced(object i_Sender, EventArgs e)
        {
            OthelloDiskPicture othelloPicture = i_Sender as OthelloDiskPicture;
            this.GameBoard.PlacePlayerDisk(othelloPicture.Position, this.getCurrentPlayer().PlayerColor);
            this.r_GameBoardForm.PlacementSound.Play();
            this.updatePlayersScore();
            this.switchPlayerTurn();
            this.updateFormVisualization();
            this.postPlacementRoutine();
        }
    }
}
