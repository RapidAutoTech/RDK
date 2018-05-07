namespace RDK.Managements
{
    using System.ComponentModel;

    /// <summary>
    /// パネル化インターフェースです。
    /// </summary>
    public interface IPanelable : INotifyPropertyChanged
    {
        /// <summary>
        /// パネルタイトルを取得設定します。
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// パネルがアクティブかどうか取得設定します。
        /// </summary>
        bool IsActive { get; set; }
    }
}
