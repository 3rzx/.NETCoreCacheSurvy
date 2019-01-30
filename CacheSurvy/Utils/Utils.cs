namespace CacheSurvy.Utils
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class Utils
    {
        public static byte[] ToByteArray(this object obj)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static T ByteArrayToObject<T>(this byte[] bytes)
        {
            if (bytes == null)
            {
                return default(T);
            }

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var obj = binaryFormatter.Deserialize(memoryStream);
                return (T)obj;
            }
        }
    }
}
