namespace RDK.Panels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using RDK.Managements;
    using RDK.ViewModels;

    /// <summary>
    /// パネルプレゼンタークラスです。
    /// </summary>
    public class PanelManagerBase : Manager, IPanelManagerBase
    {
        private readonly ObservableCollection<IToolable> tools =
            new ObservableCollection<IToolable>();

        private readonly ObservableCollection<DocumentViewModelBase> documents =
            new ObservableCollection<DocumentViewModelBase>();

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public PanelManagerBase()
            : base(Enum.GetName(typeof(ManagerKind), ManagerKind.Panel))
        {
        }

        /// <summary>
        /// ツールを追加します。
        /// </summary>
        /// <param name="tool">追加するツールです。</param>
        public void AddTool(IToolable tool)
        {
            Contract.Assume(tool != null);
            if (!this.tools.Contains(tool))
            {
                this.tools.Add(tool);
            }
        }

        /// <summary>
        /// ドキュメントを追加します。
        /// </summary>
        /// <param name="document">追加するドキュメントです。</param>
        public void AddDocument(DocumentViewModelBase document)
        {
            Contract.Assume(document != null);
            if (!this.documents.Contains(document))
            {
                this.documents.Add(document);
            }
        }

        /// <summary>
        /// ツールを削除します。
        /// </summary>
        /// <param name="tool">削除するツールです。</param>
        public void RemoveTool(IToolable tool)
        {
            Contract.Assume(tool != null);
            if (this.tools.Contains(tool))
            {
                this.tools.Remove(tool);
            }
        }

        /// <summary>
        /// ドキュメントを削除します。
        /// </summary>
        /// <param name="document">追加するドキュメントです。</param>
        public void RemoveDocument(DocumentViewModelBase document)
        {
            Contract.Assume(document != null);
            if (this.documents.Contains(document))
            {
                this.documents.Remove(document);
            }
        }

        /// <summary>
        /// 指定のツールが含まれているか判定します。
        /// </summary>
        /// <param name="tool">判定対象のツールです。</param>
        /// <returns>含まれていれば、真を返します。</returns>
        public bool ContainTool(IToolable tool)
        {
            Contract.Assume(tool != null);
            return this.tools.Contains(tool);
        }

        /// <summary>
        /// 指定のドキュメントが含まれているか判定します。
        /// </summary>
        /// <param name="document">判定対象のドキュメントです。</param>
        /// <returns>含まれていれば、真を返します。</returns>
        public bool ContainDocument(DocumentViewModelBase document)
        {
            Contract.Assume(document != null);
            return this.documents.Contains(document);
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
            this.documents.Clear();
            this.tools.Clear();
            //BindingOperations.DisableCollectionSynchronization(this.documents);
            //BindingOperations.DisableCollectionSynchronization(this.tools);
        }

        protected ObservableCollection<IToolable> Tools
        {
            get
            {
                return this.tools;
            }
        }

        protected ObservableCollection<DocumentViewModelBase> Documents
        {
            get
            {
                return this.documents;
            }
        }
    }
}
