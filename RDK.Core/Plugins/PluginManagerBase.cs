namespace RDK.Plugins
{
    using RDK.Managements;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    /// <summary>
    /// プラグインマネージャークラスです。
    /// </summary>
    public abstract class PluginManagerBase : Manager, IPluginManager
    {
        private readonly FactoryCache<PluginMenuFactory> menuFactoryCache =
            new FactoryCache<PluginMenuFactory>();

        private readonly OperationFactoryCache operationFactoryCache =
            new OperationFactoryCache();

        private readonly Dictionary<Type, PluginDialogFactory> dialogFactoryCache =
            new Dictionary<Type, PluginDialogFactory>();


        /// <summary>
        /// コンストラクタです。
        /// </summary>
        protected PluginManagerBase()
            : base(Enum.GetName(typeof(ManagerKind), ManagerKind.Plugin))
        {
        }

        /// <summary>
        /// プラグインを設定します。
        /// </summary>
        /// <param name="path">プラグインのパスです。</param>
        public abstract void Setup(string path);

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <returns>タスクを返します。</returns>
        public abstract Task Initialize();

        protected FactoryCache<PluginMenuFactory> MenuFactoryCache
        {
            get
            {
                return this.menuFactoryCache;
            }
        }

        protected OperationFactoryCache OperationFactoryCache
        {
            get
            {
                return this.operationFactoryCache;
            }
        }

        protected Dictionary<Type, PluginDialogFactory> DialogFactoryCache
        {
            get
            {
                return this.dialogFactoryCache;
            }
        }

        internal PluginOperationFactory GetOperationFactory(string factoryKey)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(factoryKey));
            return this.operationFactoryCache.GetFactory(factoryKey);
        }

        internal IEnumerable<PluginOperationFactory> GetOperationFactories()
        {
            return this.operationFactoryCache.GetFactories();
        }

        internal IEnumerable<PluginDialogFactory> GetDialogFactories()
        {
            return this.dialogFactoryCache.Values;
        }

        internal IEnumerable<PluginMenuFactory> GetMenuFactories()
        {
            foreach (var unitKey in this.menuFactoryCache.GetUnitKeys())
            {
                foreach (var factoryKey in this.menuFactoryCache.GetFactoryKeys(unitKey))
                {
                    yield return this.menuFactoryCache.GetValue(unitKey, factoryKey);
                }
            }
        }

        internal PluginDialogFactory GetDialogFactory<TDialogType>() where TDialogType : class
        {
            return this.dialogFactoryCache[typeof(TDialogType)];
        }
    }
}
