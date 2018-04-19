using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using te16mono.LevelBuilder;

namespace te16mono.Input
{
    //Anton
    static class TextInput
    {
        public static string CheckForInput(KeyboardState keyboard, KeyboardState lastKeyboard, string input, int position)
        {
            string toAdd = "";
            if (keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift))
            {
                toAdd += CheckLetters(keyboard, lastKeyboard);
            }
            else
            {
                toAdd += CheckLetters(keyboard, lastKeyboard);
                toAdd = toAdd.ToLower();
            }
            toAdd += CheckNumbers(keyboard, lastKeyboard);

            if (keyboard.IsKeyDown(Keys.Space) && lastKeyboard.IsKeyUp(Keys.Space))
            {
                toAdd += " ";
            }
            
            return input = input.Insert(position, toAdd);
        }
        public static string CheckForNumberInput(KeyboardState keyboard, KeyboardState lastKeyboard, string input, int position)
        {
            string toAdd = "";
            toAdd += CheckNumbers(keyboard, lastKeyboard);
            return input = input.Insert(position, toAdd);
        }
        static string CheckLetters(KeyboardState keyboard, KeyboardState lastKeyboard)
        {
            if (keyboard.IsKeyDown(Keys.A) && lastKeyboard.IsKeyUp(Keys.A))
            {
                return "A";
            }
            else if (keyboard.IsKeyDown(Keys.B) && lastKeyboard.IsKeyUp(Keys.B))
            {
                return "B";
            }
            else if (keyboard.IsKeyDown(Keys.C) && lastKeyboard.IsKeyUp(Keys.C))
            {
                return "C";
            }
            else if (keyboard.IsKeyDown(Keys.D) && lastKeyboard.IsKeyUp(Keys.D))
            {
                return "D";
            }
            else if (keyboard.IsKeyDown(Keys.E) && lastKeyboard.IsKeyUp(Keys.E))
            {
                return "E";
            }
            else if (keyboard.IsKeyDown(Keys.F) && lastKeyboard.IsKeyUp(Keys.F))
            {
                return "F";
            }
            else if (keyboard.IsKeyDown(Keys.G) && lastKeyboard.IsKeyUp(Keys.G))
            {
                return "G";
            }
            else if (keyboard.IsKeyDown(Keys.H) && lastKeyboard.IsKeyUp(Keys.H))
            {
                return "H";
            }
            else if (keyboard.IsKeyDown(Keys.I) && lastKeyboard.IsKeyUp(Keys.I))
            {
                return "I";
            }
            else if (keyboard.IsKeyDown(Keys.J) && lastKeyboard.IsKeyUp(Keys.J))
            {
                return "J";
            }
            else if (keyboard.IsKeyDown(Keys.K) && lastKeyboard.IsKeyUp(Keys.K))
            {
                return "K";
            }
            else if (keyboard.IsKeyDown(Keys.L) && lastKeyboard.IsKeyUp(Keys.L))
            {
                return "L";
            }
            else if (keyboard.IsKeyDown(Keys.M) && lastKeyboard.IsKeyUp(Keys.M))
            {
                return "M";
            }
            else if (keyboard.IsKeyDown(Keys.N) && lastKeyboard.IsKeyUp(Keys.N))
            {
                return "N";
            }
            else if (keyboard.IsKeyDown(Keys.O) && lastKeyboard.IsKeyUp(Keys.O))
            {
                return "O";
            }
            else if (keyboard.IsKeyDown(Keys.P) && lastKeyboard.IsKeyUp(Keys.P))
            {
                return "P";
            }
            else if (keyboard.IsKeyDown(Keys.Q) && lastKeyboard.IsKeyUp(Keys.Q))
            {
                return "Q";
            }
            else if (keyboard.IsKeyDown(Keys.R) && lastKeyboard.IsKeyUp(Keys.R))
            {
                return "R";
            }
            else if (keyboard.IsKeyDown(Keys.S) && lastKeyboard.IsKeyUp(Keys.S))
            {
                return "S";
            }
            else if (keyboard.IsKeyDown(Keys.T) && lastKeyboard.IsKeyUp(Keys.T))
            {
                return "T";
            }
            else if (keyboard.IsKeyDown(Keys.U) && lastKeyboard.IsKeyUp(Keys.U))
            {
                return "U";
            }
            else if (keyboard.IsKeyDown(Keys.V) && lastKeyboard.IsKeyUp(Keys.V))
            {
                return "V";
            }
            else if (keyboard.IsKeyDown(Keys.W) && lastKeyboard.IsKeyUp(Keys.W))
            {
                return "W";
            }
            else if (keyboard.IsKeyDown(Keys.X) && lastKeyboard.IsKeyUp(Keys.X))
            {
                return "X";
            }
            else if (keyboard.IsKeyDown(Keys.Y) && lastKeyboard.IsKeyUp(Keys.Y))
            {
                return "Y";
            }
            else if (keyboard.IsKeyDown(Keys.Z) && lastKeyboard.IsKeyUp(Keys.Z))
            {
                return "Z";
            }

            return "";
        }

