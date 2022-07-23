  using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    public class Day2
    {
        public IList<string> PrintKMoves(int K)
        {
            Ant ant = new Ant();
            Map map = new Map();
            for(int i = 0; i < K; i++)
            {
                ant.Go(map);
            }
            return map.Output(ant);
        }
    }
    public class Ant
    {
        public Point Current { get; set; } = new Point(0, 0);
        public Direction Direction { get; set; } = Direction.R;
        public void TurnLeft()
        {
            switch (this.Direction)
            {
                case Direction.U:
                    Direction = Direction.L;
                    break;
                case Direction.L:
                    Direction = Direction.D;
                    break;
                case Direction.D:
                    Direction = Direction.R;
                    break;
                case Direction.R:
                    Direction = Direction.U;
                    break;
            }
        }
        public void TurnRight()
        {
            switch (this.Direction)
            {
                case Direction.U:
                    Direction = Direction.R;
                    break;
                case Direction.R:
                    Direction = Direction.D;
                    break;
                case Direction.D:
                    Direction = Direction.L;
                    break;
                case Direction.L:
                    Direction = Direction.U;
                    break;
            }
        }
        public void GoOneStep(Map map)
        {
            switch (Direction)
            {
                case Direction.D:
                    Current = new Point(Current.X, Current.Y - 1);
                    break;
                case Direction.U:
                    Current = new Point(Current.X, Current.Y + 1);
                    break;
                case Direction.L:
                    Current = new Point(Current.X - 1, Current.Y);
                    break;
                case Direction.R:
                    Current = new Point(Current.X + 1, Current.Y);
                    break;
            }
            map.AddPoint(Current);
        }
        public void ToggleMapColor(Map map)
        {
            map.TogglePointColor(Current);
        }
        
        public void Go(Map map)
        {
            var currentColor = map.GetColor(Current);
            if(currentColor == Color._)
            {
                ToggleMapColor(map);
                TurnRight();
                GoOneStep(map);
            }
            else
            {
                ToggleMapColor(map);
                TurnLeft();
                GoOneStep(map);
            }
        }
    }
    public class Map
    {
        Dictionary<Point, Color> Points = new Dictionary<Point, Color>();
        public void AddPoint(Point point)
        {
            if (!Points.ContainsKey(point))
                Points.Add(point, Color._);
        }
        public Color GetColor(Point point)
        {
            if (Points.ContainsKey(point))
                return Points[point];
            return Color._;
        }
        public void TogglePointColor(Point point) 
        {
            if (Points.ContainsKey(point))
            {
                Points[point] = Toggle(Points[point]);
            }
            else
            {
                Points[point] = Color.X;
            }
        }

        private Color Toggle( Color c)
        {
            if (c == Color.X)
                return Color._;
            else
                return Color.X;
        }
        public List<string> Output(Ant ant)
        {
            int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
            foreach (var kv in Points)
            {
                maxX = Math.Max(kv.Key.X, maxX);
                maxY = Math.Max(kv.Key.Y, maxY);
                minX = Math.Min(kv.Key.X, minX);
                minY = Math.Min(kv.Key.Y, minY);
            }
            int width = maxX - minX+1;
            int height = maxY - minY+1;
            List<char[]> result = new List<char[]>(height);
            for(int y = 0; y < height; y++)
            {
                result.Add(new string('_', width).ToCharArray());
            }
            foreach(var kv in Points)
            {
                int x = kv.Key.X - minX;
                int y = kv.Key.Y - minY;
                result[y][x] = kv.Value == Color.X ? 'X' : '_';
            }
            int ax = ant.Current.X - minX;
            int ay = ant.Current.Y - minY;
            result[ay][ax] = ant.Direction.ToString().First();
            return result.Select(p => new string(p)).ToList();
        }
    }
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        
        
        public override int GetHashCode()
        {
            return $"{X}_{Y}".GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var o = (Point)obj ;
            return o.X == X && o.Y == Y;
        }
    }
    public enum Color
    {
        X,_
    }
    public enum Direction
    {
        L,U,R,D
    }
}
