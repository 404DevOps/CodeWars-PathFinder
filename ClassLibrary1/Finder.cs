using System.Collections.Generic;
using System.Linq;

namespace PathFinder 
{
    public class Finder
    {
        public static int PathFinder(string mazeString)
        {
            List<(int x, int y)> maze = GetCoordinates(mazeString);
            (int x, int y) exitPos = (maze.Last().x, maze.Last().y);

            Queue<(int x, int y, int distance)> openList = new();
            openList.Enqueue((0,0,0));
            HashSet<(int x, int y)> closedList = new();

            while (openList.Count > 0)
            {
                var current = openList.Dequeue();
                closedList.Add((current.x, current.y));

                if (current.x == exitPos.x && current.y == exitPos.y) { return current.distance; }
                foreach (var item in GetAdjacent(current, maze, closedList))
                {
                    openList.Enqueue(item);
                }
            }

            return -1;
        }

        public static List<(int x, int y, int distance)> GetAdjacent((int x, int y, int distance) currentNode, List<(int x, int y)> maze, HashSet<(int x, int y)> closedList)
        {
            List<(int x, int y, int distance)> results = new ();
            int newDist = currentNode.distance + 1;

            foreach (var node in maze)
            {
                if (closedList.Contains(node)) continue;
                if ((node.y == currentNode.y && (node.x == currentNode.x + 1 || node.x == currentNode.x - 1)) || 
                     node.x == currentNode.x && (node.y == currentNode.y - 1 || node.y == currentNode.y + 1))
                {
                    results.Add((node.x, node.y, newDist));
                    closedList.Add((node.x, node.y));
                }
            }

            return results;
        }

        internal static List<(int x, int y)> GetCoordinates(string maze)
        {
            List<(int x, int y)> result = new();
            List<string> rows = maze.Split("\n").ToList();

            int yPos = 0;
            foreach (string row in rows)
            {
                int xPos = 0;
                foreach (char pos in row)
                {
                    if (pos != 'W'){
                        result.Add((xPos, yPos));
                    }
                    xPos++;
                }
                yPos++;
            }
            return result;
        }

        public static int PathFinderSimple(string mazeString)
        {
            int moves = 0;
            List<(int x, int y)> maze = GetCoordinates(mazeString);
            (int x, int y) exitPos = (maze.Last().x, maze.Last().y);
            (int x, int y) currentPos = (0, 0);

            while (currentPos.x != exitPos.x || currentPos.y != exitPos.y)
            {
                int nextX = currentPos.x + 1;
                int nextY = currentPos.y + 1;
                //try move right
                if (maze.FirstOrDefault(pos => pos.x == nextX && pos.y == currentPos.y) != default)
                {
                    moves++;
                    currentPos.x++;
                }
                //try move down
                else if (maze.FirstOrDefault(pos => pos.y == nextY && pos.x == currentPos.x) != default)
                {
                    moves++;
                    currentPos.y++;
                }
                //couldnt move right, couldnt move down == no way to exit.
                else
                {
                    return -1;
                }
            }

            return moves;
        }
    }
}
