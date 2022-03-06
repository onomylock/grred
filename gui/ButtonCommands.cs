using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gui
{
    class ButtonCommands
    {
        #region RightButtons
        private void ReflectHorizontally()
        {
            bool axe = true;
        }

        private void ReflectVertically()
        {
            bool axe = false;
        }
        #endregion

        #region LeftButtons

        #endregion

        #region ICommand
        //public ICommand ReflectHorizontallyCommand
        //{
        //    get
        //    {
        //        return new ActionCommand((sender, true) =>
        //        {
        //            ReflectHorizontally();
        //        });
        //    }
        //}
        #endregion
    }
}
