using System;
using System.Collections.Generic;

namespace abc
{
    class Program
    {
        /*private static int Printer(int j)
        {
            for (var i = j; i > 0; i = Printer(i - 1))
            {
                Console.Write(i);
            }
            return j;
        }*/
             

        public static void Main(string[] args)
        {
            /*Printer(2);*/

            /* // */


            /*int[] n;

            n.GetUpperBound(0)+1;
            n.LongLength;*/


            /*int n = 2;
            for (int i = 1; i <= 6; i++){
                for (int j = 1; j <= 6; j++){
                    if (i != j) { n--; }
                }
            }
            Console.WriteLine(n);*/


            /*Console.WriteLine(1/2==1/3);*/
            /*Console.WriteLine('1'+'2'=="12");
            /*Console.WriteLine((0, 0) == (0, 0));*/
            /*Console.WriteLine(!!true);*/
            /*Console.WriteLine(' ' == 32);*/
            /* Console.WriteLine(false || true);*/
            /*Console.WriteLine(' ' == " ");*/
            /*Console.WriteLine(null == new object());*/
            /*Console.WriteLine(true == 1);*/
            /*Console.WriteLine(2f+2d==4);*/
            /*Console.WriteLine((new object()) == (new object()));*/


            /*int fun(int n)
            {
                if (n < 2) return n;
                *//*if (n % 2 == 0) return fun(n - 3) + 1;*//*
                else return fun(n - 2);
            }
            Console.WriteLine(fun(6));*/


            /*int[] a = new int[] { 5, 6, 7, 8 };
            int[] b = new int[] { 1, 2, 3, 4 };

            Stack<int> S = new Stack<int>(a);
            Queue<int> Q = new Queue<int>(b);

            S.Push(Q.Peek()); Q.Dequeue();
            S.Push(Q.Peek()); Q.Dequeue();
            S.Push(Q.Peek()); Q.Dequeue();
            Q.Enqueue(S.Peek()); S.Pop();
            Q.Enqueue(S.Peek()); S.Pop();
            S.Push(Q.Peek()); Q.Dequeue();
            

            foreach (var sex in S)
            {
                Console.WriteLine(sex);
            }
            Console.WriteLine();
            foreach (var qtz in Q)
            {
                Console.WriteLine(qtz);
            }*/
        }
        /*public class ThisUsage
        {
            int planets;
            static int suns;

            public void gaze()
            {
                int i;
                i = this.suns;
            }

        }*/
    }

}
