namespace RDK.Windows.Data
{
    using RDK.Windows.Controls;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Documents;

    public class RichTextItemsToDocumentConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var doc = new FlowDocument();

            foreach (var item in value as ICollection<RichTextItem>)
            {
                var paragraph = new Paragraph(new Run(item.Text))
                {
                    Foreground = item.Foreground,
                    FontWeight = item.FontWeight,
                    Margin = item.Margin,
                };

                doc.Blocks.Add(paragraph);
            }

            return doc;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
