using Alphabet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Rook : Piece
    {
        public ArrayList Movement(int upperLimit, int lowerLimit, int leftLimit, int rightLimit)
        {
            //to store all the possible addresses that the rook can move to
            ArrayList movement = new ArrayList();
            //to get the character from the current position of the rook
            char character = Position[0];
            //to get alphabetical index of the character in the current position of the rook
            int characterIndex = AlphabetService.GetIndexFromCharacter(character.ToString());
            int startingCharacterIndex = characterIndex;
            int number = (int)char.GetNumericValue(Position[1]);

            //to return all the available positions to the left of the rook
            for (int i = characterIndex; i > leftLimit; i--)
            {
                string leftString = AlphabetService.GetCharacterFromIndex((i % 8) + 1) + number.ToString();
                movement.Add(leftString);
            }

            //to return all the available positions to the right of the rook
            for (int i = characterIndex; i < rightLimit; i++)
            {
                string rightString = AlphabetService.GetCharacterFromIndex((i % 8) + 1) + number.ToString();
                movement.Add(rightString);
            }

            //to return all the available positions to the top of the rook
            for (int i = number; i > upperLimit; i-=8)
            {
                string topString = character.ToString() + ((int)(i / 8)).ToString();
                movement.Add(topString);
            }

            //to return all the available positions to the bottom of the rook
            for (int i = number; i < lowerLimit; i+=8)
            {
                string bottomString = character.ToString() + ((int)(i / 8)).ToString();
                movement.Add(bottomString);
            }

            return movement;

        }

        void attack(string colour, string position)
        {
            colour = Colour;
            position = Position;
        }
    }
}
