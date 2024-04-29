using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.src.AST;

namespace WindowsFormsApp1.src
{
    internal class Parser
    {
        private List<Token> tokens;
        private int pos = 0;
        public Dictionary<string, int> scoupe = new Dictionary<string, int>();
        private Dictionary<string, TokenType> tokenTypeList = new tokensList().tokenTypeList;
        Label errorLine;

        public Parser(List<Token>listTokens, Label error)
        {
            tokens = listTokens;
            errorLine = error;
        }
        public Token match(params TokenType[] expected)
        {
            if(this.pos < this.tokens.Count)
            {
                Token currentToken = this.tokens[this.pos];
                var aa = expected.FirstOrDefault(type => type.name == currentToken.type.name);
                if (aa != null)
                {
                    this.pos += 1;
                    return currentToken;
                }
            }
            return null;
        }
        public ExpressionNode parseWord()
        {
            if (match(tokenTypeList["STRING"]) == null)
            {
                return null;
            }
            var word = parseWord();
            return new ExpressionNode();
        }
        public ExpressionNode parseString()
        {
            if (match(tokenTypeList["LPAR"]) == null)
            {
                errorLine.Text = "На позиции " + this.pos + " ожидалась кавычка";
                return null;
            }
            var word = parseWord();
            if (match(tokenTypeList["LPAR"]) == null)
            {
                errorLine.Text = "На позиции " + this.pos + " ожидалась кавычка";
                return null;
            }
            return new ExpressionNode();
        }
        public ExpressionNode parseCout()
        {
            var cout = match(this.tokenTypeList["COUT"]);
            if (cout == null)
            {
                //errorLine.Text = "На позиции " + this.pos + " ожидался обьект cout";
                return null;
            }
            return new CoutNode();
        }
        public ExpressionNode parseExpression2(StatementsNode root)
        {
            ExpressionNode coutNode = parseCout();
            if (coutNode == null)
            {
                return null;
            }
            else
            {
                root.addNode(new CoutNode());
            }
            var LeftShiftOp = match(tokenTypeList["LSHIFTOP"]);
            if (LeftShiftOp != null)
            {
                var rightNode = this.parseString();
                if (rightNode == null)
                {
                    return null;
                }
                var binaryNode = new BinOperationNode(LeftShiftOp, coutNode, rightNode);
                return binaryNode;
            }
            errorLine.Text = "На позиции " + this.pos + " ожидалась <<";
            return null;
        }
        public ExpressionNode parseCode1()
        {
            StatementsNode root = new StatementsNode();
            while (this.pos < this.tokens.Count)
            {
                var codeStringNode = this.parseExpression2(root);
                if (codeStringNode == null)
                {
                    break;
                }
                var LeftShiftOp = match(tokenTypeList["LSHIFTOP"]);
                if (LeftShiftOp == null )
                {
                    if (this.match(this.tokenTypeList["SEMICOLON"]) != null)
                    {
                        break;
                    }
                    else
                    {
                        errorLine.Text = "На позиции " + this.pos + " ожидалась << или SEMICOLON";
                        break;
                    }
                }
                var endl = match(tokenTypeList["ENDL"]);
                if (endl == null)
                {
                    var ends = match(tokenTypeList["ENDS"]);
                    if (ends == null)
                    {
                        var flush = match(tokenTypeList["FLUSH"]);
                        if (flush != null)
                        {
                            break;
                        }
                        else
                        {
                            errorLine.Text = "На позиции " + this.pos + " ожидалась endl, ends или flush";
                            break;
                        }
                    }
                }
                if (this.match(this.tokenTypeList["SEMICOLON"]) != null)
                {
                    root.addNode(codeStringNode);
                    break; 
                }
                else
                {
                    errorLine.Text = "На позиции " + this.pos + " ожидалась SEMICOLON";
                    break;
                }
            }
            return root;
        }
    }
}
