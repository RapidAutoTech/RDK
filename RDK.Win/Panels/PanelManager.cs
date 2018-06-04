namespace RDK.Panels
{
    using System.ComponentModel;
    using System.Windows.Data;
    using RDK.Panels;

    public class PanelManager : PanelManagerBase, IPanelManager
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public PanelManager()
            : base()
        {
            BindingOperations.EnableCollectionSynchronization(this.Tools, this.SyncObj);
            BindingOperations.EnableCollectionSynchronization(this.Documents, this.SyncObj);
        }

        /// <summary>
        /// ツールを取得します。
        /// </summary>
        /// <returns>対象のインスタンスを返します。</returns>
        public ICollectionView GetTools()
        {
            return CollectionViewSource.GetDefaultView(this.Tools);
        }

        /// <summary>
        /// ドキュメントを取得します。
        /// </summary>
        /// <returns>対象のインスタンスを返します。</returns>
        public ICollectionView GetDocuments()
        {
            return CollectionViewSource.GetDefaultView(this.Documents);
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            BindingOperations.DisableCollectionSynchronization(this.Documents);
            BindingOperations.DisableCollectionSynchronization(this.Tools);
        }
    }
}
