using System;
using Google.Protobuf;
using ProtoBuf;
namespace DotNetty.Codecs.Protobuf.Tests
{
    [ProtoContract]
    public class SocketModel
    {
        [ProtoMember(1)]
        private int type;
        public SocketModel()
        {
        }
    }
}
