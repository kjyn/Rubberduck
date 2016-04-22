﻿using Antlr4.Runtime;
using Rubberduck.Parsing.Grammar;
using Rubberduck.Parsing.Symbols;

namespace Rubberduck.Parsing.Binding
{
    public sealed class BindingService
    {
        private readonly IBindingContext _typedBindingContext;

        public BindingService(IBindingContext typedBindingContext)
        {
            _typedBindingContext = typedBindingContext;
        }

        public IBoundExpression Resolve(Declaration module, Declaration parent, string expression)
        {
            var expr = Parse(expression);
            return _typedBindingContext.Resolve(module, parent, expr);
        }

        private VBAExpressionParser.ExpressionContext Parse(string expression)
        {
            var stream = new AntlrInputStream(expression);
            var lexer = new VBALexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new VBAExpressionParser(tokens);
            parser.AddErrorListener(new ExceptionErrorListener());
            var tree = parser.startRule();
            return tree.expression();
        }
    }
}
