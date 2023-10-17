using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 飲料訂單視窗頁面
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string,int> drinks = new Dictionary<string,int>();
        Dictionary<string, int> orders = new Dictionary<string, int>();
        public MainWindow()
        {
            InitializeComponent();
            //增新飲料品項
            AddNewDrink(drinks);

        }

        //增新飲料品項
        private void AddNewDrink(Dictionary<string, int> fdrinks)
        {
            fdrinks.Add("紅茶大杯", 60);
            fdrinks.Add("紅茶小杯", 40);
            fdrinks.Add("綠茶大杯", 60);
            fdrinks.Add("綠茶小杯", 40);
            fdrinks.Add("咖啡大杯", 60);
            fdrinks.Add("咖啡小杯", 40);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var targetTextBox = sender as TextBox;

            bool success = int.TryParse(targetTextBox.Text, out int quantity);
            if (!success) MessageBox.Show("請輸入整數值", "輸入錯誤");
            else if (quantity <= 0) MessageBox.Show("請輸入正數數值", "輸入錯誤");
            else{ 
                var targetStackPanel = targetTextBox.Parent as StackPanel;
                var targetLabel = targetStackPanel.Children[0] as Label;
            }
        }
    }
}
