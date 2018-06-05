using System;
using System.Collections.Generic;
using System.Text;

namespace RDK.Bridges
{
    public class NullScriptBridge : ScriptBridge
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="bridgeVariableName">スクリプト処理時のブリッジするための変数名です。</param>
        public NullScriptBridge()
            : base("Null")
        {

        }

        /// <summary>
        /// スクリプトを実行します。
        /// </summary>
        /// <param name="expression">スクリプトの式文字列です。</param>
        /// <returns>処理が成功した場合は、真を返します。</returns>
        public override bool Execute(string expression)
        {
            return true;
        }

        /// <summary>
        /// スクリプトをファイルから実行します。
        /// </summary>
        /// <param name="fileName">実行するスクリプトファイルです。</param>
        /// <returns>処理が成功した場合は、真を返します。</returns>
        public override bool ExecuteFromFile(string fileName)
        {
            return true;
        }

        /// <summary>
        /// スクリプト実行時のメッセージを作成します。
        /// </summary>
        /// <param name="scriptKey">スクリプトキーです。</param>
        /// <param name="args">スクリプトの引数です。</param>
        /// <returns>作成したメッセージを返します。</returns>
        public override string CreateMessage(string scriptKey, params object[] args)
        {
            return string.Empty;
        }
    }
}
