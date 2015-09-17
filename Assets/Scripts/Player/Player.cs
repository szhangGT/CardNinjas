using Assets.Scripts.Util;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Player
{
    public class Player : Character
    {
        public GridNode currHex;
        
        private bool hasStart = false;

        void Start()
        {
            grid = FindObjectOfType<GridManager>().Grid;
            currHex = grid[rowStart, colStart];
            currHex.Owner = this;
        }

        void Update()
        {
            movementCheck();
            transform.position = currHex.transform.position;
        }

        //MovementCheck does all the necessary keyPress checks and counts which ones are true. 
        //Then it calculates the moved hex in batch form, and returns the end result.
        void movementCheck()
        {
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            if (!hasStart)
            {
                currHex = grid[rowStart, colStart];
                hasStart = true;
            }
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Up))
                up = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Down))
                down = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Left))
                left = true;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Right))
                right = true;


            if (up || down || left || right)
            {
                if (up)
                {
                    if (currHex.panelExists(Enums.Direction.Up))
                    {
                        currHex.clearOccupied();
                        currHex = currHex.Up;
                    }
                }
                if (down)
                {
                    if (currHex.panelExists(Enums.Direction.Down))
                    {
                        currHex = currHex.Down;
                    }
                }
                if (left)
                {
                    if (currHex.panelExists(Enums.Direction.Left))
                    {
                        currHex.clearOccupied();
                        currHex = currHex.Left;
                    }
                }
                if (right)
                {
                    if (currHex.panelExists(Enums.Direction.Right))
                    {
                        currHex.clearOccupied();
                        currHex = currHex.Right;
                    }
                }
                currHex.Owner = (this);
            }
        }
    }
}
