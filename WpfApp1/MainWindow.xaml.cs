using Alphabet;
using System;
using System.Collections;
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
                //to add the lowest position if the rook can reach it
                if((lowerLimit < downStop) ==  false)
                {
                    lowerLimit += 8;
                }
                //to check if the rook is directly above another piece
                else if (occupied[lowerLimit + 8] != null)
                {
                    lowerLimit += 8;

                    //to check if the piece below the rook is the same colour or not
                    if (!occupied[lowerLimit].Colour.Equals(rook.Colour))
                    {
                        upperLimit += 8;
                    }
                }
            }
            
            //to check if the rook is in the bottom right corner
            if (64 > (index + 1))
            {
                //to check if the piece is left of another piece or in the bottom right corner
                while (index < rightStop && occupied[rightLimit + 1] == null)
                {
                    rightLimit++;
                }
                //to check if the piece is in the bottom right corner
                if ((index < rightStop) == false)
                {
                    rightLimit++;
                }
                //to check if the rook is left if another piece
                else if (occupied[rightLimit + 1] != null)
                {
                    rightLimit++;

                    //to check if the colour of piece that is right of the rook mathces the colour of the rook
                    if (!occupied[lowerLimit].Colour.Equals(rook.Colour))
                    {
                        rightLimit++;
                    }
                }
            }

            //to check if the piece is in the top left corner
            if (!((index - 1) < 0))
            {
                //to check if the piece is in the top left corner or to the right of another piece
                while (index > leftStop && occupied[leftLimit - 1] == null)
                {
                    leftLimit--;
                }
                //to check if the piece is in the top left corner
                if((index > leftStop) == false)
                {
                    leftLimit--;
                }
                //to check if the rook is to the right of another piece
                else if (occupied[leftLimit - 1] != null)
                {
                    leftLimit--;

                    //to check if the colour of the piece that is to the left of the rook matches the rook
                    if (!occupied[leftLimit].Colour.Equals(rook.Colour))
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
                //to check if the piece is in the top row
                if((upperLimit > upStop) == false)
                {
                    upperLimit -= 8;
                }
                //to check if the rook is below another piece
                else if (occupied[upperLimit - 8] != null) 
                {
                    upperLimit -= 8;

                    //to check if the colour of the piece that's above the rook matches or not
                    if (occupied[upperLimit].Colour.Equals(rook.Colour))
                    {
                        upperLimit -= 8;
                    }
                }
            }

            return rook.Movement(upperLimit, lowerLimit, leftLimit, rightLimit, index);
        }

        //returns the relevant movement that the pawn should do
        ArrayList PawnMovement(Pawn pawn)
        {
            return pawn.Movement;
        }

        //returns the relevant movement that the knight should do
        ArrayList KnightMovement(Knight knight)
        {
            //stores the current position of the knight
            string position = knight.Position;

            switch (position)
            {
                case "b8":
                    return knight.B8;
                case "b1":
                    return knight.B1;
                case "a8":
                    return knight.A1;
                case "a1":
                    return knight.A1;
                case "g1":
                    return knight.G1;
                case "g8":
                    return knight.G8;
                case "h8":
                    return knight.H8;
                case "h1":
                    return knight.H1;

                default:
                    break;
            }
            if (position.Contains("7"))
            {
                return knight.RowSeven;
            }
            else if (position.Contains("8"))
            {
                return knight.RowEight;
            }
            else if (position.Contains("2"))
            {
                return knight.RowTwo;
            }
            else if (position.Contains("1"))
            {
                return knight.RowOne;
            }
            else if (position.Contains("a"))
            {
                return knight.ColumnA;
            }
            else if (position.Contains("b"))
            {
                return knight.ColumnB;
            }
            else if (position.Contains("g"))
            {
                return knight.ColumnG;
            }
            else if (position.Contains("h"))
            {
                return knight.ColumnH;
            }

            return knight.AllMovement;
        }

        public void DisplayAddress(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(sender.ToString());
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
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_rook.png";
                    break;
                case 1:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_knight.png";
                    break;
                case 2:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_bishop.png";
                    break;
                case 3:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_queen.png";
                    break;
                case 4:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_king.png";
                    break;
                case 5:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_bishop.png";
                    break;
                case 6:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_knight.png";
                    break;
                case 7:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_rook.png";
                    break;
                case 8:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_pawn.png";
                    break;
                case 9:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_pawn.png";
                    break;
                case 10:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_pawn.png";
                    break;
                case 11:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_pawn.png";
                    break;
                case 12:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_pawn.png";
                    break;
                case 13:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_pawn.png";
                    break;
                case 14:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_pawn.png";
                    break;
                case 15:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\b_pawn.png";
                    break;
                case 48:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_pawn.png";
                    break;
                case 49:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_pawn.png";
                    break;
                case 50:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_pawn.png";
                    break;
                case 51:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_pawn.png";
                    break;
                case 52:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_pawn.png";
                    break;
                case 53:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_pawn.png";
                    break;
                case 54:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_pawn.png";
                    break;
                case 55:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_pawn.png";
                    break;
                case 56:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_rook.png";
                    break;
                case 57:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_knight.png";
                    break;
                case 58:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_bishop.png";
                    break;
                case 59:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_queen.png";
                    break;
                case 60:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_king.png";
                    break;
                case 61:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_bishop.png";
                    break;
                case 62:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_knight.png";
                    break;
                case 63:
                    source = rootDirectory + "\\..\\..\\..\\..\\Assets\\w_rook.png";
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
            bool isAttacking = false;

            //resets the colours of the chess board after the last time a piece was clicked on
            ResetChessBoard();

            //to get the information from the specific button that was clicked
            Button source = (Button)sender;

            //to get the name from the button, which is also the address of the button
            string name = source.Name;

            foreach (string address in newPositions)
            {
                if (name.Equals(address))
                {
                    // values passed are, position of the attacking piece in the occupied array, the name of the attacked
                    // piece, the imageId of the attacking piece and the index of the attacking piece
                    isAttacking = NewPosition(occupied[index], name, imageId, AddressToIndex(previousPieceName[0]));
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
                        movement = PawnMovement((Pawn)occupied[index]); break;
                    case "Rook": 
                        movement = RookMovement((Rook)occupied[index]); break;
                    default:
                        break;
                }

                //adds the new possible positions to the newPositions arraylist
                foreach(string move in movement)
                {
                    newPositions.Add(move);
                }

                //to check if the piece is attacking or not
                if(!isAttacking)
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
            }

        }

        //returns an integer that will be used to access the relevent element in the occupied array
        public int AddressToIndex(string address)
        {
            int index_1 = AlphabetService.GetIndexFromCharacter(address[0].ToString()) - 1;
            int index_2 = (int)char.GetNumericValue(address[1]) - 1;

            return index_1 + (index_2 * 8);
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

                return false;
            }
            return false;

        }
    }
}
