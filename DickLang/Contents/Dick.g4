grammar Dick;

program: line* EOF;

line:
	statement
	| returnStatement
	| ifBlock
	| whileBlock
	| funcBlock;

statement: (variableDeclaration | assignment | functionCall) ';';

CONST: 'nig';

variableDeclaration: (
		'dick' (CONST)? IDENTIFIER (':' type) ignmentSymbol expression
	)
	| ('dick' (CONST)? IDENTIFIER ignmentSymbol expression)
	| variableDef;

ignmentSymbol: '=' | '8==D';

variableDef: 'dick' (CONST)? IDENTIFIER (':' type)?;

type: 'int' | 'str' | 'bool' | 'float' | 'array';

arrayLiteral: '[' (expression (',' expression)*)? ']';

arrayAccess: IDENTIFIER '[' expression ']';

ifBlock: IF expression block ('else' elseIfBlock);

IF: 'if';

elseIfBlock: block | ifBlock;

whileBlock: WHILE expression block ('else' elseIfBlock)?;

WHILE: 'dolove' | 'until';

assignment: IDENTIFIER '=' expression;

funcBlock: 'fuck' IDENTIFIER funcDef block;

funcDef: '(' (variableDef (',' variableDef)*)? ')';

funcParaments: '(' (expression (',' expression)*)? ')';

functionCall: IDENTIFIER funcParaments;

returnStatement: 'return' expression ';';

expression:
	constant							# constantExpression
	| IDENTIFIER						# identifierExpression
	| functionCall						# functionCallExpression
	| arrayLiteral						# arrayLiteralExpression
	| arrayAccess						# arrayAccessExpression
	| '(' expression ')'				# parenthesesExpression
	| '!' expression					# notExpression
	| expression multOp expression		# multiplicationExpression
	| expression addOp expression		# additionExpression
	| expression compareOp expression	# comparisonExpression
	| expression boolOp expression		# booleanExpression;

multOp: '*' | '/' | '%';
addOp: '+' | '-';
compareOp: '==' | '!=' | '<' | '>' | '<=' | '>=';
boolOp: '&&' | '||';

constant: INTEGER | FLOAT | STRING | BOOL | NULL;

INTEGER: [0-9]+;
FLOAT: [0-9]* '.' [0-9]+;
STRING: ('"' ~'"'* '"') | ('\'' ~'\''* '\'');
BOOL: 'true' | 'false';
NULL: 'null';

block: '{' line* '}';

WHITESPACE: (' ' | '\t' | '\r' | '\n')+ -> skip;
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

LINE_COMMENT: '//' (~[\r\n])* -> skip;