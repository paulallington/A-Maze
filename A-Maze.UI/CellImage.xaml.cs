using System;
using System.Windows;
using System.Windows.Controls;

namespace A_Maze
{
    public partial class CellImage : UserControl
    {
        private const int borderThickness = 2;

        public Cell CellValue { get; set; }

        public static readonly DependencyProperty CellPropertyProperty = DependencyProperty.Register("CellProperty", typeof(Cell), typeof(CellImage), new PropertyMetadata(default(Cell), new PropertyChangedCallback(OnTextChanged)));
        public Cell CellProperty
        {
            get { return (Cell)GetValue(CellPropertyProperty); }
            set { SetValue(CellPropertyProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CellImage cc = d as CellImage;
            Cell content = (Cell)e.NewValue;
            cc.SetCell(content);
        }

        private void SetCell(Cell cell)
        {
            CellValue = cell;
            cell.Paths.CollectionChanged += Paths_CollectionChanged;
            //IdLabel.Content = cell._id;
            //IdLabel.Content = "o";
        }

        private void Paths_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.FindParentWindow().UiInvoke(() => CellBorder.BorderThickness = new Thickness(CellValue.Border(CellValue.Neighbours[0]), CellValue.Border(CellValue.Neighbours[1]), CellValue.Border(CellValue.Neighbours[2]), CellValue.Border(CellValue.Neighbours[3])));

            //this.FindParentWindow().UiInvoke(() => IdLabel.Content = "d");
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

        public CellImage()
        {
            InitializeComponent();
        }
    }
}
