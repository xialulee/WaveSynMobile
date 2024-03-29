﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WaveSynMobile.ViewModels {
    class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(
                ref T backingStore,
                T value,
                [CallerMemberName] string propertyName = "",
                Action onChanged = null) {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(
                sender: this,
                e: new PropertyChangedEventArgs(propertyName));
        }
    }
}
