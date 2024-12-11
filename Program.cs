using System;
using System.Collections.Generic;
using System.Linq;

public class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}

public class Solution
{
    public static double EuclideanDistance(Point p1, Point p2)
    {
        return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }

    public static double BruteForce(List<Point> points, int left, int right)
    {
        double minDist = double.MaxValue;

        for (int i = left; i <= right; i++)
        {
            for (int j = i + 1; j <= right; j++)
            {
                double dist = EuclideanDistance(points[i], points[j]);
                minDist = Math.Min(minDist, dist);
            }
        }

        return minDist;
    }

    public static double ClosestPair(List<Point> points, int left, int right)
    {
        if (right - left <= 3)
            return BruteForce(points, left, right);

        int mid = (left + right) / 2;
        double leftMin = ClosestPair(points, left, mid);
        double rightMin = ClosestPair(points, mid + 1, right);

        double minDist = Math.Min(leftMin, rightMin);

        
        var strip = new List<Point>();
        double midX = points[mid].X;

        for (int i = left; i <= right; i++)
        {
            if (Math.Abs(points[i].X - midX) < minDist)
                strip.Add(points[i]);
        }

        strip.Sort((a, b) => a.Y.CompareTo(b.Y));

        int stripSize = strip.Count;
        for (int i = 0; i < stripSize; i++)
        {
            for (int j = i + 1; j < stripSize && (strip[j].Y - strip[i].Y) < minDist; j++)
            {
                double dist = EuclideanDistance(strip[i], strip[j]);
                minDist = Math.Min(minDist, dist);
            }
        }

        return minDist;
    }

    public static double ClosestPair(List<Point> points)
    {
        points.Sort((a, b) => a.X.CompareTo(b.X));  
        return ClosestPair(points, 0, points.Count - 1);
    }

    
    public static void Test()
    {
        var random = new Random();
        int numPoints = 10000;

        var points = new List<Point>();
        for (int i = 0; i < numPoints; i++)
        {
            double x = random.NextDouble() * 10000;
            double y = random.NextDouble() * 10000;
            points.Add(new Point(x, y));
        }

        double minDist = ClosestPair(points);

        Console.WriteLine($"Минимальное расстояние между парой точек: {minDist}");
    }
}

public class Program
{
    public static void Main()
    {
        Solution.Test();
    }
}
