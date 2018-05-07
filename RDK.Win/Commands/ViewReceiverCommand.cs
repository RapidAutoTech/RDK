namespace RDK.Commands
{
    using RDK.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ビュー応答コマンドクラスです。
    /// </summary>
    /// <typeparam name="T">応答対象の型です。</typeparam>
    public class ViewReceiverCommand<T> : ViewReceiverCommandBase<T>
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="execute">実行するコールバックです。</param>
        public ViewReceiverCommand(Action<T> execute)
            : base(execute)
        {
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="execute">実行するコールバックです。</param>
        /// <param name="canExecute">実行することが可能か確認するコールバックです。</param>
        public ViewReceiverCommand(Action<T> execute, Func<T, bool> canExecute)
            : base(execute, canExecute)
        {
        }

        protected override void ExecuteEventHandlerInternal(EventHandler eventHandler)
        {
            DispatcherHelper.UIDispatcher.InvokeAsync(() => eventHandler(this, EventArgs.Empty));
        }
    }
}
