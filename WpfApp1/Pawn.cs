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

        public ArrayList Attack( bool canAttackLeft, bool canAttackRight, int index)
        {
            if (Colour == "white")
            {
                ArrayList whiteAttack = new ArrayList();

                if (canAttackLeft)
                {
                    whiteAttack.Add(((index - 9) % 8).ToString() + ((index - 9) / 8).ToString());
                }
                if (canAttackRight)
                {
                    whiteAttack.Add(((index - 7) % 8).ToString() + ((index - 7) / 8).ToString());
                }

                return whiteAttack;
            }
            else if (Colour == "black")
            {
                ArrayList blackAttack = new ArrayList();

                if (canAttackLeft)
                {
                    blackAttack.Add(((index + 7) % 8).ToString() + ((index + 7) / 8).ToString());
                }
                if (canAttackRight)
                {
                    blackAttack.Add(((index + 9) % 8).ToString() + ((index + 9) / 8).ToString());
                }

                return blackAttack;
            }

            ArrayList emptyList = new ArrayList ();
            return emptyList;
        }
    }
}
