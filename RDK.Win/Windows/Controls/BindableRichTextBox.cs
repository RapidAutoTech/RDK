namespace RDK.Windows.Controls
{
    using System.Diagnostics.Contracts;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    public sealed class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register(
                "Document",
                typeof(FlowDocument),
                typeof(BindableRichTextBox),
                new UIPropertyMetadata(null, OnRichTextItemsChanged));

        public new FlowDocument Document
        {
            get => (FlowDocument)GetValue(DocumentProperty);
            set => SetValue(DocumentProperty, value);
        }


        private static void OnRichTextItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as RichTextBox;
            Contract.Requires(control != null);
            control.Document = e.NewValue as FlowDocument;
        }
    }
}
