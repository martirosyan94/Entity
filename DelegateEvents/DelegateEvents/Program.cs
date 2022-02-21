using System;

namespace DelegateEvents
{
    public class Program
    {
        static void Main(string[] args)
        {
            Button button = new Button();
            ConsoleWriter consoleWriter = new ConsoleWriter();
            button.Clicked += consoleWriter.OnTextPrinted;
            CustomEventArgs customEventArgs = new CustomEventArgs() { Name = "custom arg" };
            button.Click(customEventArgs);
            //button.Click();
        }

    }
}
