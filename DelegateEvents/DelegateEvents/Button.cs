using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvents
{
    //public class Button
    //{
    //    public event EventHandler Clicked;
    //    public void Click(CustomEventArgs args)
    //    {
    //        OnClicked();
    //    }

    //    protected virtual void OnClicked()
    //    {
    //        Clicked?.Invoke(this, EventArgs.Empty);
    //    }
    //}
    public class Button
    {
        public event EventHandler<CustomEventArgs> Clicked;
        public void Click(CustomEventArgs args) {
            OnClicked(args);
        }

        protected virtual void OnClicked(CustomEventArgs args)
        {
            Clicked?.Invoke(this, args);
        }
    }
}
