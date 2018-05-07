namespace RDK.Plugins
{
    using RDK.Plugins;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// プラグイン入力クラスです。
    /// PluginUnit を継承したものを読み込みます。
    /// </summary>
    internal sealed class PluginImporter : IPartImportsSatisfiedNotification
    {
        private bool isImported = false;

        [ImportMany(typeof(PluginUnit), AllowRecomposition = true)]
        public IEnumerable<Lazy<PluginUnit>> Plugins
        {
            get;
            set;
        }

        internal bool IsImported
        {
            get
            {
                return this.isImported;
            }
        }

        /// <summary>
        /// パーツのインポートが満たされ、安全に使用できるようになったときに通知されます。
        /// </summary>
        public void OnImportsSatisfied()
        {
            this.isImported = true;
        }
    }
}
