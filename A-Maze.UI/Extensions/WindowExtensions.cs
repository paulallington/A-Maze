using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace A_Maze
{
    public static class WindowExtensions
    {
        public static void UiInvoke(this Window window, Action action)
        {
            Action caughtAction = () => InvokeAction(action);
            window.Dispatcher.BeginInvoke(caughtAction);
        }

        private static void InvokeAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error invoking action: " + ex.Message);
            }
        }
    }
}
