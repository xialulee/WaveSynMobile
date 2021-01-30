using System;
using System.Collections.Generic;
using System.Text;

namespace WaveSynMobile.ViewModels
{
    class BaseSendViewModel : BaseViewModel
    {
        protected string ip;
        protected int port;
        protected int password;
        protected byte[] key;
        protected byte[] iv;

        protected string statusHTML;
        public string StatusHTML
        {
            get => this.statusHTML;
            set => this.SetProperty(ref this.statusHTML, value);
        }

        public BaseSendViewModel(string ip, int port, int password, byte[] key, byte[] iv) : base()
        {
            this.ip = ip;
            this.port = port;
            this.password = password;
            this.key = key;
            this.iv = iv;
        }
    }
}
