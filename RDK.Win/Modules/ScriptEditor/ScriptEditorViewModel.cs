namespace RDK.Modules.ScriptEditor
{
    using System.Diagnostics.Contracts;
    using System.Windows.Input;
    using RDK.Applications;
    using RDK.Commands;
    using RDK.Managements;
    using RDK.ViewModels;

    /// <summary>
    /// スクリプトエディタのビューモデルクラスです。
    /// </summary>
    public sealed class ScriptEditorViewModel : ToolViewModel
    {
        private readonly ICommand executeCommand;
        private string expression = string.Empty;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public ScriptEditorViewModel()
        {
            this.executeCommand =
                new ViewReceiverCommand<object>(param => this.Execute(param), param => { return this.CanExecute(param); });
        }

        /// <summary>
        /// パネル配置を取得します。
        /// </summary>
        public override PanelLocation Location
        {
            get
            {
                return PanelLocation.Bottom;
            }
        }

        /// <summary>
        /// 実行コマンドを取得します。
        /// </summary>
        public ICommand ExecuteCommand
        {
            get
            {
                return this.executeCommand;
            }
        }

        /// <summary>
        /// スクリプト式の文字列を取得設定します。
        /// </summary>
        public string Expression
        {
            get
            {
                return this.expression;
            }

            set
            {
                this.SetPropertyForce(ref this.expression, value);
            }
        }

        private void Execute(object parameter)
        {
            var manager = GlobalManager.GetScriptManager();
            Contract.Assume(manager != null);

            manager.ExecuteFromString(this.Expression);
            GlobalManager.PushLoggerAndFlush(manager.Script.Logger);
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
