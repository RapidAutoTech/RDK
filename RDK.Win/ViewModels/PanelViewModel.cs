using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RDK.ViewModels
{
    public abstract class PanelViewModel : PanelViewModelBase
    {
        private Control panelView = null;

        /// <summary>
        /// パネルビューを取得します。
        /// </summary>
        public Control PanelView
        {
            get
            {
                return this.panelView;
            }

            set
            {
                this.SetProperty(ref this.panelView, value);
            }
        }
    }
}
