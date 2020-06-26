using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace Echo.Client
{
    [ProtoContract]
    public class SocketModel
    {
        [ProtoMember(1)]
        private int type;//消息类型
        [ProtoMember(2)]
        private int area;//消息区域码
        [ProtoMember(3)]
        private int command;//指令
        [ProtoMember(4)]
        private List<string> message;//消息
        public SocketModel()
        {

        }
        public SocketModel(int type, int area, int command, List<string> message)
        {
            this.type = type;
            this.area = area;
            this.command = command;
            this.message = message;
        }
        public int GetType()
        {
            return type;
        }
        public void SetType(int type)
        {
            this.type = type;
        }
        public int GetArea()
        {
            return this.area;
        }
        public void SetArea(int area)
        {
            this.area = area;
        }
        public int GetCommand()
        {
            return this.command;
        }
        public void SetCommand(int command)
        {
            this.command = command;
        }
        public List<string> GetMessage()
        {
            return message;
        }
        public void SetMessage(List<string> message)
        {
            this.message = message;
        }

        public override string ToString()

        {
            string str = "type:" + this.type + ",area:" + this.area + ",command:" + this.command;
            foreach (string item in message)
            {
                str += " item: " + item;
            }
            return str;
        }
    }
}
