using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulation
{
    public class Polygon
    {
        public List<Point> Vertixes { get; set; }
        public int[,] AdjacencyMatrix { get; set; }
        public int[,] Diagonals;
        public double TriangulationCost = 0;

        private double[,] costs;
        private int[,] backtrack;
        private int numberOfVertixes { get; set; }

        public Polygon( int numberOfVertixs , int[,] adjMatrix , List<Point> points)
        {
            numberOfVertixes = numberOfVertixs;
            AdjacencyMatrix = adjMatrix;
            Vertixes = points;
            costs = new double[numberOfVertixs , numberOfVertixs];
            Diagonals= new int[numberOfVertixs, numberOfVertixs];
            backtrack = new int[numberOfVertixs, numberOfVertixs];
        }

        public void traingulate()
        {
            if(Vertixes.Count <= 3) { return; }

            for (int d = 0; d < numberOfVertixes; d++)
            {
                for(int i = 0 , j = d; j < numberOfVertixes; i++ , j++)
                {
                    if( j < i + 2)
                    {
                        costs[i , j] = 0;
                    }
                    else
                    {
                        costs[i, j] = int.MaxValue;
                        for (int k = i+1; k < j; k++)
                        {
                            double val =costs[i,k] + costs[k,j] + calculateCost(i, j, k);
                            if(costs[i,j] > val)
                            {
                                costs[i, j] = val;
                                backtrack[i, j] = k;
                            }
                        }
                    }
                }
            }

            TriangulationCost = costs[0, numberOfVertixes - 1]/2;
            setDiagonals();
            
        }

        private double calculateCost(int i , int j , int k)
        {
            Point p1 = Vertixes[i], p2 = Vertixes[j], p3 = Vertixes[k];
            double totalCost = 0;
            
            if(AdjacencyMatrix[i,j] !=1)
            {
                totalCost += Point.CalculateDistance(p1, p2);
            }
            
            if (AdjacencyMatrix[j , k] != 1)
            {
                totalCost += Point.CalculateDistance(p2, p3);
            }

            if (AdjacencyMatrix[i , k] != 1)
            {
                totalCost += Point.CalculateDistance(p1, p3);
            }

            return totalCost;
        }

        private void setDiagonals()
        {
            for (int i = 0; i < numberOfVertixes; i++)
            {
                for (int j = 0; j < numberOfVertixes; j++)
                {
                    if(backtrack[i , j] != 0)
                    {
                        var k = backtrack[i, j];
                        if (AdjacencyMatrix[j , k] != 1 && (k - j < 2 && i-j < 2 && k-i <2 ))
                        {
                            Diagonals[j, k] = 1;
                        }
                    }
                }
            }
        }
    }
}
