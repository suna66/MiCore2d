#pragma warning disable CS8765
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Collision. Collision check class.
    /// </summary>
    public class CollisionUtil
    {
        /// <summary>
        /// Box Box collision detection.
        /// </summary>
        /// <param name="b1">box1 position</param>
        /// <param name="width1">width of box1</param>
        /// <param name="height1">height of box1</param>
        /// <param name="b2">box2 position</param>
        /// <param name="width2">width of box2</param>
        /// <param name="heigh2">height of box2</param>
        /// <returns>result of collision</returns>
        public static bool BoxBox(Vector3 b1, float width1, float height1, Vector3 b2, float width2, float height2)
        {
            if (b1.Z != b2.Z)
            {
                return false;
            }
            
            if (
                (b1.X + width1/2 > b2.X - width2/2)
                && (b1.X - width1/2 < b2.X + width2/2)
                && (b1.Y + height1/2 > b2.Y - height2/2)
                && (b1.Y - height1/2 < b2.Y + height2/2)
            )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Box Circle collision detection
        /// </summary>
        /// <param name="b">box position</param>
        /// <param name="widthUnit">width of box</param>
        /// <param name="heightUnit">height of box</param>
        /// <param name="c">circle position</param>
        /// <param name="radius">radius</param>
        /// <returns>result of collision</returns>
        public static bool BoxCircle(Vector3 b, float width, float height, Vector3 c, float radius)
        {
            float testX = c.X;
            float testY = c.Y;

            if (b.Z != c.Z)
            {
                return false;
            }

            float halfW = width/2;
            float halfH = height/2;

            if (c.X < b.X - halfW)
            {
                testX = b.X - halfW;
            }
            else if (c.X > b.X + halfW)
            {
                testX = b.X + halfW;
            }
            if (c.Y < b.Y - halfH)
            {
                testY = b.Y - halfH;
            }
            else if (c.Y > b.Y + halfH)
            {
                testY = b.Y + halfH;
            }

            float distX = c.X - testX;
            float distY = c.Y - testY;
            float distance = MathF.Sqrt((distX * distX) + (distY * distY));
            if (distance <= radius)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Circle Circle collision detect.
        /// </summary>
        /// <param name="c1">circle 1 position</param>
        /// <param name="radius1">radius1</param>
        /// <param name="c2">circle 2 position</param>
        /// <param name="radius2">radius2</param>
        /// <returns>result of collision</returns>
        public static bool CircleCircle(Vector3 c1, float radius1, Vector3 c2, float radius2)
        {
            float cond_distance = radius1 + radius2;

            float maxY = MathF.Max(c1.Y, c2.Y);
            float minY = MathF.Min(c1.Y, c2.Y);
            float distanceY = maxY - minY;
            float maxX = MathF.Max(c1.X, c2.X);
            float minX = MathF.Min(c1.X, c2.X);
            float distanceX = maxX - minX;

            if (c1.Z != c2.Z)
            {
                return false;
            }

            return ((cond_distance * cond_distance) >= ((distanceX * distanceX) + (distanceY * distanceY)));
        }

        /// <summary>
        /// Line Line collision detect.
        /// </summary>
        /// <param name="line1">line 1</param>
        /// <param name="line2">line 2</param>
        /// <returns>result of collision</returns>
        public static bool LineLine(Line line1, Line line2)
        {
            float uA = ((line2.Point2.X - line2.Point1.X) * (line1.Point1.Y - line2.Point1.Y) - (line2.Point2.Y - line2.Point1.Y) * (line1.Point1.X - line2.Point1.X)) 
                    / ((line2.Point2.Y - line2.Point1.Y) * (line1.Point2.X - line1.Point1.X) - (line2.Point2.X - line2.Point1.X) * (line1.Point2.Y - line1.Point1.Y));
            float uB = ((line1.Point2.X - line1.Point1.X) * (line1.Point1.Y - line2.Point1.Y) - (line1.Point2.Y - line1.Point1.Y) * (line1.Point1.X - line2.Point1.X))
                    / ((line2.Point2.Y - line2.Point1.Y) * (line1.Point2.X - line1.Point1.X) - (line2.Point2.X - line2.Point1.X) * (line1.Point2.Y - line1.Point1.Y));
            if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Line Box collision detect
        /// </summary>
        /// <param name="line">line</param>
        /// <param name="b">box position</param>
        /// <param name="width">width of box</param>
        /// <param name="height">height of box</param>
        /// <returns>result of collision</returns>
        public static bool LineBox(Line line, Vector3 b, float width, float height)
        {
            Line leftLine = new Line(b.X - width/2, b.Y + height/2, b.X - width/2, b.Y - height/2);
            Line rightLine = new Line(b.X + width/2, b.Y + height/2, b.X + width/2, b.Y - height/2);
            Line topLine = new Line(b.X - width/2, b.Y + height/2, b.X + width/2, b.Y + height/2);
            Line bottomtLine = new Line(b.X - width/2, b.Y - height/2, b.X + width/2, b.Y - height/2);

            bool left = LineLine(line, leftLine);
            bool right = LineLine(line, rightLine);
            bool top = LineLine(line, topLine);
            bool bottom = LineLine(line, bottomtLine);

            if (left || right || top || bottom)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Line Circle collision detect
        /// </summary>
        /// <param name="line">line</param>
        /// <param name="c">circle position</param>
        /// <param name="radius">radius</param>
        /// <returns>result of collision</returns>
        public static bool LineCircle(Line line, Vector3 c, float radius)
        {
            //return true;
            bool inside1 = PointCircle(line.Point1, c, radius);
            bool inside2 = PointCircle(line.Point2, c, radius);
            if (inside1 || inside2)
                return true;

            float distX = line.Point1.X - line.Point2.X;
            float distY = line.Point1.Y - line.Point2.Y;
            float len = MathF.Sqrt((distX*distX) + (distY*distY));

            float dot = (((c.X - line.Point1.X)*(line.Point2.X - line.Point1.X)) + ((c.Y - line.Point1.Y)*(line.Point2.Y - line.Point1.Y)) ) / MathF.Pow(len, 2);
            float closestX = line.Point1.X + (dot * (line.Point2.X - line.Point1.X));
            float closestY = line.Point1.Y + (dot * (line.Point2.Y - line.Point1.Y));

            bool onSegment = PointLine(new Vector2(closestX, closestY), line);
            if (!onSegment)
                return false;
            
            distX = closestX - c.X;
            distY = closestY - c.Y;
            float distance = MathF.Sqrt((distX*distX) + (distY*distY));
            if (distance <= radius)
            {
                return true;
            }
            return  false;
        }

        /// <summary>
        /// Point Box collision detect
        /// </summary>
        /// <param name="p">point position</param>
        /// <param name="b">box position</param>
        /// <param name="width">width of box</param>
        /// <param name="height">height of box</param>
        /// <returns>result of collision</returns>
        public static bool PointBox(Vector2 p,  Vector3 b, float width, float height)
        {
            if (p.X >= b.X - width/2 &&
                p.X <= b.X + width/2 &&
                p.Y >= b.Y - height/2 &&
                p.Y <= b.Y + height/2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Pint Circle collistion detect
        /// </summary>
        /// <param name="p">point position</param>
        /// <param name="c">circle position</param>
        /// <param name="radius">radius</param>
        /// <returns>result of collision</returns>
        public static bool PointCircle(Vector2 p,  Vector3 c, float radius)
        {
            float distX = p.X - c.X;
            float distY = p.Y - c.Y;
            float distance = (distX*distX) + (distY*distY);
            if (distance <= (radius * radius))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Point Line collisiton detect.
        /// </summary>
        /// <param name="p">point position</param>
        /// <param name="line">line</param>
        /// <returns>result of collision</returns>
        public static bool  PointLine(Vector2 p, Line line)
        {
            float d1 = MathF.Sqrt(((p.X - line.Point1.X)*(p.X - line.Point1.X) + (p.Y - line.Point1.Y)*(p.Y - line.Point1.Y)));
            float d2 = MathF.Sqrt(((p.X - line.Point2.X)*(p.X - line.Point2.X) + (p.Y - line.Point2.Y)*(p.Y - line.Point2.Y)));
            float lineLen = MathF.Sqrt((line.Point1.X - line.Point2.X)*(line.Point1.X - line.Point2.X) + (line.Point1.Y - line.Point2.Y)*(line.Point1.Y - line.Point2.Y));
            float buffer = 0.1f;
            if (d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// PointPoly
        /// </summary>
        /// <param name="vertix">vertix of polygon</param>
        /// <param name="p">point</param>
        /// <returns>result of collision</returns>
        public static bool PointPoly(Vector2[] vertix, Vector2 p)
        {
            bool isCollision = false;

            int next = 0;
            for (int i = 0; i < vertix.Length; i++)
            {
                next = i + 1;
                if (next == vertix.Length)
                {
                    next = 0;
                }
                
                Vector2 vc = vertix[i];
                Vector2 vn = vertix[next];

                if (((vc.Y >= p.Y && vn.Y < p.Y) || (vc.Y < p.Y && vn.Y >= p.Y))
                    && (p.X < (vn.X - vc.X) * (p.Y - vc.Y) / (vn.Y-vc.Y) + vc.X))
                {
                    isCollision = !isCollision;
                }
            }
            return isCollision;
        }

        /// <summary>
        /// CirclePoly
        /// </summary>
        /// <param name="vertix">vertix of polygon</param>
        /// <param name="c">circle centor</param>
        /// <param name="radius">radius</param>
        /// <returns>result of collision</returns>
        public static bool CirclePoly(Vector2[] vertix, Vector3 c, float radius)
        {
            int next = 0;
            for (int i = 0; i < vertix.Length; i++)
            {
                next = i + 1;
                if (next == vertix.Length)
                {
                    next = 0;
                }
                
                Vector2 vc = vertix[i];
                Vector2 vn = vertix[next];

                Line line = new Line(vc.X, vc.Y, vn.X, vn.Y);
                if (LineCircle(line, c, radius))
                {
                    return true;
                }
            }
            //check inside.
            if (PointPoly(vertix, new Vector2(c.X, c.Y)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// BoxPoly
        /// </summary>
        /// <param name="vertix">vertix of polygon</param>
        /// <param name="b">rectangle centor</param>
        /// <param name="width">rectangle width</param>
        /// <param name="height">rectangle height</param>
        /// <returns>result of collision</returns>
        public static bool BoxPoly(Vector2[] vertix, Vector3 b, float width, float height)
        {
            int next = 0;
            for (int i = 0; i < vertix.Length; i++)
            {
                next = i + 1;
                if (next == vertix.Length)
                {
                    next = 0;
                }
                
                Vector2 vc = vertix[i];
                Vector2 vn = vertix[next];

                Line line = new Line(vc.X, vc.Y, vn.X, vn.Y);
                if (LineBox(line, b, width, height))
                {
                    return true;
                }
                if (PointPoly(vertix, new Vector2(b.X, b.Y)))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// LinePoly
        /// </summary>
        /// <param name="vertix">ertix of polygon</param>
        /// <param name="line">line</param>
        /// <returns>result of collision</returns>
        public static bool LinePoly(Vector2[] vertix, Line line)
        {
            int next = 0;
            for (int i = 0; i < vertix.Length; i++)
            {
                next = i + 1;
                if (next == vertix.Length)
                {
                    next = 0;
                }
                
                Vector2 vc = vertix[i];
                Vector2 vn = vertix[next];

                Line linePoly = new Line(vc.X, vc.Y, vn.X, vn.Y);
                if (LineLine(linePoly, line))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// PolyPoly
        /// </summary>
        /// <param name="vertix1">ertix of polygon</param>
        /// <param name="vertix2">ertix of polygon</param>
        /// <returns>result of collision</returns>
        public static bool PolyPoly(Vector2[] vertix1, Vector2[] vertix2)
        {
            int next = 0;
            for (int i = 0; i < vertix1.Length; i++)
            {
                next = i + 1;
                if (next == vertix1.Length)
                {
                    next = 0;
                }
                
                Vector2 vc = vertix1[i];
                Vector2 vn = vertix1[next];

                Line line = new Line(vc.X, vc.Y, vn.X, vn.Y);
                if (LinePoly(vertix2, line))
                {
                    return true;
                }
                if (PointPoly(vertix1, vertix2[0]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}