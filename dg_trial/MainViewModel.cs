using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace dg_trial
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public bool IsChecked { get; set; }

        public long InvoiceInfoId { get; set; }

        public DateTimeOffset Time { get; set; }

        public string BatchName { get; set; }

        public int NumberInBatch { get; set; }

        public string InvoiceType { get; set; }

        public string InvoiceCode { get; set; }

        public string InvoiceNumber { get; set; }

        public double TotalAmountIncludingTax { get; set; }

        public DateTimeOffset VerifyingTime { get; set; }

        public string BuyerName { get; set; }

        /// <summary>
        /// 购买方纳税人识别号
        /// </summary>
        public string BuyerId { get; set; }

        public string SellerName { get; set; }

        /// <summary>
        /// 销售方纳税人识别号
        /// </summary>
        public string SellerId { get; set; }

        public RecognizingStatus RecognizingStatus { get; set; }

        public string RecognizingFailedMessage { get; set; }

        public InvoiceVerifyingStatus VerifyingStatus { get; set; }

        public string VerifyingFailedMessage { get; set; }


        /// <summary>
        /// 编辑发票序号
        /// </summary>
        public ICommand EditSerialNumberCommand
        {
            get
            {
                return new RelayCommand(_ => {

                });
            }
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ItemViewModel> Items { get; set; } = new ObservableCollection<ItemViewModel>();


        public MainViewModel()
        {
            for(int i =0; i < 20; i++)
            {
                this.Items.Add(new ItemViewModel() { BatchName = DateTime.Now.ToShortDateString(), NumberInBatch = i });
            }
        }


        private void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public enum InvoiceVerifyingStatus
    {
    }

    public enum RecognizingStatus
    {
        Unknown = 0,
        NotRecognized = 1,
        Recognizing = 2,
        Recognized = 3,
        Failed = 4
    }
}
