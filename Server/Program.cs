using System;
using System.Collections.Generic;
using System.Text;
using InterfaceDefn;
using System.Runtime.Serialization;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Threading;
using System.Runtime.Serialization.Formatters;

namespace Server
{
    class Program
    {
        private static AutoResetEvent _reset = new AutoResetEvent(false);

        public class CDoIt : ADoIt
        {
            public override void DoIt(IObserver o)
            {
                o.Go("agddasgd");
                _reset.Set();
            }
        }

        static void Main(string[] args)
        {
            CDoIt it = new CDoIt();

            Dictionary<string, string> props = new Dictionary<string, string>();
            props["port"] = "13584";
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
            TcpChannel chan = new TcpChannel(props, null, serverProvider);
            ChannelServices.RegisterChannel(chan, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(CDoIt), "obj_DoIt", WellKnownObjectMode.Singleton);

            while (_reset.WaitOne())
            {
                Console.WriteLine("done");
                _reset.Reset();
            }
        }
    }
}
