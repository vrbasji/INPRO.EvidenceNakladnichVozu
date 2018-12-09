using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace Data.Repositories
{
    public class SerializationHelper<T>
    {
        public static byte[] Serialize(T data)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, data);

            return mStream.ToArray();
        }

        public static object Deserialize(byte[] data)
        {
            var mStream = new MemoryStream();
            var binFormatter = new BinaryFormatter();

            mStream.Write(data, 0, data.Length);
            mStream.Position = 0;

            return binFormatter.Deserialize(mStream);
        }
    }
}