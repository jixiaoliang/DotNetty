using System;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using ProtoBuf;
using System.IO;
namespace Echo.Client
{
    public class MessageEncoder : MessageToByteEncoder<SocketModel>
    {
        private const int PRE_FIX_LENGTH = 4;
        public MessageEncoder()
        {
        }

        protected override void Encode(IChannelHandlerContext context, SocketModel message, IByteBuffer output)
        {
            output.WriteBytes(Serial(message));
        }

        private byte[] Serial(SocketModel socketModel)//将SocketModel转化成字节数组
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<SocketModel>(ms, socketModel);
                byte[] data = new byte[PRE_FIX_LENGTH+ms.Length];
                IntToBytes((int)ms.Length).CopyTo(data, 0);
                ms.Position = 0;
                ms.Read(data, PRE_FIX_LENGTH, (int)ms.Length);
                return data;
            }
        }

        private static byte[] IntToBytes(int num)
        {
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte)(num >> (24 - i * 8));
            }
            return bytes;
        }
    }
}
