using Microsoft.Xna.Framework.Input;
using Pac_Man_DesignPatterns.Utils;

namespace Pac_Man_DesignPatterns.Game
{
    public class KeyHandler
    {
        private KeyboardState aKeyBoardStateOld;
        private KeyboardState aKeyBoardStateNew;


        public Direction GetKeyInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                return Direction.Up;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                return Direction.Down;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                return Direction.Left;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                return Direction.Right;
            }

            return Direction.Nothing;
        }

        public bool GetKeyInputPressed(Keys parKey)
        {
            bool tmpIsKeyPress = false;

            aKeyBoardStateNew = Keyboard.GetState();

            if (IsKeyDown(parKey))
            {
                tmpIsKeyPress = true;
            }

            aKeyBoardStateOld = aKeyBoardStateNew;

            return tmpIsKeyPress;
        }

        private bool IsKeyDown(Keys parKey)
        {
            return (aKeyBoardStateNew.IsKeyDown(parKey) && aKeyBoardStateOld.IsKeyUp(parKey));
        }
    }
}
