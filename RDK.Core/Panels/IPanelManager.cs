namespace RDK.Panels
{
    using System.ComponentModel;
    using RDK.Managements;
    using RDK.ViewModels;

    /// <summary>
    /// パネルマネージャーのインターフェースです。
    /// </summary>
    public interface IPanelManager : IManager
    {
        /// <summary>
        /// ツールを追加します。
        /// </summary>
        /// <param name="tool">追加するツールです。</param>
        void AddTool(IToolable tool);


        /// <summary>
        /// 指定のツールが含まれているか判定します。
        /// </summary>
        /// <param name="tool">判定対象のツールです。</param>
        /// <returns>含まれていれば、真を返します。</returns>
        bool ContainTool(IToolable tool);

        /// <summary>
        /// 指定のドキュメントが含まれているか判定します。
        /// </summary>
        /// <param name="document">判定対象のドキュメントです。</param>
        /// <returns>含まれていれば、真を返します。</returns>
        bool ContainDocument(DocumentViewModelBase document);
    }
}
