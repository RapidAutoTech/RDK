namespace RDK.Menus
{
    using System.Windows.Input;

    public interface IMenu
    {
        /// <summary>
        /// メニューの種類を取得します。
        /// </summary>
        MenuKind Kind { get; }

        /// <summary>
        /// コマンドを取得します。
        /// </summary>
        ICommand Command { get; }

    }
}
