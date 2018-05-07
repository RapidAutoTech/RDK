using System;
using System.Collections.Generic;
using System.Text;

namespace RDK.ComponentModel
{
    /// <summary>
    /// 同期オブジェクトクラスです。
    /// </summary>
    public abstract class SynchronizedObject
    {
        private readonly object syncObj = new object();

        /// <summary>
        /// 同期オブジェクトを取得します。
        /// </summary>
        protected object SyncObj
        {
            get
            {
                return this.syncObj;
            }
        }
    }
}
