using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_Postman_Problem
{
    public class RandomPointManager
    {
        public IList<Point> points;
        public IList<IEnumerable<Point>> permutatedList;
        private int permutatedListCount;
        private int permutationLong;

        public void GenerateRandomPoints(int n, float xFactor, float yFactor)
        {
            var rand = new Random();
            points = new List<Point>();

            for (int i = 0; i < n; i++)
            {
                var point = new Point();
                point.id = i;

                point.x = RandomCoordGenerator(rand, xFactor);
                point.y = RandomCoordGenerator(rand, yFactor);
                points.Add(point);

                point.WriteLine();
            }

            var negativeX = points.Where(p => p.x < 0);
            var negXCount = 0;
            if (negativeX != null)
            {
                negXCount = negativeX.Count();
            }
            Console.WriteLine("NegX Count: " + negXCount);

            var negativeY = points.Where(p => p.y < 0);
            var negYCount = 0;
            if (negativeY != null)
            {
                negYCount = negativeY.Count();
            }
            Console.WriteLine("NegY Count: " + negYCount);
        }

        public void GetPermutationOfPoints()
        {
            permutationLong = points.Count;
            permutatedList = GetPermutations(points, permutationLong).ToList();
            permutatedListCount = permutatedList.Count();
            
            Console.WriteLine("Total Permutation count: " + permutatedListCount);
        }

        public void WriteFirstNElementsPermutations(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < permutatedList.ElementAt(i).Count(); j++)
                {
                    Console.Write(permutatedList.ElementAt(i).ElementAt(j));
                }
                Console.WriteLine();
            }
        }

        public float GetShortestPath()
        {
            float shortestDistance = float.MaxValue;
            int shortestPathId = 0;
            IEnumerable<Point> shortestPath = null;
            
            for (int i = 0; i < permutatedListCount; i++)
            {
                var path = permutatedList[i];
                float pathDistance = 0;
                var pathPointOne = path.ElementAt(0);

                for (int j = 1; j < permutationLong; j++)
                {
                    var pathPointTwo = path.ElementAt(j);
                    pathDistance += GetDistance(pathPointOne, pathPointTwo);
                    pathPointOne = pathPointTwo;
                }
                
                if(shortestDistance > pathDistance)
                {
                    shortestDistance = pathDistance;
                    shortestPathId = i;
                    shortestPath = path;
                }
            }

            Console.WriteLine("Shortest distance: " + shortestDistance);
            Console.WriteLine("Shortest path is: ");
            for (int i = 0; i < shortestPath.Count(); i++)
            {
                Console.WriteLine(" " + shortestPath.ElementAt(i).id);
            }

            return shortestDistance;
        }

        public float GetDistance(Point pointOne, Point pointTwo)
        {
            return (float)Math.Sqrt(Math.Pow((pointOne.x - pointTwo.x), 2)
                + Math.Pow((pointOne.y - pointTwo.y), 2));
        }

        public float RandomCoordGenerator(Random rand, float factor)
        {
            var negativeChooser = rand.Next(0, 2);
            var randomCoord = (rand.NextDouble() * factor) * (negativeChooser == 0 ? -1 : 1);

            return (float)randomCoord;
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}