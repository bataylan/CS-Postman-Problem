using System;

namespace CS_Postman_Problem
{
    public class Point
    {
        public int id;
        public float x;
        public float y;

        public void WriteLine()
        {
            Console.WriteLine(id + " - x:" + x + " y:" + y);
        }
    }
}