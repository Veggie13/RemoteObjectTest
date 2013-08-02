using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using InterfaceDefn;
using System.Runtime.Serialization.Formatters;

namespace Client
{
    class Program
    {
        public class CObserver : MarshalByRefObject, IObserver
        {
            #region IObserver Members

            public void Go(string s)
            {
                Console.WriteLine(s);
            }

            #endregion
        }

        static void Main(string[] args)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();
            props["port"] = "0";
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
            TcpChannel chan = new TcpChannel(props, null, serverProvider);
            ChannelServices.RegisterChannel(chan, false);

            CObserver o = new CObserver();
            IDoIt it = (IDoIt)Activator.GetObject(typeof(IDoIt), "tcp://localhost:13584/obj_DoIt");

            while (true)
            {
                Console.ReadLine();
                it.DoIt(o);
            }
        }
    }
}
