using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.SOLID_Principles
{
    /**
     * The idea is to keep the interfaces segregated.
     * This is done by making interfaces, where there is
     * only the functionality needed in each interface.
     * 
     * So basically, dont put too much into an interface.
     * Instead you should split them into seperate interfaces.
     */ 

    public class Document
    {

    }

    /**
     * This interface is fine for MultiFunctionPrinter as seen below,
     * because a multifunctionprinter can do it all.
     * 
     * But what if it is an old fashioned printer?
     */
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            // Fax logic ...
        }

        public void Print(Document d)
        {
            // Print logic
        }

        public void Scan(Document d)
        {
            // Scan logic
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            // Print logic is fine, since it is a printer
        }

        public void Fax(Document d)
        {
            // We gotta throw exception or handle fax error, since
            // an old fashioned printer can't handle fax
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            // Same as Fax
            throw new NotImplementedException();
        }
    }

    /**
     * To achieve interface segregation we simple need to split out big IMachine
     * into smaller interfaces. This could be done as seen below
     */ 
     public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            // Print logic
        }

        public void Scan(Document d)
        {
            // Scan logic
        }
    }

    /**
     * If you then decide you want a multifunction interface,
     * then this could be implemented with the smaller interfaces.
     * 
     * Here is an example:
     */ 
     public interface IMultiFunctionDevice : IScanner, IPrinter
    {

    }

    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }

    //public class InterfaceSegregation
    //{
    //    static void Main(string[] args)
    //    {

    //    }
    //}
}
