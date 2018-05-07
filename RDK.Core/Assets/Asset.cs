namespace RDK.Assets
{
    using System;
    using System.Diagnostics.Contracts;
    using RDK.Helpers;
    using RDK.Managements;

    /// <summary>
    /// リポジトリの抽象クラスです。
    /// </summary>
    public abstract class Asset
    {
        private EventHandler<AssetChangedEventArgs> assetChanged;

        internal event EventHandler<AssetChangedEventArgs> AssetChanged
        {
            add
            {
                this.assetChanged += value.MakeWeakEventHandler(eventHandler => this.assetChanged -= eventHandler);
            }

            remove
            {
            }
        }

        /// <summary>
        /// 状態をクリアします。
        /// </summary>
        public abstract void Clear();

        internal void CallActiveDocumentChanged(object sender, ActiveDocumentChangedEventArgs e)
        {
            Contract.Assume(e != null);

            this.OnActiveDocumentChanged(e);
        }

        /// <summary>
        /// アセット変更を通知します。
        /// </summary>
        /// <param name="changeObject">変更オブジェクトです。</param>
        protected void RaiseRepositoryChanged(object changeObject)
        {
            if (this.assetChanged != null)
            {
                this.assetChanged(this, new AssetChangedEventArgs(this, changeObject));
            }
        }

        /// <summary>
        /// ドキュメントのアクティブ状態変更を通知します。
        /// </summary>
        /// <param name="e">イベント引数です。</param>
        protected virtual void OnActiveDocumentChanged(ActiveDocumentChangedEventArgs e)
        {
        }
    }
}
