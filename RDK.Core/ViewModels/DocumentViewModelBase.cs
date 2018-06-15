namespace RDK.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using RDK.Commands;
    using RDK.Managements;

    /// <summary>
    /// ドキュメントのビューモデルクラスです。
    /// </summary>
    public abstract class DocumentViewModelBase : PanelViewModel, IDocumentable
    {
        private bool isForceClose = false;

        internal event EventHandler<ActiveChangedEventArgs> ActiveChanged;

        internal event EventHandler<DocumentClosedEventArgs> DocumentClosed;

        /// <summary>
        /// クローズコマンドを取得します。
        /// </summary>
        public abstract ICommand CloseCommand { get; }

        /// <summary>
        /// 強制クローズをするか状態を設定します。
        /// </summary>
        protected internal bool IsForceClose
        {
            set
            {
                this.isForceClose = value;
            }
        }

        /// <summary>
        /// ドキュメントをクローズします。
        /// </summary>
        public void Close()
        {
            if (this.isForceClose || this.TryClose())
            {
                if (this.DocumentClosed != null)
                {
                    this.DocumentClosed(this, new DocumentClosedEventArgs(this));
                }
            }
        }

        /// <summary>
        /// アクティブ状態変更を通知します。
        /// </summary>
        /// <param name="e">イベント引数です。</param>
        internal override void OnActiveChanged(ActiveChangedEventArgs e)
        {
            if (this.ActiveChanged != null)
            {
                this.ActiveChanged(this, e);
            }
        }

        /// <summary>
        /// クローズ処理を試みます。
        /// </summary>
        /// <returns>クローズ処理が可能な場合は真を返します。</returns>
        protected virtual bool TryClose()
        {
            return true;
        }
    }
}
