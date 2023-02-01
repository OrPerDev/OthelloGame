using System.Collections.Generic;
using System.Linq;

namespace Othello.Logic
{
    internal class GameBoard
    {
        private readonly int r_BoardSize;
        private readonly List<OthelloDisk> r_PossibleNextMoves;
        private eDiskColor?[,] m_Board;

        // Constractors
        internal GameBoard(int i_BoardSize = 8)
        {
            this.r_BoardSize = i_BoardSize;
            this.r_PossibleNextMoves = new List<OthelloDisk>();
            this.initBoard();
        }

        // Properties
        internal eDiskColor?[,] BoardMatrix
        {
            get
            {
                return this.m_Board;
            }

            set
            {
                this.m_Board = value;
            }
        }

        internal int BoardSize
        {
            get
            {
                return this.r_BoardSize;
            }
        }

        internal List<OthelloDisk> PossibleMoves
        {
            get
            {
                return this.r_PossibleNextMoves;
            }
        }

        // Methods
        private void initBoard()
        {
            int midBoardRange = (this.BoardSize / 2) - 1;
            this.BoardMatrix = new eDiskColor?[this.r_BoardSize, this.r_BoardSize];
            this.setInitialDisks(new DiskPosition(midBoardRange, midBoardRange), eDiskColor.White);
            this.setInitialDisks(new DiskPosition(midBoardRange + 1, midBoardRange + 1), eDiskColor.White);
            this.setInitialDisks(new DiskPosition(midBoardRange + 1, midBoardRange), eDiskColor.Black);
            this.setInitialDisks(new DiskPosition(midBoardRange, midBoardRange + 1), eDiskColor.Black);
            this.updateListOfPossibleMoves();
        }

        private void setInitialDisks(DiskPosition i_DiskPosition, eDiskColor i_DiskColor)
        {
            this.BoardMatrix[i_DiskPosition.Row, i_DiskPosition.Col] = i_DiskColor;
        }

        private void updateListOfPossibleMoves()
        {
            this.PossibleMoves.Clear();
            for (int row = 0; row < this.BoardSize; row++)
            {
                for (int col = 0; col < this.BoardSize; col++)
                {
                    if (this.BoardMatrix[row, col] == null)
                    {
                        this.addValidMovesInAllDirections(new DiskPosition(row, col), this.PossibleMoves);
                    }
                }
            }
        }

        private void addValidMovesInAllDirections(DiskPosition io_StartingPosition, List<OthelloDisk> io_PossibleMovesList)
        {
            List<OthelloDisk> newPossibleMoves = new List<OthelloDisk>();
            List<DiskPosition> neighborsDirections = getListOfNeighborDirections();

            foreach (DiskPosition neighborDirection in neighborsDirections)
            {
                this.validateAndAddMoveInDirection(io_StartingPosition, neighborDirection, newPossibleMoves);
            }

            io_PossibleMovesList.AddRange(newPossibleMoves);
        }

        private List<DiskPosition> getListOfNeighborDirections()
        {
            return new List<DiskPosition>()
            {
                new DiskPosition(-1, -1),
                new DiskPosition(-1, 0),
                new DiskPosition(-1, 1),
                new DiskPosition(0, -1),
                new DiskPosition(0, 1),
                new DiskPosition(1, -1),
                new DiskPosition(1, 0),
                new DiskPosition(1, 1),
            };
        }

        private void validateAndAddMoveInDirection(DiskPosition io_StatringPosition, DiskPosition io_Direction, List<OthelloDisk> io_PossibleMoves)
        {
            bool isValidDirection = true;
            int currentCol = io_StatringPosition.Col + io_Direction.Col;
            int currentRow = io_StatringPosition.Row + io_Direction.Row;
            eDiskColor? currentColor = null, firstColorInDirection;
            if (!isOutOfBoard(currentRow, currentCol))
            {
                firstColorInDirection = this.BoardMatrix[currentRow, currentCol];
                if (firstColorInDirection == null)
                {
                    isValidDirection = false;
                }
                else
                {
                    currentColor = firstColorInDirection;
                    while (firstColorInDirection == currentColor)
                    {
                        currentRow += io_Direction.Row;
                        currentCol += io_Direction.Col;
                        if (isOutOfBoard(currentRow, currentCol))
                        {
                            isValidDirection = false;
                            break;
                        }

                        currentColor = this.BoardMatrix[currentRow, currentCol];
                        if (currentColor == null)
                        {
                            isValidDirection = false;
                            break;
                        }
                    }
                }

                if (isValidDirection && !isDiskExistsInList(io_PossibleMoves, currentColor, new DiskPosition(io_StatringPosition.Row, io_StatringPosition.Col)))
                {
                    io_PossibleMoves.Add(new OthelloDisk((eDiskColor)currentColor, io_StatringPosition.Col, io_StatringPosition.Row));
                }
            }
        }

