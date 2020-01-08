using System;
using System.Collections.Generic;
using System.Text;

namespace console_chess.board
{
    class boardException : Exception
    {
        public boardException(string msg) : base(msg)
        {

        }
    }
}
