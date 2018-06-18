namespace RDK.Panels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Windows.Data;
    using RDK.Panels;
    using RDK.ViewModels;

    public class PanelManager : PanelManagerBase, IPanelManager
    {
        private readonly ObservableCollection<DocumentViewModel> documents =
            new ObservableCollection<DocumentViewModel>();

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public PanelManager()
            : base()
        {
            BindingOperations.EnableCollectionSynchronization(this.Tools, this.SyncObj);
            BindingOperations.EnableCollectionSynchronization(this.documents, this.SyncObj);
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
        /// ドキュメントを追加します。
        /// </summary>
        /// <param name="document">追加するドキュメントです。</param>
        public void AddDocument(DocumentViewModel document)
        {
            Contract.Assume(document != null);
            if (!this.documents.Contains(document))
            {
                this.documents.Add(document);
            }
        }

        /// <summary>
        /// ドキュメントを削除します。
        /// </summary>
        /// <param name="document">追加するドキュメントです。</param>
        public void RemoveDocument(DocumentViewModel document)
        {
            Contract.Assume(document != null);
            if (this.documents.Contains(document))
            {
                this.documents.Remove(document);
            }
        }

        /// <summary>
        /// 指定のドキュメントが含まれているか判定します。
        /// </summary>
        /// <param name="document">判定対象のドキュメントです。</param>
        /// <returns>含まれていれば、真を返します。</returns>
        public bool ContainDocument(DocumentViewModel document)
        {
            Contract.Assume(document != null);
            return this.documents.Contains(document);
        }

        /// <summary>
        /// ドキュメントを取得します。
        /// </summary>
        /// <returns>対象のインスタンスを返します。</returns>
        public ICollectionView GetDocuments()
        {
            return CollectionViewSource.GetDefaultView(this.documents);
        }

        protected ObservableCollection<DocumentViewModel> Documents
        {
            get
            {
                return this.documents;
            }
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
            this.Tools.Clear();
            this.documents.Clear();
            BindingOperations.DisableCollectionSynchronization(this.documents);
            BindingOperations.DisableCollectionSynchronization(this.Tools);
        }
    }
}
