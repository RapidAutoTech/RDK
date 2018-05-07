namespace RDK.Plugins
{
    using System.Collections.Generic;
    using RDK.Menus;

    /// <summary>
    /// プラグインメニューファクトリクラスです。
    /// </summary>
    public abstract class PluginMenuFactory : PluginFactory
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="factoryKey">ファクトリキーです。</param>
        protected PluginMenuFactory(string factoryKey)
            : base(factoryKey)
        {
        }

        /// <summary>
        /// メニューを取得します。
        /// </summary>
        /// <returns>メニューのインスタンスを返します。</returns>
        public virtual IEnumerable<MenuBase> GetMenus()
        {
            yield break;
        }
    }
}
