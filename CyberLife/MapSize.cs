using System;
namespace CyberLife
{
    public class MapSize
    {
        int Width;
        int Height;

      

        int CountOfPoint => Width * Height;



        public Protobuff.MapSize GetProtoMapSize()
        {
            Protobuff.MapSize ret = new Protobuff.MapSize();
            ret.Height = Height;
            ret.Width = Width;
            return ret;
        }

        public MapSize(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException("Width and height parameters should be positive.");
            Width = width;
            Height = height;

        }



        public MapSize(Protobuff.MapSize protoMapSize)
        {
            Width = protoMapSize.Width;
            Height = protoMapSize.Height;
        }
    }
}