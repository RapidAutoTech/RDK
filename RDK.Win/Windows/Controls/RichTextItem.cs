namespace RDK.Windows.Controls
{
    using System.Windows;
    using System.Windows.Media;

    public sealed class RichTextItem
    {
        public string Text { get; set; }
        public Brush Foreground { get; set; }
        public FontWeight FontWeight { get; set; }
        public Thickness Margin { get; set; }
    }
}
