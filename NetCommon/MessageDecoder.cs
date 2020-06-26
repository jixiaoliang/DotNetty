using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System.IO;
using ProtoBuf;
using System.Net.Sockets;
namespace NetCommon
{
    public class MessageDecoder : ByteToMessageDecoder
    {
        public MessageDecoder()
        {
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {


            byte[] rst = new byte[input.ReadableBytes];
            input.ReadBytes(rst);
            SocketModel message = DeSerial(rst);
            output.Add(message);
        }
    

        private SocketModel DeSerial(byte[] msg)//将字节数组转化成我们的消息类型SocketModel
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(msg, 0, msg.Length);
                ms.Position = 0;
                SocketModel socketModel = Serializer.Deserialize<SocketModel>(ms);
                Console.WriteLine("decode from server: " + socketModel.ToString());
                return socketModel;
            }
        }
    }
}
