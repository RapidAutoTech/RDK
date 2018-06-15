namespace RDK.Modules.Output
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows.Input;
    using RDK.Commands;
    using RDK.Managements;
    using RDK.ViewModels;
    using RDK.Windows.Controls;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// アウトプットビューのビューモデルクラスです。
    /// </summary>
    public class OutputViewModel : ToolViewModel
    {
        private readonly ObservableCollection<RichTextItem> logMessages =
            new ObservableCollection<RichTextItem>();


        private readonly ICollectionView logMessagesView;

        private readonly ICommand clearCommand;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public OutputViewModel()
        {
            this.logMessagesView = CollectionViewSource.GetDefaultView(this.logMessages);

            this.clearCommand =
                new ViewReceiverCommand<object>(this.ExecuteClear, this.ExecuteCanClear);

            BindingOperations.EnableCollectionSynchronization(this.logMessages, this.SyncObj);
        }

        /// <summary>
        /// パネル配置を取得します。
        /// </summary>
        public override PanelLocation Location
        {
            get => PanelLocation.Bottom;
        }

        /// <summary>
        /// メッセージを取得します。
        /// </summary>
        public ICollectionView LogMessages
        {
            get
            {
                return this.logMessagesView;
            }
        }

        /// <summary>
        /// クリアコマンドを取得します。
        /// </summary>
        public ICommand ClearCommand
        {
            get => this.clearCommand;
        }

        internal void AddMessage(string message)
        {
            var item = new RichTextItem()
            {
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Black,
                Margin = new Thickness(0),
                Text = message
            };

            this.logMessages.Add(item);
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
            BindingOperations.DisableCollectionSynchronization(this.logMessages);
        }

        private void ExecuteClear(object parameter)
        {
            this.logMessages.Clear();
        }

        private bool ExecuteCanClear(object parameter)
        {
            return true;
        }
    }
}
