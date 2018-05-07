namespace RDK.Plugins
{
    using RDK.Managements;
    using System.Threading.Tasks;

    /// <summary>
    /// プラグインマネージャーのインターフェースです。
    /// </summary>
    public interface IPluginManager : IManager
    {
        /// <summary>
        /// プラグインを設定します。
        /// </summary>
        /// <param name="path">プラグインのパスです。</param>
        void Setup(string path);

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <returns>タスクを返します。</returns>
        Task Initialize();
    }
}
