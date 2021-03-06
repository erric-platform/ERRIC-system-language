﻿using System;
using System.Diagnostics;
using src.Parser;
using src.Tokenizer;
using src.Utils;

namespace src.Exceptions
{
    public class SyntaxError : CompilationError
    {
        public SyntaxError(string message) : base(message)
        {
        }

        public SyntaxError(string message, Token token) : base(message, token)
        {
        }

        public SyntaxError(string message, Position position) : base(message, position)
        {
        }

        public static SyntaxError Make(string message, Position position = null)
        {
            var callee = GetCallee("parse");
            var text = $"{callee.Name} failed: {message}";

            return position == null ? new SyntaxError(text) : new SyntaxError(text, position);
        }

        public static SyntaxError Make(string message, Token token)
        {
            return token == null ? Make(message) : Make(message, token.Position);
        }

        public static SyntaxError Make(Func<string> message)
        {
            return Make(message.Invoke());
        }

        public static SyntaxError Make(Func<Token, string> message, Token token)
        {
            return Make(message.Invoke(token), token);
        }

        public static SyntaxError Make(Func<string, Token, string> message, string val, Token token)
        {
            return Make(message.Invoke(val, token), token);
        }
    }

    public class SyntaxErrorMessages
    {
        public static string INVALID_TOKEN(Token token)
        {
            return $"Invalid token `{token}` encountered";
        }

        public static string UNEXPECTED_EOS()
        {
            return "Unexpected end of stream";
        }

        public static string UNEXPECTED_TOKEN(string expected, Token received) =>
            GENERIC_EXPECTED_VALUE($"`{expected}`", received);

        public static string STATEMENT_EXPECTED(Token received) => GENERIC_EXPECTED_VALUE("some statement", received);
        public static string IDENTIFIER_EXPECTED(Token received) => GENERIC_EXPECTED_VALUE("identifier", received);
        public static string LITERAL_EXPECTED(Token received) => GENERIC_EXPECTED_VALUE("numeric literal", received);
        public static string REGISTER_EXPECTED(Token received) => GENERIC_EXPECTED_VALUE("register", received);
        public static string TYPE_EXPECTED(Token received) => GENERIC_EXPECTED_VALUE("variable type", received);
        public static string PRIMARY_EXPECTED(Token received) => GENERIC_EXPECTED_VALUE("primary argument", received);
        public static string EXPRESSION_EXPECTED(Token received) => GENERIC_EXPECTED_VALUE("expression", received);
        public static string OPERAND_EXPECTED(Token received) => GENERIC_EXPECTED_VALUE("expression operand", received);

        private static string GENERIC_EXPECTED_VALUE(string expected, Token received)
        {
            return $"Unexpected token. Expected {expected} but got `{received}`";
        }

        public static string INVALID_ASM_DEREFERENCE_USE()
        {
            return "Dereference in assembly can be used only with assignment operator";
        }
    }
}