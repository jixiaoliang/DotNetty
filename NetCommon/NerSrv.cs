using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
namespace NetCommon
{
    public class NetSrv { 
     static async Task RunClientAsync()
    {

        var group = new MultithreadEventLoopGroup();

        X509Certificate2 cert = null;
        string targetHost = null;
       
        targetHost = cert.GetNameInfo(X509NameType.DnsName, false);
        try
        {
            var bootstrap = new Bootstrap();
            bootstrap
                .Group(group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;

                    if (cert != null)
                    {
                            // pipeline.AddLast("tls", new TlsHandler(stream => new SslStream(stream, true, (sender, certificate, chain, errors) => true), new ClientTlsSettings(targetHost)));
                        }
                        // pipeline.AddLast(new LoggingHandler());
                        pipeline.AddLast(new LengthFieldBasedFrameDecoder(1024, 0, 4, 0, 4));
                    pipeline.AddLast("framing-enc", new MessageEncoder());
                    pipeline.AddLast("framing-dec", new MessageDecoder());

                    pipeline.AddLast("echo", new EchoClientHandler());
                }));

            IChannel clientChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8007));

            Console.ReadLine();

            await clientChannel.CloseAsync();
        }
        finally
        {
            await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
        }
    }

    static void Main() => RunClientAsync().Wait();
}
}
