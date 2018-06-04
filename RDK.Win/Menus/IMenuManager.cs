namespace RDK.Menus
{
    using System.ComponentModel;

    /// <summary>
    /// メニューマネージャーのインターフェースです。
    /// </summary>
    public interface IMenuManager : IMenuManagerBase
    {
        /// <summary>
        /// メニューを取得します。
        /// </summary>
        /// <param name="kind">取得するメニューの種類です。</param>
        /// <returns>該当するメニューを返します。</returns>
        ICollectionView GetMenus(MenuKind kind);

        /// <summary>
        /// ビューメニューを取得します。
        /// </summary>
        /// <returns>該当するメニューを返します。</returns>
        ICollectionView GetViewMenus();
    }
}