        public static string CheckNumbers(KeyboardState keyboardState, KeyboardState lastKeyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.NumPad0) && lastKeyboardState.IsKeyDown(Keys.NumPad0) == false || keyboardState.IsKeyDown(Keys.D0) && lastKeyboardState.IsKeyDown(Keys.D0) == false)
            {
                return "0";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad1) && lastKeyboardState.IsKeyDown(Keys.NumPad1) == false || keyboardState.IsKeyDown(Keys.D1) && lastKeyboardState.IsKeyDown(Keys.D1) == false)
            {
                return "1";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad2) && lastKeyboardState.IsKeyDown(Keys.NumPad2) == false || keyboardState.IsKeyDown(Keys.D2) && lastKeyboardState.IsKeyDown(Keys.D2) == false)
            {
                return "2";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad3) && lastKeyboardState.IsKeyDown(Keys.NumPad3) == false || keyboardState.IsKeyDown(Keys.D3) && lastKeyboardState.IsKeyDown(Keys.D3) == false)
            {
                return "3";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad4) && lastKeyboardState.IsKeyDown(Keys.NumPad4) == false || keyboardState.IsKeyDown(Keys.D4) && lastKeyboardState.IsKeyDown(Keys.D4) == false)
            {
                return "4";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad5) && lastKeyboardState.IsKeyDown(Keys.NumPad5) == false || keyboardState.IsKeyDown(Keys.D5) && lastKeyboardState.IsKeyDown(Keys.D5) == false)
            {
                return "5";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad6) && lastKeyboardState.IsKeyDown(Keys.NumPad6) == false || keyboardState.IsKeyDown(Keys.D6) && lastKeyboardState.IsKeyDown(Keys.D6) == false)
            {
                return "6";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad7) && lastKeyboardState.IsKeyDown(Keys.NumPad7) == false || keyboardState.IsKeyDown(Keys.D7) && lastKeyboardState.IsKeyDown(Keys.D7) == false)
            {
                return "7";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad8) && lastKeyboardState.IsKeyDown(Keys.NumPad8) == false || keyboardState.IsKeyDown(Keys.D8) && lastKeyboardState.IsKeyDown(Keys.D8) == false)
            {
                return "8";
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad9) && lastKeyboardState.IsKeyDown(Keys.NumPad9) == false || keyboardState.IsKeyDown(Keys.D9) && lastKeyboardState.IsKeyDown(Keys.D9) == false)
            {
                return "9";
            }
            else if (keyboardState.IsKeyDown(Keys.Decimal) && lastKeyboardState.IsKeyDown(Keys.Decimal) == false || keyboardState.IsKeyDown(Keys.OemComma) && lastKeyboardState.IsKeyDown(Keys.OemComma) == false)
            {
                return ",";
            }
            else if (keyboardState.IsKeyDown(Keys.OemMinus) && lastKeyboardState.IsKeyDown(Keys.OemMinus) == false || keyboardState.IsKeyDown(Keys.Subtract) && lastKeyboardState.IsKeyDown(Keys.Subtract) == false)
            {
                return "-";
            }
            return "";
        }

        public static string CheckForBackSpace(KeyboardState keyboardState, KeyboardState lastKeyboardState, string input, int position)
        {
            if (input.Length > 0 && keyboardState.IsKeyDown(Keys.Back) && lastKeyboardState.IsKeyDown(Keys.Back) == false && position > 0)
            {
                if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                {
                    return input.Remove(0, position);
                }
                else
                {
                    return input.Remove(position - 1, 1);
                }
            }
            return input;
        }
        public static int AdjustPosition(string newString, string oldString)
        {
            if (newString != oldString)
            {
                if (oldString == "")
                {
                    return newString.Length;
                }
                else
                    return newString.Length - oldString.Length;
            }
            else
                return 0;
        }
        public static int CheckEditPosition(int position, int stringLenght)
        {
            if (MainLevelBuilder.keyboardState.IsKeyDown(Keys.Left) && MainLevelBuilder.lastKeyboardState.IsKeyUp(Keys.Left) && position > 0)
            {
                position--;
            }
            else if ((MainLevelBuilder.keyboardState.IsKeyDown(Keys.Right) && MainLevelBuilder.lastKeyboardState.IsKeyUp(Keys.Right) && position < stringLenght))
            {
                position++;
            }
            return position;
        }
    }

    
}