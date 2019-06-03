using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Threading.Tasks;

namespace BlazorTicTacToe.Pages
{
    public class TicTacToeComponentModel : BlazorComponent
    {
        protected char CurrentPlayer { get; set; }

        protected char Winner { get; set; }

        protected bool Win { get; set; }

        protected int[] GameBoard = { 0, 1, 2 };

        protected char[,] CellValue = new char[ 3, 3 ];

        protected override async Task OnInitAsync()
        {
            await Task.Run(ResetGame);
        }

        protected bool GameWon()
        {
            Win = WinningOnStraightLine() || WinningOnDiagonal();
            return Win;
        }

        protected void SetCellValue(int row, int col)
        {
            if (CellValue[row, col] == '\0' && !GameWon())
            {
                CellValue[row, col] = CurrentPlayer;

                if (GameWon())
                {
                    Winner = CurrentPlayer;
                }

                CurrentPlayer = CurrentPlayer == 'X' ? 'O' : 'X';
            }
        }

        private bool WinningOnStraightLine()
        {
            for (var i = 0; i < 3; i++)
            {
                if (WinningOnRow(i) || WinningOnColumn(i))
                {
                    return true;
                }
            }

            return false;
        }

        private bool WinningOnRow(int column)
        {
            return CellValue[column, 0] != '\0'
                   && CellValue[column, 0] == CellValue[column, 1]
                   && CellValue[column, 1] == CellValue[column, 2];
        }

        private bool WinningOnColumn(int row)
        {
            return CellValue[0, row] != '\0'
                   && CellValue[0, row] == CellValue[1, row]
                   && CellValue[1, row] == CellValue[2, row];
        }

        private bool WinningOnDiagonal()
        {
            return (CellValue[0, 0] != '\0'
                   && CellValue[0, 0] == CellValue[1, 1]
                   && CellValue[1, 1] == CellValue[2, 2])
                   || (CellValue[0, 2] != '\0'
                   && CellValue[0, 2] == CellValue[1, 1]
                   && CellValue[1, 1] == CellValue[2, 0]) ;
    }

        protected void ResetGame()
        {
            CurrentPlayer = 'X';
            Win = false;
            Array.Clear(CellValue, 0, CellValue.Length);
        }
    }
}