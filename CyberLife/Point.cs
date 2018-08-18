using System;
namespace CyberLife
{
    public class Point
    {
        int _x;
        int _y;



        public int X
        {
            get => _x;
            set => _x=value;
        }


        public int Y
        {
            get => _y;
            set => _y = value;
        }


        public override string ToString()
        {
            return "" + _x + "|" + _y;
        }



        public static Point FromString(string str)
        {
            Point pnt;
            try
            {
                var coords = str.Trim(' ').Split('|');

                pnt = new Point(int.Parse(coords[0]), int.Parse(coords[1])); 

            }
            catch (FormatException)
            {
                throw new ArgumentException("String isn't contain point", nameof(str));

            }
            catch (IndexOutOfRangeException)
            {

                throw new ArgumentException("String isn't contain point", nameof(str));
            }
            return pnt;
        }




        public Protobuff.Point GetProtoPoint()
        {
            Protobuff.Point ret = new Protobuff.Point
            {
                X = _x,
                Y = _y
            };

            return ret;
        }



        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }


        public Point(Protobuff.Point protoPoint)
        {
            if (protoPoint == null)
                throw  new ArgumentNullException(nameof(protoPoint));
            _x = protoPoint.X;
            _y = protoPoint.Y;
        }
    }
}