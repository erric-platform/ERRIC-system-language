namespace src.Tokenizer
{
    public enum TokenType
    {
        Identifier,
        Delimiter,
        Operator,
        Keyword,
        Number,
        Register,
        Comment,
        NewLine,
    }

    public class Token
    {
        public readonly TokenType Type;
        public readonly string Value;
        public readonly (int, int) Position;

        public Token(TokenType type, string value, (int, int) position)
        {
            this.Type = type;
            this.Value = value;
            this.Position = position;
        }

        public string ToJsonString()
        {
            return $"{{type: {Type}, pos: {Position}, value: {Value}}}";
        } 

        public override string ToString() => Value;

        public bool Equals(Token obj)
        {
            return obj != null && ToJsonString().Equals(obj.ToJsonString());
        }
    }
}