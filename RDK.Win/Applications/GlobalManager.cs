namespace RDK.Applications
{
    using RDK.Assets;
    using RDK.Bridges;
    using RDK.Helpers;
    using RDK.Logs;
    using RDK.Managements;
    using RDK.Menus;
    using RDK.Operations;
    using RDK.Panels;
    using RDK.Plugins;
    using RDK.Plugins.Applications;
    using RDK.Plugins.Generic;
    using RDK.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;

    public class GlobalManager : GlobalManagerBase
    {
        protected internal static readonly Global Instance = new Global();

        static GlobalManager()
        {
        }

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <returns>タスクを返します。</returns>
        public static async Task Initialize()
        {
            var pluginManager = GetPluginManager() as PluginManager;
            Contract.Assume(pluginManager != null);

            var menuManager = GetMenuManager();
            Contract.Assume(menuManager != null);

            var scriptManager = GetScriptManager();
            Contract.Assume(scriptManager != null);

            await pluginManager.Initialize();
#if true
            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetMenuFactories())
                {
                    foreach (var menu in factory.GetMenus())
                    {
                        menuManager.AddMenu(menu.Kind, menu);
                    }
                }

                foreach (var menu in CreateViewMenus())
                {
                    menuManager.AddViewMenu(menu);
                }
            });


            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetOperationFactories())
                {
                    factory.SetCreateMessageDelegate(scriptManager.Bridge.CreateMessage);
                }
            });

            await Task.Run(() =>
            {
                scriptManager.Script.SetRunScriptOperationDelegate(Instance.CallRunScriptOperation);
                scriptManager.Script.SetEchoInfoDelegate(Instance.CallEchoInfo);
                scriptManager.Script.SetEchoErrorDelegate(Instance.CallEchoError);
            });

            await Task.Run(() =>
            {
                var assetManager = GetAssetManager() as AssetManager;
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
#endif
        }

        [Conditional("DEBUG")]
        public static void PushDebug(string message, [CallerFilePath] string fileName = null, [CallerLineNumber] int fileLine = 0, [CallerMemberName] string memberName = null)
        {
            Logger logger = new Logger();
            logger.WriteDebug(message, fileName, fileLine, memberName);
            Instance.PushLogger(logger);
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
            Contract.Requires(document is DocumentViewModel);

            Instance.AddDocument((DocumentViewModel)document);
        }

        /// <summary>
        /// メニューマネージャーを取得します。
        /// </summary>
        /// <returns>メニューマネージャーのインスタンスを返します。</returns>
        public static IMenuManager GetMenuManager()
        {
            return Instance.GetManager<IMenuManager>(MenuManagerKey);
        }

        protected internal static IPanelManager GetPanelManager()
        {
            return Instance.GetManager<IPanelManager>(PanelManagerKey);
        }

        internal static void SetActiveDocument(IDocumentable document)
        {
            Instance.SetActiveDocument(document);
        }

        internal static ScriptManager GetScriptManager()
        {
            return Instance.GetManager<ScriptManager>(ScriptManagerKey);
        }

        protected internal static PluginToolFactory GetToolFactory(string factoryFullName)
        {
            return Instance.GetManager<PluginManager>(PluginManagerKey).GetToolFactory(factoryFullName);
        }

        protected internal static ToolViewModel AddTool(PluginToolFactory factory, IPanelManager panelManager)
        {
            Contract.Requires(factory != null);
            Contract.Requires(panelManager != null);

            var viewModel = factory.CreateTool() as ToolViewModel;
            Contract.Requires(viewModel != null);

            viewModel.IsVisible = true;
            viewModel.IsActive = true;

            if (!panelManager.ContainTool(viewModel))
            {
                Instance.ActiveDocumentChanged += viewModel.CallActiveDocumentChanged;

                foreach (var asset in Instance.GetManager<AssetManager>(AssetManagerKey).GetAssets())
                {
                    asset.AssetChanged += viewModel.CallAssetChanged;
                }

                panelManager.AddTool(viewModel);
            }

            return viewModel;
        }

        /// <summary>
        /// デフォルトツールを作成します。
        /// </summary>
        public static void MakeDefaultTools()
        {
            var manager = GlobalManager.GetPanelManager();
            Contract.Assume(manager != null);

            IToolable outputViewModel = null;
            {
                var factory = GetToolFactory(typeof(OutputToolFactory).FullName);
                outputViewModel = AddTool(factory, manager);
            }

            outputViewModel.IsActive = true;
        }

        private static IEnumerable<Menu> CreateViewMenus()
        {
            var pluginManager = GetPluginManager() as PluginManager;
            Contract.Assume(pluginManager != null);

            var panelManager = GetPanelManager();
            Contract.Assume(panelManager != null);

            foreach (var factory in pluginManager.GetToolFactories())
            {
                Menu menu = new Menu(
                    param =>
                    {
                        AddTool(factory, panelManager);
                    },
                    param =>
                    {
                        return true;
                    });

                menu.Label = factory.Label;
                yield return menu;
            }
        }

        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        /// <typeparam name="TDialog">表示するダイアログの型です。</typeparam>
        /// <returns>表示に問題がない場合は、真を返します。</returns>
        public static bool Show<TDialog>()
                where TDialog : class
        {
            var pluginManager = GetPluginManager() as PluginManager;
            Contract.Assume(pluginManager != null);

            var dialog = pluginManager.GetDialogFactory<TDialog>();
            Contract.Assume(dialog != null);

            return dialog.Show();
        }

        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        /// <typeparam name="TDialog">表示するダイアログの型です。</typeparam>
        /// <typeparam name="TArgs">ダイアログの引数の型です。</typeparam>
        /// <param name="args">表示時に渡す引数です。</param>
        /// <returns>表示に問題がない場合は、真を返します。</returns>
        internal static bool Show<TDialog, TArgs>(TArgs args)
            where TDialog : class
            where TArgs : PluginDialogArgs
        {
            var pluginManager = GetPluginManager() as PluginManager;
            Contract.Assume(pluginManager != null);

            var dialog = pluginManager.GetDialogFactory<TDialog>();
            Contract.Assume(dialog != null);

            return ((PluginDialogFactory<TArgs>)dialog).Show(args);
        }

        /// <summary>
        /// オーナーウィンドウを設定します。
        /// </summary>
        /// <param name="owner">オーナーになるウィンドウです。</param>
        public static void SetOwner(Window owner)
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

        /*
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
    */

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

            private Window owner = null;

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

            internal Window Owner
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

            internal void SetOwner(Window owner)
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

            internal DocumentViewModel CreateDocument(string unitKey, string factoryKey)
            {
                var pluginManager = this.GetManager<PluginManager>(PluginManagerKey);
                Contract.Assume(pluginManager != null);

                var factory = pluginManager.GetDocumentFactory(unitKey, factoryKey);
                Contract.Assume(factory != null);

                var viewModel = factory.CreateDocument();
                Contract.Assume(viewModel != null);

                viewModel.DocumentClosed += this.CallDocumentClosed;

                return viewModel;
            }

            internal void AddDocument(DocumentViewModel document)
            {
                Contract.Requires(document != null);

                var panelManager = this.GetManager<PanelManager>(PanelManagerKey);
                Contract.Assume(panelManager != null);

                panelManager.AddDocument(document);
            }

            private void CallDocumentClosed(object sender, DocumentClosedEventArgs e)
            {
                Contract.Assume(sender != null);
                Contract.Assume(e != null);

                var panelManager = this.GetManager<PanelManager>(PanelManagerKey);
                Contract.Assume(panelManager != null);

                IDocumentable document = null;
                if (e.WeakDocument.TryGetTarget(out document))
                {
                    var viewModel = document as DocumentViewModel;
                    Contract.Assume(viewModel != null);
                    Contract.Assume(panelManager.ContainDocument(viewModel));

                    viewModel.PanelView.DataContext = null;
                    viewModel.DocumentClosed -= this.CallDocumentClosed;
                    panelManager.RemoveDocument(viewModel);
                }
            }
        }
    }
}
