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
        public ArrayList Movement
        {
            get
            {
                ArrayList arrayList = new ArrayList();
                //checks what colour the pawn is
                if (Colour == "white" && Position[1] != '7')
                {
                    arrayList.Add(Position[0] + (char.GetNumericValue(Position[1]) - 1).ToString());
                    return arrayList;
                }
                else if (Colour == "black" && Position[1] != '2')
                {
                    arrayList.Add(Position[0] + (char.GetNumericValue(Position[1]) + 1).ToString());
                    return arrayList;
                }
                else if (Colour == "white" && Position[1] == '7')
                {
                    arrayList.Add(Position[0] + (char.GetNumericValue(Position[1]) - 1).ToString());
                    arrayList.Add(Position[0] + (char.GetNumericValue(Position[1]) - 2).ToString());
                    return arrayList;
                }
                else if (Colour == "black" && Position[1] == '2')
                {
                    arrayList.Add(Position[0] + (char.GetNumericValue(Position[1]) + 1).ToString());
                    arrayList.Add(Position[0] + (char.GetNumericValue(Position[1]) + 2).ToString());
                    return arrayList;
                }
                return arrayList;
            }
        }

        public string TwoMovement()
        {
            //checks what colour the pawn is
            if (Colour == "white")
            {
                return int.Parse((Position[1] + 2).ToString()).ToString() + Position[0];
            }
            else if (Colour == "black")
            {
                return int.Parse((Position[1] - 2).ToString()).ToString() + Position[0];
            }
            return "pawn";
        }

        public string[] attack()
        {
            if (Colour == "white")
            {
                int left = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string leftWhiteAttack = int.Parse((Position[1] + 1).ToString()).ToString() + AlphabetService.GetCharacterFromIndex(left);

                int right = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string rightWhiteAttack = int.Parse((Position[1] + 1).ToString()).ToString() + AlphabetService.GetCharacterFromIndex(right);

                string[] whiteAttack = { leftWhiteAttack, rightWhiteAttack };

                return whiteAttack;
            }
            else if (Colour == "black")
            {
                int left = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) - 1;
                string leftBlackAttack = int.Parse((Position[1] - 1).ToString()).ToString() + AlphabetService.GetCharacterFromIndex(left);

                int right = AlphabetService.GetIndexFromCharacter(Position[0].ToString()) + 1;
                string rightBlackAttack = int.Parse((Position[1] - 1).ToString()).ToString() + AlphabetService.GetCharacterFromIndex(right);

                string[] blackAttack = { leftBlackAttack, rightBlackAttack };

                return blackAttack;
            }

            string[] error = { "pawn attack", Colour };
            return error;
        }
    }
}
