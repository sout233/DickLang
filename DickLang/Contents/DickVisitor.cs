//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:/Users/sout/source/repos/DickLang/DickLang/Contents/Dick.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="DickParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IDickVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] DickParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] DickParser.LineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] DickParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclaration([NotNull] DickParser.VariableDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.variableDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDef([NotNull] DickParser.VariableDefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] DickParser.TypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.ifBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfBlock([NotNull] DickParser.IfBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.elseIfBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseIfBlock([NotNull] DickParser.ElseIfBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.whileBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileBlock([NotNull] DickParser.WhileBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] DickParser.AssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.funcBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncBlock([NotNull] DickParser.FuncBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.funcDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncDef([NotNull] DickParser.FuncDefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.funcParaments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncParaments([NotNull] DickParser.FuncParamentsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] DickParser.FunctionCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.returnStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStatement([NotNull] DickParser.ReturnStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>parenthesesExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesesExpression([NotNull] DickParser.ParenthesesExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>constantExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstantExpression([NotNull] DickParser.ConstantExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>additionExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAdditionExpression([NotNull] DickParser.AdditionExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>identifierExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifierExpression([NotNull] DickParser.IdentifierExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>functionCallExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCallExpression([NotNull] DickParser.FunctionCallExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>notExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotExpression([NotNull] DickParser.NotExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>multiplicationExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplicationExpression([NotNull] DickParser.MultiplicationExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>comparisonExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparisonExpression([NotNull] DickParser.ComparisonExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>booleanExpression</c>
	/// labeled alternative in <see cref="DickParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBooleanExpression([NotNull] DickParser.BooleanExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.multOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultOp([NotNull] DickParser.MultOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.addOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddOp([NotNull] DickParser.AddOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.compareOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompareOp([NotNull] DickParser.CompareOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.boolOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolOp([NotNull] DickParser.BoolOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstant([NotNull] DickParser.ConstantContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="DickParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] DickParser.BlockContext context);
}
