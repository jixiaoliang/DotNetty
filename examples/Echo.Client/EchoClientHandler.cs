// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Echo.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;
    using Examples.Common;
    public class EchoClientHandler : ChannelHandlerAdapter
    {

        public EchoClientHandler()
        {
            
        }

        public override void ChannelActive(IChannelHandlerContext context) {
            // => context.WriteAndFlushAsync(this.initialMessage);
           /* SocketModel sm = new SocketModel();
            sm.SetType(1);
            sm.SetCommand(2);
            sm.SetArea(3);
            context.WriteAndFlushAsync(sm);*/
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            SocketModel sm= message as SocketModel;
            if (sm != null)
            {
                Console.WriteLine("Received from server: " + sm.ToString());
            }
            List<string> names = new List<string>();
            names.Add("learning hard1");
            names.Add("learning hard2");
            names.Add("learning hard3");
            sm.SetMessage(names);


            context.WriteAndFlushAsync(sm);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }

    }
}