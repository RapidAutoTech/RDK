namespace RDK.Panels
{
    using RDK.ViewModels;
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

        /// <summary>
        /// 指定のドキュメントが含まれているか判定します。
        /// </summary>
        /// <param name="document">判定対象のドキュメントです。</param>
        /// <returns>含まれていれば、真を返します。</returns>
        bool ContainDocument(DocumentViewModel document);
    }
}
