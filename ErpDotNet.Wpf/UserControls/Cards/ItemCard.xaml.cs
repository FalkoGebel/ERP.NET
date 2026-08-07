using ErpDotNet.Repository;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ErpDotNet.Wpf.UserControls.Cards
{
    /// <summary>
    /// Interaktionslogik für ItemCard.xaml
    /// </summary>
    public partial class ItemCard : UserControl
    {
        public ItemCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Item to show in the card.
        /// </summary>
        public Item Item
        {
            get => (Item)GetValue(ItemProperty);

            set
            {
                SetValue(ItemProperty, value);
            }
        }

        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item",
                                                                                              typeof(Item),
                                                                                              typeof(ItemCard));

        #region Ok
        public static readonly DependencyProperty OkProperty =
            DependencyProperty.Register(
                "Ok",
                typeof(ICommand),
                typeof(ItemCard),
                new UIPropertyMetadata(null));

        public ICommand Ok
        {
            get { return (ICommand)GetValue(OkProperty); }
            set { SetValue(OkProperty, value); }
        }
        #endregion

        #region Cancel
        public static readonly DependencyProperty CancelProperty =
            DependencyProperty.Register(
                "Cancel",
                typeof(ICommand),
                typeof(ItemCard),
                new UIPropertyMetadata(null));

        public ICommand Cancel
        {
            get { return (ICommand)GetValue(CancelProperty); }
            set { SetValue(CancelProperty, value); }
        }
        #endregion
    }
}