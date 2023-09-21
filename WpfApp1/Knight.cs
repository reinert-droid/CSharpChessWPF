using Alphabet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfApp1
{
    internal class Knight : Piece
    {
        public ArrayList AllMovement
        {
            get
            {
                //calculates the position to the left, then top of the piece
                int leftF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftFront = AlphabetService.GetCharacterFromIndex(leftF) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then left of the piece
                int frontL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string frontLeft = AlphabetService.GetCharacterFromIndex(frontL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the right, then top of the piece
                int rightF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightFront = AlphabetService.GetCharacterFromIndex(rightF) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then right of the piece
                int frontR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string frontRight = AlphabetService.GetCharacterFromIndex(frontR) + (char.GetNumericValue(Position[1]) - 2).ToString().ToString();

                //calculates the position to the left, then bottom of the piece
                int leftB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftBack = AlphabetService.GetCharacterFromIndex(leftB) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then left of the piece
                int backL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string backLeft = AlphabetService.GetCharacterFromIndex(backL) + (char.GetNumericValue(Position[1]) + 2).ToString();

                //calculates the position to the right, then bottom of the piece
                int rightB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightBack = AlphabetService.GetCharacterFromIndex(rightB) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + (char.GetNumericValue(Position[1]) + 2).ToString();

                ArrayList movements = new ArrayList{ leftFront, frontLeft, rightFront, frontRight, leftBack, backLeft, rightBack, backRight };

                return movements;
            }
        }

        public ArrayList RowSeven
        {
            get
            {
                //calculates the position to the left, then top of the piece
                int leftF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftFront = AlphabetService.GetCharacterFromIndex(leftF) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the right, then top of the piece
                int rightF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightFront = AlphabetService.GetCharacterFromIndex(rightF) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the left, then bottom of the piece
                int leftB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftBack = AlphabetService.GetCharacterFromIndex(leftB) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then left of the piece
                int backL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string backLeft = AlphabetService.GetCharacterFromIndex(backL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the right, then bottom of the piece
                int rightB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightBack = AlphabetService.GetCharacterFromIndex(rightB) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                ArrayList movements = new ArrayList { leftFront, rightFront, leftBack, backLeft, rightBack, backRight };

                return movements;
            }
        }

        public ArrayList RowEight
        {
            get
            {
                //calculates the position to the left, then bottom of the piece
                int leftB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftBack = AlphabetService.GetCharacterFromIndex(leftB) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the bottom, then left of the piece
                int backL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string backLeft = AlphabetService.GetCharacterFromIndex(backL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the right, then bottom of the piece
                int rightB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightBack = AlphabetService.GetCharacterFromIndex(rightB) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the bottom, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                ArrayList movements = new ArrayList{ leftBack, backLeft, rightBack, backRight };

                return movements;
            }
        }

        public ArrayList ColumnB
        {
            get
            {
                //calculates the position to the top, then left of the piece
                int frontL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string frontLeft = AlphabetService.GetCharacterFromIndex(frontL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the right, then top of the piece
                int rightF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightFront = AlphabetService.GetCharacterFromIndex(rightF) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then right of the piece
                int frontR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string frontRight = AlphabetService.GetCharacterFromIndex(frontR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the bottom, then left of the piece
                int backL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string backLeft = AlphabetService.GetCharacterFromIndex(backL) + (char.GetNumericValue(Position[1]) + 2).ToString();

                //calculates the position to the right, then bottom of the piece
                int rightB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightBack = AlphabetService.GetCharacterFromIndex(rightB) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + (char.GetNumericValue(Position[1]) + 2).ToString();

                ArrayList movements = new ArrayList{ frontLeft, rightFront, frontRight, backLeft, rightBack, backRight };

                return movements;
            }
        }

        public ArrayList ColumnA
        {
            get
            {
                //calculates the position to the right, then top of the piece
                int rightF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightFront = AlphabetService.GetCharacterFromIndex(rightF) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then right of the piece
                int frontR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string frontRight = AlphabetService.GetCharacterFromIndex(frontR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the right, then bottom of the piece
                int rightB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightBack = AlphabetService.GetCharacterFromIndex(rightB) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + (char.GetNumericValue(Position[1]) + 2).ToString();

                ArrayList movements = new ArrayList{ rightFront, frontRight, rightBack, backRight };

                return movements;
            }
        }

        public ArrayList ColumnG
        {
            get
            {
                //calculates the position to the left, then top of the piece
                int leftT = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftTop = AlphabetService.GetCharacterFromIndex(leftT) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then left of the piece
                int topL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string topLeft = AlphabetService.GetCharacterFromIndex(topL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the top, then right of the piece
                int topR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string topRight = AlphabetService.GetCharacterFromIndex(topR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the left, then bottom of the piece
                int leftB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftBottom = AlphabetService.GetCharacterFromIndex(leftB) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then left of the piece
                int bottomL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string bottomLeft = AlphabetService.GetCharacterFromIndex(bottomL) + (char.GetNumericValue(Position[1]) + 2).ToString();

                //calculates the position to the bottom, then right of the piece
                int bottomR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string bottomRight = AlphabetService.GetCharacterFromIndex(bottomR) + (char.GetNumericValue(Position[1]) + 2).ToString();

                ArrayList movements = new ArrayList { leftTop, topLeft, topRight, leftBottom, bottomLeft, bottomRight };

                return movements;
            }
        }

        public ArrayList ColumnH
        {
            get
            {
                //calculates the position to the left, then top of the piece
                int leftT = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftTop = AlphabetService.GetCharacterFromIndex(leftT) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then left of the piece
                int topL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string topLeft = AlphabetService.GetCharacterFromIndex(topL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the left, then bottom of the piece
                int leftB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftBottom = AlphabetService.GetCharacterFromIndex(leftB) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then left of the piece
                int bottomL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string bottomLeft = AlphabetService.GetCharacterFromIndex(bottomL) + (char.GetNumericValue(Position[1]) + 2).ToString();

                ArrayList movements = new ArrayList { leftTop, topLeft, leftBottom, bottomLeft };

                return movements;
            }
        }

        public ArrayList B8
        {
            get
            {
                //calculates the position to the top, then left of the piece
                int backL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string backLeft = AlphabetService.GetCharacterFromIndex(backL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the right, then top of the piece
                int rightB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightBack = AlphabetService.GetCharacterFromIndex(rightB) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                ArrayList movements = new ArrayList { backLeft, rightBack, backRight };

                return movements;
            }
        }

        public ArrayList B1
        {
            get
            {
                //calculates the position to the top, then left of the piece
                int frontL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string frontLeft = AlphabetService.GetCharacterFromIndex(frontL) + (char.GetNumericValue(Position[1]) + 2).ToString();

                //calculates the position to the right, then top of the piece
                int rightF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightFront = AlphabetService.GetCharacterFromIndex(rightF) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the top, then right of the piece
                int frontR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string frontRight = AlphabetService.GetCharacterFromIndex(frontR) + (char.GetNumericValue(Position[1]) + 2).ToString();

                ArrayList movements = new ArrayList { frontLeft, rightFront, frontRight};

                return movements;
            }
        }

        public ArrayList A8
        {
            get
            {
                //calculates the position to the right, then top of the piece
                int rightB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightBack = AlphabetService.GetCharacterFromIndex(rightB) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                ArrayList movements = new ArrayList { rightBack, backRight };

                return movements;
            }
        }

        public ArrayList A1
        {
            get
            {
                //calculates the position to the right, then top of the piece
                int rightF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 2;
                string rightFront = AlphabetService.GetCharacterFromIndex(rightF) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then right of the piece
                int frontR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string frontRight = AlphabetService.GetCharacterFromIndex(frontR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                ArrayList movements = new ArrayList { rightFront, frontRight};

                return movements;
            }
        }

        public ArrayList G8
        {
            get
            {
                //calculates the position to the left, then top of the piece
                int leftF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftFront = AlphabetService.GetCharacterFromIndex(leftF) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the top, then left of the piece
                int frontL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string frontLeft = AlphabetService.GetCharacterFromIndex(frontL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                //calculates the position to the top, then right of the piece
                int frontR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string frontRight = AlphabetService.GetCharacterFromIndex(frontR) + (char.GetNumericValue(Position[1]) - 2).ToString();

                ArrayList movements = new ArrayList { leftFront, frontLeft, frontRight};

                return movements;
            }
        }

        public ArrayList G1
        {
            get
            {
                //calculates the position to the left, then bottom of the piece
                int leftB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftBack = AlphabetService.GetCharacterFromIndex(leftB) + (char.GetNumericValue(Position[1]) + 1).ToString();
                int number = (int)char.GetNumericValue(Position[1]) + 1;

                //calculates the position to the bottom, then left of the piece
                int backL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string backLeft = AlphabetService.GetCharacterFromIndex(backL) + (char.GetNumericValue(Position[1]) + 2).ToString();

                //calculates the position to the bototm, then right of the piece
                int backR = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string backRight = AlphabetService.GetCharacterFromIndex(backR) + (char.GetNumericValue(Position[1]) + 2).ToString();

                ArrayList movements = new ArrayList { leftBack, backLeft, backRight };

                return movements;
            }
        }

        public ArrayList H8
        {
            get
            {
                //calculates the position to the left, then bottom of the piece
                int leftB = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftBack = AlphabetService.GetCharacterFromIndex(leftB) + (char.GetNumericValue(Position[1]) - 1).ToString();

                //calculates the position to the bottom, then left of the piece
                int backL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string backLeft = AlphabetService.GetCharacterFromIndex(backL) + (char.GetNumericValue(Position[1]) - 2).ToString();

                ArrayList movements = new ArrayList { leftBack, backLeft};

                return movements;
            }
        }

        public ArrayList H1
        {
            get
            {
                //calculates the position to the left, then bottom of the piece
                int leftF = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 2;
                string leftFront = AlphabetService.GetCharacterFromIndex(leftF) + (char.GetNumericValue(Position[1]) + 1).ToString();

                //calculates the position to the bottom, then left of the piece
                int frontL = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string frontLeft = AlphabetService.GetCharacterFromIndex(frontL) + (char.GetNumericValue(Position[1]) + 2).ToString();

                ArrayList movements = new ArrayList { leftFront, frontLeft};

                return movements;
            }
        }
    }
}
