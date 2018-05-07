namespace RDK.Managements
{
    using System.Windows.Input;

    /// <summary>
    /// ドキュメント化可能インターフェースです。
    /// </summary>
    public interface IDocumentable : IPanelable
    {
        /// <summary>
        /// クローズコマンドを取得します。
        /// </summary>
        ICommand CloseCommand { get; }

        /// <summary>
        /// ドキュメントをクローズします。
        /// </summary>
        void Close();
    }
}
