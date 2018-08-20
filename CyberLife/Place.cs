using System.Collections.Generic;
using System;
using System.CodeDom;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CyberLife
{
    /// <summary>
    /// Представляет собой способ, 
    /// с помощью которого класс Place описывает пространство
    /// </summary>
    public enum PlaceType
    {
        /// <summary>
        /// Описание с помощью задания множества точек пространства
        /// </summary>
        Array = 0, 
        
        /// <summary>
        /// Описание с помощью задания двух
        /// диагонально противоположных точек прямоугольника
        /// (левый нижний и правый верхний угол)
        /// </summary>
        Rectangle = 1
    }

    
    /// <summary>
    /// Представляет описание пространства 
    /// как множества его точек
    /// </summary>
    public class Place
    {   private List<Point> _points;

        private PlaceType _placeType;


        /// <summary>
        /// Опорные точки
        /// </summary>
        internal List<Point> Points { get => _points;  }



        /// <summary>
        /// Способ задания
        /// </summary>
        public PlaceType PlaceType
        {
            get { return _placeType; }
        }




        /// <summary>
        /// Преобразует Place в инициализирующую строку,
        /// которая может быть в дальнейшем использована для 
        /// реконструкции экземпляра класса. 
        /// </summary>
        /// <returns>Строка вида "PlaceType+x1|y1 x2|y2 ..."</returns>
        public override string ToString()
        {
            return "" + (int)_placeType + "+" + _points.Select(x => x.ToString()).Aggregate("", (x, y) => x + " " + y);

        }



        /// <summary>
        /// Создает экземпляр класса из специальной
        /// инициализирующей строки
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Строка вида </returns>
        public static Place FromString(string str)
        {
           
            return new Place(str);
        }



        /// <summary>
        /// Определяет, задан ли данный экземпляр 
        /// класса Place как "все поле". 
        /// </summary>
        /// <param name="place"></param>
        /// <returns>Все поле?</returns>
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


        /// <summary>
        /// Находит пересечение с иным Place
        /// Фактически, выполняет операцию пересечения
        /// множеств точек.
        /// </summary>
        /// <param name="anotherPlace">Пространство для пересечение с текущим</param>
        /// <returns></returns>
        public Place Intersect(Place anotherPlace)
        {
            if (anotherPlace == null)
                throw new ArgumentNullException(nameof(anotherPlace));

            // A э B => A П B = A 
            if (IsEverything(this))
                return anotherPlace;
            if (IsEverything(anotherPlace))
                return this;
                


            if (_placeType == PlaceType.Array || anotherPlace.PlaceType == PlaceType.Array)
                return new Place(_points.Intersect(anotherPlace.Points).ToList());

            if (PlaceType == anotherPlace.PlaceType && PlaceType == PlaceType.Rectangle)
            {
                // всегда выполняется, что place.PlaceType = Rectangle => place.Points[0].X <= place.Points[1].X && place.Points[0].Y <= place.Points[1].Y
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

            //impossible
            throw  new NotImplementedException();
        }



        /// <summary>
        /// Формирует прототип Place
        /// </summary>
        /// <returns>прототип GoogleProtobuf</returns>
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



        /// <summary>
        /// Формирует Place, описывающее все поле
        /// </summary>
        /// <returns></returns>
        public static Place Everything()
        {
           
            return new Place((new Point[2]{new Point(-1, -1), new Point(1, 1)}).ToList());
        }


        /// <summary>
        /// Инициализирует экземпляр Place
        /// из множества опорных точек
        /// </summary>
        /// <param name="points">Опорные точки</param>
        /// <param name="placeType">Способ описания</param>
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

        /// <summary>
        /// Инициализирует экземпляр Place из 
        /// специальной инициализирующей строки
        /// Строка может быть получена с помощью place.ToString()
        /// </summary>
        /// <param name="strPlace">Строка вида "PlaceType:point"</param>
        public Place(string strPlace)
        {
            try
            {

                _points = new List<Point>();
                var str2 = strPlace.Split('+');
                PlaceType placeType = (PlaceType) int.Parse(str2[0]);

                foreach (var point in str2[1].Trim(' ').Split(' '))
                {
                    _points.Add(Point.FromString(point));

                }

                _placeType = placeType;
            }
            catch (FormatException e)
            {

                throw new ArgumentException("Failed parsing strPlace." +
                                            " Possible strPlace isn't an initializing string for Place type.",
                    nameof(strPlace), e);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Failed parsing strPlace." +
                                            " Possible strPlace isn't an initializing string for Place type.",
                    nameof(strPlace), e);
            }
           

        }



        /// <summary>
        /// Инициализирует экземпляр класса из его прототипа.
        /// </summary>
        /// <param name="protoPlace">GoogleProtobuf прототип</param>
        public Place(Protobuff.Place protoPlace)
        {
            _points = new List<Point>();
            _points.AddRange(protoPlace.Points.Select(x=>new Point(x)));
            _placeType = (PlaceType)protoPlace.PlaceType;
        }

       
    }
}