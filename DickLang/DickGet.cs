using Antlr4.Runtime.Misc;
using Pastel;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using static DickParser;

namespace DickLang
{
    public class DickGet : DickBaseVisitor<object?>
    {
        private static Dictionary<string, object> _variables = new();

        private Dictionary<string, bool> _variableMutability = new();

        public static string CurrentLineText { get; set; } = "";
        public static int CurrentLineCount { get; set; } = 1;

        public DickGet()
        {
            _variables["PI"] = (float)Math.PI;
            _variables["E"] = (float)Math.E;
            _variables["Print"] = new Func<object?[], object?>(Print);
        }

        public static Dictionary<string, object> Variables { get { return _variables; } }

        private object? Print(object?[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
            return null;
        }

        public override object? VisitStatement([NotNull] StatementContext context)
        {
            CurrentLineText = context.GetText();
            CurrentLineCount++;
            return base.VisitStatement(context);
        }

        public override object? VisitAssignment([NotNull] AssignmentContext context)
        {
            var varName = context.IDENTIFIER().GetText();
            var newValue = context.expression().Accept(this);

            if (_variableMutability.TryGetValue(varName, out var isMutable) && !isMutable)
            {
                DickExceptionDo.Out(msg: $"Dick '{varName}' is immutable and cannot be reassigned.", offendingSymbol: varName);
            }

            if (newValue != null)
                _variables[varName] = newValue;
            else
                DickExceptionDo.Out(msg: $"Value of Dick {varName} is null", offendingSymbol: varName);

            return null;
        }

        public override object? VisitVariableDeclaration([NotNull] VariableDeclarationContext context)
        {
            var isConstant = context.CONST() != null;
            var varName = context.IDENTIFIER().GetText();
            var initialValue = context.expression().Accept(this);

            if (initialValue == null)
            {
                DickExceptionDo.Out(msg: $"Initial value of variable '{varName}' cannot be undefined", offendingSymbol: varName);
            }
            else
            {
                _variables[varName] = initialValue;
                _variableMutability[varName] = !isConstant;
            }

            return null;
        }

        public override object VisitConstant([NotNull] ConstantContext context)
        {
            if (context.INTEGER() is { } i)
                return int.Parse(i.GetText());
            if (context.FLOAT() is { } f)
                return float.Parse(f.GetText());
            if (context.STRING() is { } s)
                return s.GetText()[1..^1];
            if (context.BOOL() is { } b)
                return b.GetText() == "true";

            throw new NotImplementedException();
        }

        public override object VisitIdentifierExpression([NotNull] IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();
            if (!_variables.ContainsKey(varName))
            {
                DickExceptionDo.Out(msg: $"Dick '{varName}' is not defined", offendingSymbol: varName);
            }
            return _variables[varName];
        }

        public override object VisitFunctionCallExpression([NotNull] FunctionCallExpressionContext context)
        {
            return VisitFunctionCall(context.functionCall());
        }

        public override object? VisitReturnStatement([NotNull] ReturnStatementContext context)
        {
            return context.expression().Accept(this);
        }

        public override object VisitAdditionExpression([NotNull] AdditionExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.addOp().GetText();

            return op switch
            {
                "+" => DickValueProcess.Add(left, right),
                "-" => DickValueProcess.Subtract(left, right),
                _ => throw new NotImplementedException()
            };
        }
        public override object VisitMultiplicationExpression([NotNull] MultiplicationExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.multOp().GetText();

            return op switch
            {
                "*" => DickValueProcess.Multiply(left, right),
                "/" => DickValueProcess.Subtract(left, right),
                "%" => DickValueProcess.Modulo(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public override object? VisitWhileBlock([NotNull] WhileBlockContext context)
        {
            Func<object?, bool>
                condition = context.WHILE().GetText() == "until" ? DickValueProcess.IsTrue : DickValueProcess.IsFalse;

            if (condition(Visit(context.expression())))
            {
                do
                {
                    Visit(context.block());
                } while (condition(Visit(context.expression())));
            }
            else
            {
                Visit(context.elseIfBlock());
            }

            return null;
        }

        public override object VisitIfBlock([NotNull] IfBlockContext context)
        {
            Func<object?, bool> condition = context.IF().GetText() == "if" ? DickValueProcess.IsTrue : DickValueProcess.IsFalse;

            if (condition(Visit(context.expression())))
            {
                Visit(context.block());
            }
            else
            {
                Visit(context.elseIfBlock());
            }

            return null;
        }


        public override object VisitComparisonExpression([NotNull] ComparisonExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.compareOp().GetText();

            return op switch
            {
                "<" => DickValueProcess.LessThan(left, right),
                "<=" => DickValueProcess.LessThanOrEqual(left, right),
                ">" => DickValueProcess.GreaterThan(left, right),
                ">=" => DickValueProcess.GreaterThanOrEqual(left, right),
                "==" => DickValueProcess.IsEqual(left, right),
                "!=" => DickValueProcess.NotEqual(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public override object VisitFuncBlock([NotNull] FuncBlockContext context)
        {
            var funcName = context.IDENTIFIER().GetText();
            var paraments = context.funcDef().variableDef().Select(p => p.GetText()).ToList();
            var paraVarNames = context.funcDef().variableDef().Select(p => p.IDENTIFIER().GetText()).ToList();
            var functionBlock = context.block();

            _variables[funcName] = new Func<object?[], object?>(args =>
            {
                var paraVarValues = new List<object>();

                if (paraments.Count != args.Length)
                {
                    DickExceptionDo.Out(msg: $"Missing arguments for '{funcName}'. Expected {paraments.Count} arguments, but got {args.Length}.", offendingSymbol: funcName, exit: false);
                }

                if (paraments != null && args != null)
                {
                    for (int i = 0; i < paraments.Count; i++)
                    {
                        paraVarValues.Add(args[i] ?? -1);
                    }
                }

                for (int i = 0; i < paraVarValues.Count; i++)
                {
                    _variables[paraVarNames[i]] = paraVarValues[i];
                }

                var result = Visit(functionBlock);

                return result;
            });

            return null;
        }

        public override object VisitBlock([NotNull] BlockContext context)
        {
            foreach (var lineContext in context.line())
            {
                if (lineContext.returnStatement() != null)
                {
                    return VisitReturnStatement(lineContext.returnStatement());
                }
                else
                {
                    VisitStatement(lineContext.statement());
                }
            }
            return null;
        }

        public override object? VisitFunctionCall([NotNull] FunctionCallContext context)
        {
            var name = context.IDENTIFIER().GetText();
            var args = context.funcParaments().expression().Select(Visit).ToArray();

            if (!_variables.ContainsKey(name))
            {
                DickExceptionDo.Out(msg: $"Fuck '{name}' is not defined", offendingSymbol: name);
            }

            if (_variables[name] is not Func<object?[], object?> func)
            {
                throw new DickException($"Fuck {name} is not a function");
            }

            foreach (var arg in args)
            {
                arg.ToString();
            }

            return func(args);
        }
    }
}
