using DataAccess.Repository;
using System.Windows;
using System.Windows.Input;

namespace SaleWPFApp
{
    /// <summary>
    /// Interaction logic for AdminManager.xaml
    /// </summary>
    public partial class AdminManager : Window
    {
        private readonly IProductRepository productRepository;
        private readonly IMemberRepository memberRepository;
        private readonly IOrderRepository orderRepository;
        private readonly MainWindow mainWindow;
        public AdminManager(MainWindow _mainWindow, IProductRepository _productRepository, IMemberRepository _memberRepository, IOrderRepository _orderRepository)
        {
            InitializeComponent();
            this.productRepository = _productRepository;
            this.memberRepository = _memberRepository;
            this.orderRepository = _orderRepository;
            this.mainWindow = _mainWindow;
            Closing += AdminManager_Closing;
        }

        private void AdminManager_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }



        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            logo.Visibility = Visibility.Hidden;
            AdminMember adminMember = new AdminMember(memberRepository);
            frameMain.Content = adminMember;
           
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            logo.Visibility = Visibility.Hidden;
            AdminProduct adminProduct = new AdminProduct(productRepository);
            frameMain.Content = adminProduct;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            logo.Visibility = Visibility.Hidden;
            AdminOrder adminOrder = new AdminOrder(orderRepository);
            frameMain.Content = adminOrder;
        }
    }
}