        private bool isOutOfBoard(int io_Row, int io_Col)
        {
            return io_Col < 0 || io_Col >= this.BoardSize || io_Row < 0 || io_Row >= this.BoardSize;
        }

        private bool isDiskExistsInList(List<OthelloDisk> o_PossibleMovesList, eDiskColor? o_CurrentColor, DiskPosition o_CurrentPosition)
        {
            return o_PossibleMovesList.Any(disk => disk.Position == o_CurrentPosition && o_CurrentColor == disk.DiskColor);
        }

        internal void PlacePlayerDisk(DiskPosition i_PlacementPosition, eDiskColor? i_PlayerColor)
        {
            this.placeNewDisk(i_PlacementPosition, i_PlayerColor);
            this.swapColorsAfterPlacement(i_PlacementPosition, i_PlayerColor);
            this.updateListOfPossibleMoves();
        }

        internal List<OthelloDisk> GetPlayerPossibleMoves(eDiskColor? o_PlayerColor)
        {
            return this.PossibleMoves.Where(disk => disk.DiskColor == o_PlayerColor).ToList();
        }

        internal bool IsPlayerMoveValid(DiskPosition o_PlacementPosition, eDiskColor? i_PlayerColor)
        {
            List<OthelloDisk> playerPossibleMoves = GetPlayerPossibleMoves(i_PlayerColor);
            return playerPossibleMoves.Any(possiblePositions => possiblePositions.Position == o_PlacementPosition);
        }

        private void placeNewDisk(DiskPosition io_PlacementPosition, eDiskColor? io_CurrentColor)
        {
            this.BoardMatrix[io_PlacementPosition.Row, io_PlacementPosition.Col] = io_CurrentColor;
        }

        private void swapColorsAfterPlacement(DiskPosition io_PlacementPosition, eDiskColor? io_CurrentColor)
        {
            List<DiskPosition> neighborsDirections = getListOfNeighborDirections();
            foreach (DiskPosition neighborDirection in neighborsDirections)
            {
                this.validateDirectionAndSwapColors(io_PlacementPosition, neighborDirection, io_CurrentColor);
            }
        }

        private void validateDirectionAndSwapColors(DiskPosition io_PlacementPosition, DiskPosition i_NeighborDirection, eDiskColor? i_PlacedDiskColor)
        {
            eDiskColor? currentPositionColor;
            int currentRow = io_PlacementPosition.Row + i_NeighborDirection.Row;
            int currentCol = io_PlacementPosition.Col + i_NeighborDirection.Col;
            bool validDirectionForSwap = true;
            List<DiskPosition> disksPositionsInDirection = new List<DiskPosition>();
            if (!isOutOfBoard(currentRow, currentCol))
            {
                currentPositionColor = this.BoardMatrix[currentRow, currentCol];
                while (currentPositionColor != i_PlacedDiskColor)
                {
                    if (currentPositionColor == null)
                    {
                        validDirectionForSwap = false;
                        break;
                    }

                    disksPositionsInDirection.Add(new DiskPosition(currentRow, currentCol));
                    currentRow += i_NeighborDirection.Row;
                    currentCol += i_NeighborDirection.Col;
                    if (isOutOfBoard(currentRow, currentCol))
                    {
                        validDirectionForSwap = false;
                        break;
                    }

                    currentPositionColor = this.BoardMatrix[currentRow, currentCol];
                }

                if (validDirectionForSwap)
                {
                    this.swapDisksInValidDirection(disksPositionsInDirection, i_PlacedDiskColor);
                }
            }
        }

        private void swapDisksInValidDirection(List<DiskPosition> o_PositionsToSwap, eDiskColor? io_ColorToSwap)
        {
            foreach (DiskPosition position in o_PositionsToSwap)
            {
                this.BoardMatrix[position.Row, position.Col] = io_ColorToSwap;
            }
        }
    }
}
