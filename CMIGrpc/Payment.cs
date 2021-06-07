using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMIGrpc
{
    public class Payment
    {
        public string TAG_AMOUNT { get; set; }
        public string TAG_EFT_STAN { get; set; }
        public string TAG_CRYPT_CARD_NUM { get; set; }
    }
}
