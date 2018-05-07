namespace RDK.Modules.Output
{
    using RDK.Managements;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// OutputView.xaml の相互作用ロジック
    /// </summary>
    public partial class OutputView : UserControl, IControlView
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public OutputView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// データコンテキストを設定します。
        /// </summary>
        /// <param name="dataContext"></param>
        public void SetDataContext(object dataContext)
        {
            this.DataContext = dataContext;
        }
    }
}
