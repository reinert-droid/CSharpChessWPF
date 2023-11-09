using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Alphabet;

namespace WpfApp1
{
    internal class Pawn : Piece
    {
        public ArrayList Movement(bool canAttackLeft, bool canAttackRight, int index, int moveLimit)
        {
            ArrayList arrayList = new ArrayList();

            //checks what colour the pawn is and how far the pawn can move
            if(Colour.Equals("white"))
            {
                for (int i = 1; i <= moveLimit; i++)
                {
                    arrayList.Add(Position[0] + ((index - (8 * i)) / 8 + 1).ToString());
                }

                if (canAttackLeft)
                {
                    arrayList.Add(AlphabetService.GetCharacterFromIndex((index - 9) % 8 + 1) + ((index - 9) / 8 + 1).ToString());
                }
                if (canAttackRight)
                {
                    arrayList.Add(AlphabetService.GetCharacterFromIndex((index - 7) % 8 + 1) + ((index - 7) / 8 + 1).ToString());
                }

                return arrayList;
            }
            else if (Colour.Equals("black"))
            {
                for (int i = 1; i <= moveLimit; i++)
                {
                    arrayList.Add(Position[0] + ((index + (8 * i)) / 8 + 1).ToString());
                }

                if (canAttackLeft)
                {
                    arrayList.Add(AlphabetService.GetCharacterFromIndex((index + 7) % 8 + 1) + ((index + 7) / 8 + 1).ToString());
                }
                if (canAttackRight)
                {
                    arrayList.Add(AlphabetService.GetCharacterFromIndex((index + 9) % 8 + 1) + ((index + 9) / 8 + 1).ToString());
                }

                return arrayList;
            }

            return arrayList;

        }
    }
}
