using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ErpDotNet.Wpf.UserControls.Lists
{
    /// <summary>
    /// Interaktionslogik für GeneralList.xaml
    /// </summary>
    public partial class GeneralList : UserControl
    {
        private readonly ResourceSet? _resourceSet;

        public GeneralList()
        {
            InitializeComponent();
            _resourceSet = Texts.GeneralList.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        }

        /// <summary>
        /// Type of the objects in the list. This is used to determine which properties to display as columns in the ListView.
        /// </summary>
        public Type Type
        {
            get => (Type)GetValue(TypeProperty);

            set
            {
                SetValue(TypeProperty, value);
            }
        }

        /// <summary>
        /// The collection of line objects to display in the ListView. Each object should be of the type specified in the Type property.
        /// </summary>
        public List<object> Lines
        {
            get => (List<object>)GetValue(LinesProperty);

            set
            {
                SetValue(LinesProperty, value);
            }
        }

        /// <summary>
        /// The index of the currently selected item in the ListView.
        /// </summary>
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        /// <summary>
        /// The visibility of the ListView.
        /// </summary>
        public Visibility ListVisibility
        {
            get => (Visibility)GetValue(ListVisibilityProperty);

            set
            {
                SetValue(ListVisibilityProperty, value);
            }
        }

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type",
                                                                                              typeof(Type),
                                                                                              typeof(GeneralList));

        public static readonly DependencyProperty LinesProperty = DependencyProperty.Register("Lines",
                                                                                              typeof(List<object>),
                                                                                              typeof(GeneralList));

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex",
                                                                                                    typeof(int),
                                                                                                    typeof(GeneralList));

        public static readonly DependencyProperty ListVisibilityProperty = DependencyProperty.Register("ListVisibility",
                                                                                                      typeof(Visibility),
                                                                                                      typeof(GeneralList));

        private void ListView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
                return;

            var gv = new GridView();

            foreach (PropertyInfo property in Type.GetProperties())
            {
                string header = Texts.GeneralList.MissingHeader + property.Name;

                foreach (DictionaryEntry entry in _resourceSet!)
                {
                    if (entry.Key.ToString() == Type.Name + property.Name)
                    {
                        header = entry.Value!.ToString()!;
                        break;
                    }
                }

                var gvc = new GridViewColumn
                {
                    Header = header,
                    DisplayMemberBinding = new System.Windows.Data.Binding(property.Name)
                };

                gv.Columns.Add(gvc);
            }

            listView.View = gv;
        }

        #region OpenCard
        public static readonly DependencyProperty OpenCardProperty =
            DependencyProperty.Register(
                "OpenCard",
                typeof(ICommand),
                typeof(GeneralList),
                new UIPropertyMetadata(null));

        public ICommand OpenCard
        {
            get { return (ICommand)GetValue(OpenCardProperty); }
            set { SetValue(OpenCardProperty, value); }
        }
        #endregion
    }
}