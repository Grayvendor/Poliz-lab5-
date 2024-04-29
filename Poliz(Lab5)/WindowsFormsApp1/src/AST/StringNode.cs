using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src.AST
{
    internal class StringleNode: ExpressionNode
    {
        Token String;

        public StringleNode(Token String)
        {
            this.String = String;
        }
    }
}
