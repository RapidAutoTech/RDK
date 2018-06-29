namespace RDK.Windows.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class LayoutItemTemplateSelector : DataTemplateSelector
    {
        public List<DataTemplate> Items { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LayoutItemTemplateSelector()
        {
            this.Items = new List<DataTemplate>();
        }

        /// <summary>
        /// LayoutItem のコンテンツに適用する DataTemplate を選択します。
        /// </summary>
        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            // item には ViewModel が入っている。
            // ViewModel の型に対応するテンプレートを返す。
            var template = Items.Find((dt) => item.GetType().Equals(dt.DataType));
            if (template != null)
            {
                return template;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
