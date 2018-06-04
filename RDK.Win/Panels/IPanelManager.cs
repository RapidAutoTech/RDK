namespace RDK.Panels
{
    using System.ComponentModel;

    /// <summary>
    /// パネルマネージャーのインターフェースです。
    /// </summary>
    public interface IPanelManager : IPanelManagerBase
    {
        /// <summary>
        /// ツールを取得します。
        /// </summary>
        /// <returns>対象のインスタンスを返します。</returns>
        ICollectionView GetTools();

        /// <summary>
        /// ドキュメントを取得します。
        /// </summary>
        /// <returns>対象のインスタンスを返します。</returns>
        ICollectionView GetDocuments();
    }
}
