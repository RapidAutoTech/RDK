namespace RDK.Menus
{
    using RDK.Commands;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Input;

    public class Menu : MenuBase
    {
        private readonly object syncRoot = new object();
        private readonly ICommand command;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="execute">メニュー選択時に実行するコールバックです。</param>
        /// <param name="canExecute">メニュー選択時に実行可能か確認するコールバックです。</param>
        public Menu(Action<object> execute, Func<object, bool> canExecute)
            : base()
        {
            Contract.Requires(execute != null);
            Contract.Requires(canExecute != null);

            this.command = new ViewReceiverCommand<object>(execute, canExecute);
            BindingOperations.EnableCollectionSynchronization(this.SubMenus, this.syncRoot);
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public Menu()
        {
            this.command = new ViewReceiverCommand<object>(param => this.Execute(param), param => this.CanExecute(param));
            BindingOperations.EnableCollectionSynchronization(this.SubMenus, this.syncRoot);
        }

        /// <summary>
        /// コマンドを取得します。
        /// </summary>
        public override ICommand Command
        {
            get
            {
                return this.command;
            }
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
            BindingOperations.DisableCollectionSynchronization(this.SubMenus);
            base.DisposeInternal();
        }
    }
}
