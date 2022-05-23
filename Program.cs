using System;

namespace CS_Postman_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            var randomPointManager =  new RandomPointManager();
            randomPointManager.GenerateRandomPoints(11, 0.5f, 0.5f);
            randomPointManager.GetPermutationOfPoints();
            randomPointManager.GetShortestPath();
            Console.WriteLine(DateTime.Now);
        }
    }
}
