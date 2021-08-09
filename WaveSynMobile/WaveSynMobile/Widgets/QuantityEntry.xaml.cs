using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WaveSynMobile.Utils;

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

        public static readonly BindableProperty QuantityNumberProperty = BindableProperty.Create(
            nameof(QuantityNumber),
            typeof(double),
            typeof(QuantityEntry),
            default(double),
            BindingMode.TwoWay);

        public double QuantityNumber
        {
            get => (double)GetValue(QuantityNumberProperty);
            set => SetValue(QuantityNumberProperty, value);
        }

        private void OnQuantityNumberEntryChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == "")
            {
                QuantityNumber = 0.0;
            }
            else
            {
                QuantityNumber = Convert.ToDouble(e.NewTextValue);
            }
        }

        private string _oldUnit = "";

        private void OnQuantityUnitPickerChanged(object sender, EventArgs e)
        {
            var newUnit = (string)quantityUnitPicker.SelectedItem;

            if (_oldUnit == "")
            {
                _oldUnit = newUnit;
                return;
            }

            if (QuantityNumber != 0.0) 
            {
                QuantityNumber *= PhysicalQuantities.Units[_oldUnit] / PhysicalQuantities.Units[newUnit];
            }
            _oldUnit = newUnit;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == QuantityNameProperty.PropertyName)
            {
                quantityNameLabel.Text = QuantityName;
            }
            else if (propertyName == QuantityNumberProperty.PropertyName)
            {
                quantityNumberEntry.Text = QuantityNumber.ToString();
            }
            else if (propertyName == QuantityTypeProperty.PropertyName)
            {
                switch (QuantityType.ToLower())
                {
                    case "frequency":
                        foreach (var unit in new List<string> { "Hz", "kHz", "MHz", "GHz"}) 
                        {
                            quantityUnitPicker.Items.Add(unit);
                        }
                        quantityUnitPicker.SelectedItem = "Hz";
                        break;
                    case "length":
                        foreach (var unit in new List<string> { "nm", "μm", "mm", "cm", "dm", "m", "km" })
                        {
                            quantityUnitPicker.Items.Add(unit);
                        }
                        quantityUnitPicker.SelectedItem = "m";
                        break;
                    case "time":
                        foreach (var unit in new List<string> { "ns", "μs", "ms", "s" })
                        {
                            quantityUnitPicker.Items.Add(unit);
                        }
                        quantityUnitPicker.SelectedItem = "s";
                        break;
                }
            }
            else if (propertyName == QuantityUnitPickerWidthProperty.PropertyName)
            {
                quantityUnitPicker.WidthRequest = QuantityUnitPickerWidth;
            }
            
        }

        public string QuantityUnit
        {
            get => (string)quantityUnitPicker.SelectedItem;
        }

        public QuantityEntry()
        {
            InitializeComponent();
        }
    }
}