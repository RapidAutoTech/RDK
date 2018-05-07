namespace RDK.ViewModels
{
    using RDK.ComponentModel;

    /// <summary>
    /// ビューモデルの抽象クラスです。
    /// </summary>
    public abstract class ViewModel : DisposableNotifiableObject
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        protected ViewModel()
        {
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
        }
    }
}
