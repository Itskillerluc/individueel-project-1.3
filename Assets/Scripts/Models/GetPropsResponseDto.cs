using System;

namespace Models
{
    public class GetPropsResponse
    {
        public Guid propId;
        public string prefabId;
        public float posX;
        public float posY;
        public float rotation;
        public float scaleX;
        public float scaleY;
        public int sortingLayer;
        public Guid roomId;
    }
}