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
        public ArrayList AllMovement(int upThenLeft, int upThenRight, int leftThenUp, int rightThenUp, int leftThenDown, int rightThenDown, int downThenLeft, int downThenRight, int index)
        {
            ArrayList movement = new ArrayList();
            int[] farRightIndexes = { 7, 15, 23, 31, 39, 47, 55, 63 };
            int[] almostFarRightIndexes = { 6, 14, 22, 30, 38, 46, 54, 62 };
            int[] farLeftIndexes = { 0, 8, 16, 24, 32, 40, 48, 56 };
            int[] almostFarLeftIndexes = { 1, 9, 17, 25, 33, 41, 49, 57 };


            if (upThenLeft != index)
            {
                if (index % 8 != 0)
                {
                    string upThenLeftMovement = AlphabetService.GetCharacterFromIndex(upThenLeft % 8 + 1) + ((int)((upThenLeft / 8) + 1)).ToString();
                    movement.Add(upThenLeftMovement);
                }
            }

            if (upThenRight != index)
            {
                if (!isFarRight(index))
                {
                    string upThenRightMovement = AlphabetService.GetCharacterFromIndex(upThenRight % 8 + 1) + ((int)((upThenRight / 8) + 1)).ToString();
                    movement.Add(upThenRightMovement);
                }
            }

            if (leftThenUp != index)
            {
                if (index % 8 != 0 && !isAlmostFarLeft(index))
                {
                    string leftThenUpMovement = AlphabetService.GetCharacterFromIndex(leftThenUp % 8 + 1) + ((int)((leftThenUp / 8) + 1)).ToString();
                    movement.Add(leftThenUpMovement);
                }
            }

            if (rightThenUp != index)
            {
                if(!isFarRight(index) && !isAlmostFarRight(index))
                {
                    string rightThenUpMovement = AlphabetService.GetCharacterFromIndex(rightThenUp % 8 + 1) + ((int)((rightThenUp / 8) + 1)).ToString();
                    movement.Add(rightThenUpMovement);
                }
            }

            if (leftThenDown != index)
            {
                if (index % 8 != 0 && !isAlmostFarLeft(index))
                {
                    string leftThenDownMovement = AlphabetService.GetCharacterFromIndex(leftThenDown % 8 + 1) + ((int)((leftThenDown / 8) + 1)).ToString();
                    movement.Add(leftThenDownMovement);
                }
                
            }

            if (rightThenDown != index)
            {
                if(!isAlmostFarRight(index) && !isFarRight(index))
                {
                    string rightThenDownMovement = AlphabetService.GetCharacterFromIndex(rightThenDown % 8 + 1) + ((int)((rightThenDown / 8) + 1)).ToString();
                    movement.Add(rightThenDownMovement);
                }
            }

            if (downThenLeft != index)
            {
                if(index % 8 != 0)
                {
                    string downThenLeftMovement = AlphabetService.GetCharacterFromIndex(downThenLeft % 8 + 1) + ((int)((downThenLeft / 8) + 1)).ToString();
                    movement.Add(downThenLeftMovement);
                }
            }

            if (downThenRight != index)
            {
                if (!isFarRight(index))
                {
                    string downThenRightMovement = AlphabetService.GetCharacterFromIndex(downThenRight % 8 + 1) + ((int)((downThenRight / 8) + 1)).ToString();
                    movement.Add(downThenRightMovement);
                }
            }
            return movement;
        }

        public static bool isAlmostFarLeft(int index)
        {
            return index == 1 ? true : index == 9 ? true : index == 17 ? true : index == 25 ? true : index == 33 ? true :
                index == 41 ? true: index == 49? true: index == 57? true: false ;
        }

        public static bool isAlmostFarRight(int index)
        {
            return index == 6 ? true : index == 14 ? true : index == 22 ? true : index == 30 ? true : index == 38 ? true :
                index == 46 ? true : index == 54 ? true : index == 62 ? true : false;
        }

        public static bool isFarRight(int index)
        {
            return index == 7 ? true : index == 15 ? true : index == 23 ? true : index == 31 ? true : index == 39 ? true :
                index == 47 ? true : index == 55 ? true : index == 63 ? true : false;
        }
    }
}
