using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace InterfaceDefn
{
    public interface IObserver
    {
        void Go(string s);
    }

    public interface IDoIt
    {
        void DoIt(IObserver o);
    }

    public abstract class ADoIt : MarshalByRefObject, IDoIt
    {
        public abstract void DoIt(IObserver o);
        //{
        //    throw new Exception();
        //}
    }
}
