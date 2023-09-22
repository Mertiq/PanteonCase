using Controllers;

namespace Extensions
{
    public struct PathfindingTile
    {
        public TileController parent;
        public float gCost;
        public float hCost;
        public float FCost => gCost + hCost;

        public PathfindingTile(TileController parent, float gCost, float hCost) : this()
        {
            this.parent = parent;
            this.gCost = gCost;
            this.hCost = hCost;
        }
    }
}