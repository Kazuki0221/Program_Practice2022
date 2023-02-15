using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkipScript
{
    public class SkipRequestSource
    {
        /// <summary>
        /// スキップ判定用のトークンを返す。
        /// </summary>
        public SkipRequestToken Token
            => new SkipRequestToken(this);

        /// <summary>
        /// スキップを要求されている場合は true。
        /// </summary>
        public bool IsSkipRequested { get; private set; }

        /// <summary>
        /// スキップを要求する。
        /// </summary>
        public void Skip() { IsSkipRequested = true; }
    }

    public struct SkipRequestToken
    {
        private SkipRequestSource _source;

        public SkipRequestToken(SkipRequestSource source)
            => _source = source;

        /// <summary>
        /// スキップを要求されている場合は true。
        /// </summary>
        public bool IsSkipRequested => _source.IsSkipRequested;
    }


}


