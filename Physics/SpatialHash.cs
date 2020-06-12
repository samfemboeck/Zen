namespace Zen
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Zen.Util;

    public class SpatialHash
    {
        public Rectangle GridBounds = new Rectangle();
      
        int _cellSize;

        float _inverseCellSize;

        IntIntDictionary _cellDict = new IntIntDictionary();

        HashSet<Collider> _tempHashset = new HashSet<Collider>();


        public SpatialHash(int cellSize = 100)
        {
            _cellSize = cellSize;
            _inverseCellSize = 1f / _cellSize;
        }

        Point CellCoords(int x, int y)
        {
            return new Point(Mathf.FloorToInt(x * _inverseCellSize), Mathf.FloorToInt(y * _inverseCellSize));
        }

        Point CellCoords(float x, float y)
        {
            return new Point(Mathf.FloorToInt(x * _inverseCellSize), Mathf.FloorToInt(y * _inverseCellSize));
        }

        List<Collider> CellAtPosition(int x, int y, bool createCellIfEmpty = false)
        {
            List<Collider> cell = null;
            if (!_cellDict.TryGetValue(x, y, out cell))
            {
                if (createCellIfEmpty)
                {
                    cell = new List<Collider>();
                    _cellDict.Add(x, y, cell);
                }
            }

            return cell;
        }

        public void Add(Collider collider)
        {
            var bounds = collider.BroadphaseBounds;
            var p1 = CellCoords(bounds.X, bounds.Y);
            var p2 = CellCoords(bounds.Right, bounds.Bottom);

            // update our bounds to keep track of our grid size
            if (!GridBounds.Contains(p1))
                RectangleEx.Union(ref GridBounds, ref p1, out GridBounds);

            if (!GridBounds.Contains(p2))
                RectangleEx.Union(ref GridBounds, ref p2, out GridBounds);

            for (var x = p1.X; x <= p2.X; x++)
            {
                for (var y = p1.Y; y <= p2.Y; y++)
                {
                    // we need to create the cell if there is none
                    var c = CellAtPosition(x, y, true);
                    c.Add(collider);
                }
            }
        }

        public void RemoveWithinBounds(Collider collider)
        {
            RectangleF bounds = collider.BroadphaseBounds;
            var p1 = CellCoords(bounds.X, bounds.Y);
            var p2 = CellCoords(bounds.Right, bounds.Bottom);

            for (var x = p1.X; x <= p2.X; x++)
            {
                for (var y = p1.Y; y <= p2.Y; y++)
                {
                    // the cell should always exist since this collider should be in all queryed cells
                    var cell = CellAtPosition(x, y);
                    if (cell != null)
                        cell.Remove(collider);
                }
            }
        }

        public void RemoveBruteForce(Collider collider) => _cellDict.Remove(collider);

        public bool BoxCast(RectangleF box, Collider excludeCollider, out HashSet<Collider> colliders, int collisionLayer = 0)
        {
            _tempHashset.Clear();

            var p1 = CellCoords(box.X, box.Y);
            var p2 = CellCoords(box.Right, box.Bottom);

            for (var x = p1.X; x <= p2.X; x++)
            {
                for (var y = p1.Y; y <= p2.Y; y++)
                {
                    var cell = CellAtPosition(x, y);
                    if (cell == null)
                        continue;

                    // we have a cell. loop through and fetch all the Colliders
                    for (var i = 0; i < cell.Count; i++)
                    {
                        var collider = cell[i];

                        // skip this collider if it is our excludeCollider or if it doesnt match our layerMask
                        if (collider == excludeCollider)
                            continue;

                        if (collisionLayer != 0 && !collider.CollisionLayer.HasFlag((CollisionLayer)collisionLayer))
                            continue;

                        if (box.Intersects(collider.BroadphaseBounds))
                            _tempHashset.Add(collider);
                    }
                }
            }

            colliders = _tempHashset;
            return _tempHashset.Count > 0;
        }

        class IntIntDictionary
        {
            Dictionary<long, List<Collider>> _store = new Dictionary<long, List<Collider>>();

            long GetKey(int x, int y)
            {
                return unchecked((long)x << 32 | (uint)y);
            }

            public void Add(int x, int y, List<Collider> list)
            {
                _store.Add(GetKey(x, y), list);
            }

            public void Remove(Collider obj)
            {
                foreach (var list in _store.Values)
                {
                    if (list.Contains(obj))
                        list.Remove(obj);
                }
            }

            public bool TryGetValue(int x, int y, out List<Collider> list)
            {
                return _store.TryGetValue(GetKey(x, y), out list);
            }
        }
    }
}