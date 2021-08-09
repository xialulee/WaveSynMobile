using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile.Widgets
{
    

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuantityEntry : ContentView
    {
        public static readonly BindableProperty QuantityNameProperty = BindableProperty.Create(
            nameof(QuantityName), 
            typeof(string), 
            typeof(QuantityEntry), 
            default(string), 
            BindingMode.OneWay);
        
        public string QuantityName
        {
            get => (string)GetValue(QuantityNameProperty);
            set => SetValue(QuantityNameProperty, value);
        }

        public static readonly BindableProperty QuantityValueProperty = BindableProperty.Create(
            nameof(QuantityValue),
            typeof(double),
            typeof(QuantityEntry),
            default(double),
            BindingMode.TwoWay);

        public double QuantityValue
        {
            get => (double)GetValue(QuantityValueProperty);
            set => SetValue(QuantityValueProperty, value);
        }

        private void OnQuantityValueEntryChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine(e.NewTextValue);
            QuantityValue = Convert.ToDouble(e.NewTextValue);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == QuantityNameProperty.PropertyName)
            {
                quantityNameLabel.Text = QuantityName;
            }
            else if (propertyName == QuantityValueProperty.PropertyName)
            {
                quantityValueEntry.Text = QuantityValue.ToString();
            }
        }

        public QuantityEntry()
        {
            InitializeComponent();
        }
    }
}