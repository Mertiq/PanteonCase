using Controllers.GameBoardControllers;

namespace Extensions
{
    public readonly struct PathfindingTile
    {
        public readonly TileController parent;
        public readonly float gCost;
        public readonly float hCost;
        public float FCost => gCost + hCost;

        public PathfindingTile(TileController parent, float gCost, float hCost) : this()
        {
            this.parent = parent;
            this.gCost = gCost;
            this.hCost = hCost;
        }
    }
}