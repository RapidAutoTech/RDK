namespace RDK.Logs
{
    using System.Collections.Generic;

    /// <summary>
    /// ログ出力をしないログファクトリクラスです。
    /// </summary>
    public sealed class NullLogFactory : LogFactory
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public NullLogFactory()
            : base()
        {
        }

        /// <summary>
        /// ログをフラッシュします。
        /// </summary>
        /// <param name="logs">フラッシュ対象のログです。</param>
        /// <returns>フラッシュ成功ならば真を返します。</returns>
        public override bool Flush(IReadOnlyCollection<Log> logs)
        {
            return true;
        }
    }
}
