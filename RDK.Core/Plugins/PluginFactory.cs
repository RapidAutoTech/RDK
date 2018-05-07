namespace RDK.Plugins
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// プラグインファクトリの抽象クラスです。
    /// </summary>
    public abstract class PluginFactory
    {
        private readonly string factoryKey;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="factoryKey">ファクトリキーです。</param>
        protected PluginFactory(string factoryKey)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(factoryKey));
            this.factoryKey = factoryKey;
        }

        /// <summary>
        /// ファクトリキーを取得します。
        /// </summary>
        public string FactoryKey
        {
            get
            {
                return this.factoryKey;
            }
        }
    }
}
