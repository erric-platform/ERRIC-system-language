namespace src.Parser
{
    public enum NodeType
    {
        None,
        Program,
        Unit,
        Code,
        Data,
        Module,
        Declaration,
        Variable,
        Type,
        VarDefinition,
        Constant,
        ConstDefinition,
        Statement,
        Label,
        Routine,
        Attribute,
        Parameters,
        Parameter,
        RoutineBody,
        Primary,
        Expression,
        Operand,
        Operator,
        PrimitiveOperator,
        AssemblerStatement,
        OperationOnRegisters,
        Register,
        Directive,
        ExtensionStatement,
        For,
        While,
        LoopBody,
        Break,
        Swap,
        Goto,
        Assignment,
        If,
        Call,
        Literal,
    }
}