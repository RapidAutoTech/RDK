namespace RDK.Applications
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using RDK.Bridges;
    using RDK.Helpers;
    using RDK.Logs;
    using RDK.Menus;
    using RDK.Operations;
    using RDK.Panels;
    using RDK.Plugins;
    using RDK.Assets;
    using RDK.Managements;
    using RDK.ViewModels;

    /// <summary>
    /// 拡張側で処理を行う場合の全体を管理するマネージャークラスです。
    /// </summary>
    public abstract class GlobalManagerBase
    {
        /// <summary>
        /// オペレーションマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string OperationManagerKey;

        /// <summary>
        /// ログマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string LogManagerKey;

        /// <summary>
        /// プラグインマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string PluginManagerKey;

        /// <summary>
        /// パネルマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string PanelManagerKey;

        /// <summary>
        /// メニューマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string MenuManagerKey;

        /// <summary>
        /// アセットマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string AssetManagerKey;

        /// <summary>
        /// スクリプトマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string ScriptManagerKey;

        protected internal static readonly Global Instance = new Global();

        private static readonly object SyncRoot = new object();
        private static readonly Dictionary<string, IManager> Managers =
            new Dictionary<string, IManager>();

        /// <summary>
        /// 静的クラスのコンストラクタです。
        /// </summary>
        static GlobalManagerBase()
        {
            OperationManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Operation);
            LogManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Log);
            PluginManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Plugin);
            PanelManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Panel);
            MenuManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Menu);
            AssetManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Asset);
            ScriptManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Script);
        }

        /*
        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <returns>タスクを返します。</returns>
        public static async Task Initialize()
        {
            var pluginManager = Instance.GetManager<PluginManager>(PluginManagerKey);
            Contract.Assume(pluginManager != null);

            var menuPresenter = Instance.GetManager<MenuManager>(MenuManagerKey);
            Contract.Assume(menuPresenter != null);

            var scriptPresenter = Instance.GetManager<ScriptManager>(ScriptManagerKey);
            Contract.Assume(scriptPresenter != null);

            await pluginManager.Initialize();

            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetMenuFactories())
                {
                    foreach (var menu in factory.GetMenus())
                    {
                        menuPresenter.AddMenu(menu.Kind, menu);
                    }
                }

                foreach (var menu in CreateViewMenus())
                {
                    menuPresenter.AddViewMenu(menu);
                }
            });

            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetOperationFactories())
                {
                    factory.SetCreateMessageDelegate(scriptPresenter.Bridge.CreateMessage);
                }
            });

            await Task.Run(() =>
            {
                scriptPresenter.Script.SetRunScriptOperationDelegate(Instance.CallRunScriptOperation);
                scriptPresenter.Script.SetEchoInfoDelegate(Instance.CallEchoInfo);
                scriptPresenter.Script.SetEchoErrorDelegate(Instance.CallEchoError);
            });

            await Task.Run(() =>
            {
                var assetManager = Instance.GetManager<AssetManager>(AssetManagerKey);
                Contract.Assume(assetManager != null);

                foreach (var asset in assetManager.GetAssets())
                {
                    Instance.AssetActiveDocumentChanged += asset.CallActiveDocumentChanged;
                }
            });

            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetDialogFactories())
                {
                    factory.Owner = Instance.Owner;
                }
            });
        }
        */

        /// <summary>
        /// オーナーウィンドウを設定します。
        /// </summary>
        /// <param name="owner">オーナーになるウィンドウです。</param>
        public static void SetOwner(IWindow owner)
        {
            Instance.SetOwner(owner);
        }

        /// <summary>
        /// オペレーションマネージャーを取得します。
        /// </summary>
        /// <returns>オペレーションマネージャーのインスタンスを返します。</returns>
        public static IOperationManager GetOperationManager()
        {
            return Instance.GetManager<IOperationManager>(OperationManagerKey);
        }

        /// <summary>
        /// ログマネージャーを取得します。
        /// </summary>
        /// <returns>ログマネージャーのインスタンスを返します。</returns>
        public static ILogManager GetLogManager()
        {
            return Instance.GetManager<ILogManager>(LogManagerKey);
        }

        /// <summary>
        /// プラグインマネージャーを取得します。
        /// </summary>
        /// <returns>プラグインマネージャーのインスタンスを返します。</returns>
        public static IPluginManager GetPluginManager()
        {
            return Instance.GetManager<IPluginManager>(PluginManagerKey);
        }

        /// <summary>
        /// アセットマネージャーを取得します。
        /// </summary>
        /// <returns>アセットマネージャーのインスタンスを返します。</returns>
        public static IAssetManager GetAssetManager()
        {
            return Instance.GetManager<IAssetManager>(AssetManagerKey);
        }        

        /// <summary>
        /// 指定されたユニットの中のファクトリからドキュメントを作成します。
        /// </summary>
        /// <param name="unitKey">ユニットキーです。</param>
        /// <param name="factoryKey">ファクトリキーです。</param>
        /// <returns>ドキュメントのインスタンスを返します。</returns>
        public static IDocumentable CreateDocument(string unitKey, string factoryKey)
        {
            return Instance.CreateDocument(unitKey, factoryKey);
        }

        /// <summary>
        /// ドキュメントを追加します。
        /// </summary>
        /// <param name="document">追加するドキュメントです。</param>
        public static void AddDocument(IDocumentable document)
        {
            Contract.Requires(document is DocumentViewModelBase);

            Instance.AddDocument((DocumentViewModelBase)document);
        }

        /// <summary>
        /// 指定ファクトリからスクリプトオペレーションを作成します。
        /// </summary>
        /// <param name="factoryKey">ファクトリキーです。</param>
        /// <param name="args">オペレーション作成時に渡す引数です。</param>
        /// <returns>オペレーションのインスタンスを返します。</returns>
        public static Operation CreateScriptOperation(string factoryKey, params object[] args)
        {
            return Instance.CreateScriptOperation(factoryKey, args);
        }

        /// <summary>
        /// 指定オペレーションを実行します。
        /// </summary>
        /// <param name="operation">実行するオペレーションです。</param>
        public static void RunOperation(Operation operation)
        {
            Instance.RunOperation(operation);
        }

        /// <summary>
        /// ロガーをプッシュします。
        /// </summary>
        /// <param name="logger">プッシュするロガーです。</param>
        public static void PushLogger(Logger logger)
        {
            Instance.PushLogger(logger);
        }

        /// <summary>
        /// ログをフラッシュします。
        /// この処理を実行してためているログをすべて出力します。
        /// </summary>
        public static void FlushLogs()
        {
            Instance.FlushLogs();
        }

        /// <summary>
        /// ロガーをプッシュ後フラッシュします。
        /// </summary>
        /// <param name="logger">プッシュするロガーです。</param>
        public static void PushLoggerAndFlush(Logger logger)
        {
            Instance.PushLoggerAndFlush(logger);
        }

        /*
        /// <summary>
        /// デフォルトツールを作成します。
        /// </summary>
        public static void MakeDefaultTools()
        {
            var panelManager = Instance.GetManager<PanelManager>(PanelManagerKey);
            Contract.Assume(panelManager != null);

            ToolViewModel outputViewModel = null;
            {
                var factory = GetToolFactory(typeof(OutputToolFactory).FullName);
                outputViewModel = AddTool(factory, panelManager);
            }

            {
                var factory = GetToolFactory(typeof(ScriptEditorToolFactory).FullName);
                AddTool(factory, panelManager);
            }

            outputViewModel.IsActive = true;
        }
        */

        internal static void SetActiveDocument(IDocumentable document)
        {
            Instance.SetActiveDocument(document);
        }

        internal static ScriptManager GetScriptManager()
        {
            return Instance.GetManager<ScriptManager>(ScriptManagerKey);
        }

        internal static PluginToolFactory GetToolFactory(string factoryFullName)
        {
            return Instance.GetManager<PluginManagerBase>(PluginManagerKey).GetToolFactory(factoryFullName);
        }

        protected internal static IToolable AddTool(PluginToolFactory factory, IPanelManagerBase panelManager)
        {
            Contract.Requires(factory != null);
            Contract.Requires(panelManager != null);

            var viewModel = factory.CreateTool();
            viewModel.IsVisible = true;
            viewModel.IsActive = true;

            if (!panelManager.ContainTool(viewModel))
            {
                var internalViewModel = viewModel as ToolViewModelBase;
                if (internalViewModel != null)
                {
                    Instance.ActiveDocumentChanged += internalViewModel.CallActiveDocumentChanged;

                    foreach (var asset in Instance.GetManager<AssetManager>(AssetManagerKey).GetAssets())
                    {
                        asset.AssetChanged += internalViewModel.CallAssetChanged;
                    }
                }                

                panelManager.AddTool(viewModel);
            }

            return viewModel;
        }

        /// <summary>
        /// マネージャーを追加します。内部処理
        /// </summary>
        /// <param name="managerKey">追加するマネージャーのキーです。</param>
        /// <param name="manager">追加するマネージャーです。</param>
        protected void AddManagerInternal(string managerKey, IManager manager)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(managerKey));
            Contract.Requires(manager != null);

            Instance.AddManager(managerKey, manager);
        }

        /// <summary>
        /// マネージャーをクリアします。内部処理
        /// </summary>
        protected void ClearManagersInternal()
        {
            Instance.ClearManagers();
        }

        /*
        private static IEnumerable<Menu> CreateViewMenus()
        {
            var pluginPresenter = Instance.GetManager<PluginManager>(PluginManagerKey);
            Contract.Assume(pluginPresenter != null);

            var panelPresenter = Instance.GetManager<PanelManager>(PanelManagerKey);
            Contract.Assume(panelPresenter != null);

            foreach (var factory in pluginPresenter.GetToolFactories())
            {
                Menu menu = new Menu(
                    param =>
                    {
                        AddTool(factory, panelPresenter);
                    },
                    param =>
                    {
                        return true;
                    });

                menu.Label = factory.Label;
                yield return menu;
            }
        }
        */

        /// <summary>
        /// グローバルクラスです。
        /// </summary>
        protected internal class Global : Manager
        {
            private readonly Dictionary<string, IManager> managers =
                new Dictionary<string, IManager>();

            private readonly WeakReference<IDocumentable> activeDocument =
                new WeakReference<IDocumentable>(null);

            private IWindow owner = null;

            private EventHandler<ActiveDocumentChangedEventArgs> assetActiveDocumentChanged;
            private EventHandler<ActiveDocumentChangedEventArgs> activeDocumentChanged;

            /// <summary>
            /// コンストラクタです。
            /// </summary>
            public Global()
                : base("Global")
            {
            }

            internal event EventHandler<ActiveDocumentChangedEventArgs> AssetActiveDocumentChanged
            {
                add
                {
                    this.assetActiveDocumentChanged += value.MakeWeakEventHandler(eventHandler => this.assetActiveDocumentChanged -= eventHandler);
                }

                remove
                {
                }
            }

            internal event EventHandler<ActiveDocumentChangedEventArgs> ActiveDocumentChanged
            {
                add
                {
                    this.activeDocumentChanged += value.MakeWeakEventHandler(eventHandler => this.activeDocumentChanged -= eventHandler);
                }

                remove
                {
                }
            }

            internal IWindow Owner
            {
                get
                {
                    return this.owner;
                }
            }

            internal Dictionary<string, IManager> Managers
            {
                get
                {
                    return this.managers;
                }
            }

            internal WeakReference<IDocumentable> ActiveDocument
            {
                get
                {
                    return this.activeDocument;
                }
            }

            internal TManager GetManager<TManager>(string managerKey) where TManager : class, IManager
            {
                return this.managers[managerKey] as TManager;
            }

            internal void AddManager(string managerKey, IManager manager)
            {
                Contract.Requires(!string.IsNullOrWhiteSpace(managerKey));
                Contract.Requires(manager != null);

                lock (this.SyncObj)
                {
                    this.managers.Add(managerKey, manager);
                }
            }

            internal void ClearManagers()
            {
                lock (this.SyncObj)
                {
                    this.managers.Clear();
                }
            }

            internal void SetOwner(IWindow owner)
            {
                Contract.Requires(owner != null);
                this.owner = owner;
            }

            internal void SetActiveDocument(IDocumentable document)
            {
                this.ActiveDocument.SetTarget(document);

                if (this.assetActiveDocumentChanged != null)
                {
                    this.assetActiveDocumentChanged(this, new ActiveDocumentChangedEventArgs(document));
                }

                if (this.activeDocumentChanged != null)
                {
                    this.activeDocumentChanged(this, new ActiveDocumentChangedEventArgs(document));
                }
            }


            internal Operation CreateScriptOperation(string factoryKey, params object[] args)
            {
                var pluginPresenter = this.GetManager<PluginManagerBase>(PluginManagerKey);
                Contract.Assume(pluginPresenter != null);

                var factory = pluginPresenter.GetOperationFactory(factoryKey);
                return factory.CreateScriptOperation(args);
            }

            internal void RunOperation(Operation operation)
            {
                Contract.Requires(operation != null);

                var operationManager = this.GetManager<OperationManager>(OperationManagerKey);
                Contract.Assume(operationManager != null);

                operationManager.Run(operation);

                if (operation.IsScriptable)
                {
                    Logger logger = new Logger();
                    logger.WriteInformation(operation.ScriptMessage);
                    this.PushLoggerAndFlush(logger);
                }
            }

            internal DocumentViewModelBase CreateDocument(string unitKey, string factoryKey)
            {
                var pluginPresenter = this.GetManager<PluginManagerBase>(PluginManagerKey);
                Contract.Assume(pluginPresenter != null);

                var factory = pluginPresenter.GetDocumentFactory(unitKey, factoryKey);
                Contract.Assume(factory != null);

                var viewModel = factory.CreateDocument();
                Contract.Assume(viewModel != null);

                viewModel.DocumentClosed += this.CallDocumentClosed;

                return viewModel;
            }

            internal void AddDocument(DocumentViewModelBase document)
            {
                Contract.Requires(document != null);

                var panelPresenter = this.GetManager<PanelManagerBase>(PanelManagerKey);
                Contract.Assume(panelPresenter != null);

                panelPresenter.AddDocument(document);
            }

            internal void PushLogger(Logger logger)
            {
                Contract.Assume(logger != null);

                var logPresenter = this.GetManager<LogManager>(LogManagerKey);
                Contract.Assume(logPresenter != null);

                logPresenter.PushLogger(logger);
            }

            internal void FlushLogs()
            {
                var logManager = this.GetManager<LogManager>(LogManagerKey);
                Contract.Assume(logManager != null);

                logManager.Flush();
            }

            internal void PushLoggerAndFlush(Logger logger)
            {
                Contract.Assume(logger != null);

                var logManager = this.GetManager<LogManager>(LogManagerKey);
                Contract.Assume(logManager != null);

                logManager.PushLogger(logger);
                logManager.Flush();
            }

            internal bool CallRunScriptOperation(out object returnValue, string factoryKey, params object[] args)
            {
                var operation = this.CreateScriptOperation(factoryKey, args);
                this.RunOperation(operation);
                returnValue = operation.ReturnValue;
                return operation.Result;
            }

            internal void CallEchoInfo(string message)
            {
                Logger logger = new Logger();
                logger.WriteInformation(message);
                this.PushLoggerAndFlush(logger);
            }

            internal void CallEchoError(string message)
            {
                Logger logger = new Logger();
                logger.WriteError(message);
                this.PushLoggerAndFlush(logger);
            }

            protected override void DisposeInternal()
            {
                this.ClearManagers();
            }

            private void CallDocumentClosed(object sender, DocumentClosedEventArgs e)
            {
                Contract.Assume(sender != null);
                Contract.Assume(e != null);

                var panelManager = this.GetManager<PanelManagerBase>(PanelManagerKey);
                Contract.Assume(panelManager != null);

                IDocumentable document = null;
                if (e.WeakDocument.TryGetTarget(out document))
                {
                    var viewModel = document as DocumentViewModelBase;
                    Contract.Assume(viewModel != null);
                    Contract.Assume(panelManager.ContainDocument(viewModel));

                    viewModel.PanelView.SetDataContext(null);
                    viewModel.DocumentClosed -= this.CallDocumentClosed;
                    panelManager.RemoveDocument(viewModel);
                }
            }
        }
    }
}
