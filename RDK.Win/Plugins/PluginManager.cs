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
        private readonly ToolFactoryCache toolFactoryCache =
            new ToolFactoryCache();

        private readonly FactoryCache<PluginDocumentFactory> documentFactoryCache =
            new FactoryCache<PluginDocumentFactory>();

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
        public override void Setup(string path, bool hasDefaultPlugin=true)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            DirectoryCatalog pluginCatalog = new DirectoryCatalog(path);
            CatalogExportProvider pluginCatalogProvider = new CatalogExportProvider(pluginCatalog);

            if (hasDefaultPlugin)
            {
                AssemblyCatalog defaultCatalog = new AssemblyCatalog(Assembly.GetAssembly(typeof(PluginManager)));
                CatalogExportProvider defaultCatalogProvider = new CatalogExportProvider(defaultCatalog);
                this.container = new CompositionContainer(pluginCatalogProvider, defaultCatalogProvider);
                defaultCatalogProvider.SourceProvider = this.container;
            }
            else
            {
                this.container = new CompositionContainer(pluginCatalogProvider);
            }
            
            pluginCatalogProvider.SourceProvider = this.container;
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

        internal IEnumerable<PluginToolFactory> GetToolFactories()
        {
            foreach (var unitKey in this.toolFactoryCache.GetUnitKeys())
            {
                foreach (var factoryKey in this.toolFactoryCache.GetFactoryKeys(unitKey))
                {
                    yield return this.toolFactoryCache.GetValue(unitKey, factoryKey);
                }
            }
        }

        internal PluginToolFactory GetToolFactory(string factoryFullName)
        {
            return this.toolFactoryCache.GetFactory(factoryFullName);
        }

        internal PluginDocumentFactory GetDocumentFactory(string unitKey, string factoryKey)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(unitKey));
            Contract.Requires(!string.IsNullOrWhiteSpace(factoryKey));

            return this.documentFactoryCache.GetValue(unitKey, factoryKey);
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
                        this.documentFactoryCache.AddValue(unit.Value.UnitKey, documentFactory.FactoryKey, documentFactory);
                    }

                    foreach (var toolFactory in unit.Value.GetToolFactories())
                    {
                        this.toolFactoryCache.AddValue(unit.Value.UnitKey, toolFactory.FactoryKey, toolFactory);
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
