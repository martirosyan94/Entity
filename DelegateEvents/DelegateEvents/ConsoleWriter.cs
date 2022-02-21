using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvents
{
    public class ConsoleWriter
    {
        //public void OnTextPrinted(object source, EventArgs args)
        //{
        //    Console.WriteLine("You have successfully printed");
        //}
        public void OnTextPrinted(object source, CustomEventArgs args)
        {
            Console.WriteLine("You have successfully printed " + args.Name);
        }

    }
}
