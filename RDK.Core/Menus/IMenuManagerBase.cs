namespace RDK.Menus
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using RDK.Managements;

    /// <summary>
    /// メニューマネージャーのインターフェースです。
    /// </summary>
    public interface IMenuManagerBase : IManager
    {
        /// <summary>
        /// メニューを追加します。
        /// </summary>
        /// <param name="kind">追加するメニューの種類です。</param>
        /// <param name="menu">追加するメニューです。</param>
        void AddMenu(MenuKind kind, IMenu menu);

        /// <summary>
        /// ビューメニューを追加します。
        /// </summary>
        /// <param name="menu">追加するメニューです。</param>
        void AddViewMenu(IMenu menu);
    }
}
