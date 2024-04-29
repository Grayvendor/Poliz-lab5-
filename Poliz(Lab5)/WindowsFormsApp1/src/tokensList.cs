using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src
{
    internal class tokensList
    {
        public Dictionary<string, TokenType> tokenTypeList = new Dictionary<string, TokenType>
        {
            //{ "STRING", new TokenType("STRING", "\"[a-z]*\"") },
            //{ "LPAR", new TokenType("LPAR", "\"") },
            //{ "RPAR", new TokenType("RPAR", "\\)") },
            { "LSHIFTOP", new TokenType("LSHIFTOP", "\\<<") },
            { "COUT", new TokenType("COUT", "cout") },
            { "ENDL", new TokenType("ENDL", "endl") },
            { "ENDS", new TokenType("ENDS", "ends") },
            { "FLUSH", new TokenType("FLUSH", "flush") },
            { "SEMICOLON", new TokenType("SEMICOLON", ";") },
            { "NLINE", new TokenType("NLINE", "\\/n") },
            
            
            
            //{ "STRING", new TokenType("STRING", "\"(.*)\"") }
             
            //{ "STRING", new TokenType("STRING", "\"([a-zA-Z]+\\s)*\"") },

            //{ "LPAR", new TokenType("LPAR", "\"") }
        };
    }
}

//declare const
