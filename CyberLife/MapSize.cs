using System;
namespace CyberLife
{
    /// <summary>
    /// Представляет собой размер поля
    /// </summary>
    public class MapSize
    {

        int Width;
        int Height;

      
        /// <summary>
        /// Число точек на поле
        /// </summary>
        int CountOfPoint => Width * Height;


        /// <summary>
        /// Формирует прототип данного размера пооя
        /// </summary>
        /// <returns>прототип GoogleProtobuf</returns>
        public Protobuff.MapSize GetProtoMapSize()
        {
            Protobuff.MapSize ret = new Protobuff.MapSize();
            ret.Height = Height;
            ret.Width = Width;
            return ret;
        }




        /// <summary>
        /// Инициализирует экземпляр MapSize из
        /// ширины и длины поля.
        /// </summary>
        /// <param name="width">Ширина поля</param>
        /// <param name="height">Высота поля</param>
        public MapSize(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException("Width and height parameters should be positive.");
            Width = width;
            Height = height;

        }


        /// <summary>
        /// Инициализирует экземпляр MapSize из его прототипа
        /// </summary>
        /// <param name="protoMapSize">GoogleProtobuf прототип</param>
        public MapSize(Protobuff.MapSize protoMapSize)
        {
            Width = protoMapSize.Width;
            Height = protoMapSize.Height;
        }
    }
}