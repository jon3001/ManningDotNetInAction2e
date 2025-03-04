using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.UnitTests
{
    public class SolverTests02
    {
        [Theory]
        [InlineData(8, false)]
        [InlineData(9, true)]
        public void EmptyBoardSizes(int size, bool isValid)
        {
            if (!isValid)
            {
                Assert.Throws<ArgumentException>("size", () => new ArrayBoard(size));
            }
            else
            {
                _ = new ArrayBoard(size);
            }
        }


        [Theory]
        [MemberData(nameof(Boards))]
        public void CheckRules(IBoard board, bool isValid)
        {
            var solver = new Solver(board);
            Assert.Equal(isValid, solver.IsValid());
        }
        public static IEnumerable<object[]> Boards
        {
            get
            {
                IBoard board = new ArrayBoard(4);
                board[1, 0] = 1;
                board[3, 0] = 1;
                yield return new object[] { board, false };
                board = new ArrayBoard(4);
                board[1, 0] = 1;
                board[1, 2] = 1;
                yield return new object[] { board, false };
                board = new ArrayBoard(4);
                board[1, 2] = 1;
                board[0, 3] = 1;
                yield return new object[] { board, false };
                board = new ArrayBoard(4);
                board[1, 1] = 1;
                board[2, 3] = 1;
                yield return new object[] { board, true };
            }
        }

    }
}
