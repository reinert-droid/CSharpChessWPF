using Alphabet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    internal class Bishop : Piece
    {
        public ArrayList Movement(int bottomRight, int bottomLeft, int upperLeft, int upperRight, int index)
        {
            //to store all the possible addresses that the bishop can move to
            ArrayList movement = new ArrayList();

            //to return all the available positions to the left up diagonal of the bishop
            for (int i = index; i >= upperLeft; i -= 9)
            {
                string leftUp = AlphabetService.GetCharacterFromIndex((i % 8) + 1).ToString() + ((i / 8) + 1).ToString();
                movement.Add(leftUp);
            }

            //to return all the available positions to the right up diagonal of the rook
            for (int i = index; i >= upperRight; i -= 7)
            {
                string rightUp = AlphabetService.GetCharacterFromIndex((i % 8) + 1).ToString() + ((i / 8) + 1).ToString();
                movement.Add(rightUp);
            }

            //to return all the available positions to the right down diagonal of the rook
            for (int i = index; i <= bottomRight; i+=9)
            {
                string rightDown = AlphabetService.GetCharacterFromIndex((i % 8) + 1).ToString() + ((i / 8) + 1).ToString();
                movement.Add(rightDown);
            }

            //to return all the available positions to the left down diagonal of the rook
            for (int i = index; i <= bottomLeft; i+=7)
            {
                string leftDown = AlphabetService.GetCharacterFromIndex((i % 8) + 1).ToString() + ((i / 8) + 1).ToString();
                movement.Add(leftDown);
            }

            //to store a list of movements with the position of the clicked on piece removed
            ArrayList finalMovements = new ArrayList();
            foreach (string move in movement)
            {
                if (move.Equals(Position))
                {
                    continue;
                }
                else
                {
                    finalMovements.Add(move);
                }
            }

            return finalMovements;
        }
    }
}
