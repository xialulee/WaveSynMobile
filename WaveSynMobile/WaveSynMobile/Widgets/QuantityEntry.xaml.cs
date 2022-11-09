using System;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WaveSynMobile.Utils;

namespace WaveSynMobile.Widgets {

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuantityEntry : ContentView {

        public event EventHandler InputFinished;

        public void OnOkButtonClicked(object sender, EventArgs e) {
            InputFinished?.Invoke(this, e);
        }

        public void OnClearButtonClicked(object sender, EventArgs e) {
            quantityNumberEntry.Text = "";
        }

        public static readonly BindableProperty QuantityTypeProperty = BindableProperty.Create(
            nameof(QuantityType),
            typeof(string),
            typeof(QuantityEntry),
            default(string),
            BindingMode.OneWay);

        public string QuantityType {
            get => (string)GetValue(QuantityTypeProperty);
            set => SetValue(QuantityTypeProperty, value);
        }

        public static readonly BindableProperty QuantityValidProperty = BindableProperty.Create(
            nameof(QuantityValid),
            typeof(bool),
            typeof(QuantityEntry),
            default(bool),
            BindingMode.OneWayToSource);

        public bool QuantityValid {
            get => (bool)GetValue(QuantityValidProperty);
            set => SetValue(QuantityValidProperty, value);
        }

        public static readonly BindableProperty QuantityNameProperty = BindableProperty.Create(
            nameof(QuantityName),
            typeof(string),
            typeof(QuantityEntry),
            default(string),
            BindingMode.OneWay);

        public string QuantityName {
            get => (string)GetValue(QuantityNameProperty);
            set => SetValue(QuantityNameProperty, value);
        }

        public static readonly BindableProperty QuantityUnitPickerWidthProperty = BindableProperty.Create(
            nameof(QuantityUnitPickerWidth),
            typeof(double),
            typeof(QuantityEntry),
            default(double),
            BindingMode.OneWay);

        public double QuantityUnitPickerWidth {
            get => (double)GetValue(QuantityUnitPickerWidthProperty);
            set => SetValue(QuantityUnitPickerWidthProperty, value);
        }

        public static readonly BindableProperty IsOkButtonVisibleProperty = BindableProperty.Create(
            nameof(IsOkButtonVisible),
            typeof(bool),
            typeof(QuantityEntry),
            true,
            BindingMode.OneWay);

        public bool IsOkButtonVisible {
            get => (bool) GetValue(IsOkButtonVisibleProperty);
            set => SetValue(IsOkButtonVisibleProperty, value);
        }

        public static readonly BindableProperty IsClearButtonVisibleProperty = BindableProperty.Create(
            nameof(IsClearButtonVisible),
            typeof(bool),
            typeof(QuantityEntry),
            true,
            BindingMode.OneWay);

        public bool IsClearButtonVisible {
            get => (bool) GetValue(IsClearButtonVisibleProperty);
            set => SetValue(IsClearButtonVisibleProperty, value);
        }

        public static readonly BindableProperty QuantityNumberProperty = BindableProperty.Create(
            nameof(QuantityNumber),
            typeof(double),
            typeof(QuantityEntry),
            default(double),
            BindingMode.TwoWay);

        public double QuantityNumber {
            get => (double)GetValue(QuantityNumberProperty);
            set => SetValue(QuantityNumberProperty, value);
        }

        private void OnQuantityNumberEntryChanged(object sender, TextChangedEventArgs e) {
            try {
                QuantityNumber = Convert.ToDouble(e.NewTextValue);
                QuantityValid = true;
            } catch (FormatException) {
                QuantityNumber = 0.0;
                QuantityValid = false;
            }
        }

        public static readonly BindableProperty QuantityUnitProperty = BindableProperty.Create(
            nameof(QuantityUnit),
            typeof(string),
            typeof(QuantityEntry),
            default(string),
            BindingMode.OneWayToSource);

        public string QuantityUnit {
            get => (string)GetValue(QuantityUnitProperty);
            set => SetValue(QuantityUnitProperty, value);
        }

        private string _oldUnit = "";

        private void OnQuantityUnitPickerChanged(object sender, EventArgs e) {
            var newUnit = (string)quantityUnitPicker.SelectedItem;
            QuantityUnit = newUnit;

            if (_oldUnit == "") {
                _oldUnit = newUnit;
                return;
            }

            if (QuantityNumber != 0.0) {
                QuantityNumber *= PhysicalQuantities.Units[_oldUnit] / PhysicalQuantities.Units[newUnit];
            }
            _oldUnit = newUnit;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName == QuantityNameProperty.PropertyName) {
                quantityNameLabel.Text = QuantityName;
            } else if (propertyName == QuantityNumberProperty.PropertyName) {
                quantityNumberEntry.Text = QuantityNumber.ToString();
            } else if (propertyName == QuantityTypeProperty.PropertyName) {
                switch (QuantityType.ToLower()) {
                    case "frequency":
                        foreach (var unit in PhysicalQuantities.UnitsOfFrequency.Keys) {
                            quantityUnitPicker.Items.Add(unit);
                        }
                        quantityUnitPicker.SelectedItem = "Hz";
                        break;
                    case "length":
                        foreach (var unit in PhysicalQuantities.UnitsOfLength.Keys) {
                            quantityUnitPicker.Items.Add(unit);
                        }
                        quantityUnitPicker.SelectedItem = "m";
                        break;
                    case "time":
                        foreach (var unit in PhysicalQuantities.UnitsOfTime.Keys) {
                            quantityUnitPicker.Items.Add(unit);
                        }
                        quantityUnitPicker.SelectedItem = "s";
                        break;
                    case "angle":
                        foreach (var unit in PhysicalQuantities.UnitsOfAngle.Keys) {
                            quantityUnitPicker.Items.Add(unit);
                        }
                        quantityUnitPicker.SelectedItem = "°";
                        break;
                    default:
                        break;
                }
            } else if (propertyName == QuantityUnitPickerWidthProperty.PropertyName) {
                quantityUnitPicker.WidthRequest = QuantityUnitPickerWidth;
            }

        }

        public QuantityEntry() {
            InitializeComponent();
        }
    }
}