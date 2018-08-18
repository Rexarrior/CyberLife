using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CyberLife
{

    public enum PlaceType
    {
        Array = 0, 
        Rectangle = 1
    }


    public class Place
    {
        private List<Point> _points;

        private PlaceType _placeType; 


        internal List<Point> Points { get => _points;  }



        public PlaceType PlaceType
        {
            get { return _placeType; }
        }





        public override string ToString()
        {
            return "" + (int)_placeType + "+" + _points.Select(x => x.ToString()).Aggregate("", (x, y) => x + " " + y);

        }




        public static Place FromString(string str)
        {
            List<Point> points = new List<Point>();
            var str2 = str.Split('+');
            CyberLife.PlaceType placeType = (PlaceType) int.Parse(str2[0]);
            foreach (var point in str2[1].Trim(' ').Split(' '))
            {
                points.Add(Point.FromString(point));

            }
            return new Place(points);
        }




        public static bool IsEverything(Place place)
        {
            if (place == null ||
                place.PlaceType != PlaceType.Rectangle ||
                place.Points.Count != 2 ||
                place.Points[0].X != -1 ||
                place.Points[0].Y != -1 ||
                place.Points[0].X != -1 ||
                place.Points[0].Y != -1
            )
                return false;


            return true;
        }



        public Place Intersect(Place anotherPlace)
        {
            if (anotherPlace == null)
                throw new ArgumentNullException(nameof(anotherPlace));

            if (IsEverything(this))
                return anotherPlace;
            if (IsEverything(anotherPlace))
                return this;
                


            if (_placeType == PlaceType.Array || anotherPlace.PlaceType == PlaceType.Array)
                return new Place(_points.Intersect(anotherPlace.Points).ToList());

            if (PlaceType == anotherPlace.PlaceType && PlaceType == PlaceType.Rectangle)
            {
                int minX = (_points[0].X > anotherPlace.Points[0].X) ? _points[0].X : anotherPlace.Points[0].X;
                int maxX = (_points[1].X < anotherPlace.Points[1].X) ? _points[1].X : anotherPlace.Points[1].X;
                int minY = (_points[0].Y > anotherPlace.Points[0].Y) ? _points[0].Y : anotherPlace.Points[0].Y;
                int maxY = (_points[1].Y < anotherPlace.Points[1].Y) ? _points[1].Y : anotherPlace.Points[1].Y;
                
                Point min = new Point(minX, minY);
                Point max = new Point(maxX, maxY);
                return new Place(new Point[2]{min, max}.ToList());

            }

            if (_placeType == PlaceType.Array && anotherPlace.PlaceType == PlaceType.Rectangle ||
                _placeType == PlaceType.Rectangle && anotherPlace.PlaceType == PlaceType.Array)
            {
                Place arrPlace = (_placeType == PlaceType.Array)?this:anotherPlace; 
                Place rectPlace = (_placeType == PlaceType.Rectangle) ? this : anotherPlace;

                List<Point> endPoints = new List<Point>();
                foreach (var point in arrPlace.Points)
                {
                    if (point.X > rectPlace.Points[0].X &&
                        point.Y > rectPlace.Points[0].Y &&
                        point.X < rectPlace.Points[1].X &&
                        point.Y < rectPlace.Points[1].Y)
                            endPoints.Add(point);
                }

                return  new Place(endPoints);

            }

            throw  new NotImplementedException();
        }




        public Protobuff.Place GetProtoPlace()
        {
            Protobuff.Place ret = new Protobuff.Place();
            foreach (var point in _points)
            {
                ret.Points.Add(point.GetProtoPoint());
            }

            ret.PlaceType = (int) PlaceType;
            return ret;
        }




        public static Place Everything()
        {
           
            return new Place((new Point[2]{new Point(-1, -1), new Point(1, 1)}).ToList());
        }



        public Place(List<Point> points, PlaceType placeType = PlaceType.Array)
        {


            if (placeType == PlaceType.Array)
                _points = points ?? throw new ArgumentNullException(nameof(points));
            if (PlaceType == PlaceType.Rectangle)
            {
                _points = points ?? throw new ArgumentNullException(nameof(points));
                if (points.Count != 2)  
                    throw  new ArgumentException("To use 'Rectangle' place type need define  2 points. " +
                                                 "If more or less points has been defined, Exception will be thrown.",
                                                    nameof(points));

                if (_points[0].X > _points[1].X)
                {
                    var tmp = _points[0].X;
                    _points[0].X = _points[1].X;
                    _points[1].X = tmp;
                }

                if (_points[0].Y > _points[1].Y)
                {
                    var tmp = _points[0].Y;
                    _points[0].Y = _points[1].Y;
                    _points[1].Y = tmp;
                }
            }



        }


        public Place(string strPlace)
        {
            _points =  Place.FromString(strPlace)._points;
        }




        public Place(Protobuff.Place protoPlace)
        {
            _points = new List<Point>();
            _points.AddRange(protoPlace.Points.Select(x=>new Point(x)));
            _placeType = (PlaceType)protoPlace.PlaceType;
        }

       
    }
}