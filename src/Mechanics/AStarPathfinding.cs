using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IsometricRTS
{
    public class AStarPathfinding
    {
        private readonly Tile[,] _tiles;
        private readonly Point _mapSize;

        public AStarPathfinding()
        {
            _tiles = Globals.currentMap._tiles;
            _mapSize = Globals.currentMap.MAP_SIZE;
        }

        public List<Point> FindPath(Point start, Point goal)
        {
            var openList = new List<Node>();
            var closedList = new HashSet<Point>();

            var startNode = new Node(start, null, 0, GetHeuristic(start, goal));
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                openList.Sort((node1, node2) => node1.FCost.CompareTo(node2.FCost));
                var currentNode = openList[0];

                if (currentNode.Position == goal)
                {
                    return ReconstructPath(currentNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode.Position);

                foreach (var neighbor in GetNeighbors(currentNode.Position))
                {
                    if (closedList.Contains(neighbor))
                    {
                        continue;
                    }

                    var gCost = currentNode.GCost + 1;
                    var hCost = GetHeuristic(neighbor, goal);
                    var existingNode = openList.Find(node => node.Position == neighbor);

                    if (existingNode == null)
                    {
                        var neighborNode = new Node(neighbor, currentNode, gCost, hCost);
                        openList.Add(neighborNode);
                    }
                    else if (gCost < existingNode.GCost)
                    {
                        existingNode.Parent = currentNode;
                        existingNode.GCost = gCost;
                        existingNode.HCost = hCost;
                    }
                }
            }

            return null;
        }

        private List<Point> ReconstructPath(Node node)
        {
            var path = new List<Point>();
            while (node != null)
            {
                path.Add(node.Position);
                node = node.Parent;
            }
            path.Reverse();
            return path;
        }

        private List<Point> GetNeighbors(Point position)
        {
            var neighbors = new List<Point>();

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue; // Skip the current position
                    }

                    var neighbor = new Point(position.X + dx, position.Y + dy);
                    if (IsValidNeighbor(position, neighbor))
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }

            return neighbors;
        }

        private bool IsValidNeighbor(Point current, Point neighbor)
        {

            // Check if the neighbor is within bounds
            if (neighbor.X < 0 || neighbor.X >= _mapSize.X || neighbor.Y < 0 || neighbor.Y >= _mapSize.Y)
            {
                return false;
            }

            // Check if the neighbor tile has collision
            if (_tiles[neighbor.X, neighbor.Y].HasCollision)
            {
                return false;
            }

            // Prevent cutting corners through collision tiles
            int dx = neighbor.X - current.X;
            int dy = neighbor.Y - current.Y;

            if (Math.Abs(dx) == 1 && Math.Abs(dy) == 1)
            {
                if ((current.X + dx >= 0 && current.X + dx < _mapSize.X && _tiles[current.X + dx, current.Y].HasCollision) ||
                    (current.Y + dy >= 0 && current.Y + dy < _mapSize.Y && _tiles[current.X, current.Y + dy].HasCollision))
                {
                    return false;
                }
            }

            return true;
        }

        private int GetHeuristic(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private class Node
        {
            public Point Position { get; }
            public Node Parent { get; set; }
            public int GCost { get; set; }
            public int HCost { get; set; }
            public int FCost => GCost + HCost;

            public Node(Point position, Node parent, int gCost, int hCost)
            {
                Position = position;
                Parent = parent;
                GCost = gCost;
                HCost = hCost;
            }
        }
    }
}
