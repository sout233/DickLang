using Antlr4.Runtime.Misc;
using System;
using System.Diagnostics;
using System.Linq;
using static DickParser;

namespace DickLang
{
    public class DickGet : DickBaseVisitor<object?>
    {
        private Dictionary<string, object> _variables = new();

        private Dictionary<string, bool> _variableMutability = new();

        public static string CurrentLine { get; set; } = "";

        public DickGet()
        {
            _variables["PI"] = (float)Math.PI;
            _variables["E"] = (float)Math.E;
            _variables["Print"] = new Func<object?[], object?>(Print);
        }

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
            CurrentLine = context.GetText();
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

        public override object VisitAdditionExpression([NotNull] AdditionExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.addOp().GetText();

            return op switch
            {
                "+" => Add(left, right),
                "-" => Subtract(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public object Add(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l + r;
            if (left is float lf && right is float rf)
                return lf + rf;
            if (left is int li2 && right is float rf2)
                return li2 + rf2;
            if (left is float lf3 && right is int ri3)
                return lf3 + ri3;
            if (left is string ls && right is string rs)
                return ls + rs;
            if (left is string ls2 && right is int ri4)
                return ls2 + ri4.ToString();
            if (left is int li && right is string rs2)
                return li.ToString() + rs2;
            if (left is float lf4 && right is string rs3)
                return lf4.ToString() + rs3;
            if (left is string ls3 && right is float rf4)
                return ls3 + rf4.ToString();

            throw new Exception($"Cannot add dicks of types {left?.GetType()} and {right?.GetType()}");
        }

        public object Subtract(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l - r;
            if (left is float lf && right is float rf)
                return lf - rf;
            if (left is int li2 && right is float rf2)
                return li2 - rf2;
            if (left is float lf3 && right is int ri3)
                return lf3 - ri3;

            throw new Exception($"Cannot subtract dicks of types {left?.GetType()} and {right?.GetType()}");
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
            var paraments = context.funcParaments().expression().Select(p => p.GetText()).ToList();
            var functionBlock = context.block();

            _variables[funcName] = new Func<object?[], object?>(args =>
            {
                var functionScope = new Dictionary<string, object>();
                if (paraments != null && args != null && paraments.Count == args.Length)
                {
                    for (int i = 0; i < paraments.Count; i++)
                    {
                        functionScope[paraments[i]] = args[i];
                    }
                }

                var result = Visit(functionBlock);

                return result;
            });

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

            return func(args);
        }
    }
}
