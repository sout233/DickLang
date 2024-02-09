grammar Dick;

program: line* EOF;

line: statement | ifBlock | whileBlock | funcBlock;

statement: (variableDeclaration | assignment | functionCall) ';';

CONST: 'nig';

variableDeclaration: (
		'dick' (CONST)? IDENTIFIER (':' type) '=' expression
	)
	| ('dick' (CONST)? IDENTIFIER ':=' expression)
	| variableDef;

variableDef: 'dick' (CONST)? IDENTIFIER (':' type);

parameterList: IDENTIFIER (',' IDENTIFIER)*;

type: 'int' | 'str' | 'bool' | 'float';

ifBlock: IF expression block ('else' elseIfBlock);

IF: 'if';

elseIfBlock: block | ifBlock;

whileBlock: WHILE expression block ('else' elseIfBlock);

WHILE: 'dolove' | 'until';

assignment: IDENTIFIER '=' expression;

funcBlock: 'fuck' IDENTIFIER funcDef block;

funcDef: '(' (variableDef (',' variableDef)*)? ')';

funcParaments: '(' (expression (',' expression)*)? ')';

functionCall: IDENTIFIER funcParaments;

expression:
	constant							# constantExpression
	| IDENTIFIER						# identifierExpression
	| functionCall						# functionCallExpression
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