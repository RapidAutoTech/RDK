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
    /// ツールのビューモデルクラスです。
    /// </summary>
    public class ToolViewModel : ToolViewModelBase
    {
        private readonly ICommand closeCommand;

        public ToolViewModel()
            : base()
        {
            this.closeCommand = new ViewReceiverCommand<object>(param => this.IsVisible = false, param => { return true; });
        }

        /// <summary>
        /// クローズコマンドを取得します。
        /// </summary>
        public override ICommand CloseCommand
        {
            get
            {
                return this.closeCommand;
            }
        }
    }
}
