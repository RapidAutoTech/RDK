namespace RDK.ViewModels
{
    using RDK.Applications;
    using RDK.Menus;
    using RDK.Panels;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// ワークスペースビューモデルクラスです。
    /// </summary>
    public class WorkspaceViewModel : ViewModel
    {
        private DocumentViewModel activeDocument = null;

        private ICollectionView fileNewMenus = null;
        private ICollectionView fileOpenMenus = null;
        private ICollectionView fileSaveMenus = null;
        private ICollectionView toolMenus = null;

        private ICollectionView documents = null;
        private ICollectionView tools = null;
        private ICollectionView viewMenus = null;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public WorkspaceViewModel()
        {
        }

        /// <summary>
        /// アクティブドキュメントを取得設定します。
        /// </summary>
        public DocumentViewModel ActiveDocument
        {
            get => this.activeDocument;

            set
            {
                this.SetProperty(ref this.activeDocument, value);
                GlobalManager.SetActiveDocument(value);
            }
        }

        /// <summary>
        /// ファイルニューメニューを取得します。
        /// </summary>
        public ICollectionView FileNewMenus
        {
            get
            {
                if (this.fileNewMenus == null)
                {
                    IMenuManager manager = GlobalManager.GetMenuManager();
                    Contract.Assume(manager != null);
                    this.fileNewMenus = manager.GetMenus(MenuKind.FileNew);
                }

                return this.fileNewMenus;
            }
        }

        /// <summary>
        /// ファイルオープンメニューを取得します。
        /// </summary>
        public ICollectionView FileOpenMenus
        {
            get
            {
                if (this.fileOpenMenus == null)
                {
                    IMenuManager manager = GlobalManager.GetMenuManager();
                    Contract.Assume(manager != null);
                    this.fileOpenMenus = manager.GetMenus(MenuKind.FileOpen);
                }

                return this.fileOpenMenus;
            }
        }

        /// <summary>
        /// ファイルセーブメニューを取得します。
        /// </summary>
        public ICollectionView FileSaveMenus
        {
            get
            {
                if (this.fileSaveMenus == null)
                {
                    IMenuManager manager = GlobalManager.GetMenuManager();
                    Contract.Assume(manager != null);
                    this.fileSaveMenus = manager.GetMenus(MenuKind.FileSave);
                }

                return this.fileSaveMenus;
            }
        }

        /// <summary>
        /// ツールメニューを取得します。
        /// </summary>
        public ICollectionView ToolMenus
        {
            get
            {
                if (this.toolMenus == null)
                {
                    IMenuManager manager = GlobalManager.GetMenuManager();
                    Contract.Assume(manager != null);
                    this.toolMenus = manager.GetMenus(MenuKind.Tool);
                }

                return this.toolMenus;
            }
        }

        /// <summary>
        /// ドキュメントを取得します。
        /// </summary>
        public ICollectionView Documents
        {
            get
            {
                if (this.documents == null)
                {
                    IPanelManager manager = GlobalManager.GetPanelManager();
                    Contract.Assume(manager != null);
                    this.documents = manager.GetDocuments();
                }

                return this.documents;
            }
        }

        /// <summary>
        /// ツールを取得します。
        /// </summary>
        public ICollectionView Tools
        {
            get
            {
                if (this.tools == null)
                {
                    IPanelManager manager = GlobalManager.GetPanelManager();
                    Contract.Assume(manager != null);
                    this.tools = manager.GetTools();
                }

                return this.tools;
            }
        }

        /// <summary>
        /// ビューを取得します。
        /// </summary>
        public ICollectionView ViewMenus
        {
            get
            {
                if (this.viewMenus == null)
                {
                    IMenuManager manager = GlobalManager.GetMenuManager();
                    Contract.Assume(manager != null);
                    this.viewMenus = manager.GetViewMenus();
                }

                return this.viewMenus;
            }
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
        }
    }
}