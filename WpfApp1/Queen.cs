using Alphabet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp1
{
    internal class Queen : Piece
    {
        ArrayList Movement()
        {
            //to store all the possible addresses that the queen can move to
            ArrayList movement = new ArrayList();
            //to get the character from the current position of the queen
            char character = Position[0];
            //to get alphabetical index of the character in the current position of the queen
            int characterIndex = AlphabetService.GetIndexFromCharacter(character.ToString());
            int number = (int)char.GetNumericValue(Position[1]);

            int j = characterIndex;

            //to return all the available positions to the left of the queen
            for (int i = number; i >= 1; i--)
            {
                string leftString = character.ToString() + i.ToString();
                movement.Add(leftString);
            }

            //to return all the available positions to the right of the queen
            for (int i = number; i <= 8; i++)
            {
                string rightString = character.ToString() + i.ToString();
                movement.Add(rightString);
            }

            //to return all the available positions to the top of the queen
            for (int i = characterIndex; i <= 8; i++)
            {
                string topString = AlphabetService.GetCharacterFromIndex(i).ToString() + Position[1].ToString();
                movement.Add(topString);
            }

            //to return all the available positions to the bottom of the queen
            for (int i = characterIndex; i >= 1; i--)
            {
                string bottomString = AlphabetService.GetCharacterFromIndex(i).ToString() + Position[1].ToString();
                movement.Add(bottomString);
            }

            //to return all the available positions to the left up diagonal of the queen
            for (int i = number; i <= 8; i++)
            {
                string leftUp = AlphabetService.GetCharacterFromIndex(j).ToString() + i.ToString();
                j--;
                movement.Add(leftUp);
            }

            j = characterIndex;

            //to return all the available positions to the right up diagonal of the queen
            for (int i = number; i <= 8; i++)
            {
                string rightUp = AlphabetService.GetCharacterFromIndex(j).ToString() + i.ToString();
                j++;
                movement.Add(rightUp);
            }

            j = characterIndex;

            //to return all the available positions to the right down diagonal of the queen
            for (int i = number; i <= 8; i++)
            {
                string rightDown = AlphabetService.GetCharacterFromIndex(j).ToString() + i.ToString();
                j--;
                movement.Add(rightDown);
            }

            j = characterIndex;

            //to return all the available positions to the left down diagonal of the queen
            for (int i = number; i >= 1; i--)
            {
                string leftDown = AlphabetService.GetCharacterFromIndex(j).ToString() + i.ToString();
                j--;
                movement.Add(leftDown);
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
