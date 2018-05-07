namespace RDK.Managements
{
    /// <summary>
    /// WPF の Control クラスなどの隠蔽インターフェースです。
    /// </summary>
    public interface IControlView
    {
        /// <summary>
        /// データコンテキストを設定します。
        /// </summary>
        /// <param name="dataContext"></param>
        void SetDataContext(object dataContext);
    }
}
