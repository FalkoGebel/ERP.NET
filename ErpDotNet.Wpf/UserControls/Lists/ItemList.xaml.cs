using ErpDotNet.Repository;
using System.Windows;
using System.Windows.Controls;

namespace ErpDotNet.Wpf.UserControls.Lists
{
    /// <summary>
    /// Interaktionslogik für ItemList.xaml
    /// </summary>
    public partial class ItemList : UserControl
    {
        public ItemList()
        {
            InitializeComponent();
        }

        public List<Item> Items
        {
            get => (List<Item>)GetValue(ItemsProperty);

            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items",
                                                                                              typeof(List<Item>),
                                                                                              typeof(ItemList));
    }
}