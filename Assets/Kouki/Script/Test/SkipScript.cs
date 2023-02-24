using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkipScript
{
    public class SkipRequestSource
    {
        /// <summary>
        /// �X�L�b�v����p�̃g�[�N����Ԃ��B
        /// </summary>
        public SkipRequestToken Token
            => new SkipRequestToken(this);

        /// <summary>
        /// �X�L�b�v��v������Ă���ꍇ�� true�B
        /// </summary>
        public bool IsSkipRequested { get; private set; }

        /// <summary>
        /// �X�L�b�v��v������B
        /// </summary>
        public void Skip() { IsSkipRequested = true; }
    }

    public struct SkipRequestToken
    {
        private SkipRequestSource _source;

        public SkipRequestToken(SkipRequestSource source)
            => _source = source;

        /// <summary>
        /// �X�L�b�v��v������Ă���ꍇ�� true�B
        /// </summary>
        public bool IsSkipRequested => _source.IsSkipRequested;
    }


}


