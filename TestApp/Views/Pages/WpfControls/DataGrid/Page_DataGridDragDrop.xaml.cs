using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestApp.Views.Pages.WpfControls.DataGrid
{
    /// <summary>
    /// Interaction logic for Page_DataGridDragDrop.xaml
    /// </summary>
    public partial class Page_DataGridDragDrop : Page
    {
        ObservableCollection<Model_DataGrid> _list1;
        ObservableCollection<Model_DataGrid> _list2;
        Point _targetMousePoint;//Drag时Mouse的Point

        public Page_DataGridDragDrop()
        {
            InitializeComponent();
            InitializeDataGrid1();
            InitializeDataGrid2();
        }

        private void InitializeDataGrid1()
        {
            _list1 = new ObservableCollection<Model_DataGrid>();
            _list1.Add(new Model_DataGrid { Id = 1, Name = "N1" });
            _list1.Add(new Model_DataGrid { Id = 2, Name = "N2" });
            _list1.Add(new Model_DataGrid { Id = 3, Name = "N3" });
            _list1.Add(new Model_DataGrid { Id = 4, Name = "N4" });
            _list1.Add(new Model_DataGrid { Id = 5, Name = "N5" });
            _list1.Add(new Model_DataGrid { Id = 6, Name = "N6" });
            _list1.Add(new Model_DataGrid { Id = 7, Name = "N7" });
            _list1.Add(new Model_DataGrid { Id = 8, Name = "N8" });
            _list1.Add(new Model_DataGrid { Id = 9, Name = "N9" });
            this.DataGrid1.ItemsSource = _list1;
        }

        private void InitializeDataGrid2()
        {
            _list2 = new ObservableCollection<Model_DataGrid>();
            _list2.Add(new Model_DataGrid { Id = 1, Name = "Na1" });
            _list2.Add(new Model_DataGrid { Id = 2, Name = "Na2" });
            _list2.Add(new Model_DataGrid { Id = 3, Name = "Na3" });
            _list2.Add(new Model_DataGrid { Id = 4, Name = "Na4" });
            _list2.Add(new Model_DataGrid { Id = 5, Name = "Na5" });
            _list2.Add(new Model_DataGrid { Id = 6, Name = "Na6" });
            this.DataGrid2.ItemsSource = _list2;
        }

        /// <summary>
        /// 拖动处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Model_DataGrid draggedItem = null;//源Row
            Model_DataGrid targetItem = null;//目标Row
            //查找鼠标点击的源Row
            IInputElement element = DataGrid1.InputHitTest(e.GetPosition(DataGrid1));
            while (element != DataGrid1)
            {
                if (element != null && element is DataGridRow)
                {
                    DataGrid1.SelectedItem = ((DataGridRow)element).Item;
                    draggedItem = (Model_DataGrid)DataGrid1.SelectedItem;
                    break;
                }
                else
                {
                    DataGrid1.SelectedItem = null;
                    element = System.Windows.Media.VisualTreeHelper.GetParent(element as System.Windows.DependencyObject) as System.Windows.IInputElement;
                }
            }

            if (this.DataGrid1.SelectedCells.Count > 0)
            {
                Model_DataGrid dragData = this.DataGrid1.SelectedCells[0].Item as Model_DataGrid;
                DragDrop.DoDragDrop(DataGrid1, dragData, DragDropEffects.Move);
                //拖动结束
                element = DataGrid1.InputHitTest(_targetMousePoint);
                while (element != DataGrid1)
                {
                    if (element != null && element is DataGridRow)
                    {
                        targetItem = (Model_DataGrid)((DataGridRow)element).Item;
                        break;
                    }
                    else
                    {
                        element = System.Windows.Media.VisualTreeHelper.GetParent(element as System.Windows.DependencyObject) as System.Windows.IInputElement;
                    }
                }
                //处理排序
                if (targetItem != null && !ReferenceEquals(draggedItem, targetItem))
                {
                    //remove the source from the list
                    _list1.Remove(draggedItem);

                    //get target index
                    var targetIndex = _list1.IndexOf(targetItem);

                    //move source at the target's location
                    _list1.Insert(targetIndex, draggedItem);

                    //select the dropped item
                    DataGrid1.SelectedItem = draggedItem;
                }
            }
        }

        private void DataGrid2_Drop(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            Model_DataGrid obj = (Model_DataGrid)data.GetData(typeof(Model_DataGrid));
            Console.WriteLine(obj.Name);
        }

        /// <summary>
        /// 获取拖动结束时鼠标的Point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid1_DragOver(object sender, DragEventArgs e)
        {
            _targetMousePoint = e.GetPosition(DataGrid1);
        }
    }

    public class Model_DataGrid
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
