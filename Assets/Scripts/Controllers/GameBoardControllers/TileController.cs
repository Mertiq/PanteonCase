using Extensions;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.GameBoardControllers
{
    public class TileController : MonoBehaviour, IRightClickable
    {
        public bool isWalkable;
        public Vector2 position;
        public PathfindingTile pathfindingTile = new();

        private SpriteRenderer spriteRenderer;
        
        private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

        private void Start() => isWalkable = true;

        public void Setup(float x, float y, int id)
        {
            var pos = new Vector3(x, y);
            transform.SetLocalPositionAndRotation(pos, Quaternion.identity);
            name = $"Tile ({x}, {y})";
            position = pos;
        }

        public void OnRightClick()
        {
            if (!isWalkable) return;
            GameBoardSignals.Instance.onTileSelected.Invoke(this);
        }

        public void Paint(Color color) => spriteRenderer.color = color;
    }
}