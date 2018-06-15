namespace RDK.Plugins
{
    using RDK.Menus;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Data;

    public sealed class PluginManager : PluginManagerBase
    {
        private readonly ObservableCollection<MenuBase> panelMenus =
            new ObservableCollection<MenuBase>();

        private CompositionContainer container = null;
        private PluginImporter importer = null;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public PluginManager()
        {
            BindingOperations.EnableCollectionSynchronization(this.panelMenus, this.SyncObj);
        }

        /// <summary>
        /// プラグインを設定します。
        /// </summary>
        /// <param name="path">プラグインのパスです。</param>
        public override void Setup(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            DirectoryCatalog pluginCatalog = new DirectoryCatalog(path);
            CatalogExportProvider pluginCatalogProvider = new CatalogExportProvider(pluginCatalog);

            AssemblyCatalog defaultCatalog = new AssemblyCatalog(Assembly.GetAssembly(typeof(PluginManager)));
            CatalogExportProvider defaultCatalogProvider = new CatalogExportProvider(defaultCatalog);
            this.container = new CompositionContainer(pluginCatalogProvider, defaultCatalogProvider);
            pluginCatalogProvider.SourceProvider = this.container;
            defaultCatalogProvider.SourceProvider = this.container;
            this.importer = new PluginImporter();
        }

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <returns>タスクを返します。</returns>
        public override async Task Initialize()
        {
            Contract.Assume(this.importer != null);

            await Task.Run(() =>
            {
                container.ComposeParts(this.importer);
            });

            await this.InitializeFactoriesAsync();
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
            BindingOperations.DisableCollectionSynchronization(this.panelMenus);
        }

        /// <summary>
        /// 非同期でファクトリを初期化します。
        /// </summary>
        /// <returns>タスクを返します。</returns>
        private Task InitializeFactoriesAsync()
        {
            Contract.Assume(this.importer.Plugins != null);

            return Task.Run(() =>
            {
                foreach (var unit in this.importer.Plugins)
                {
                    unit.Value.BeginRegistration();

                    foreach (var menuFactory in unit.Value.GetMenuFactories())
                    {
                        this.MenuFactoryCache.AddValue(unit.Value.UnitKey, menuFactory.FactoryKey, menuFactory);
                    }

                    foreach (var documentFactory in unit.Value.GetDocumentFactories())
                    {
                        this.DocumentFactoryCache.AddValue(unit.Value.UnitKey, documentFactory.FactoryKey, documentFactory);
                    }

                    foreach (var toolFactory in unit.Value.GetToolFactories())
                    {
                        this.ToolFactoryCache.AddValue(unit.Value.UnitKey, toolFactory.FactoryKey, toolFactory);
                    }

                    foreach (var operationFactory in unit.Value.GetOperationFactories())
                    {
                        this.OperationFactoryCache.AddFactory(operationFactory.FactoryKey, operationFactory);
                    }

                    foreach (var dialogFactory in unit.Value.GetDialogFactories())
                    {
                        this.DialogFactoryCache.Add(dialogFactory.DialogType, dialogFactory);
                    }
                }

                foreach (var unit in this.importer.Plugins)
                {
                    unit.Value.EndRegistration();
                }
            });
        }
    }
}
