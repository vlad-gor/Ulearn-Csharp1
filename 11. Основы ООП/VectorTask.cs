using System;
using System.Collections.Generic;
using System.Text;


namespace GeometryTasks
{
	 public class Segment
     {
        public Vector Begin;
        public Vector End;

        public static double GetLength(Vector vector)
        {
            return Geometry.GetLength(vector);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
     }
	
	 public class Vector
     {
        public double X;
        public double Y;

        public Vector()
        {
            X = 0;
            Y = 0;
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }


        public double GetLength()
        {
            return Geometry.GetLength(this);
        }


        public Vector Add(Vector vector)
        {
            return Geometry.Add(vector, this);
        }


        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
		}
     }	
		

     public static class Geometry
     {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + 
							 Math.Pow(vector.Y, 2));
        }


        public static double GetLength(Segment segment)
        {
            return Math.Sqrt(Math.Pow(segment.End.X - 
									  segment.Begin.X, 2) +
                             Math.Pow(segment.End.Y - 
									  segment.Begin.Y, 2));
        }


        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector { 
				X = vector1.X + vector2.X, Y = 
				vector1.Y + vector2.Y };
        }


        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var segmentLength = Geometry.GetLength(segment);
            var length1 = Math.Sqrt(Math.Pow(vector.X - 
											 segment.Begin.X, 2) + 
									Math.Pow(vector.Y - 
											 segment.Begin.Y, 2));
            var length2 = Math.Sqrt(Math.Pow(vector.X - 
											 segment.End.X, 2) + 
									Math.Pow(vector.Y - 
											 segment.End.Y, 2));
            return AlmostEqual((length2 + length1), segmentLength);
        }

        public static bool AlmostEqual(double a, double b)
        {
            const double epsilon = 0.1;
            return Math.Abs(a - b) < epsilon;
        }
     }
}