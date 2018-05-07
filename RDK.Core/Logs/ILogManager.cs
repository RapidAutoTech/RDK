namespace RDK.Logs
{
    using RDK.Managements;

    /// <summary>
    /// ログマネージャーのインターフェースです。
    /// </summary>
    public interface ILogManager : IManager
    {
        /// <summary>
        /// ロガーをプッシュします。
        /// </summary>
        /// <param name="logger">プッシュ対象のロガーです。</param>
        void PushLogger(Logger logger);

        /// <summary>
        /// ログをフラッシュします。
        /// </summary>
        void Flush();
    }
}
