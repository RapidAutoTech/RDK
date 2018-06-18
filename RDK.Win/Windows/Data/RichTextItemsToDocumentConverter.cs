namespace RDK.Windows.Data
{
    using RDK.Windows.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Documents;

    //[ValueConversion(typeof(string), typeof(string))]
    public class RichTextItemsToDocumentConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollectionView)
            {
                var textItems = ((ICollectionView)value).SourceCollection;

                var doc = new FlowDocument();

                foreach (RichTextItem item in textItems)
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

            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
