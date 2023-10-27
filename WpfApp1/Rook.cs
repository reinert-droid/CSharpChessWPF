using Alphabet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace WpfApp1
{
    internal class Rook : Piece
    {
        public ArrayList Movement(int upperLimit, int lowerLimit, int leftLimit, int rightLimit, int index)
        {
            //to store all the possible addresses that the rook can move to
            ArrayList movement = new ArrayList();
            //to get the character from the current position of the rook
            char character = Position[0];

            int number = (int)char.GetNumericValue(Position[1]);

            //to return all the available positions to the left of the rook
            for (int i = index; i >= leftLimit; i--)
            {
                string leftString = AlphabetService.GetCharacterFromIndex((i % 8) + 1) + number.ToString();

                if (leftString.Length == 1)
                {
                    continue;
                }

                movement.Add(leftString);
            }

            //to return all the available positions to the right of the rook
            for (int i = index; i <= rightLimit; i++)
            {
                string rightString = AlphabetService.GetCharacterFromIndex((i % 8) + 1) + number.ToString();

                if (rightString.Length == 1)
                {
                    continue;
                }

                movement.Add(rightString);
            }

            //to return all the available positions to the top of the rook
            for (int i = index; i >= upperLimit; i-=8)
            {
                string topString = character.ToString() + ((int)(i / 8) + 1).ToString();

                if (topString.Contains("0"))
                {
                    continue;
                }

                movement.Add(topString);
            }

            //to return all the available positions to the bottom of the rook
            for (int i = index; i <= lowerLimit; i+=8)
            {
                string bottomString = character.ToString() + ((int)(i / 8) + 1).ToString();

                if (bottomString.Contains("9"))
                {
                    continue;
                }

                movement.Add(bottomString);
            }

            //to store a list of movements with the position of the clicked on piece removed
            ArrayList finalMovements = new ArrayList();
            foreach(string move in movement)
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
