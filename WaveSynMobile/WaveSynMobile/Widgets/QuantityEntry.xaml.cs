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
        public static readonly BindableProperty QuantityTypeProperty = BindableProperty.Create(
            nameof(QuantityType),
            typeof(string),
            typeof(QuantityEntry),
            default(string),
            BindingMode.OneWay);

        public string QuantityType
        {
            get => (string)GetValue(QuantityTypeProperty);
            set => SetValue(QuantityTypeProperty, value);
        }

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

        public static readonly BindableProperty QuantityUnitPickerWidthProperty = BindableProperty.Create(
            nameof(QuantityUnitPickerWidth),
            typeof(double),
            typeof(QuantityEntry),
            default(double),
            BindingMode.OneWay);

        public double QuantityUnitPickerWidth
        {
            get => (double)GetValue(QuantityUnitPickerWidthProperty);
            set => SetValue(QuantityUnitPickerWidthProperty, value);
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
            else if (propertyName == QuantityTypeProperty.PropertyName)
            {
                switch (QuantityType.ToLower())
                {
                    case "frequency":
                        quantityUnitPicker.Items.Add("Hz");
                        quantityUnitPicker.Items.Add("kHz");
                        quantityUnitPicker.Items.Add("MHz");
                        quantityUnitPicker.Items.Add("GHz");
                        quantityUnitPicker.SelectedIndex = 0;
                        break;
                }
            }
            else if (propertyName == QuantityUnitPickerWidthProperty.PropertyName)
            {
                quantityUnitPicker.WidthRequest = QuantityUnitPickerWidth;
            }
            
        }

        public QuantityEntry()
        {
            InitializeComponent();
        }
    }
}