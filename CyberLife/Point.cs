using System;
namespace CyberLife
{
    /// <summary>
    /// Точка двумерного пространства
    /// </summary>
    public class Point
    {
        int _x;
        int _y;


        /// <summary>
        /// Абцисса точки
        /// </summary>
        public int X
        {
            get => _x;
            set => _x=value;
        }

        /// <summary>
        /// Ордината точки
        /// </summary>
        public int Y
        {
            get => _y;
            set => _y = value;
        }

        /// <summary>
        /// Преобразует Point в инициализирующую строку,
        /// которая может быть в дальнейшем использована для 
        /// реконструкции экземпляра класса. 
        /// </summary>
        /// <returns>Строка вида "X|Y" </returns>
        public override string ToString()
        {
            return "" + _x + "|" + _y;
        }


        /// <summary>
        /// Создает экземпляр класса из специальной инициализирующей строки
        /// </summary>
        /// <param name="str">Строка вида X|Y</param>
        /// <returns>Точка, описанная данной строкой</returns>
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



        /// <summary>
        /// Формирует из этой точки прототип googleProtobuf
        /// </summary>
        /// <returns>Прототип googleProtobuf</returns>
        public Protobuff.Point GetProtoPoint()
        {
            Protobuff.Point ret = new Protobuff.Point
            {
                X = _x,
                Y = _y
            };

            return ret;
        }


        /// <summary>
        /// Инициализирует экземпляр точки
        /// </summary>
        /// <param name="x">Абцисса точки</param>
        /// <param name="y">Ордината точки</param>
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Инициализирует экземпляр точки из прототипа
        /// </summary>
        /// <param name="protoPoint">Прототип googleProtobuf</param>
        public Point(Protobuff.Point protoPoint)
        {
            if (protoPoint == null)
                throw  new ArgumentNullException(nameof(protoPoint));
            _x = protoPoint.X;
            _y = protoPoint.Y;
        }
    }
}