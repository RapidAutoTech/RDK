using System;
using System.Collections.Generic;
using System.Text;

namespace RDK.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using RDK.Commands;
    using RDK.Managements;

    /// <summary>
    /// パネルのビューモデルクラスです。
    /// </summary>
    public abstract class PanelViewModel : ViewModel, IPanelable
    {
        private IControlView panelView = null;

        private string title = string.Empty;
        private bool isActive = false;

        /// <summary>
        /// パネルビューを取得します。
        /// </summary>
        public IControlView PanelView
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

        /// <summary>
        /// パネルタイトルを取得設定します。
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.SetProperty(ref this.title, value);
            }
        }

        /// <summary>
        /// パネルがアクティブかどうか取得設定します。
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                this.SetProperty(ref this.isActive, value);
                this.OnActiveChanged(new ActiveChangedEventArgs(this.isActive));
            }
        }

        /// <summary>
        /// アクティブ状態の変更を通知します。
        /// </summary>
        /// <param name="e">イベント引数です。</param>
        internal virtual void OnActiveChanged(ActiveChangedEventArgs e)
        {
        }
    }
}
