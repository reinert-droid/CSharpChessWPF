using Alphabet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Pawn[] whitePawns = new Pawn[8];
        static Pawn[] blackPawns = new Pawn[8];
        static Bishop[] whiteBishops = new Bishop[2];
        static Bishop[] blackBishops = new Bishop[2];
        static Rook[] whiteRooks = new Rook[2];
        static Rook[] blackRooks = new Rook[2];
        static Knight[] whiteKnights = new Knight[2];
        static Knight[] blackKnights = new Knight[2];
        static King blackKing = new King();
        static King whiteKing = new King();
        static Queen whiteQueen = new Queen();
        static Queen blackQueen = new Queen();

        static Piece[] occupied = new Piece[64];

        static ArrayList newPositions = new ArrayList();

        static int index = 0;

        static int imageId = 0;

        static string[] previousPieceName = new string[1];

        static string[] playerTurn = { "white"};

        ArrayList whiteAttackBlocks = new ArrayList();

        ArrayList blackAttackBlocks = new ArrayList();

        public MainWindow()
        {
            InitializeComponent();

            Binding binding = new Binding("ActualHeight")
            {
                Source = chessBoard,
                Converter = (IValueConverter)Resources["HeightToWidth"],
                Mode = BindingMode.OneWay
            };
            chessBoard.SetBinding(Grid.WidthProperty, binding);
            
            InitializeChessBoard();

            InitializePieces();

            InitializeOccupied();
        }

        //returns the relevant movement that the bishop should do
        ArrayList BishopMovement(Bishop bishop)
        {
            //to get the index of the position of the bishop in the occupied array
            int index = AddressToIndex(bishop.Position);

            //to set the initial positions of each possible movement
            int upperRight = index;
            int bottomRight = index;
            int bottomLeft = index;
            int upperLeft = index;

            //to check if the bishop can move down and right
            if (64 > (index + 9))
            {
                //to check if the address is correct for the bishop, the modulo operator at the end is to prevent the code
                    //iterating to the far left side
                while ((bottomRight + 9) < 64 && occupied[bottomRight + 9] == null && (bottomRight + 9) % 8 != 0)
                {
                    bottomRight += 9;
                }

                //to check if the bishop is diagonally left up from another piece
                if (bottomRight + 9 < 64 && occupied[bottomRight + 9] != null && (bottomRight + 9) % 8 != 0)
                {
                    //to check if the piece right down to the bishop is the same colour or not
                    if (!occupied[bottomRight + 9].Colour.Equals(bishop.Colour))
                    {
                        bottomRight += 9;
                    }
                }
            }

            //to check if the bishop can move down and left
            if (64 > (index + 7))
            {
                //to check if the piece is left of another piece or in the bottom right corner
                while ((bottomLeft + 7) < 64 && occupied[bottomLeft + 7] == null && isBottomLeftNotFarRight(bottomLeft))
                {
                    bottomLeft += 7;
                }

                //to check if the bishop is up and right from another piece
                if (bottomLeft + 7 < 64 && occupied[bottomLeft + 7] != null && isBottomLeftNotFarRight(bottomLeft))
                {
                    //to check if the colour of piece that is down and left of the bishop mathces the colour of the bishop
                    if (!occupied[bottomLeft + 7].Colour.Equals(bishop.Colour))
                    {
                        bottomLeft += 7;
                    }
                }
            }

            //to check if the bishop can move up and left
            if (!((index - 9) < 0))
            {
                //to check if the bishop is correct for this movement, the isUpperLeftNotFarRight method makes sure that the
                    //bishop does not overlap to the far right of the board
                while ((upperLeft - 9) >= 0 && occupied[upperLeft - 9] == null && isUpperLeftNotFarRight(upperLeft))
                {
                    upperLeft -= 9;
                }

                //to check if the bishop is down and right to another piece
                if ((upperLeft - 9) >= 0 && occupied[upperLeft - 9] != null && isUpperLeftNotFarRight(upperLeft))
                {
                    //to check if the colour of the piece that is up and left of the bishop matches the bishop
                    if (!occupied[upperLeft - 9].Colour.Equals(bishop.Colour))
                    {
                        upperLeft -= 9;
                    }
                }
            }

            //to check if the piece is in the top row
            if (!((index - 7) <= 0))
            {
                //to check if the piece is in the top row or below another piece
                while (upperRight - 7 >= 0 && occupied[upperRight - 7] == null && (upperRight - 7) % 8 != 0)
                {
                    upperRight -= 7;
                }

                //to check if the bishop is below another piece
                if (upperRight - 7 >= 0 && occupied[upperRight - 7] != null && (upperRight - 7) % 8 != 0)
                {
                    //to check if the colour of the piece that's above the bishop matches or not
                    if (!occupied[upperRight - 7].Colour.Equals(bishop.Colour))
                    {
                        upperRight -= 7;
                    }
                }
            }
            return bishop.Movement(bottomRight, bottomLeft, upperLeft, upperRight, index);
        }

        //returns the relevent movement that the rook should do
        ArrayList RookMovement(Rook rook)
        {
            //to get the index of the position of the rook in the occupied array
            int index = AddressToIndex(rook.Position);
            //to get the most left position on the board
            int leftStop = AddressToIndex("a" + rook.Position[1].ToString());
            //to get the most right position on the board
            int rightStop = AddressToIndex("h" + rook.Position[1].ToString());
            //to get the most upper position on the board
            int upStop = AddressToIndex(rook.Position[0].ToString() + "1");
            //to get the lowest position on the board
            int downStop = AddressToIndex(rook.Position[0].ToString() + "8");
            int upperLimit = index;
            int lowerLimit = index;
            int rightLimit = index;
            int leftLimit = index;

            //to check if the rook is already in the lowest row
            if (64 > (index + 8))
            {
                //to check if the rook is not in the lowest row or directly above another piece
                while (lowerLimit < downStop && occupied[lowerLimit + 8] == null)
                {
                    lowerLimit += 8;
                }

                //to check if the rook is directly above another piece
                if (lowerLimit < downStop && occupied[lowerLimit + 8] != null)
                {
                    //to check if the piece below the rook is the same colour or not
                    if (!occupied[lowerLimit + 8].Colour.Equals(rook.Colour))
                    {
                        lowerLimit += 8;
                    }
                }
            }
            
            //to check if the rook is in the bottom right corner
            if (64 > (index + 1))
            {
                //to check if the piece is left of another piece or in the bottom right corner
                while (rightLimit < rightStop && occupied[rightLimit + 1] == null)
                {
                    rightLimit++;
                }
                
                //to check if the rook is left of another piece
                if (rightLimit < rightStop && occupied[rightLimit + 1] != null)
                {
                    //to check if the colour of piece that is right of the rook mathces the colour of the rook
                    if (!occupied[rightLimit + 1].Colour.Equals(rook.Colour))
                    {
                        rightLimit++;
                    }
                }
            }

            //to check if the piece is in the top left corner
            if (!((index - 1) < 0))
            {
                //to check if the piece is in the top left corner or to the right of another piece
                while (leftLimit > leftStop && occupied[leftLimit - 1] == null)
                {
                    leftLimit--;
                }

                //to check if the rook is to the right of another piece
                if (leftLimit > leftStop && occupied[leftLimit - 1] != null)
                {
                    //to check if the colour of the piece that is to the left of the rook matches the rook
                    if (!occupied[leftLimit - 1].Colour.Equals(rook.Colour))
                    {
                        leftLimit--;
                    }
                }
            }

            //to check if the piece is in the top row
            if (!((index - 8) < 0))
            {
                //to check if the piece is in the top row or below another piece
                while (upperLimit > upStop && occupied[upperLimit - 8] == null)
                {
                    upperLimit -= 8;
                }

                //to check if the rook is below another piece
                if (upperLimit > upStop && occupied[upperLimit - 8] != null)
                {
                    //to check if the colour of the piece that's above the rook matches or not
                    if (!occupied[upperLimit - 8].Colour.Equals(rook.Colour))
                    {
                        upperLimit -= 8;
                    }
                }
            }
            return rook.Movement(upperLimit, lowerLimit, leftLimit, rightLimit, index);
        }

        //returns the relevant movement that the king should do
        ArrayList KingMovement(King king)
        {
            //to get the index of the position of the rook in the occupied array
            int index = AddressToIndex(king.Position);
            //to get the most left position on the board
            int leftStop = AddressToIndex("a" + king.Position[1].ToString());
            //to get the most right position on the board
            int rightStop = AddressToIndex("h" + king.Position[1].ToString());
            //to get the most upper position on the board
            int upStop = AddressToIndex(king.Position[0].ToString() + "1");
            //to get the lowest position on the board
            int downStop = AddressToIndex(king.Position[0].ToString() + "8");
            //to set the initial positions of each possible movement
            int upperRight = index;
            int bottomRight = index;
            int bottomLeft = index;
            int upperLeft = index;
            int upperLimit = index;
            int lowerLimit = index;
            int rightLimit = index;
            int leftLimit = index;

            //to check if the rook is already in the lowest row
            if (64 > (index + 8))
            {
                //to check if the rook is not in the lowest row or directly above another piece
                if (lowerLimit < downStop && occupied[lowerLimit + 8] == null)
                {
                    lowerLimit += 8;
                }
                //to check if the rook is directly above another piece
                else if (lowerLimit < downStop && occupied[lowerLimit + 8] != null)
                {
                    //to check if the piece below the rook is the same colour or not
                    if (!occupied[lowerLimit + 8].Colour.Equals(king.Colour))
                    {
                        lowerLimit += 8;
                    }
                }
            }

            //to check if the rook is in the bottom right corner
            if (64 > (index + 1))
            {
                //to check if the piece is left of another piece or in the bottom right corner
                if (rightLimit < rightStop && occupied[rightLimit + 1] == null)
                {
                    rightLimit++;
                }
                //to check if the rook is left of another piece
                else if (rightLimit < rightStop && occupied[rightLimit + 1] != null)
                {
                    //to check if the colour of piece that is right of the rook mathces the colour of the rook
                    if (!occupied[rightLimit + 1].Colour.Equals(king.Colour))
                    {
                        rightLimit++;
                    }
                }
            }

            //to check if the piece is in the top left corner
            if (!((index - 1) < 0))
            {
                //to check if the piece is in the top left corner or to the right of another piece
                if (leftLimit > leftStop && occupied[leftLimit - 1] == null)
                {
                    leftLimit--;
                }
                //to check if the rook is to the right of another piece
                else if (leftLimit > leftStop && occupied[leftLimit - 1] != null)
                {
                    //to check if the colour of the piece that is to the left of the rook matches the rook
                    if (!occupied[leftLimit - 1].Colour.Equals(king.Colour))
                    {
                        leftLimit--;
                    }
                }
            }

            //to check if the piece is in the top row
            if (!((index - 8) < 0))
            {
                //to check if the piece is in the top row or below another piece
                if (upperLimit > upStop && occupied[upperLimit - 8] == null)
                {
                    upperLimit -= 8;
                }
                //to check if the rook is below another piece
                else if (upperLimit > upStop && occupied[upperLimit - 8] != null)
                {
                    //to check if the colour of the piece that's above the rook matches or not
                    if (!occupied[upperLimit - 8].Colour.Equals(king.Colour))
                    {
                        upperLimit -= 8;
                    }
                }
            }

            

            //to check if the bishop can move down and right
            if (64 > (index + 9))
            {
                //to check if the address is correct for the bishop, the modulo operator at the end is to prevent the code
                //iterating to the far left side
                if ((bottomRight + 9) < 64 && occupied[bottomRight + 9] == null && (bottomRight + 9) % 8 != 0)
                {
                    bottomRight += 9;
                }
                //to check if the bishop is diagonally left up from another piece
                else if (bottomRight + 9 < 64 && occupied[bottomRight + 9] != null && (bottomRight + 9) % 8 != 0)
                {
                    //to check if the piece right down to the bishop is the same colour or not
                    if (!occupied[bottomRight + 9].Colour.Equals(king.Colour))
                    {
                        bottomRight += 9;
                    }
                }
            }

            //to check if the bishop can move down and left
            if (64 > (index + 7))
            {
                //to check if the piece is left of another piece or in the bottom right corner
                if ((bottomLeft + 7) < 64 && occupied[bottomLeft + 7] == null && isBottomLeftNotFarRight(bottomLeft))
                {
                    bottomLeft += 7;
                }
                //to check if the bishop is up and right from another piece
                else if (bottomLeft + 7 < 64 && occupied[bottomLeft + 7] != null && isBottomLeftNotFarRight(bottomLeft))
                {
                    //to check if the colour of piece that is down and left of the bishop mathces the colour of the bishop
                    if (!occupied[bottomLeft + 7].Colour.Equals(king.Colour))
                    {
                        bottomLeft += 7;
                    }
                }
            }

            //to check if the bishop can move up and left
            if (!((index - 9) < 0))
            {
                //to check if the bishop is correct for this movement, the isUpperLeftNotFarRight method makes sure that the
                //bishop does not overlap to the far right of the board
                if ((upperLeft - 9) >= 0 && occupied[upperLeft - 9] == null && isUpperLeftNotFarRight(upperLeft))
                {
                    upperLeft -= 9;
                }
                //to check if the bishop is down and right to another piece
                else if ((upperLeft - 9) >= 0 && occupied[upperLeft - 9] != null && isUpperLeftNotFarRight(upperLeft))
                {
                    //to check if the colour of the piece that is up and left of the bishop matches the bishop
                    if (!occupied[upperLeft - 9].Colour.Equals(king.Colour))
                    {
                        upperLeft -= 9;
                    }
                }
            }

            //to check if the piece is in the top row
            if (!((index - 7) <= 0))
            {
                //to check if the piece is in the top row or below another piece
                if (upperRight - 7 >= 0 && occupied[upperRight - 7] == null && (upperRight - 7) % 8 != 0)
                {
                    upperRight -= 7;
                }
                //to check if the bishop is below another piece
                else if (upperRight - 7 >= 0 && occupied[upperRight - 7] != null && (upperRight - 7) % 8 != 0)
                {
                    //to check if the colour of the piece that's above the bishop matches or not
                    if (!occupied[upperRight - 7].Colour.Equals(king.Colour))
                    {
                        upperRight -= 7;
                    }
                }
            }

            return king.Movement(bottomRight, bottomLeft, upperLeft, upperRight, upperLimit, lowerLimit, leftLimit, rightLimit, index);
        }

        //returns the relevant movement that the queen should do
        ArrayList QueenMovement(Queen queen)
        {
            //to get the index of the position of the rook in the occupied array
            int index = AddressToIndex(queen.Position);
            //to get the most left position on the board
            int leftStop = AddressToIndex("a" + queen.Position[1].ToString());
            //to get the most right position on the board
            int rightStop = AddressToIndex("h" + queen.Position[1].ToString());
            //to get the most upper position on the board
            int upStop = AddressToIndex(queen.Position[0].ToString() + "1");
            //to get the lowest position on the board
            int downStop = AddressToIndex(queen.Position[0].ToString() + "8");
            int upperLimit = index;
            int lowerLimit = index;
            int rightLimit = index;
            int leftLimit = index;

            //to check if the rook is already in the lowest row
            if (64 > (index + 8))
            {
                //to check if the rook is not in the lowest row or directly above another piece
                while (lowerLimit < downStop && occupied[lowerLimit + 8] == null)
                {
                    lowerLimit += 8;
                }

                //to check if the rook is directly above another piece
                if (lowerLimit < downStop && occupied[lowerLimit + 8] != null)
                {
                    //to check if the piece below the rook is the same colour or not
                    if (!occupied[lowerLimit + 8].Colour.Equals(queen.Colour))
                    {
                        lowerLimit += 8;
                    }
                }
            }

            //to check if the rook is in the bottom right corner
            if (64 > (index + 1))
            {
                //to check if the piece is left of another piece or in the bottom right corner
                while (rightLimit < rightStop && occupied[rightLimit + 1] == null)
                {
                    rightLimit++;
                }

                //to check if the rook is left of another piece
                if (rightLimit < rightStop && occupied[rightLimit + 1] != null)
                {
                    //to check if the colour of piece that is right of the rook mathces the colour of the rook
                    if (!occupied[rightLimit + 1].Colour.Equals(queen.Colour))
                    {
                        rightLimit++;
                    }
                }
            }

            //to check if the piece is in the top left corner
            if (!((index - 1) < 0))
            {
                //to check if the piece is in the top left corner or to the right of another piece
                while (leftLimit > leftStop && occupied[leftLimit - 1] == null)
                {
                    leftLimit--;
                }

                //to check if the rook is to the right of another piece
                if (leftLimit > leftStop && occupied[leftLimit - 1] != null)
                {
                    //to check if the colour of the piece that is to the left of the rook matches the rook
                    if (!occupied[leftLimit - 1].Colour.Equals(queen.Colour))
                    {
                        leftLimit--;
                    }
                }
            }

            //to check if the piece is in the top row
            if (!((index - 8) < 0))
            {
                //to check if the piece is in the top row or below another piece
                while (upperLimit > upStop && occupied[upperLimit - 8] == null)
                {
                    upperLimit -= 8;
                }

                //to check if the rook is below another piece
                if (upperLimit > upStop && occupied[upperLimit - 8] != null)
                {
                    //to check if the colour of the piece that's above the rook matches or not
                    if (!occupied[upperLimit - 8].Colour.Equals(queen.Colour))
                    {
                        upperLimit -= 8;
                    }
                }
            }

            //to set the initial positions of each possible movement
            int upperRight = index;
            int bottomRight = index;
            int bottomLeft = index;
            int upperLeft = index;

            //to check if the bishop can move down and right
            if (64 > (index + 9))
            {
                //to check if the address is correct for the bishop, the modulo operator at the end is to prevent the code
                //iterating to the far left side
                while ((bottomRight + 9) < 64 && occupied[bottomRight + 9] == null && (bottomRight + 9) % 8 != 0)
                {
                    bottomRight += 9;
                }

                //to check if the bishop is diagonally left up from another piece
                if (bottomRight + 9 < 64 && occupied[bottomRight + 9] != null && (bottomRight + 9) % 8 != 0)
                {
                    //to check if the piece right down to the bishop is the same colour or not
                    if (!occupied[bottomRight + 9].Colour.Equals(queen.Colour))
                    {
                        bottomRight += 9;
                    }
                }
            }

            //to check if the bishop can move down and left
            if (64 > (index + 7))
            {
                //to check if the piece is left of another piece or in the bottom right corner
                while ((bottomLeft + 7) < 64 && occupied[bottomLeft + 7] == null && isBottomLeftNotFarRight(bottomLeft))
                {
                    bottomLeft += 7;
                }

                //to check if the bishop is up and right from another piece
                if (bottomLeft + 7 < 64 && occupied[bottomLeft + 7] != null && isBottomLeftNotFarRight(bottomLeft))
                {
                    //to check if the colour of piece that is down and left of the bishop mathces the colour of the bishop
                    if (!occupied[bottomLeft + 7].Colour.Equals(queen.Colour))
                    {
                        bottomLeft += 7;
                    }
                }
            }

            //to check if the bishop can move up and left
            if (!((index - 9) < 0))
            {
                //to check if the bishop is correct for this movement, the isUpperLeftNotFarRight method makes sure that the
                //bishop does not overlap to the far right of the board
                while ((upperLeft - 9) >= 0 && occupied[upperLeft - 9] == null && isUpperLeftNotFarRight(upperLeft))
                {
                    upperLeft -= 9;
                }

                //to check if the bishop is down and right to another piece
                if ((upperLeft - 9) >= 0 && occupied[upperLeft - 9] != null && isUpperLeftNotFarRight(upperLeft))
                {
                    //to check if the colour of the piece that is up and left of the bishop matches the bishop
                    if (!occupied[upperLeft - 9].Colour.Equals(queen.Colour))
                    {
                        upperLeft -= 9;
                    }
                }
            }

            //to check if the piece is in the top row
            if (!((index - 7) <= 0))
            {
                //to check if the piece is in the top row or below another piece
                while (upperRight - 7 >= 0 && occupied[upperRight - 7] == null && (upperRight - 7) % 8 != 0)
                {
                    upperRight -= 7;
                }

                //to check if the bishop is below another piece
                if (upperRight - 7 >= 0 && occupied[upperRight - 7] != null && (upperRight - 7) % 8 != 0)
                {
                    //to check if the colour of the piece that's above the bishop matches or not
                    if (!occupied[upperRight - 7].Colour.Equals(queen.Colour))
                    {
                        upperRight -= 7;
                    }
                }
            }

            return queen.Movement(bottomRight, bottomLeft, upperLeft, upperRight, upperLimit, lowerLimit, leftLimit, rightLimit, index);
        }

        //returns the relevant movement that the pawn should do
        ArrayList PawnMovement(Pawn pawn)
        {
            //to get the index of the pawn in the occupied array
            int index = AddressToIndex(pawn.Position);

            int moveLimit = 0;

            //to store all the possible movements
            ArrayList movement = new ArrayList();

            //to check if the pawn is in its intial position
            if (!pawn.Position.Contains("7") && pawn.Colour.Equals("white"))
            {
                //to check if the piece can move upwards
                if (occupied[index - 8] == null)
                {
                    moveLimit++;
                }

                //to check if the pawn can attack and in which direction the pawn can attack
                if (occupied[index - 9] != null && occupied[index - 7] != null && !isPawnFarRight(index) && index % 8 != 0)
                {
                    return pawn.Movement(true, true, index, moveLimit);
                }
                else if (occupied[index - 9] != null && occupied[index - 7] == null && index % 8 != 0)
                {
                    return pawn.Movement(true, false, index, moveLimit);
                }
                else if (occupied[index - 9] == null && occupied[index - 7] != null && !isPawnFarRight(index))
                {
                    return pawn.Movement(false, true, index, moveLimit);
                }
                return pawn.Movement(false, false, index, moveLimit);

            }
            //to check if the pawn is in its initial position
            else if (!pawn.Position.Contains("2") && pawn.Colour.Equals("black"))
            {
                //to check if the piece can move downwards
                if (occupied[index + 8] == null)
                {
                    moveLimit++;
                }

                //to check if the pawn can attack and in which direction the pawn can attack
                if (occupied[index + 7] != null && occupied[index + 9] != null && !isPawnFarRight(index) && index % 8 != 0)
                {
                    return pawn.Movement(true, true, index, moveLimit);
                }
                else if (occupied[index + 7] != null && occupied[index + 9] == null && index % 8 != 0)
                {
                    return pawn.Movement(true, false, index, moveLimit);
                }
                else if (occupied[index + 7] == null && occupied[index + 9] != null && !isPawnFarRight(index))
                {
                    return pawn.Movement(false, true, index, moveLimit);
                }
                return pawn.Movement(false, false, index, moveLimit);
            }
            //runs the code if the pawn is in its initial position
            else if (pawn.Position.Contains("7") && pawn.Colour.Equals("white"))
            {
                //to check how far the piece can move upwards
                if (occupied[index - 8] == null)
                {
                    moveLimit++;
                    if (occupied[index - 16] == null)
                    {
                        moveLimit++;
                    }
                }

                //to check if the pawn can attack and in which direction the pawn can attack
                if (occupied[index - 9] != null && occupied[index - 7] != null && !isPawnFarRight(index) && index % 8 != 0)
                {
                    return pawn.Movement(true, true, index, moveLimit);
                }
                else if (occupied[index - 9] != null && occupied[index - 7] == null && index % 8 != 0)
                {
                    return pawn.Movement(true, false, index, moveLimit);
                }
                else if (occupied[index - 9] == null && occupied[index - 7] != null && !isPawnFarRight(index))
                {
                    return pawn.Movement(false, true, index, moveLimit);
                }
                return pawn.Movement(false, false, index, moveLimit);
            }
            //runs the code if the pawn is in its initial position
            else if (pawn.Position.Contains("2") && pawn.Colour.Equals("black"))
            {
                //to check how far the pawn can move downwards
                if (occupied[index + 8] == null)
                {
                    moveLimit++;
                    if (occupied[index + 16] == null)
                    {
                        moveLimit++;
                    }
                }

                //to check if the pawn can attack and in which direction the pawn can attack
                if (occupied[index + 7] != null && occupied[index + 9] != null && !isPawnFarRight(index) && index % 8 != 0)
                {
                    return pawn.Movement(true, true, index, moveLimit);
                }
                else if (occupied[index + 7] != null && occupied[index + 9] == null && index % 8 != 0)
                {
                    return pawn.Movement(true, false, index, moveLimit);
                }
                else if (occupied[index + 7] == null && occupied[index + 9] != null && !isPawnFarRight(index))
                {
                    return pawn.Movement(false, true, index, moveLimit);
                }
                return pawn.Movement(false, false, index, moveLimit);
            }

            return pawn.Movement(false, false, index, moveLimit);
        }

        //returns the relevant movement that the knight should do
        ArrayList KnightMovement(Knight knight)
        {
            //stores the current position of the knight
            int index = AddressToIndex(knight.Position);
            int upThenLeft = index;
            int upThenRight = index;
            int leftThenUp = index;
            int rightThenUp = index;
            int leftThenDown = index;
            int rightThenDown = index;
            int downThenLeft = index;
            int downThenRight = index;

            if(upThenLeft - 17 >= 0)
            {
                if (occupied[index - 17] == null)
                {
                    upThenLeft -= 17;
                }
                else if (occupied[index - 17] != null)
                {
                    if (!occupied[index - 17].Colour.Equals(knight.Colour))
                    {
                        upThenLeft -= 17;
                    }
                }
            }

            if (upThenRight - 15 >= 0)
            {
                if (occupied[index - 15] == null)
                {
                    upThenRight -= 15;
                }
                else if (occupied[index - 15] != null)
                {
                    if (!occupied[index - 15].Colour.Equals(knight.Colour))
                    {
                        upThenRight -= 15;
                    }
                }
            }

            if (leftThenUp - 10 >= 0)
            {
                if (occupied[index - 10] == null)
                {
                    leftThenUp -= 10;
                }
                else if (occupied[index - 10] != null)
                {
                    if (!occupied[index - 10].Colour.Equals(knight.Colour))
                    {
                        leftThenUp -= 10;
                    }
                }
            }

            if (rightThenUp - 6 >= 0)
            {
                if (occupied[index - 6] == null)
                {
                    rightThenUp -= 6;
                }
                else if (occupied[index - 6] != null)
                {
                    if (!occupied[index - 6].Colour.Equals(knight.Colour))
                    {
                        rightThenUp -= 6;
                    }
                }
            }

            if (leftThenDown + 6 < 64)
            {
                if (occupied[index + 6] == null)
                {
                    leftThenDown += 6;
                }
                else if (occupied[index + 6] != null)
                {
                    if (!occupied[index + 6].Colour.Equals(knight.Colour))
                    {
                        leftThenDown += 6;
                    }
                }
            }

            if (rightThenDown + 10 < 64)
            {
                if (occupied[index + 10] == null)
                {
                    rightThenDown += 10;
                }
                else if (occupied[index + 10] != null)
                {
                    if (!occupied[index + 10].Colour.Equals(knight.Colour))
                    {
                        rightThenDown += 10;
                    }
                }
            }

            if (downThenLeft + 15 < 64)
            {
                if (occupied[index + 15] == null)
                {
                    downThenLeft += 15;
                }
                else if (occupied[index + 15] != null)
                {
                    if (!occupied[index + 15].Colour.Equals(knight.Colour))
                    {
                        downThenLeft += 15;
                    }
                }
            }

            if (downThenRight + 17 < 64)
            {
                if (occupied[index + 17] == null)
                {
                    downThenRight += 17;
                }
                else if (occupied[index + 17] != null)
                {
                    if (!occupied[index + 17].Colour.Equals(knight.Colour))
                    {
                        downThenRight += 17;
                    }
                }
            }

            return knight.AllMovement(upThenLeft, upThenRight, leftThenUp, rightThenUp, leftThenDown, rightThenDown, downThenLeft, downThenRight, index);
        }

        //adds the relevant information to every piece on the board
        public void InitializePieces()
        {
            //to cache the two colours that each piece can have
            string white = "white";
            string black = "black";

            //sets the colour
            for (int i = 0; i < whiteKnights.Length; i++)
            {
                whiteKnights[i] = new Knight();
                whiteKnights[i].Colour = white;

                whiteRooks[i] = new Rook();
                whiteRooks[i].Colour = white;

                whiteBishops[i] = new Bishop();
                whiteBishops[i].Colour = white;

                blackKnights[i] = new Knight();
                blackKnights[i].Colour = black;

                blackRooks[i] = new Rook();
                blackRooks[i].Colour = black;

                blackBishops[i] = new Bishop();
                blackBishops[i].Colour = black;
            }

            //initializes the pawns
            for (int i = 1; i <= whitePawns.Length; i++) 
            { 
                whitePawns[i - 1] = new Pawn();
                whitePawns[i - 1].Position = AlphabetService.GetCharacterFromIndex(i).ToString() + 7.ToString();
                whitePawns[i - 1].Colour = white;
                whitePawns[i - 1].Name = "Pawn";
                blackPawns[i - 1] = new Pawn();
                blackPawns[i - 1].Position = AlphabetService.GetCharacterFromIndex(i).ToString() + 2.ToString();
                blackPawns[i - 1].Colour = black;
                blackPawns[i - 1].Name = "Pawn";
            }

            blackKing.Colour = black;
            blackKing.Name = "King";
            whiteKing.Colour = white;
            whiteKing.Name = "King";

            whiteQueen.Colour = white;
            whiteQueen.Name = "Queen";
            blackQueen.Colour = black;
            blackQueen.Name = "Queen";

            whiteRooks[0].Position = "a8";
            whiteRooks[0].Name = "Rook";
            whiteKnights[0].Position = "b8";
            whiteKnights[0].Name = "Knight";
            whiteBishops[0].Position = "c8";
            whiteBishops[0].Name = "Bishop";
            whiteQueen.Position = "d8";
            whiteKing.Position = "e8";
            whiteBishops[1].Position = "f8";
            whiteBishops[1].Name = "Bishop";
            whiteKnights[1].Position = "g8";
            whiteKnights[1].Name = "Knight";
            whiteRooks[1].Position = "h8";
            whiteRooks[1].Name = "Rook";

            blackRooks[0].Position = "a1";
            blackRooks[0].Name = "Rook";
            blackKnights[0].Position = "b1";
            blackKnights[0].Name = "Knight";
            blackBishops[0].Position = "c1";
            blackBishops[0].Name = "Bishop";
            blackQueen.Position = "d1";
            blackKing.Position = "e1";
            blackBishops[1].Position = "f1";
            blackBishops[1].Name = "Bishop";
            blackKnights[1].Position = "g1";
            blackKnights[1].Name = "Knight";
            blackRooks[1].Position = "h1";
            blackRooks[1].Name = "Rook";
        }

        //adds the assets of the pieces to the block that they belong to
        public void InitializeChessBoard()
        {
            for (int i = 0; i < 64; i++)
            {
                int row = i / 8;
                int col = i % 8;
                Button button = (Button)chessBoard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
                if(i < 16 || i > 47)
                {
                    button.Content = image(i);
                }
                else
                {
                    button.Content = null;
                }
            }
        }

        //to return the image from the relevant directory
        public System.Windows.Controls.Image image(int number)
        {
            string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string source = string.Empty;
            switch (number)
            {
                case 0: 
                    source = rootDirectory + "\\Assets\\b_rook.png";
                    break;
                case 1:
                    source = rootDirectory + "\\Assets\\b_knight.png";
                    break;
                case 2:
                    source = rootDirectory + "\\Assets\\b_bishop.png";
                    break;
                case 3:
                    source = rootDirectory + "\\Assets\\b_queen.png";
                    break;
                case 4:
                    source = rootDirectory + "\\Assets\\b_king.png";
                    break;
                case 5:
                    source = rootDirectory + "\\Assets\\b_bishop.png";
                    break;
                case 6:
                    source = rootDirectory + "\\Assets\\b_knight.png";
                    break;
                case 7:
                    source = rootDirectory + "\\Assets\\b_rook.png";
                    break;
                case 8:
                    source = rootDirectory + "\\Assets\\b_pawn.png";
                    break;
                case 9:
                    source = rootDirectory + "\\Assets\\b_pawn.png";
                    break;
                case 10:
                    source = rootDirectory + "\\Assets\\b_pawn.png";
                    break;
                case 11:
                    source = rootDirectory + "\\Assets\\b_pawn.png";
                    break;
                case 12:
                    source = rootDirectory + "\\Assets\\b_pawn.png";
                    break;
                case 13:
                    source = rootDirectory + "\\Assets\\b_pawn.png";
                    break;
                case 14:
                    source = rootDirectory + "\\Assets\\b_pawn.png";
                    break;
                case 15:
                    source = rootDirectory + "\\Assets\\b_pawn.png";
                    break;
                case 48:
                    source = rootDirectory + "\\Assets\\w_pawn.png";
                    break;
                case 49:
                    source = rootDirectory + "\\Assets\\w_pawn.png";
                    break;
                case 50:
                    source = rootDirectory + "\\Assets\\w_pawn.png";
                    break;
                case 51:
                    source = rootDirectory + "\\Assets\\w_pawn.png";
                    break;
                case 52:
                    source = rootDirectory + "\\Assets\\w_pawn.png";
                    break;
                case 53:
                    source = rootDirectory + "\\Assets\\w_pawn.png";
                    break;
                case 54:
                    source = rootDirectory + "\\Assets\\w_pawn.png";
                    break;
                case 55:
                    source = rootDirectory + "\\Assets\\w_pawn.png";
                    break;
                case 56:
                    source = rootDirectory + "\\Assets\\w_rook.png";
                    break;
                case 57:
                    source = rootDirectory + "\\Assets\\w_knight.png";
                    break;
                case 58:
                    source = rootDirectory + "\\Assets\\w_bishop.png";
                    break;
                case 59:
                    source = rootDirectory + "\\Assets\\w_queen.png";
                    break;
                case 60:
                    source = rootDirectory + "\\Assets\\w_king.png";
                    break;
                case 61:
                    source = rootDirectory + "\\Assets\\w_bishop.png";
                    break;
                case 62:
                    source = rootDirectory + "\\Assets\\w_knight.png";
                    break;
                case 63:
                    source = rootDirectory + "\\Assets\\w_rook.png";
                    break;
                default:
                    break;
            }
            BitmapImage piece = new BitmapImage(new Uri(source));
            System.Windows.Controls.Image control = new System.Windows.Controls.Image();
            control.Source = piece;
            return control;
        }

        //to handle the double click event on a square on the board
        private void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bool movedOnly = false;
            bool isAttacking = false;

            //resets the colours of the chess board after the last time a piece was clicked on
            ResetChessBoard();

            //to get the information from the specific button that was clicked
            Button source = (Button)sender;

            //to get the name from the button, which is also the address of the button
            string name = source.Name;

            //to get the index of the newly clicked on block
            int newIndex = AddressToIndex(name);

            //iterates over the positions returned by the previous piece
            foreach (string address in newPositions)
            {
                if (name.Equals(address))
                {
                    //checks if the new clicked on postion and the previous clicked on position are occupied or not
                    if (occupied[index] != null && occupied[newIndex] != null)
                    {
                        //compares the colours of the previous clicked on piece and the new clicked on piece
                        if (occupied[index].Colour.Equals(playerTurn[0]) && !occupied[newIndex].Colour.Equals(playerTurn[0]))
                        {
                            // values passed are, position of the attacking piece in the occupied array, the name of the attacked
                            // piece, the imageId of the attacking piece and the index of the attacking piece
                            isAttacking = NewPosition(occupied[index], name, imageId, AddressToIndex(previousPieceName[0]));
                            break;
                        }
                        
                    }
                    //checks if the previous piece is occupied and the new piece is not occupied
                    else if (occupied[index] != null && occupied[newIndex] == null)
                    {
                        //checks if the previous piece corresponds to the playerturn colour
                        if (occupied[index].Colour.Equals(playerTurn[0]))
                        {
                            // values passed are, position of the attacking piece in the occupied array, the name of the attacked
                            // piece, the imageId of the attacking piece and the index of the attacking piece
                            isAttacking = NewPosition(occupied[index], name, imageId, AddressToIndex(previousPieceName[0]));
                            movedOnly = true;
                            break;
                        }
                    }
                }
                //checks if the player clicked on their own piece when it is not their turn
                else if (occupied[index] != null && occupied[newIndex] != null)
                {
                    if (!occupied[index].Colour.Equals(playerTurn[0]) && !occupied[newIndex].Colour.Equals(playerTurn[0]))
                    {
                        movedOnly = true;
                    }
                    if (occupied[index].Colour.Equals(playerTurn[0]) && !occupied[newIndex].Colour.Equals(playerTurn[0]))
                    {
                        movedOnly = true;
                    }
                }
            }

            //to get the index of where the piece is stored
            index = AddressToIndex(name);
            
            newPositions.Clear();

            //to check if the button that was clicked on actually contains a chess piece
            if (occupied[index] != null)
            {
                //to store the possible movements that the chess piece can do
                ArrayList movement = new ArrayList();

                movement = checkPieces(index);

                //adds the new possible positions to the newPositions arraylist
                foreach(string move in movement)
                {
                    newPositions.Add(move);
                }

                //to check if the piece is attacking or not and if the piece only moved to a new spot
                if(!isAttacking && !movedOnly)
                {
                    foreach (string move in movement)
                    {
                        //to highlight all the possible spaces that the piece can move to on the board
                        int column = (int)char.GetNumericValue(move[1]) - 1;
                        int row = AlphabetService.GetIndexFromCharacter(move[0].ToString()) - 1;
                        Button button = (Button)chessBoard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == column && Grid.GetColumn(e) == row);
                        button.Background = new SolidColorBrush(Colors.Red);
                    }
                }
                previousPieceName[0] = name;

                if (isKingDead())
                {
                    if (playerTurn[0].Equals("white"))
                    {
                        MessageBox.Show("Black wins!");
                    }
                    else
                    {
                        MessageBox.Show("White wins!");
                    }

                    InitializePieces();
                    InitializeChessBoard();
                    InitializeOccupied();
                    playerTurn[0] = "white";
                }
            }

        }

        //returns an integer that will be used to access the relevent element in the occupied array
        public int AddressToIndex(string address)
        {
            int index_1 = AlphabetService.GetIndexFromCharacter(address[0].ToString()) - 1;
            int index_2 = (int)char.GetNumericValue(address[1]) - 1;

            return index_1 + (index_2 * 8);
        }

        //to check what piece is in the relevant position in the occupied array
        public ArrayList checkPieces(int index)
        {
            ArrayList movement = new ArrayList();
            //to check which type of piece it is
            switch (occupied[index].Name)
            {
                case "Knight":
                    movement = KnightMovement((Knight)occupied[index]);
                    if (occupied[index].Colour.Equals("black"))
                    {
                        imageId = 1;
                    }
                    else if (occupied[index].Colour.Equals("white"))
                    {
                        imageId = 57;
                    }
                    break;
                case "Pawn":
                    movement = PawnMovement((Pawn)occupied[index]);
                    if (occupied[index].Colour.Equals("black"))
                    {
                        imageId = 9;
                    }
                    else if (occupied[index].Colour.Equals("white"))
                    {
                        imageId = 54;
                    }
                    break;
                case "Rook":
                    movement = RookMovement((Rook)occupied[index]);
                    if (occupied[index].Colour.Equals("black"))
                    {
                        imageId = 0;
                    }
                    else if (occupied[index].Colour.Equals("white"))
                    {
                        imageId = 56;
                    }
                    break;
                case "Bishop":
                    movement = BishopMovement((Bishop)occupied[index]);
                    if (occupied[index].Colour.Equals("black"))
                    {
                        imageId = 2;
                    }
                    else if (occupied[index].Colour.Equals("white"))
                    {
                        imageId = 58;
                    }
                    break;
                case "Queen":
                    movement = QueenMovement((Queen)occupied[index]);
                    if (occupied[index].Colour.Equals("black"))
                    {
                        imageId = 3;
                    }
                    else if (occupied[index].Colour.Equals("white"))
                    {
                        imageId = 59;
                    }
                    break;
                case "King":
                    movement = KingMovement((King)occupied[index]);
                    if (occupied[index].Colour.Equals("black"))
                    {
                        imageId = 4;
                    }
                    else if (occupied[index].Colour.Equals("white"))
                    {
                        imageId = 60;
                    }
                    break;
                default:
                    break;
            }
            return movement;
        }

        //initializes the occupied array
        public void InitializeOccupied()
        {
            occupied[0] = blackRooks[0];
            occupied[1] = blackKnights[0];
            occupied[2] = blackBishops[0];
            occupied[3] = blackQueen;
            occupied[4] = blackKing;
            occupied[5] = blackBishops[1];
            occupied[6] = blackKnights[1];
            occupied[7] = blackRooks[1];

            for(int i = 16; i < 48; i++)
            {
                occupied[i] = null;
            }

            for (int i = 8; i < 16; i++)
            {
                occupied[i] = blackPawns[i - 8];
            }

            for(int i = 48; i < 56; i++) 
            {
                occupied[i] = whitePawns[i - 48];
            }

            occupied[56] = whiteRooks[0];
            occupied[57] = whiteKnights[0];
            occupied[58] = whiteBishops[0];
            occupied[59] = whiteQueen;
            occupied[60] = whiteKing;
            occupied[61] = whiteBishops[1];
            occupied[62] = whiteKnights[1];
            occupied[63] = whiteRooks[1];
        }

        //resets the chess board after a few blocks have been highlighted
        public void ResetChessBoard()
        {
            Button[] row_1 = { a1, b1, c1, d1, e1, f1, g1, h1 };
            Button[] row_2 = { a2, b2, c2, d2, e2, f2, g2, h2 };
            Button[] row_3 = { a3, b3, c3, d3, e3, f3, g3, h3 };
            Button[] row_4 = { a4, b4, c4, d4, e4, f4, g4, h4 };
            Button[] row_5 = { a5, b5, c5, d5, e5, f5, g5, h5 };
            Button[] row_6 = { a6, b6, c6, d6, e6, f6, g6, h6 };
            Button[] row_7 = { a7, b7, c7, d7, e7, f7, g7, h7 };
            Button[] row_8 = { a8, b8, c8, d8, e8, f8, g8, h8 };
            for (int i = 0; i < 7; i += 2) 
            {
                row_1[i].Background = Brushes.DimGray;
                row_1[i + 1].Background = Brushes.DarkSlateGray;
            }
            for (int i = 0; i < 7; i += 2)
            {
                row_2[i].Background = Brushes.DarkSlateGray;
                row_2[i + 1].Background = Brushes.DimGray;
            }
            for (int i = 0; i < 7; i += 2)
            {
                row_3[i].Background = Brushes.DimGray;
                row_3[i + 1].Background = Brushes.DarkSlateGray;
            }
            for (int i = 0; i < 7; i += 2)
            {
                row_4[i].Background = Brushes.DarkSlateGray;
                row_4[i + 1].Background = Brushes.DimGray;
            }
            for (int i = 0; i < 7; i += 2)
            {
                row_5[i].Background = Brushes.DimGray;
                row_5[i + 1].Background = Brushes.DarkSlateGray;
            }
            for (int i = 0; i < 7; i += 2)
            {
                row_6[i].Background = Brushes.DarkSlateGray;
                row_6[i + 1].Background = Brushes.DimGray;
            }
            for (int i = 0; i < 7; i += 2)
            {
                row_7[i].Background = Brushes.DimGray;
                row_7[i + 1].Background = Brushes.DarkSlateGray;
            }
            for (int i = 0; i < 7; i += 2)
            {
                row_8[i].Background = Brushes.DarkSlateGray;
                row_8[i + 1].Background = Brushes.DimGray;
            }
        }

        public bool NewPosition(Piece piece, string newPosition, int imageNum, int index)
        {
            if (playerTurn[0].Equals("white"))
            {
                playerTurn[0] = "black";
            }
            else
            {
                playerTurn[0] = "white";
            }
            //to check if there is a piece in the new position
            if (occupied[AddressToIndex(newPosition)] != null)
            {
                //to check if the colours of the two pieces are the same
                if (!occupied[AddressToIndex(newPosition)].Colour.Equals(occupied[index].Colour))
                {
                    //to store the address of the piece that was clicked on
                    string address = piece.Position.ToString();
                    //to set the new position of the piece
                    piece.Position = newPosition;

                    //to remove the picture of the piece from the previous position
                    int column = (int)char.GetNumericValue(address[1]) - 1;
                    int row = AlphabetService.GetIndexFromCharacter(address[0].ToString()) - 1;
                    Button button = (Button)chessBoard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == column && Grid.GetColumn(e) == row);
                    button.Content = null;

                    //to move the picture of the piece to the new position
                    int newColumn = (int)char.GetNumericValue(newPosition[1]) - 1;
                    int newRow = AlphabetService.GetIndexFromCharacter(newPosition[0].ToString()) - 1;
                    Button newButton = (Button)chessBoard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == newColumn && Grid.GetColumn(e) == newRow);
                    newButton.Content = image(imageNum);

                    //replaces the piece in the position if the colours do not match
                    occupied[AddressToIndex(newPosition)] = piece;

                    occupied[index] = null;

                    //Asserts the blocks that each colour can attack on
                    //AssertWhiteAttackBlocks();
                    //AssertBlackAttackBlocks();

                    //IsWhiteKingSafe();
                    //IsBlackKingSafe();

                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (occupied[AddressToIndex(newPosition)] == null)
            {
                //to store the address of the piece that was clicked on
                string address = piece.Position.ToString();
                
                //to set the new position of the piece
                piece.Position = newPosition;

                //to remove the picture of the piece from the previous position
                int column = (int)char.GetNumericValue(address[1]) - 1;
                int row = AlphabetService.GetIndexFromCharacter(address[0].ToString()) - 1;
                Button button = (Button)chessBoard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == column && Grid.GetColumn(e) == row);
                button.Content = null;

                //to move the picture of the piece to the new position
                int newColumn = (int)char.GetNumericValue(newPosition[1]) - 1;
                int newRow = AlphabetService.GetIndexFromCharacter(newPosition[0].ToString()) - 1;
                Button newButton = (Button)chessBoard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == newColumn && Grid.GetColumn(e) == newRow);
                newButton.Content = image(imageNum);

                //to add the piece to the new position in the occupied array
                occupied[AddressToIndex(newPosition)] = piece;

                occupied[index] = null;

                //Asserts the blocks that can each colour can attack on
                //AssertWhiteAttackBlocks();
                //AssertBlackAttackBlocks();

                //IsWhiteKingSafe();
                //IsBlackKingSafe();
                
                return false;
            }
            return false;

        }

        public bool isBottomLeftNotFarRight(int index)
        {
            return (index + 7) == 7 ? false: (index + 7) == 15? false: (index + 7) == 23? false: (index + 7) == 31? false:
                (index + 7) == 39? false: (index + 7) == 47? false: (index + 7) == 55? false: (index + 7) == 63? false: true;
        }

        public bool isUpperLeftNotFarRight(int index)
        {
            return (index - 9) == 7 ? false : (index - 9) == 15 ? false : (index - 9) == 23 ? false : (index - 9) == 31 ? false :
                (index - 9) == 39 ? false : (index - 9) == 47 ? false : (index - 9) == 55 ? false : true;
        }

        //asserts the blocks that white could attack on
        public void AssertWhiteAttackBlocks()
        {
            List<string> blocks = new List<string>();
            List<string> tempBlocks = new List<string>();

            int i = 0;

            foreach(Piece piece in occupied)
            {
                if (piece == null) 
                { 
                    i++; 
                    continue;
                }
                if (!occupied[i].Colour.Equals("white"))
                {
                    i++;
                    continue;
                }

                tempBlocks = checkPieces(i).Cast<string>().ToList();
                foreach(string block in tempBlocks)
                {
                    string test = piece.Name;
                    blocks.Add(block);
                }
                i++;

            }

            blocks.Distinct().ToList();

            ArrayList finalBlocks = new ArrayList(blocks);

            foreach (string block in finalBlocks)
            {
                //to highlight all the possible spaces that the piece can move to on the board
                int column = (int)char.GetNumericValue(block[1]) - 1;
                int row = AlphabetService.GetIndexFromCharacter(block[0].ToString()) - 1;
                Button button = (Button)chessBoard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == column && Grid.GetColumn(e) == row);
                button.Background = new SolidColorBrush(Colors.DeepPink);
            }

            whiteAttackBlocks.Clear();
            whiteAttackBlocks = finalBlocks;
        }

        //asserts the blocks that black can attack on
        public void AssertBlackAttackBlocks()
        {
            List<string> blocks = new List<string>();
            List<string> tempBlocks = new List<string>();

            int i = 0;

            foreach (Piece piece in occupied)
            {
                if (piece == null)
                {
                    i++;
                    continue;
                }
                if (!occupied[i].Colour.Equals("black"))
                {
                    i++;
                    continue;
                }

                tempBlocks = checkPieces(i).Cast<string>().ToList();
                foreach (string block in tempBlocks)
                {
                    string test = piece.Name;
                    blocks.Add(block);
                }
                i++;

            }

            blocks.Distinct().ToList();

            ArrayList finalBlocks = new ArrayList(blocks);

            foreach (string block in finalBlocks)
            {
                //to highlight all the possible spaces that the piece can move to on the board
                int column = (int)char.GetNumericValue(block[1]) - 1;
                int row = AlphabetService.GetIndexFromCharacter(block[0].ToString()) - 1;
                Button button = (Button)chessBoard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == column && Grid.GetColumn(e) == row);
                button.Background = new SolidColorBrush(Colors.Purple);
            }

            blackAttackBlocks.Clear();
            blackAttackBlocks = finalBlocks;
        }

        //checks if the black king is safe
        public void IsBlackKingSafe()
        {
            //checks if the king is in one of the blocks that the other colour could potentially attack on
            foreach (string position in whiteAttackBlocks)
            {
                //runs the code if the king is in one of the positions that the other colour could potentially attack on
                if (position.Equals(blackKing.Position))
                {
                    Check("black");
                }
            }
        }

        //checks if the white king is safe
        public void IsWhiteKingSafe()
        {
            //checks if the king is in one of the blocks that the other colour could potentially attack on
            foreach (string position in blackAttackBlocks)
            {
                //runs the code if the king is in one of the positions that the other colour could potentially attack on
                if (position.Equals(whiteKing.Position))
                {
                    Check("white");
                }
            }
        }

        public void Check(string colour)
        {
            //to store the blocks
            List<string> blocks = new List<string>();
            List<string> tempBlocks = new List<string>();

            //to iterate over the elements in the array
            int i = 0;

            //iterates over the elements in the occupied array
            foreach (Piece piece in occupied)
            {
                //checks if the element is initialized or not
                if (piece == null)
                {
                    i++;
                    continue;
                }
                //checks if the piece at the element in the array matches the colour of the king or not
                if (!occupied[i].Colour.Equals(colour))
                {
                    i++;
                    continue;
                }

                tempBlocks = checkPieces(i).Cast<string>().ToList();
                foreach (string block in tempBlocks)
                {
                    blocks.Add(block);
                }
                i++;

            }

            blocks.Distinct().ToList();
        }

        public bool isPawnFarRight(int index)
        {
            return index == 7? true: index == 15? true: index == 23? true: index == 31? true: index == 39 ? true : 
                index == 47 ? true : index == 55 ? true : index == 63 ? true : false;
        }

        public bool isOnlyCheck(List<string> blocks, int index)
        {
            //to store the initial position of the piece
            string initialPosition = occupied[index].Position;

            //iterates over every possible move the current piece could make to get the king out of check
            foreach (string block in blocks)
            {
                //temporarily changes the position that the current piece is on
                occupied[index].Position = block;
                //asserts the blocks that both the colours could attack on
                AssertBlackAttackBlocks();
                AssertWhiteAttackBlocks();

                //stores how many pieces can attack the king
                int amountOfChecks = 0;


                if (occupied[index].Colour.Equals("white"))
                {
                    foreach (string position in blackAttackBlocks)
                    {
                        if (!position.Equals(whiteKing.Position))
                        {
                            continue;
                        }
                        else
                        {
                            amountOfChecks++;
                            break;
                        }
                    }
                }
                else if (occupied[index].Colour.Equals("black"))
                {
                    foreach (string position in whiteAttackBlocks)
                    {
                        if (!position.Equals(blackKing.Position))
                        {
                            continue;
                        }
                        else
                        {
                            amountOfChecks++;
                            break;
                        }
                    }
                }
                if (amountOfChecks > 0)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public bool isKingDead()
        {
            int j = 0;
            for(int i = 0; i < 64; i++)
            {
                if (occupied[i] != null)
                {
                    if (occupied[i].Name.Equals(blackKing.Name) || occupied[i].Name.Equals(whiteKing.Name))
                    {
                        j++;
                    }
                }                
            }
            if(j == 2)
            {
                return false;
            }
            return true;
        }
    }
}
