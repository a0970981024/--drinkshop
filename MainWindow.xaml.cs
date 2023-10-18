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

            //顯示飲料品項
            DisplayDrinkMenu(drinks);

        }

        private void DisplayDrinkMenu(Dictionary<string, int> f2drinks)
        {

            foreach (var drink in f2drinks) {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Margin = new Thickness(5);

                CheckBox cb = new CheckBox();
                cb.Content = drink.Key;
                cb.FontSize = 20;
                cb.FontWeight = FontWeights.Bold;
                cb.Foreground = Brushes.Green;
                cb.VerticalContentAlignment = VerticalAlignment.Center;
                cb.Width = 120;

                Label lb = new Label();
                lb.Content = $"{drink.Value}元";
                lb.HorizontalAlignment = HorizontalAlignment.Center;
                lb.VerticalContentAlignment = VerticalAlignment.Center;
                lb.FontSize = 20;
                lb.Width = 110;

                Slider sl = new Slider();
                sl.Width = 100;
                sl.Value = 0;
                sl.Minimum = 0;
                sl.Maximum = 10;
                sl.IsSnapToTickEnabled = true;

                Label lb2 = new Label();
                lb2.Content = "0";
                lb2.HorizontalAlignment = HorizontalAlignment.Center;
                lb2.VerticalContentAlignment = VerticalAlignment.Center;
                lb2.FontSize = 20;
                lb2.Width = 110;
                lb.Foreground = Brushes.Brown;

                //data binding
                Binding myBinding = new Binding("Value");
                myBinding.Source = sl;
                lb2.SetBinding(ContentProperty,myBinding);

                sp.Children.Add(cb);
                sp.Children.Add(lb);
                sp.Children.Add(sl);
                sp.Children.Add(lb2);
                
                stackpanel1.Children.Add(sp);

            }
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
                string drinkName = targetLabel.Content.ToString();

                if(orders.ContainsKey(drinkName)) orders.Remove(drinkName);
                orders.Add(drinkName, quantity);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double total = 0.0;
            string discountString = "";
            string displayString = "訂購清單如下: \n";
            double sellPrice = 0.0;
            

            foreach(var item in orders)
            {
                string drinkName = item.Key;
                int quantity = orders[drinkName];
                int price = drinks[drinkName];
                total += price * quantity;
                displayString += $"{drinkName} X {quantity}杯，每杯{price}元，總共{price * quantity}元\n";
            }

            if (total >= 500)
            {
                discountString = "訂購滿500元以上打8折";
                sellPrice = total * 0.8;
            }
            else if (total >= 300)
            {
                discountString = "訂購滿300元以上打85折";
                sellPrice = total * 0.85;
            }
            else if (total >= 200)
            {
                discountString = "訂購滿200元以上打9折";
                sellPrice = total * 0.9;
            }
            else
            {
                discountString = "訂購未滿200不打折";
                sellPrice = total;
            }

            displayString += $"本次訂購{orders.Count}項飲品，{discountString}，售價{sellPrice}元。";
            textblock1.Text = displayString;
        }
    }
}
