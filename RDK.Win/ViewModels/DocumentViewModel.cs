namespace RDK.ViewModels
{
    using RDK.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// ドキュメントのビューモデルクラスです。
    /// </summary>
    public abstract class DocumentViewModel : DocumentViewModelBase
    {
        private readonly ICommand closeCommand;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        protected DocumentViewModel()
        {
            this.closeCommand =
                new ViewReceiverCommand<object>(param => this.Close(), param => { return true; });
        }

        /// <summary>
        /// クローズコマンドを取得します。
        /// </summary>
        public override ICommand CloseCommand
        {
            get => this.closeCommand;
        }
    }
}
