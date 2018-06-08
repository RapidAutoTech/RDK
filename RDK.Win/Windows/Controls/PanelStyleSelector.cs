namespace RDK.Windows.Controls
{
    using RDK.ViewModels;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// パネルのスタイルセレクタークラスです。
    /// </summary>
    public sealed class PanelStyleSelector : StyleSelector
    {
        /// <summary>
        /// ツールスタイルを取得設定します。
        /// </summary>
        public Style ToolStyle
        {
            get;
            set;
        }

        /// <summary>
        /// ドキュメントスタイルを取得設定します。
        /// </summary>
        public Style DocumentStyle
        {
            get;
            set;
        }

        /// <summary>
        /// スタイルを返します。
        /// </summary>
        /// <param name="item">コンテンツです。</param>
        /// <param name="container">スタイルの適用対象の要素です。</param>
        /// <returns>該当するスタイルを返します。それ以外は、nullを返します。</returns>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is ToolViewModel)
            {
                return this.ToolStyle;
            }

            if (item is DocumentViewModel)
            {
                return this.DocumentStyle;
            }

            return base.SelectStyle(item, container);
        }
    }
}
