using System.Windows;
using System.Windows.Media;

namespace A_Maze
{
    public static class UserControlExtensions
    {
        public static Window FindParentWindow(this DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            //CHeck if this is the end of the tree
            if (parent == null) return null;

            Window parentWindow = parent as Window;
            if (parentWindow != null)
            {
                return parentWindow;
            }
            else
            {
                //use recursion until it reaches a Window
                return FindParentWindow(parent);
            }
        }
    }
}
