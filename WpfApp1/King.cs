using Alphabet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class King : Piece
    {
        public string[] AllMovement
        {
            get
            {
                int row = int.Parse(Position[1].ToString());
                char col = Position[0];

                int leftCol = AlphabetService.GetIndexFromCharacter(col.ToString()) - 1;
                int rightCol = AlphabetService.GetIndexFromCharacter(col.ToString()) + 1;

                string leftMovement = AlphabetService.GetCharacterFromIndex(leftCol) + row.ToString();
                string rightMovement = AlphabetService.GetCharacterFromIndex(rightCol) + row.ToString();

                string frontLeft = AlphabetService.GetCharacterFromIndex(leftCol) + (row + 1).ToString();
                string frontRight = AlphabetService.GetCharacterFromIndex(rightCol) + (row + 1).ToString();
                string bottom = col + (row + 1).ToString();
                string bottomLeft = AlphabetService.GetCharacterFromIndex(leftCol) + (row - 1).ToString();
                string top = col + (row + 1).ToString();
                string bottomRight = AlphabetService.GetCharacterFromIndex(rightCol) + (row - 1).ToString();

                string[] movements = { leftMovement, frontLeft, rightMovement, frontRight, bottom, bottomLeft, top, bottomRight };

                return movements;
            }
        }

        public string[] RowEight
        {
            get
            {
                //calculates the position to the left of the piece
                int left = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string leftMovement = AlphabetService.GetCharacterFromIndex(left) + int.Parse(Position[1].ToString()).ToString();

                //calculates the position to the bottom, then left of the piece
                int backL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string backLeft = AlphabetService.GetCharacterFromIndex(backL) + int.Parse((Position[1] - 1).ToString()).ToString();

                //calculates the position to the right of the piece
                int right = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string rightMovement = AlphabetService.GetCharacterFromIndex(right) + int.Parse(Position[1].ToString()).ToString();

                //calculates the position to the bottom, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + int.Parse((Position[1] - 1).ToString()).ToString();

                //calculates the position to the bottom of the piece
                int bottom = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string bottomMovement = AlphabetService.GetCharacterFromIndex(bottom) + int.Parse((Position[1] - 1).ToString()).ToString();

                string[] movements = { leftMovement, backLeft, rightMovement, backRight, bottomMovement };

                return movements;
            }
        }

        public string[] RowOne
        {
            get
            {
                //calculates the position to the left of the piece
                int left = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string leftMovement = AlphabetService.GetCharacterFromIndex(left) + int.Parse(Position[1].ToString()).ToString();

                //calculates the position to the top, then left of the piece
                int topL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string topLeft = AlphabetService.GetCharacterFromIndex(topL) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the right of the piece
                int right = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string rightMovement = AlphabetService.GetCharacterFromIndex(right) + int.Parse(Position[1].ToString()).ToString();

                //calculates the position to the top, then right of the piece
                int topR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string topRight = AlphabetService.GetCharacterFromIndex(topR) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the top of the piece
                int top = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string topMovement = AlphabetService.GetCharacterFromIndex(top) + int.Parse((Position[1] + 1).ToString()).ToString();

                string[] movements = { leftMovement, topLeft, rightMovement, topRight, topMovement };

                return movements;
            }
        }

        public string[] ColumnA
        {
            get
            {
                //calculates the position to the top of the piece
                int top = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string topMovement = AlphabetService.GetCharacterFromIndex(top) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the top, then right of the piece
                int topR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string topRight = AlphabetService.GetCharacterFromIndex(topR) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the right of the piece
                int right = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string rightMovement = AlphabetService.GetCharacterFromIndex(right) + int.Parse(Position[1].ToString()).ToString();

                //calculates the position to the bottom, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + int.Parse((Position[1] - 1).ToString()).ToString();

                //calculates the position to the bottom of the piece
                int bottom = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string bottomMovement = AlphabetService.GetCharacterFromIndex(bottom) + int.Parse((Position[1] - 1).ToString()).ToString();

                string[] movements = { topMovement, topRight, rightMovement, backRight, bottomMovement };

                return movements;
            }
        }

        public string[] ColumnH
        {
            get
            {
                //calculates the position to the top of the piece
                int top = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string topMovement = AlphabetService.GetCharacterFromIndex(top) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the top, then left of the piece
                int topL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string topLeft = AlphabetService.GetCharacterFromIndex(topL) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the left of the piece
                int left = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string leftMovement = AlphabetService.GetCharacterFromIndex(left) + int.Parse(Position[1].ToString()).ToString();

                //calculates the position to the bottom, then left of the piece
                int bottomL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string bottomLeft = AlphabetService.GetCharacterFromIndex(bottomL) + int.Parse((Position[1] - 1).ToString()).ToString();

                //calculates the position to the bottom of the piece
                int bottom = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string bottomMovement = AlphabetService.GetCharacterFromIndex(bottom) + int.Parse((Position[1] - 1).ToString()).ToString();

                string[] movements = { topMovement, topLeft, leftMovement, bottomLeft, bottomMovement };

                return movements;
            }
        }

        public string[] A_1
        {
            get
            {
                //calculates the position to the top of the piece
                int top = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string topMovement = AlphabetService.GetCharacterFromIndex(top) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the top right of the piece
                int topR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string topRight = AlphabetService.GetCharacterFromIndex(topR) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the right of the piece
                int right = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string rightMovement = AlphabetService.GetCharacterFromIndex(right) + int.Parse(Position[1].ToString()).ToString();

                string[] movements = { topMovement, topRight, rightMovement};

                return movements;
            }
        }

        public string[] A_8
        {
            get
            {
                //calculates the position to the top of the piece
                int bottom = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string bottomMovement = AlphabetService.GetCharacterFromIndex(bottom) + int.Parse((Position[1] - 1).ToString()).ToString();

                //calculates the position to the bottom right of the piece
                int bottomR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string bottomRight = AlphabetService.GetCharacterFromIndex(bottomR) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the right of the piece
                int right = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string rightMovement = AlphabetService.GetCharacterFromIndex(right) + int.Parse(Position[1].ToString()).ToString();

                string[] movements = { bottomMovement, bottomRight, rightMovement };

                return movements;
            }
        }

        public string[] H_1
        {
            get
            {
                //calculates the position to the top of the piece
                int top = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string topMovement = AlphabetService.GetCharacterFromIndex(top) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the top left of the piece
                int topL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string topLeft = AlphabetService.GetCharacterFromIndex(topL) + int.Parse((Position[1] + 1).ToString()).ToString();

                //calculates the position to the left of the piece
                int left = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string leftMovement = AlphabetService.GetCharacterFromIndex(left) + int.Parse(Position[1].ToString()).ToString();

                string[] movements = { topMovement, topLeft, leftMovement };

                return movements;
            }
        }

        public string[] H_8
        {
            get
            {
                //calculates the position to the bottom of the piece
                int bottom = AlphabetService.GetIndexFromCharacter(Position[0].ToString());
                string bottomMovement = AlphabetService.GetCharacterFromIndex(bottom) + int.Parse((Position[1] - 1).ToString()).ToString();

                //calculates the position to the bottom left of the piece
                int bottomL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string bottomLeft = AlphabetService.GetCharacterFromIndex(bottomL) + int.Parse((Position[1] - 1).ToString()).ToString();

                //calculates the position to the left of the piece
                int left = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string leftMovement = AlphabetService.GetCharacterFromIndex(left) + int.Parse(Position[1].ToString()).ToString();

                string[] movements = { bottomMovement, bottomLeft, leftMovement };

                return movements;
            }
        }

        void attack(string colour, string position)
        {
            colour = Colour;
            position = Position;
        }
    }
}
