using UnityEngine;
using Assets.Scripts.Player;
using Assets.Scripts.Util;

namespace Assets.Scripts.Grid
{
    public class GridNode : MonoBehaviour
    {
        private GridNode[] neighbors = new GridNode[4];
        private bool occupied = false;
        private Character panelOwner;
        private Enums.FieldType type;

        public GridNode Up
        {
            get { return neighbors[(int)Enums.Direction.Up]; }
            set { neighbors[(int)Enums.Direction.Up] = value; }
        }
        public GridNode Down
        {
            get { return neighbors[(int)Enums.Direction.Down]; }
            set { neighbors[(int)Enums.Direction.Down] = value; }
        }
        public GridNode Left
        {
            get { return neighbors[(int)Enums.Direction.Left]; }
            set { neighbors[(int)Enums.Direction.Left] = value; }
        }
        public GridNode Right
        {
            get { return neighbors[(int)Enums.Direction.Right]; }
            set { neighbors[(int)Enums.Direction.Right] = value; }
        }

        public bool Occupied
        {
            get { return occupied; }
        }

        public Character Owner
        {
            get { return panelOwner; }
            set
            {
                occupied = true;
                panelOwner = value;
            }
        }

        public Enums.FieldType Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool panelExists(Enums.Direction direction)
        {
            if (direction == Enums.Direction.Up)
                return Up != null;
            if (direction == Enums.Direction.Down)
                return Down != null;
            if (direction == Enums.Direction.Left)
                return Left != null;
            if (direction == Enums.Direction.Right)
                return Right != null;
            return false;
        }
        
        public void clearOccupied()
        {
            occupied = false;
            panelOwner = null;
        }
    }
}
