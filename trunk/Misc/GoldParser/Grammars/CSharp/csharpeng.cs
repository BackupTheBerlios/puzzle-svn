
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF                                  = 0  , // (EOF)
        SYMBOL_ERROR                                = 1  , // (Error)
        SYMBOL_WHITESPACE                           = 2  , // (Whitespace)
        SYMBOL_COMMENTEND                           = 3  , // (Comment End)
        SYMBOL_COMMENTLINE                          = 4  , // (Comment Line)
        SYMBOL_COMMENTSTART                         = 5  , // (Comment Start)
        SYMBOL_MINUS                                = 6  , // '-'
        SYMBOL_MINUSMINUS                           = 7  , // '--'
        SYMBOL_EXCLAM                               = 8  , // '!'
        SYMBOL_EXCLAMEQ                             = 9  , // '!='
        SYMBOL_PERCENT                              = 10 , // '%'
        SYMBOL_PERCENTEQ                            = 11 , // '%='
        SYMBOL_AMP                                  = 12 , // '&'
        SYMBOL_AMPAMP                               = 13 , // '&&'
        SYMBOL_AMPEQ                                = 14 , // '&='
        SYMBOL_LPARAN                               = 15 , // '('
        SYMBOL_RPARAN                               = 16 , // ')'
        SYMBOL_TIMES                                = 17 , // '*'
        SYMBOL_TIMESEQ                              = 18 , // '*='
        SYMBOL_COMMA                                = 19 , // ','
        SYMBOL_DOT                                  = 20 , // '.'
        SYMBOL_DIV                                  = 21 , // '/'
        SYMBOL_DIVEQ                                = 22 , // '/='
        SYMBOL_COLON                                = 23 , // ':'
        SYMBOL_SEMI                                 = 24 , // ';'
        SYMBOL_QUESTION                             = 25 , // '?'
        SYMBOL_LBRACKET                             = 26 , // '['
        SYMBOL_RBRACKET                             = 27 , // ']'
        SYMBOL_CARET                                = 28 , // '^'
        SYMBOL_CARETEQ                              = 29 , // '^='
        SYMBOL_LBRACE                               = 30 , // '{'
        SYMBOL_PIPE                                 = 31 , // '|'
        SYMBOL_PIPEPIPE                             = 32 , // '||'
        SYMBOL_PIPEEQ                               = 33 , // '|='
        SYMBOL_RBRACE                               = 34 , // '}'
        SYMBOL_TILDE                                = 35 , // '~'
        SYMBOL_PLUS                                 = 36 , // '+'
        SYMBOL_PLUSPLUS                             = 37 , // '++'
        SYMBOL_PLUSEQ                               = 38 , // '+='
        SYMBOL_LT                                   = 39 , // '<'
        SYMBOL_LTLT                                 = 40 , // '<<'
        SYMBOL_LTLTEQ                               = 41 , // '<<='
        SYMBOL_LTEQ                                 = 42 , // '<='
        SYMBOL_EQ                                   = 43 , // '='
        SYMBOL_MINUSEQ                              = 44 , // '-='
        SYMBOL_EQEQ                                 = 45 , // '=='
        SYMBOL_GT                                   = 46 , // '>'
        SYMBOL_GTEQ                                 = 47 , // '>='
        SYMBOL_GTGT                                 = 48 , // '>>'
        SYMBOL_GTGTEQ                               = 49 , // '>>='
        SYMBOL_GTGTGT                               = 50 , // '>>>'
        SYMBOL_GTGTGTEQ                             = 51 , // '>>>='
        SYMBOL_ABSTRACT                             = 52 , // abstract
        SYMBOL_BASE                                 = 53 , // base
        SYMBOL_BOOLEAN                              = 54 , // boolean
        SYMBOL_BOOLEANLITERAL                       = 55 , // BooleanLiteral
        SYMBOL_BREAK                                = 56 , // break
        SYMBOL_BYTE                                 = 57 , // byte
        SYMBOL_CASE                                 = 58 , // case
        SYMBOL_CATCH                                = 59 , // catch
        SYMBOL_CHAR                                 = 60 , // char
        SYMBOL_CLASS                                = 61 , // class
        SYMBOL_CONTINUE                             = 62 , // continue
        SYMBOL_DEFAULT                              = 63 , // default
        SYMBOL_DO                                   = 64 , // do
        SYMBOL_DOUBLE                               = 65 , // double
        SYMBOL_ELSE                                 = 66 , // else
        SYMBOL_EXTENDS                              = 67 , // extends
        SYMBOL_FINALLY                              = 68 , // finally
        SYMBOL_FLOAT                                = 69 , // float
        SYMBOL_FLOATINGPOINTLITERAL                 = 70 , // FloatingPointLiteral
        SYMBOL_FLOATINGPOINTLITERALEXPONENT         = 71 , // FloatingPointLiteralExponent
        SYMBOL_FOR                                  = 72 , // for
        SYMBOL_GET                                  = 73 , // get
        SYMBOL_HEXESCAPECHARLITERAL                 = 74 , // HexEscapeCharLiteral
        SYMBOL_HEXINTEGERLITERAL                    = 75 , // HexIntegerLiteral
        SYMBOL_IDENTIFIER                           = 76 , // Identifier
        SYMBOL_IF                                   = 77 , // if
        SYMBOL_INDIRECTCHARLITERAL                  = 78 , // IndirectCharLiteral
        SYMBOL_INSTANCEOF                           = 79 , // instanceof
        SYMBOL_INT                                  = 80 , // int
        SYMBOL_INTERFACE                            = 81 , // interface
        SYMBOL_INTERNAL                             = 82 , // internal
        SYMBOL_LONG                                 = 83 , // long
        SYMBOL_NAMESPACE                            = 84 , // namespace
        SYMBOL_NEW                                  = 85 , // new
        SYMBOL_NULLLITERAL                          = 86 , // NullLiteral
        SYMBOL_OCTALESCAPECHARLITERAL               = 87 , // OctalEscapeCharLiteral
        SYMBOL_OCTALINTEGERLITERAL                  = 88 , // OctalIntegerLiteral
        SYMBOL_OUT                                  = 89 , // out
        SYMBOL_PARAMS                               = 90 , // params
        SYMBOL_PRIVATE                              = 91 , // private
        SYMBOL_PROTECTED                            = 92 , // protected
        SYMBOL_PUBLIC                               = 93 , // public
        SYMBOL_REF                                  = 94 , // ref
        SYMBOL_RETURN                               = 95 , // return
        SYMBOL_SEALED                               = 96 , // sealed
        SYMBOL_SET                                  = 97 , // set
        SYMBOL_SHORT                                = 98 , // short
        SYMBOL_STANDARDESCAPECHARLITERAL            = 99 , // StandardEscapeCharLiteral
        SYMBOL_STARTWITHNOZERODECIMALINTEGERLITERAL = 100, // StartWithNoZeroDecimalIntegerLiteral
        SYMBOL_STARTWITHZERODECIMALINTEGERLITERAL   = 101, // StartWithZeroDecimalIntegerLiteral
        SYMBOL_STATIC                               = 102, // static
        SYMBOL_STRINGLITERAL                        = 103, // StringLiteral
        SYMBOL_SWITCH                               = 104, // switch
        SYMBOL_SYNCHRONIZED                         = 105, // synchronized
        SYMBOL_THIS                                 = 106, // this
        SYMBOL_THROW                                = 107, // throw
        SYMBOL_TRY                                  = 108, // try
        SYMBOL_UNSAFE                               = 109, // unsafe
        SYMBOL_USING                                = 110, // using
        SYMBOL_WHILE                                = 111, // while
        SYMBOL_VOLATILE                             = 112, // volatile
        SYMBOL_ABSTRACTMETHODDECLARATION            = 113, // <AbstractMethodDeclaration>
        SYMBOL_ADDITIVEEXPRESSION                   = 114, // <AdditiveExpression>
        SYMBOL_ALIASDECLARATION                     = 115, // <AliasDeclaration>
        SYMBOL_ANDEXPRESSION                        = 116, // <AndExpression>
        SYMBOL_ARGUMENTLIST                         = 117, // <ArgumentList>
        SYMBOL_ARRAYACCESS                          = 118, // <ArrayAccess>
        SYMBOL_ARRAYCREATIONEXPRESSION              = 119, // <ArrayCreationExpression>
        SYMBOL_ARRAYINITIALIZER                     = 120, // <ArrayInitializer>
        SYMBOL_ARRAYTYPE                            = 121, // <ArrayType>
        SYMBOL_ASSIGNMENT                           = 122, // <Assignment>
        SYMBOL_ASSIGNMENTEXPRESSION                 = 123, // <AssignmentExpression>
        SYMBOL_ASSIGNMENTOPERATOR                   = 124, // <AssignmentOperator>
        SYMBOL_BLOCK                                = 125, // <Block>
        SYMBOL_BLOCKSTATEMENT                       = 126, // <BlockStatement>
        SYMBOL_BLOCKSTATEMENTS                      = 127, // <BlockStatements>
        SYMBOL_BREAKSTATEMENT                       = 128, // <BreakStatement>
        SYMBOL_CASTEXPRESSION                       = 129, // <CastExpression>
        SYMBOL_CATCHCLAUSE                          = 130, // <CatchClause>
        SYMBOL_CATCHDEFAULT                         = 131, // <CatchDefault>
        SYMBOL_CATCHES                              = 132, // <Catches>
        SYMBOL_CHARACTERLITERAL                     = 133, // <CharacterLiteral>
        SYMBOL_CLASSBODY                            = 134, // <ClassBody>
        SYMBOL_CLASSBODYDECLARATION                 = 135, // <ClassBodyDeclaration>
        SYMBOL_CLASSBODYDECLARATIONS                = 136, // <ClassBodyDeclarations>
        SYMBOL_CLASSDECLARATION                     = 137, // <ClassDeclaration>
        SYMBOL_CLASSINSTANCECREATIONEXPRESSION      = 138, // <ClassInstanceCreationExpression>
        SYMBOL_CLASSMEMBERDECLARATION               = 139, // <ClassMemberDeclaration>
        SYMBOL_CLASSORINTERFACETYPE                 = 140, // <ClassOrInterfaceType>
        SYMBOL_CLASSTYPE                            = 141, // <ClassType>
        SYMBOL_CLASSTYPELIST                        = 142, // <ClassTypeList>
        SYMBOL_CODE                                 = 143, // <Code>
        SYMBOL_CODES                                = 144, // <Codes>
        SYMBOL_COMPILATIONUNIT                      = 145, // <CompilationUnit>
        SYMBOL_CONDITIONALANDEXPRESSION             = 146, // <ConditionalAndExpression>
        SYMBOL_CONDITIONALEXPRESSION                = 147, // <ConditionalExpression>
        SYMBOL_CONDITIONALOREXPRESSION              = 148, // <ConditionalOrExpression>
        SYMBOL_CONSTANTDECLARATION                  = 149, // <ConstantDeclaration>
        SYMBOL_CONSTANTEXPRESSION                   = 150, // <ConstantExpression>
        SYMBOL_CONSTRUCTORBODY                      = 151, // <ConstructorBody>
        SYMBOL_CONSTRUCTORDECLARATION               = 152, // <ConstructorDeclaration>
        SYMBOL_CONSTRUCTORDECLARATOR                = 153, // <ConstructorDeclarator>
        SYMBOL_CONTINUESTATEMENT                    = 154, // <ContinueStatement>
        SYMBOL_DECIMALINTEGERLITERAL                = 155, // <DecimalIntegerLiteral>
        SYMBOL_DIMEXPR                              = 156, // <DimExpr>
        SYMBOL_DIMEXPRS                             = 157, // <DimExprs>
        SYMBOL_DIMS                                 = 158, // <Dims>
        SYMBOL_DOSTATEMENT                          = 159, // <DoStatement>
        SYMBOL_EMPTYSTATEMENT                       = 160, // <EmptyStatement>
        SYMBOL_EQUALITYEXPRESSION                   = 161, // <EqualityExpression>
        SYMBOL_EXCLUSIVEOREXPRESSION                = 162, // <ExclusiveOrExpression>
        SYMBOL_EXPLICITCONSTRUCTORINVOCATION        = 163, // <ExplicitConstructorInvocation>
        SYMBOL_EXPRESSION                           = 164, // <Expression>
        SYMBOL_EXPRESSIONSTATEMENT                  = 165, // <ExpressionStatement>
        SYMBOL_EXTENDSINTERFACES                    = 166, // <ExtendsInterfaces>
        SYMBOL_FIELDACCESS                          = 167, // <FieldAccess>
        SYMBOL_FIELDDECLARATION                     = 168, // <FieldDeclaration>
        SYMBOL_FINALLY2                             = 169, // <Finally>
        SYMBOL_FLOATINGPOINTTYPE                    = 170, // <FloatingPointType>
        SYMBOL_FLOATPOINTLITERAL                    = 171, // <FloatPointLiteral>
        SYMBOL_FORINIT                              = 172, // <ForInit>
        SYMBOL_FORMALPARAMETER                      = 173, // <FormalParameter>
        SYMBOL_FORMALPARAMETERLIST                  = 174, // <FormalParameterList>
        SYMBOL_FORSTATEMENT                         = 175, // <ForStatement>
        SYMBOL_FORSTATEMENTNOSHORTIF                = 176, // <ForStatementNoShortIf>
        SYMBOL_FORUPDATE                            = 177, // <ForUpdate>
        SYMBOL_GENERICTYPE                          = 178, // <GenericType>
        SYMBOL_IFTHENELSESTATEMENT                  = 179, // <IfThenElseStatement>
        SYMBOL_IFTHENELSESTATEMENTNOSHORTIF         = 180, // <IfThenElseStatementNoShortIf>
        SYMBOL_IFTHENSTATEMENT                      = 181, // <IfThenStatement>
        SYMBOL_INCLUSIVEOREXPRESSION                = 182, // <InclusiveOrExpression>
        SYMBOL_INHERITS                             = 183, // <Inherits>
        SYMBOL_INTEGERLITERAL                       = 184, // <IntegerLiteral>
        SYMBOL_INTEGRALTYPE                         = 185, // <IntegralType>
        SYMBOL_INTERFACEBODY                        = 186, // <InterfaceBody>
        SYMBOL_INTERFACEDECLARATION                 = 187, // <InterfaceDeclaration>
        SYMBOL_INTERFACEMEMBERDECLARATION           = 188, // <InterfaceMemberDeclaration>
        SYMBOL_INTERFACEMEMBERDECLARATIONS          = 189, // <InterfaceMemberDeclarations>
        SYMBOL_INTERFACETYPE                        = 190, // <InterfaceType>
        SYMBOL_LABELEDSTATEMENT                     = 191, // <LabeledStatement>
        SYMBOL_LABELEDSTATEMENTNOSHORTIF            = 192, // <LabeledStatementNoShortIf>
        SYMBOL_LEFTHANDSIDE                         = 193, // <LeftHandSide>
        SYMBOL_LITERAL                              = 194, // <Literal>
        SYMBOL_LOCALVARIABLEDECLARATION             = 195, // <LocalVariableDeclaration>
        SYMBOL_LOCALVARIABLEDECLARATIONSTATEMENT    = 196, // <LocalVariableDeclarationStatement>
        SYMBOL_METHODBODY                           = 197, // <MethodBody>
        SYMBOL_METHODDECLARATION                    = 198, // <MethodDeclaration>
        SYMBOL_METHODDECLARATOR                     = 199, // <MethodDeclarator>
        SYMBOL_METHODHEADER                         = 200, // <MethodHeader>
        SYMBOL_METHODINVOCATION                     = 201, // <MethodInvocation>
        SYMBOL_MODIFIER                             = 202, // <Modifier>
        SYMBOL_MODIFIERS                            = 203, // <Modifiers>
        SYMBOL_MULTIPLICATIVEEXPRESSION             = 204, // <MultiplicativeExpression>
        SYMBOL_NAME                                 = 205, // <Name>
        SYMBOL_NAMESPACEDECLARATION                 = 206, // <NamespaceDeclaration>
        SYMBOL_NAMESPACEDECLARATIONBODY             = 207, // <NamespaceDeclarationBody>
        SYMBOL_NAMESPACEDECLARATIONHEADER           = 208, // <NamespaceDeclarationHeader>
        SYMBOL_NUMERICTYPE                          = 209, // <NumericType>
        SYMBOL_OUTARG                               = 210, // <OutArg>
        SYMBOL_OUTFORMALPARAMETER                   = 211, // <OutFormalParameter>
        SYMBOL_PARAMSARG                            = 212, // <ParamsArg>
        SYMBOL_PARAMSFORMALPARAMETER                = 213, // <ParamsFormalParameter>
        SYMBOL_POSTDECREMENTEXPRESSION              = 214, // <PostDecrementExpression>
        SYMBOL_POSTFIXEXPRESSION                    = 215, // <PostfixExpression>
        SYMBOL_POSTINCREMENTEXPRESSION              = 216, // <PostIncrementExpression>
        SYMBOL_PREDECREMENTEXPRESSION               = 217, // <PreDecrementExpression>
        SYMBOL_PREINCREMENTEXPRESSION               = 218, // <PreIncrementExpression>
        SYMBOL_PRIMARY                              = 219, // <Primary>
        SYMBOL_PRIMARYNONEWARRAY                    = 220, // <PrimaryNoNewArray>
        SYMBOL_PRIMITIVETYPE                        = 221, // <PrimitiveType>
        SYMBOL_PROPERTYBODY                         = 222, // <PropertyBody>
        SYMBOL_PROPERTYDECLARATION                  = 223, // <PropertyDeclaration>
        SYMBOL_PROPERTYDECLARATOR                   = 224, // <PropertyDeclarator>
        SYMBOL_PROPERTYGETBODY                      = 225, // <PropertyGetBody>
        SYMBOL_PROPERTYHEADER                       = 226, // <PropertyHeader>
        SYMBOL_PROPERTYSETBODY                      = 227, // <PropertySetBody>
        SYMBOL_QUALIFIEDNAME                        = 228, // <QualifiedName>
        SYMBOL_REFARG                               = 229, // <RefArg>
        SYMBOL_REFERENCETYPE                        = 230, // <ReferenceType>
        SYMBOL_REFFORMALPARAMETER                   = 231, // <RefFormalParameter>
        SYMBOL_RELATIONALEXPRESSION                 = 232, // <RelationalExpression>
        SYMBOL_RETURNSTATEMENT                      = 233, // <ReturnStatement>
        SYMBOL_SHIFTEXPRESSION                      = 234, // <ShiftExpression>
        SYMBOL_SIMPLENAME                           = 235, // <SimpleName>
        SYMBOL_SPECIALARG                           = 236, // <SpecialArg>
        SYMBOL_SPECIALFORMALPARAMETER               = 237, // <SpecialFormalParameter>
        SYMBOL_STATEMENT                            = 238, // <Statement>
        SYMBOL_STATEMENTEXPRESSION                  = 239, // <StatementExpression>
        SYMBOL_STATEMENTEXPRESSIONLIST              = 240, // <StatementExpressionList>
        SYMBOL_STATEMENTNOSHORTIF                   = 241, // <StatementNoShortIf>
        SYMBOL_STATEMENTWITHOUTTRAILINGSUBSTATEMENT = 242, // <StatementWithoutTrailingSubstatement>
        SYMBOL_STATICINITIALIZER                    = 243, // <StaticInitializer>
        SYMBOL_SWITCHBLOCK                          = 244, // <SwitchBlock>
        SYMBOL_SWITCHBLOCKSTATEMENTGROUP            = 245, // <SwitchBlockStatementGroup>
        SYMBOL_SWITCHBLOCKSTATEMENTGROUPS           = 246, // <SwitchBlockStatementGroups>
        SYMBOL_SWITCHLABEL                          = 247, // <SwitchLabel>
        SYMBOL_SWITCHLABELS                         = 248, // <SwitchLabels>
        SYMBOL_SWITCHSTATEMENT                      = 249, // <SwitchStatement>
        SYMBOL_SYNCHRONIZEDSTATEMENT                = 250, // <SynchronizedStatement>
        SYMBOL_THROWSTATEMENT                       = 251, // <ThrowStatement>
        SYMBOL_TRYSTATEMENT                         = 252, // <TryStatement>
        SYMBOL_TYPE                                 = 253, // <Type>
        SYMBOL_TYPEDECLARATION                      = 254, // <TypeDeclaration>
        SYMBOL_TYPELIST                             = 255, // <TypeList>
        SYMBOL_UNARYEXPRESSION                      = 256, // <UnaryExpression>
        SYMBOL_UNARYEXPRESSIONNOTPLUSMINUS          = 257, // <UnaryExpressionNotPlusMinus>
        SYMBOL_USINGDECLARATION                     = 258, // <UsingDeclaration>
        SYMBOL_VARIABLEDECLARATOR                   = 259, // <VariableDeclarator>
        SYMBOL_VARIABLEDECLARATORID                 = 260, // <VariableDeclaratorId>
        SYMBOL_VARIABLEDECLARATORS                  = 261, // <VariableDeclarators>
        SYMBOL_VARIABLEINITIALIZER                  = 262, // <VariableInitializer>
        SYMBOL_VARIABLEINITIALIZERS                 = 263, // <VariableInitializers>
        SYMBOL_WHILESTATEMENT                       = 264, // <WhileStatement>
        SYMBOL_WHILESTATEMENTNOSHORTIF              = 265  // <WhileStatementNoShortIf>
    };

    enum RuleConstants : int
    {
        RULE_CHARACTERLITERAL_INDIRECTCHARLITERAL                       = 0  , // <CharacterLiteral> ::= IndirectCharLiteral
        RULE_CHARACTERLITERAL_STANDARDESCAPECHARLITERAL                 = 1  , // <CharacterLiteral> ::= StandardEscapeCharLiteral
        RULE_CHARACTERLITERAL_OCTALESCAPECHARLITERAL                    = 2  , // <CharacterLiteral> ::= OctalEscapeCharLiteral
        RULE_CHARACTERLITERAL_HEXESCAPECHARLITERAL                      = 3  , // <CharacterLiteral> ::= HexEscapeCharLiteral
        RULE_DECIMALINTEGERLITERAL_STARTWITHZERODECIMALINTEGERLITERAL   = 4  , // <DecimalIntegerLiteral> ::= StartWithZeroDecimalIntegerLiteral
        RULE_DECIMALINTEGERLITERAL_STARTWITHNOZERODECIMALINTEGERLITERAL = 5  , // <DecimalIntegerLiteral> ::= StartWithNoZeroDecimalIntegerLiteral
        RULE_FLOATPOINTLITERAL_FLOATINGPOINTLITERAL                     = 6  , // <FloatPointLiteral> ::= FloatingPointLiteral
        RULE_FLOATPOINTLITERAL_FLOATINGPOINTLITERALEXPONENT             = 7  , // <FloatPointLiteral> ::= FloatingPointLiteralExponent
        RULE_INTEGERLITERAL                                             = 8  , // <IntegerLiteral> ::= <DecimalIntegerLiteral>
        RULE_INTEGERLITERAL_HEXINTEGERLITERAL                           = 9  , // <IntegerLiteral> ::= HexIntegerLiteral
        RULE_INTEGERLITERAL_OCTALINTEGERLITERAL                         = 10 , // <IntegerLiteral> ::= OctalIntegerLiteral
        RULE_LITERAL                                                    = 11 , // <Literal> ::= <IntegerLiteral>
        RULE_LITERAL2                                                   = 12 , // <Literal> ::= <FloatPointLiteral>
        RULE_LITERAL_BOOLEANLITERAL                                     = 13 , // <Literal> ::= BooleanLiteral
        RULE_LITERAL3                                                   = 14 , // <Literal> ::= <CharacterLiteral>
        RULE_LITERAL_STRINGLITERAL                                      = 15 , // <Literal> ::= StringLiteral
        RULE_LITERAL_NULLLITERAL                                        = 16 , // <Literal> ::= NullLiteral
        RULE_TYPE                                                       = 17 , // <Type> ::= <PrimitiveType>
        RULE_TYPE2                                                      = 18 , // <Type> ::= <ReferenceType>
        RULE_TYPE3                                                      = 19 , // <Type> ::= <GenericType>
        RULE_GENERICTYPE_LT_GT                                          = 20 , // <GenericType> ::= <Name> '<' <TypeList> '>'
        RULE_TYPELIST                                                   = 21 , // <TypeList> ::= <Type>
        RULE_TYPELIST_COMMA                                             = 22 , // <TypeList> ::= <TypeList> ',' <Type>
        RULE_PRIMITIVETYPE                                              = 23 , // <PrimitiveType> ::= <NumericType>
        RULE_PRIMITIVETYPE_BOOLEAN                                      = 24 , // <PrimitiveType> ::= boolean
        RULE_NUMERICTYPE                                                = 25 , // <NumericType> ::= <IntegralType>
        RULE_NUMERICTYPE2                                               = 26 , // <NumericType> ::= <FloatingPointType>
        RULE_INTEGRALTYPE_BYTE                                          = 27 , // <IntegralType> ::= byte
        RULE_INTEGRALTYPE_SHORT                                         = 28 , // <IntegralType> ::= short
        RULE_INTEGRALTYPE_INT                                           = 29 , // <IntegralType> ::= int
        RULE_INTEGRALTYPE_LONG                                          = 30 , // <IntegralType> ::= long
        RULE_INTEGRALTYPE_CHAR                                          = 31 , // <IntegralType> ::= char
        RULE_FLOATINGPOINTTYPE_FLOAT                                    = 32 , // <FloatingPointType> ::= float
        RULE_FLOATINGPOINTTYPE_DOUBLE                                   = 33 , // <FloatingPointType> ::= double
        RULE_REFERENCETYPE                                              = 34 , // <ReferenceType> ::= <ClassOrInterfaceType>
        RULE_REFERENCETYPE2                                             = 35 , // <ReferenceType> ::= <ArrayType>
        RULE_CLASSORINTERFACETYPE                                       = 36 , // <ClassOrInterfaceType> ::= <Name>
        RULE_CLASSTYPE                                                  = 37 , // <ClassType> ::= <ClassOrInterfaceType>
        RULE_INTERFACETYPE                                              = 38 , // <InterfaceType> ::= <ClassOrInterfaceType>
        RULE_ARRAYTYPE_LBRACKET_RBRACKET                                = 39 , // <ArrayType> ::= <PrimitiveType> '[' ']'
        RULE_ARRAYTYPE_LBRACKET_RBRACKET2                               = 40 , // <ArrayType> ::= <Name> '[' ']'
        RULE_ARRAYTYPE_LBRACKET_RBRACKET3                               = 41 , // <ArrayType> ::= <ArrayType> '[' ']'
        RULE_NAME                                                       = 42 , // <Name> ::= <SimpleName>
        RULE_NAME2                                                      = 43 , // <Name> ::= <QualifiedName>
        RULE_SIMPLENAME_IDENTIFIER                                      = 44 , // <SimpleName> ::= Identifier
        RULE_QUALIFIEDNAME_DOT_IDENTIFIER                               = 45 , // <QualifiedName> ::= <Name> '.' Identifier
        RULE_COMPILATIONUNIT                                            = 46 , // <CompilationUnit> ::= <Codes>
        RULE_CODES                                                      = 47 , // <Codes> ::= <Code> <Codes>
        RULE_CODES2                                                     = 48 , // <Codes> ::= <Code>
        RULE_CODE                                                       = 49 , // <Code> ::= <TypeDeclaration>
        RULE_CODE2                                                      = 50 , // <Code> ::= <NamespaceDeclaration>
        RULE_CODE3                                                      = 51 , // <Code> ::= <UsingDeclaration>
        RULE_CODE4                                                      = 52 , // <Code> ::= <AliasDeclaration>
        RULE_NAMESPACEDECLARATION                                       = 53 , // <NamespaceDeclaration> ::= <NamespaceDeclarationHeader> <NamespaceDeclarationBody>
        RULE_NAMESPACEDECLARATIONHEADER_NAMESPACE                       = 54 , // <NamespaceDeclarationHeader> ::= namespace <Name>
        RULE_NAMESPACEDECLARATIONBODY_LBRACE_RBRACE                     = 55 , // <NamespaceDeclarationBody> ::= '{' '}'
        RULE_NAMESPACEDECLARATIONBODY_LBRACE_RBRACE2                    = 56 , // <NamespaceDeclarationBody> ::= '{' <Codes> '}'
        RULE_USINGDECLARATION_USING_SEMI                                = 57 , // <UsingDeclaration> ::= using <Name> ';'
        RULE_ALIASDECLARATION_USING_EQ_SEMI                             = 58 , // <AliasDeclaration> ::= using <SimpleName> '=' <Name> ';'
        RULE_TYPEDECLARATION                                            = 59 , // <TypeDeclaration> ::= <ClassDeclaration>
        RULE_TYPEDECLARATION2                                           = 60 , // <TypeDeclaration> ::= <InterfaceDeclaration>
        RULE_TYPEDECLARATION_SEMI                                       = 61 , // <TypeDeclaration> ::= ';'
        RULE_MODIFIERS                                                  = 62 , // <Modifiers> ::= <Modifier>
        RULE_MODIFIERS2                                                 = 63 , // <Modifiers> ::= <Modifiers> <Modifier>
        RULE_MODIFIER_PUBLIC                                            = 64 , // <Modifier> ::= public
        RULE_MODIFIER_PROTECTED                                         = 65 , // <Modifier> ::= protected
        RULE_MODIFIER_PRIVATE                                           = 66 , // <Modifier> ::= private
        RULE_MODIFIER_STATIC                                            = 67 , // <Modifier> ::= static
        RULE_MODIFIER_ABSTRACT                                          = 68 , // <Modifier> ::= abstract
        RULE_MODIFIER_SEALED                                            = 69 , // <Modifier> ::= sealed
        RULE_MODIFIER_UNSAFE                                            = 70 , // <Modifier> ::= unsafe
        RULE_MODIFIER_INTERNAL                                          = 71 , // <Modifier> ::= internal
        RULE_MODIFIER_VOLATILE                                          = 72 , // <Modifier> ::= volatile
        RULE_CLASSDECLARATION_CLASS_IDENTIFIER                          = 73 , // <ClassDeclaration> ::= <Modifiers> class Identifier <Inherits> <ClassBody>
        RULE_CLASSDECLARATION_CLASS_IDENTIFIER2                         = 74 , // <ClassDeclaration> ::= <Modifiers> class Identifier <ClassBody>
        RULE_CLASSDECLARATION_CLASS_IDENTIFIER3                         = 75 , // <ClassDeclaration> ::= class Identifier <Inherits> <ClassBody>
        RULE_CLASSDECLARATION_CLASS_IDENTIFIER4                         = 76 , // <ClassDeclaration> ::= class Identifier <ClassBody>
        RULE_INHERITS_COLON                                             = 77 , // <Inherits> ::= ':' <ClassTypeList>
        RULE_CLASSBODY_LBRACE_RBRACE                                    = 78 , // <ClassBody> ::= '{' <ClassBodyDeclarations> '}'
        RULE_CLASSBODY_LBRACE_RBRACE2                                   = 79 , // <ClassBody> ::= '{' '}'
        RULE_CLASSBODYDECLARATIONS                                      = 80 , // <ClassBodyDeclarations> ::= <ClassBodyDeclaration>
        RULE_CLASSBODYDECLARATIONS2                                     = 81 , // <ClassBodyDeclarations> ::= <ClassBodyDeclarations> <ClassBodyDeclaration>
        RULE_CLASSBODYDECLARATION                                       = 82 , // <ClassBodyDeclaration> ::= <ClassMemberDeclaration>
        RULE_CLASSBODYDECLARATION2                                      = 83 , // <ClassBodyDeclaration> ::= <StaticInitializer>
        RULE_CLASSBODYDECLARATION3                                      = 84 , // <ClassBodyDeclaration> ::= <ConstructorDeclaration>
        RULE_CLASSMEMBERDECLARATION                                     = 85 , // <ClassMemberDeclaration> ::= <FieldDeclaration>
        RULE_CLASSMEMBERDECLARATION2                                    = 86 , // <ClassMemberDeclaration> ::= <PropertyDeclaration>
        RULE_CLASSMEMBERDECLARATION3                                    = 87 , // <ClassMemberDeclaration> ::= <MethodDeclaration>
        RULE_FIELDDECLARATION_SEMI                                      = 88 , // <FieldDeclaration> ::= <Modifiers> <Type> <VariableDeclarators> ';'
        RULE_FIELDDECLARATION_SEMI2                                     = 89 , // <FieldDeclaration> ::= <Type> <VariableDeclarators> ';'
        RULE_VARIABLEDECLARATORS                                        = 90 , // <VariableDeclarators> ::= <VariableDeclarator>
        RULE_VARIABLEDECLARATORS_COMMA                                  = 91 , // <VariableDeclarators> ::= <VariableDeclarators> ',' <VariableDeclarator>
        RULE_VARIABLEDECLARATOR                                         = 92 , // <VariableDeclarator> ::= <VariableDeclaratorId>
        RULE_VARIABLEDECLARATOR_EQ                                      = 93 , // <VariableDeclarator> ::= <VariableDeclaratorId> '=' <VariableInitializer>
        RULE_VARIABLEDECLARATORID_IDENTIFIER                            = 94 , // <VariableDeclaratorId> ::= Identifier
        RULE_VARIABLEDECLARATORID_LBRACKET_RBRACKET                     = 95 , // <VariableDeclaratorId> ::= <VariableDeclaratorId> '[' ']'
        RULE_VARIABLEINITIALIZER                                        = 96 , // <VariableInitializer> ::= <Expression>
        RULE_VARIABLEINITIALIZER2                                       = 97 , // <VariableInitializer> ::= <ArrayInitializer>
        RULE_METHODDECLARATION                                          = 98 , // <MethodDeclaration> ::= <MethodHeader> <MethodBody>
        RULE_METHODHEADER                                               = 99 , // <MethodHeader> ::= <Modifiers> <Type> <MethodDeclarator>
        RULE_METHODHEADER2                                              = 100, // <MethodHeader> ::= <Type> <MethodDeclarator>
        RULE_METHODDECLARATOR_IDENTIFIER_LPARAN_RPARAN                  = 101, // <MethodDeclarator> ::= Identifier '(' <FormalParameterList> ')'
        RULE_METHODDECLARATOR_IDENTIFIER_LPARAN_RPARAN2                 = 102, // <MethodDeclarator> ::= Identifier '(' ')'
        RULE_METHODDECLARATOR_LBRACKET_RBRACKET                         = 103, // <MethodDeclarator> ::= <MethodDeclarator> '[' ']'
        RULE_FORMALPARAMETERLIST                                        = 104, // <FormalParameterList> ::= <FormalParameter>
        RULE_FORMALPARAMETERLIST_COMMA                                  = 105, // <FormalParameterList> ::= <FormalParameterList> ',' <FormalParameter>
        RULE_FORMALPARAMETER                                            = 106, // <FormalParameter> ::= <Type> <VariableDeclaratorId>
        RULE_FORMALPARAMETER2                                           = 107, // <FormalParameter> ::= <SpecialFormalParameter>
        RULE_SPECIALFORMALPARAMETER                                     = 108, // <SpecialFormalParameter> ::= <ParamsFormalParameter>
        RULE_SPECIALFORMALPARAMETER2                                    = 109, // <SpecialFormalParameter> ::= <RefFormalParameter>
        RULE_SPECIALFORMALPARAMETER3                                    = 110, // <SpecialFormalParameter> ::= <OutFormalParameter>
        RULE_PARAMSFORMALPARAMETER_PARAMS                               = 111, // <ParamsFormalParameter> ::= params <ArrayType> <VariableDeclaratorId>
        RULE_REFFORMALPARAMETER_REF                                     = 112, // <RefFormalParameter> ::= ref <Type> <VariableDeclaratorId>
        RULE_OUTFORMALPARAMETER_OUT                                     = 113, // <OutFormalParameter> ::= out <Type> <VariableDeclaratorId>
        RULE_PROPERTYDECLARATION                                        = 114, // <PropertyDeclaration> ::= <PropertyHeader> <PropertyBody>
        RULE_PROPERTYHEADER                                             = 115, // <PropertyHeader> ::= <Modifiers> <Type> <PropertyDeclarator>
        RULE_PROPERTYHEADER2                                            = 116, // <PropertyHeader> ::= <Type> <PropertyDeclarator>
        RULE_PROPERTYDECLARATOR_IDENTIFIER                              = 117, // <PropertyDeclarator> ::= Identifier
        RULE_PROPERTYBODY_LBRACE_RBRACE                                 = 118, // <PropertyBody> ::= '{' <PropertyGetBody> <PropertySetBody> '}'
        RULE_PROPERTYBODY_LBRACE_RBRACE2                                = 119, // <PropertyBody> ::= '{' <PropertyGetBody> '}'
        RULE_PROPERTYBODY_LBRACE_RBRACE3                                = 120, // <PropertyBody> ::= '{' <PropertySetBody> '}'
        RULE_PROPERTYGETBODY_GET                                        = 121, // <PropertyGetBody> ::= get <Block>
        RULE_PROPERTYGETBODY_GET_SEMI                                   = 122, // <PropertyGetBody> ::= get ';'
        RULE_PROPERTYSETBODY_SET                                        = 123, // <PropertySetBody> ::= set <Block>
        RULE_PROPERTYSETBODY_SET_SEMI                                   = 124, // <PropertySetBody> ::= set ';'
        RULE_CLASSTYPELIST                                              = 125, // <ClassTypeList> ::= <ClassType>
        RULE_CLASSTYPELIST_COMMA                                        = 126, // <ClassTypeList> ::= <ClassTypeList> ',' <ClassType>
        RULE_METHODBODY                                                 = 127, // <MethodBody> ::= <Block>
        RULE_METHODBODY_SEMI                                            = 128, // <MethodBody> ::= ';'
        RULE_STATICINITIALIZER_STATIC                                   = 129, // <StaticInitializer> ::= static <Block>
        RULE_CONSTRUCTORDECLARATION                                     = 130, // <ConstructorDeclaration> ::= <Modifiers> <ConstructorDeclarator> <ConstructorBody>
        RULE_CONSTRUCTORDECLARATION2                                    = 131, // <ConstructorDeclaration> ::= <ConstructorDeclarator> <ConstructorBody>
        RULE_CONSTRUCTORDECLARATOR_LPARAN_RPARAN                        = 132, // <ConstructorDeclarator> ::= <SimpleName> '(' <FormalParameterList> ')'
        RULE_CONSTRUCTORDECLARATOR_LPARAN_RPARAN2                       = 133, // <ConstructorDeclarator> ::= <SimpleName> '(' ')'
        RULE_CONSTRUCTORDECLARATOR_LPARAN_RPARAN_COLON                  = 134, // <ConstructorDeclarator> ::= <SimpleName> '(' <FormalParameterList> ')' ':' <ExplicitConstructorInvocation>
        RULE_CONSTRUCTORDECLARATOR_LPARAN_RPARAN_COLON2                 = 135, // <ConstructorDeclarator> ::= <SimpleName> '(' ')' ':' <ExplicitConstructorInvocation>
        RULE_CONSTRUCTORBODY_LBRACE_RBRACE                              = 136, // <ConstructorBody> ::= '{' <BlockStatements> '}'
        RULE_CONSTRUCTORBODY_LBRACE_RBRACE2                             = 137, // <ConstructorBody> ::= '{' '}'
        RULE_EXPLICITCONSTRUCTORINVOCATION_THIS_LPARAN_RPARAN           = 138, // <ExplicitConstructorInvocation> ::= this '(' <ArgumentList> ')'
        RULE_EXPLICITCONSTRUCTORINVOCATION_THIS_LPARAN_RPARAN2          = 139, // <ExplicitConstructorInvocation> ::= this '(' ')'
        RULE_EXPLICITCONSTRUCTORINVOCATION_BASE_LPARAN_RPARAN           = 140, // <ExplicitConstructorInvocation> ::= base '(' <ArgumentList> ')'
        RULE_EXPLICITCONSTRUCTORINVOCATION_BASE_LPARAN_RPARAN2          = 141, // <ExplicitConstructorInvocation> ::= base '(' ')'
        RULE_INTERFACEDECLARATION_INTERFACE_IDENTIFIER                  = 142, // <InterfaceDeclaration> ::= <Modifiers> interface Identifier <ExtendsInterfaces> <InterfaceBody>
        RULE_INTERFACEDECLARATION_INTERFACE_IDENTIFIER2                 = 143, // <InterfaceDeclaration> ::= <Modifiers> interface Identifier <InterfaceBody>
        RULE_INTERFACEDECLARATION_INTERFACE_IDENTIFIER3                 = 144, // <InterfaceDeclaration> ::= interface Identifier <ExtendsInterfaces> <InterfaceBody>
        RULE_INTERFACEDECLARATION_INTERFACE_IDENTIFIER4                 = 145, // <InterfaceDeclaration> ::= interface Identifier <InterfaceBody>
        RULE_EXTENDSINTERFACES_EXTENDS                                  = 146, // <ExtendsInterfaces> ::= extends <InterfaceType>
        RULE_EXTENDSINTERFACES_COMMA                                    = 147, // <ExtendsInterfaces> ::= <ExtendsInterfaces> ',' <InterfaceType>
        RULE_INTERFACEBODY_LBRACE_RBRACE                                = 148, // <InterfaceBody> ::= '{' <InterfaceMemberDeclarations> '}'
        RULE_INTERFACEBODY_LBRACE_RBRACE2                               = 149, // <InterfaceBody> ::= '{' '}'
        RULE_INTERFACEMEMBERDECLARATIONS                                = 150, // <InterfaceMemberDeclarations> ::= <InterfaceMemberDeclaration>
        RULE_INTERFACEMEMBERDECLARATIONS2                               = 151, // <InterfaceMemberDeclarations> ::= <InterfaceMemberDeclarations> <InterfaceMemberDeclaration>
        RULE_INTERFACEMEMBERDECLARATION                                 = 152, // <InterfaceMemberDeclaration> ::= <ConstantDeclaration>
        RULE_INTERFACEMEMBERDECLARATION2                                = 153, // <InterfaceMemberDeclaration> ::= <AbstractMethodDeclaration>
        RULE_CONSTANTDECLARATION                                        = 154, // <ConstantDeclaration> ::= <FieldDeclaration>
        RULE_ABSTRACTMETHODDECLARATION_SEMI                             = 155, // <AbstractMethodDeclaration> ::= <MethodHeader> ';'
        RULE_ARRAYINITIALIZER_LBRACE_COMMA_RBRACE                       = 156, // <ArrayInitializer> ::= '{' <VariableInitializers> ',' '}'
        RULE_ARRAYINITIALIZER_LBRACE_RBRACE                             = 157, // <ArrayInitializer> ::= '{' <VariableInitializers> '}'
        RULE_ARRAYINITIALIZER_LBRACE_COMMA_RBRACE2                      = 158, // <ArrayInitializer> ::= '{' ',' '}'
        RULE_ARRAYINITIALIZER_LBRACE_RBRACE2                            = 159, // <ArrayInitializer> ::= '{' '}'
        RULE_VARIABLEINITIALIZERS                                       = 160, // <VariableInitializers> ::= <VariableInitializer>
        RULE_VARIABLEINITIALIZERS_COMMA                                 = 161, // <VariableInitializers> ::= <VariableInitializers> ',' <VariableInitializer>
        RULE_BLOCK_LBRACE_RBRACE                                        = 162, // <Block> ::= '{' <BlockStatements> '}'
        RULE_BLOCK_LBRACE_RBRACE2                                       = 163, // <Block> ::= '{' '}'
        RULE_BLOCKSTATEMENTS                                            = 164, // <BlockStatements> ::= <BlockStatement>
        RULE_BLOCKSTATEMENTS2                                           = 165, // <BlockStatements> ::= <BlockStatements> <BlockStatement>
        RULE_BLOCKSTATEMENT                                             = 166, // <BlockStatement> ::= <LocalVariableDeclarationStatement>
        RULE_BLOCKSTATEMENT2                                            = 167, // <BlockStatement> ::= <Statement>
        RULE_LOCALVARIABLEDECLARATIONSTATEMENT_SEMI                     = 168, // <LocalVariableDeclarationStatement> ::= <LocalVariableDeclaration> ';'
        RULE_LOCALVARIABLEDECLARATION                                   = 169, // <LocalVariableDeclaration> ::= <Type> <VariableDeclarators>
        RULE_STATEMENT                                                  = 170, // <Statement> ::= <StatementWithoutTrailingSubstatement>
        RULE_STATEMENT2                                                 = 171, // <Statement> ::= <LabeledStatement>
        RULE_STATEMENT3                                                 = 172, // <Statement> ::= <IfThenStatement>
        RULE_STATEMENT4                                                 = 173, // <Statement> ::= <IfThenElseStatement>
        RULE_STATEMENT5                                                 = 174, // <Statement> ::= <WhileStatement>
        RULE_STATEMENT6                                                 = 175, // <Statement> ::= <ForStatement>
        RULE_STATEMENTNOSHORTIF                                         = 176, // <StatementNoShortIf> ::= <StatementWithoutTrailingSubstatement>
        RULE_STATEMENTNOSHORTIF2                                        = 177, // <StatementNoShortIf> ::= <LabeledStatementNoShortIf>
        RULE_STATEMENTNOSHORTIF3                                        = 178, // <StatementNoShortIf> ::= <IfThenElseStatementNoShortIf>
        RULE_STATEMENTNOSHORTIF4                                        = 179, // <StatementNoShortIf> ::= <WhileStatementNoShortIf>
        RULE_STATEMENTNOSHORTIF5                                        = 180, // <StatementNoShortIf> ::= <ForStatementNoShortIf>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT                       = 181, // <StatementWithoutTrailingSubstatement> ::= <Block>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT2                      = 182, // <StatementWithoutTrailingSubstatement> ::= <EmptyStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT3                      = 183, // <StatementWithoutTrailingSubstatement> ::= <ExpressionStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT4                      = 184, // <StatementWithoutTrailingSubstatement> ::= <SwitchStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT5                      = 185, // <StatementWithoutTrailingSubstatement> ::= <DoStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT6                      = 186, // <StatementWithoutTrailingSubstatement> ::= <BreakStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT7                      = 187, // <StatementWithoutTrailingSubstatement> ::= <ContinueStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT8                      = 188, // <StatementWithoutTrailingSubstatement> ::= <ReturnStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT9                      = 189, // <StatementWithoutTrailingSubstatement> ::= <SynchronizedStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT10                     = 190, // <StatementWithoutTrailingSubstatement> ::= <ThrowStatement>
        RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT11                     = 191, // <StatementWithoutTrailingSubstatement> ::= <TryStatement>
        RULE_EMPTYSTATEMENT_SEMI                                        = 192, // <EmptyStatement> ::= ';'
        RULE_LABELEDSTATEMENT_IDENTIFIER_COLON                          = 193, // <LabeledStatement> ::= Identifier ':' <Statement>
        RULE_LABELEDSTATEMENTNOSHORTIF_IDENTIFIER_COLON                 = 194, // <LabeledStatementNoShortIf> ::= Identifier ':' <StatementNoShortIf>
        RULE_EXPRESSIONSTATEMENT_SEMI                                   = 195, // <ExpressionStatement> ::= <StatementExpression> ';'
        RULE_STATEMENTEXPRESSION                                        = 196, // <StatementExpression> ::= <Assignment>
        RULE_STATEMENTEXPRESSION2                                       = 197, // <StatementExpression> ::= <PreIncrementExpression>
        RULE_STATEMENTEXPRESSION3                                       = 198, // <StatementExpression> ::= <PreDecrementExpression>
        RULE_STATEMENTEXPRESSION4                                       = 199, // <StatementExpression> ::= <PostIncrementExpression>
        RULE_STATEMENTEXPRESSION5                                       = 200, // <StatementExpression> ::= <PostDecrementExpression>
        RULE_STATEMENTEXPRESSION6                                       = 201, // <StatementExpression> ::= <MethodInvocation>
        RULE_STATEMENTEXPRESSION7                                       = 202, // <StatementExpression> ::= <ClassInstanceCreationExpression>
        RULE_IFTHENSTATEMENT_IF_LPARAN_RPARAN                           = 203, // <IfThenStatement> ::= if '(' <Expression> ')' <Statement>
        RULE_IFTHENELSESTATEMENT_IF_LPARAN_RPARAN_ELSE                  = 204, // <IfThenElseStatement> ::= if '(' <Expression> ')' <StatementNoShortIf> else <Statement>
        RULE_IFTHENELSESTATEMENTNOSHORTIF_IF_LPARAN_RPARAN_ELSE         = 205, // <IfThenElseStatementNoShortIf> ::= if '(' <Expression> ')' <StatementNoShortIf> else <StatementNoShortIf>
        RULE_SWITCHSTATEMENT_SWITCH_LPARAN_RPARAN                       = 206, // <SwitchStatement> ::= switch '(' <Expression> ')' <SwitchBlock>
        RULE_SWITCHBLOCK_LBRACE_RBRACE                                  = 207, // <SwitchBlock> ::= '{' <SwitchBlockStatementGroups> <SwitchLabels> '}'
        RULE_SWITCHBLOCK_LBRACE_RBRACE2                                 = 208, // <SwitchBlock> ::= '{' <SwitchBlockStatementGroups> '}'
        RULE_SWITCHBLOCK_LBRACE_RBRACE3                                 = 209, // <SwitchBlock> ::= '{' <SwitchLabels> '}'
        RULE_SWITCHBLOCK_LBRACE_RBRACE4                                 = 210, // <SwitchBlock> ::= '{' '}'
        RULE_SWITCHBLOCKSTATEMENTGROUPS                                 = 211, // <SwitchBlockStatementGroups> ::= <SwitchBlockStatementGroup>
        RULE_SWITCHBLOCKSTATEMENTGROUPS2                                = 212, // <SwitchBlockStatementGroups> ::= <SwitchBlockStatementGroups> <SwitchBlockStatementGroup>
        RULE_SWITCHBLOCKSTATEMENTGROUP                                  = 213, // <SwitchBlockStatementGroup> ::= <SwitchLabels> <BlockStatements>
        RULE_SWITCHLABELS                                               = 214, // <SwitchLabels> ::= <SwitchLabel>
        RULE_SWITCHLABELS2                                              = 215, // <SwitchLabels> ::= <SwitchLabels> <SwitchLabel>
        RULE_SWITCHLABEL_CASE_COLON                                     = 216, // <SwitchLabel> ::= case <ConstantExpression> ':'
        RULE_SWITCHLABEL_DEFAULT_COLON                                  = 217, // <SwitchLabel> ::= default ':'
        RULE_WHILESTATEMENT_WHILE_LPARAN_RPARAN                         = 218, // <WhileStatement> ::= while '(' <Expression> ')' <Statement>
        RULE_WHILESTATEMENTNOSHORTIF_WHILE_LPARAN_RPARAN                = 219, // <WhileStatementNoShortIf> ::= while '(' <Expression> ')' <StatementNoShortIf>
        RULE_DOSTATEMENT_DO_WHILE_LPARAN_RPARAN_SEMI                    = 220, // <DoStatement> ::= do <Statement> while '(' <Expression> ')' ';'
        RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN                   = 221, // <ForStatement> ::= for '(' <ForInit> ';' <Expression> ';' <ForUpdate> ')' <Statement>
        RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN2                  = 222, // <ForStatement> ::= for '(' <ForInit> ';' <Expression> ';' ')' <Statement>
        RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN3                  = 223, // <ForStatement> ::= for '(' <ForInit> ';' ';' <ForUpdate> ')' <Statement>
        RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN4                  = 224, // <ForStatement> ::= for '(' <ForInit> ';' ';' ')' <Statement>
        RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN5                  = 225, // <ForStatement> ::= for '(' ';' <Expression> ';' <ForUpdate> ')' <Statement>
        RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN6                  = 226, // <ForStatement> ::= for '(' ';' <Expression> ';' ')' <Statement>
        RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN7                  = 227, // <ForStatement> ::= for '(' ';' ';' <ForUpdate> ')' <Statement>
        RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN8                  = 228, // <ForStatement> ::= for '(' ';' ';' ')' <Statement>
        RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN          = 229, // <ForStatementNoShortIf> ::= for '(' <ForInit> ';' <Expression> ';' <ForUpdate> ')' <StatementNoShortIf>
        RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN2         = 230, // <ForStatementNoShortIf> ::= for '(' <ForInit> ';' <Expression> ';' ')' <StatementNoShortIf>
        RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN3         = 231, // <ForStatementNoShortIf> ::= for '(' <ForInit> ';' ';' <ForUpdate> ')' <StatementNoShortIf>
        RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN4         = 232, // <ForStatementNoShortIf> ::= for '(' <ForInit> ';' ';' ')' <StatementNoShortIf>
        RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN5         = 233, // <ForStatementNoShortIf> ::= for '(' ';' <Expression> ';' <ForUpdate> ')' <StatementNoShortIf>
        RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN6         = 234, // <ForStatementNoShortIf> ::= for '(' ';' <Expression> ';' ')' <StatementNoShortIf>
        RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN7         = 235, // <ForStatementNoShortIf> ::= for '(' ';' ';' <ForUpdate> ')' <StatementNoShortIf>
        RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN8         = 236, // <ForStatementNoShortIf> ::= for '(' ';' ';' ')' <StatementNoShortIf>
        RULE_FORINIT                                                    = 237, // <ForInit> ::= <StatementExpressionList>
        RULE_FORINIT2                                                   = 238, // <ForInit> ::= <LocalVariableDeclaration>
        RULE_FORUPDATE                                                  = 239, // <ForUpdate> ::= <StatementExpressionList>
        RULE_STATEMENTEXPRESSIONLIST                                    = 240, // <StatementExpressionList> ::= <StatementExpression>
        RULE_STATEMENTEXPRESSIONLIST_COMMA                              = 241, // <StatementExpressionList> ::= <StatementExpressionList> ',' <StatementExpression>
        RULE_BREAKSTATEMENT_BREAK_IDENTIFIER_SEMI                       = 242, // <BreakStatement> ::= break Identifier ';'
        RULE_BREAKSTATEMENT_BREAK_SEMI                                  = 243, // <BreakStatement> ::= break ';'
        RULE_CONTINUESTATEMENT_CONTINUE_IDENTIFIER_SEMI                 = 244, // <ContinueStatement> ::= continue Identifier ';'
        RULE_CONTINUESTATEMENT_CONTINUE_SEMI                            = 245, // <ContinueStatement> ::= continue ';'
        RULE_RETURNSTATEMENT_RETURN_SEMI                                = 246, // <ReturnStatement> ::= return <Expression> ';'
        RULE_RETURNSTATEMENT_RETURN_SEMI2                               = 247, // <ReturnStatement> ::= return ';'
        RULE_THROWSTATEMENT_THROW_SEMI                                  = 248, // <ThrowStatement> ::= throw <Expression> ';'
        RULE_THROWSTATEMENT_THROW_SEMI2                                 = 249, // <ThrowStatement> ::= throw ';'
        RULE_SYNCHRONIZEDSTATEMENT_SYNCHRONIZED_LPARAN_RPARAN           = 250, // <SynchronizedStatement> ::= synchronized '(' <Expression> ')' <Block>
        RULE_TRYSTATEMENT_TRY                                           = 251, // <TryStatement> ::= try <Block> <Catches>
        RULE_TRYSTATEMENT_TRY2                                          = 252, // <TryStatement> ::= try <Block> <CatchDefault>
        RULE_TRYSTATEMENT_TRY3                                          = 253, // <TryStatement> ::= try <Block> <Catches> <CatchDefault>
        RULE_TRYSTATEMENT_TRY4                                          = 254, // <TryStatement> ::= try <Block> <Catches> <Finally>
        RULE_TRYSTATEMENT_TRY5                                          = 255, // <TryStatement> ::= try <Block> <Catches> <CatchDefault> <Finally>
        RULE_TRYSTATEMENT_TRY6                                          = 256, // <TryStatement> ::= try <Block> <CatchDefault> <Finally>
        RULE_TRYSTATEMENT_TRY7                                          = 257, // <TryStatement> ::= try <Block> <Finally>
        RULE_CATCHES                                                    = 258, // <Catches> ::= <CatchClause>
        RULE_CATCHES2                                                   = 259, // <Catches> ::= <Catches> <CatchClause>
        RULE_CATCHCLAUSE_CATCH_LPARAN_RPARAN                            = 260, // <CatchClause> ::= catch '(' <FormalParameter> ')' <Block>
        RULE_CATCHDEFAULT_CATCH                                         = 261, // <CatchDefault> ::= catch <Block>
        RULE_FINALLY_FINALLY                                            = 262, // <Finally> ::= finally <Block>
        RULE_PRIMARY                                                    = 263, // <Primary> ::= <PrimaryNoNewArray>
        RULE_PRIMARY2                                                   = 264, // <Primary> ::= <ArrayCreationExpression>
        RULE_PRIMARYNONEWARRAY                                          = 265, // <PrimaryNoNewArray> ::= <Literal>
        RULE_PRIMARYNONEWARRAY_THIS                                     = 266, // <PrimaryNoNewArray> ::= this
        RULE_PRIMARYNONEWARRAY_LPARAN_RPARAN                            = 267, // <PrimaryNoNewArray> ::= '(' <Expression> ')'
        RULE_PRIMARYNONEWARRAY2                                         = 268, // <PrimaryNoNewArray> ::= <ClassInstanceCreationExpression>
        RULE_PRIMARYNONEWARRAY3                                         = 269, // <PrimaryNoNewArray> ::= <FieldAccess>
        RULE_PRIMARYNONEWARRAY4                                         = 270, // <PrimaryNoNewArray> ::= <MethodInvocation>
        RULE_PRIMARYNONEWARRAY5                                         = 271, // <PrimaryNoNewArray> ::= <ArrayAccess>
        RULE_CLASSINSTANCECREATIONEXPRESSION_NEW_LPARAN_RPARAN          = 272, // <ClassInstanceCreationExpression> ::= new <ClassType> '(' <ArgumentList> ')'
        RULE_CLASSINSTANCECREATIONEXPRESSION_NEW_LPARAN_RPARAN2         = 273, // <ClassInstanceCreationExpression> ::= new <ClassType> '(' ')'
        RULE_ARGUMENTLIST                                               = 274, // <ArgumentList> ::= <SpecialArg>
        RULE_ARGUMENTLIST_COMMA                                         = 275, // <ArgumentList> ::= <ArgumentList> ',' <SpecialArg>
        RULE_ARGUMENTLIST2                                              = 276, // <ArgumentList> ::= <Expression>
        RULE_ARGUMENTLIST_COMMA2                                        = 277, // <ArgumentList> ::= <ArgumentList> ',' <Expression>
        RULE_SPECIALARG                                                 = 278, // <SpecialArg> ::= <ParamsArg>
        RULE_SPECIALARG2                                                = 279, // <SpecialArg> ::= <RefArg>
        RULE_SPECIALARG3                                                = 280, // <SpecialArg> ::= <OutArg>
        RULE_PARAMSARG_PARAMS_IDENTIFIER                                = 281, // <ParamsArg> ::= params Identifier
        RULE_REFARG_REF_IDENTIFIER                                      = 282, // <RefArg> ::= ref Identifier
        RULE_OUTARG_OUT_IDENTIFIER                                      = 283, // <OutArg> ::= out Identifier
        RULE_ARRAYCREATIONEXPRESSION_NEW                                = 284, // <ArrayCreationExpression> ::= new <PrimitiveType> <DimExprs> <Dims>
        RULE_ARRAYCREATIONEXPRESSION_NEW2                               = 285, // <ArrayCreationExpression> ::= new <PrimitiveType> <DimExprs>
        RULE_ARRAYCREATIONEXPRESSION_NEW3                               = 286, // <ArrayCreationExpression> ::= new <ClassOrInterfaceType> <DimExprs> <Dims>
        RULE_ARRAYCREATIONEXPRESSION_NEW4                               = 287, // <ArrayCreationExpression> ::= new <ClassOrInterfaceType> <DimExprs>
        RULE_DIMEXPRS                                                   = 288, // <DimExprs> ::= <DimExpr>
        RULE_DIMEXPRS2                                                  = 289, // <DimExprs> ::= <DimExprs> <DimExpr>
        RULE_DIMEXPR_LBRACKET_RBRACKET                                  = 290, // <DimExpr> ::= '[' <Expression> ']'
        RULE_DIMS_LBRACKET_RBRACKET                                     = 291, // <Dims> ::= '[' ']'
        RULE_DIMS_LBRACKET_RBRACKET2                                    = 292, // <Dims> ::= <Dims> '[' ']'
        RULE_FIELDACCESS_DOT_IDENTIFIER                                 = 293, // <FieldAccess> ::= <Primary> '.' Identifier
        RULE_FIELDACCESS_BASE_DOT_IDENTIFIER                            = 294, // <FieldAccess> ::= base '.' Identifier
        RULE_METHODINVOCATION_LPARAN_RPARAN                             = 295, // <MethodInvocation> ::= <Name> '(' <ArgumentList> ')'
        RULE_METHODINVOCATION_LPARAN_RPARAN2                            = 296, // <MethodInvocation> ::= <Name> '(' ')'
        RULE_METHODINVOCATION_DOT_IDENTIFIER_LPARAN_RPARAN              = 297, // <MethodInvocation> ::= <Primary> '.' Identifier '(' <ArgumentList> ')'
        RULE_METHODINVOCATION_DOT_IDENTIFIER_LPARAN_RPARAN2             = 298, // <MethodInvocation> ::= <Primary> '.' Identifier '(' ')'
        RULE_METHODINVOCATION_BASE_DOT_IDENTIFIER_LPARAN_RPARAN         = 299, // <MethodInvocation> ::= base '.' Identifier '(' <ArgumentList> ')'
        RULE_METHODINVOCATION_BASE_DOT_IDENTIFIER_LPARAN_RPARAN2        = 300, // <MethodInvocation> ::= base '.' Identifier '(' ')'
        RULE_ARRAYACCESS_LBRACKET_RBRACKET                              = 301, // <ArrayAccess> ::= <Name> '[' <Expression> ']'
        RULE_ARRAYACCESS_LBRACKET_RBRACKET2                             = 302, // <ArrayAccess> ::= <PrimaryNoNewArray> '[' <Expression> ']'
        RULE_POSTFIXEXPRESSION                                          = 303, // <PostfixExpression> ::= <Primary>
        RULE_POSTFIXEXPRESSION2                                         = 304, // <PostfixExpression> ::= <Name>
        RULE_POSTFIXEXPRESSION3                                         = 305, // <PostfixExpression> ::= <PostIncrementExpression>
        RULE_POSTFIXEXPRESSION4                                         = 306, // <PostfixExpression> ::= <PostDecrementExpression>
        RULE_POSTINCREMENTEXPRESSION_PLUSPLUS                           = 307, // <PostIncrementExpression> ::= <PostfixExpression> '++'
        RULE_POSTDECREMENTEXPRESSION_MINUSMINUS                         = 308, // <PostDecrementExpression> ::= <PostfixExpression> '--'
        RULE_UNARYEXPRESSION                                            = 309, // <UnaryExpression> ::= <PreIncrementExpression>
        RULE_UNARYEXPRESSION2                                           = 310, // <UnaryExpression> ::= <PreDecrementExpression>
        RULE_UNARYEXPRESSION_PLUS                                       = 311, // <UnaryExpression> ::= '+' <UnaryExpression>
        RULE_UNARYEXPRESSION_MINUS                                      = 312, // <UnaryExpression> ::= '-' <UnaryExpression>
        RULE_UNARYEXPRESSION3                                           = 313, // <UnaryExpression> ::= <UnaryExpressionNotPlusMinus>
        RULE_PREINCREMENTEXPRESSION_PLUSPLUS                            = 314, // <PreIncrementExpression> ::= '++' <UnaryExpression>
        RULE_PREDECREMENTEXPRESSION_MINUSMINUS                          = 315, // <PreDecrementExpression> ::= '--' <UnaryExpression>
        RULE_UNARYEXPRESSIONNOTPLUSMINUS                                = 316, // <UnaryExpressionNotPlusMinus> ::= <PostfixExpression>
        RULE_UNARYEXPRESSIONNOTPLUSMINUS_TILDE                          = 317, // <UnaryExpressionNotPlusMinus> ::= '~' <UnaryExpression>
        RULE_UNARYEXPRESSIONNOTPLUSMINUS_EXCLAM                         = 318, // <UnaryExpressionNotPlusMinus> ::= '!' <UnaryExpression>
        RULE_UNARYEXPRESSIONNOTPLUSMINUS2                               = 319, // <UnaryExpressionNotPlusMinus> ::= <CastExpression>
        RULE_CASTEXPRESSION_LPARAN_RPARAN                               = 320, // <CastExpression> ::= '(' <PrimitiveType> <Dims> ')' <UnaryExpression>
        RULE_CASTEXPRESSION_LPARAN_RPARAN2                              = 321, // <CastExpression> ::= '(' <PrimitiveType> ')' <UnaryExpression>
        RULE_CASTEXPRESSION_LPARAN_RPARAN3                              = 322, // <CastExpression> ::= '(' <Expression> ')' <UnaryExpressionNotPlusMinus>
        RULE_CASTEXPRESSION_LPARAN_RPARAN4                              = 323, // <CastExpression> ::= '(' <Name> <Dims> ')' <UnaryExpressionNotPlusMinus>
        RULE_MULTIPLICATIVEEXPRESSION                                   = 324, // <MultiplicativeExpression> ::= <UnaryExpression>
        RULE_MULTIPLICATIVEEXPRESSION_TIMES                             = 325, // <MultiplicativeExpression> ::= <MultiplicativeExpression> '*' <UnaryExpression>
        RULE_MULTIPLICATIVEEXPRESSION_DIV                               = 326, // <MultiplicativeExpression> ::= <MultiplicativeExpression> '/' <UnaryExpression>
        RULE_MULTIPLICATIVEEXPRESSION_PERCENT                           = 327, // <MultiplicativeExpression> ::= <MultiplicativeExpression> '%' <UnaryExpression>
        RULE_ADDITIVEEXPRESSION                                         = 328, // <AdditiveExpression> ::= <MultiplicativeExpression>
        RULE_ADDITIVEEXPRESSION_PLUS                                    = 329, // <AdditiveExpression> ::= <AdditiveExpression> '+' <MultiplicativeExpression>
        RULE_ADDITIVEEXPRESSION_MINUS                                   = 330, // <AdditiveExpression> ::= <AdditiveExpression> '-' <MultiplicativeExpression>
        RULE_SHIFTEXPRESSION                                            = 331, // <ShiftExpression> ::= <AdditiveExpression>
        RULE_SHIFTEXPRESSION_LTLT                                       = 332, // <ShiftExpression> ::= <ShiftExpression> '<<' <AdditiveExpression>
        RULE_SHIFTEXPRESSION_GTGT                                       = 333, // <ShiftExpression> ::= <ShiftExpression> '>>' <AdditiveExpression>
        RULE_SHIFTEXPRESSION_GTGTGT                                     = 334, // <ShiftExpression> ::= <ShiftExpression> '>>>' <AdditiveExpression>
        RULE_RELATIONALEXPRESSION                                       = 335, // <RelationalExpression> ::= <ShiftExpression>
        RULE_RELATIONALEXPRESSION_LT                                    = 336, // <RelationalExpression> ::= <RelationalExpression> '<' <ShiftExpression>
        RULE_RELATIONALEXPRESSION_GT                                    = 337, // <RelationalExpression> ::= <RelationalExpression> '>' <ShiftExpression>
        RULE_RELATIONALEXPRESSION_LTEQ                                  = 338, // <RelationalExpression> ::= <RelationalExpression> '<=' <ShiftExpression>
        RULE_RELATIONALEXPRESSION_GTEQ                                  = 339, // <RelationalExpression> ::= <RelationalExpression> '>=' <ShiftExpression>
        RULE_RELATIONALEXPRESSION_INSTANCEOF                            = 340, // <RelationalExpression> ::= <RelationalExpression> instanceof <ReferenceType>
        RULE_EQUALITYEXPRESSION                                         = 341, // <EqualityExpression> ::= <RelationalExpression>
        RULE_EQUALITYEXPRESSION_EQEQ                                    = 342, // <EqualityExpression> ::= <EqualityExpression> '==' <RelationalExpression>
        RULE_EQUALITYEXPRESSION_EXCLAMEQ                                = 343, // <EqualityExpression> ::= <EqualityExpression> '!=' <RelationalExpression>
        RULE_ANDEXPRESSION                                              = 344, // <AndExpression> ::= <EqualityExpression>
        RULE_ANDEXPRESSION_AMP                                          = 345, // <AndExpression> ::= <AndExpression> '&' <EqualityExpression>
        RULE_EXCLUSIVEOREXPRESSION                                      = 346, // <ExclusiveOrExpression> ::= <AndExpression>
        RULE_EXCLUSIVEOREXPRESSION_CARET                                = 347, // <ExclusiveOrExpression> ::= <ExclusiveOrExpression> '^' <AndExpression>
        RULE_INCLUSIVEOREXPRESSION                                      = 348, // <InclusiveOrExpression> ::= <ExclusiveOrExpression>
        RULE_INCLUSIVEOREXPRESSION_PIPE                                 = 349, // <InclusiveOrExpression> ::= <InclusiveOrExpression> '|' <ExclusiveOrExpression>
        RULE_CONDITIONALANDEXPRESSION                                   = 350, // <ConditionalAndExpression> ::= <InclusiveOrExpression>
        RULE_CONDITIONALANDEXPRESSION_AMPAMP                            = 351, // <ConditionalAndExpression> ::= <ConditionalAndExpression> '&&' <InclusiveOrExpression>
        RULE_CONDITIONALOREXPRESSION                                    = 352, // <ConditionalOrExpression> ::= <ConditionalAndExpression>
        RULE_CONDITIONALOREXPRESSION_PIPEPIPE                           = 353, // <ConditionalOrExpression> ::= <ConditionalOrExpression> '||' <ConditionalAndExpression>
        RULE_CONDITIONALEXPRESSION                                      = 354, // <ConditionalExpression> ::= <ConditionalOrExpression>
        RULE_CONDITIONALEXPRESSION_QUESTION_COLON                       = 355, // <ConditionalExpression> ::= <ConditionalOrExpression> '?' <Expression> ':' <ConditionalExpression>
        RULE_ASSIGNMENTEXPRESSION                                       = 356, // <AssignmentExpression> ::= <ConditionalExpression>
        RULE_ASSIGNMENTEXPRESSION2                                      = 357, // <AssignmentExpression> ::= <Assignment>
        RULE_ASSIGNMENT                                                 = 358, // <Assignment> ::= <LeftHandSide> <AssignmentOperator> <AssignmentExpression>
        RULE_LEFTHANDSIDE                                               = 359, // <LeftHandSide> ::= <Name>
        RULE_LEFTHANDSIDE2                                              = 360, // <LeftHandSide> ::= <FieldAccess>
        RULE_LEFTHANDSIDE3                                              = 361, // <LeftHandSide> ::= <ArrayAccess>
        RULE_ASSIGNMENTOPERATOR_EQ                                      = 362, // <AssignmentOperator> ::= '='
        RULE_ASSIGNMENTOPERATOR_TIMESEQ                                 = 363, // <AssignmentOperator> ::= '*='
        RULE_ASSIGNMENTOPERATOR_DIVEQ                                   = 364, // <AssignmentOperator> ::= '/='
        RULE_ASSIGNMENTOPERATOR_PERCENTEQ                               = 365, // <AssignmentOperator> ::= '%='
        RULE_ASSIGNMENTOPERATOR_PLUSEQ                                  = 366, // <AssignmentOperator> ::= '+='
        RULE_ASSIGNMENTOPERATOR_MINUSEQ                                 = 367, // <AssignmentOperator> ::= '-='
        RULE_ASSIGNMENTOPERATOR_LTLTEQ                                  = 368, // <AssignmentOperator> ::= '<<='
        RULE_ASSIGNMENTOPERATOR_GTGTEQ                                  = 369, // <AssignmentOperator> ::= '>>='
        RULE_ASSIGNMENTOPERATOR_GTGTGTEQ                                = 370, // <AssignmentOperator> ::= '>>>='
        RULE_ASSIGNMENTOPERATOR_AMPEQ                                   = 371, // <AssignmentOperator> ::= '&='
        RULE_ASSIGNMENTOPERATOR_CARETEQ                                 = 372, // <AssignmentOperator> ::= '^='
        RULE_ASSIGNMENTOPERATOR_PIPEEQ                                  = 373, // <AssignmentOperator> ::= '|='
        RULE_EXPRESSION                                                 = 374, // <Expression> ::= <AssignmentExpression>
        RULE_CONSTANTEXPRESSION                                         = 375  // <ConstantExpression> ::= <Expression>
    };

    public class MyParser
    {
        private LALRParser parser;

        public MyParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
            parser.OnAccept += new LALRParser.AcceptHandler(AcceptEvent);
            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            parser.Parse(source);

        }

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                args.Continue = false;
                //todo: Report message to UI?
            }
        }

        private Object CreateObject(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //(Whitespace)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENTEND :
                //(Comment End)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENTLINE :
                //(Comment Line)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENTSTART :
                //(Comment Start)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAM :
                //'!'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENTEQ :
                //'%='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AMP :
                //'&'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AMPAMP :
                //'&&'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AMPEQ :
                //'&='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPARAN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPARAN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESEQ :
                //'*='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOT :
                //'.'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIVEQ :
                //'/='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_QUESTION :
                //'?'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET :
                //'['
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET :
                //']'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CARET :
                //'^'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CARETEQ :
                //'^='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PIPE :
                //'|'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PIPEPIPE :
                //'||'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PIPEEQ :
                //'|='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TILDE :
                //'~'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSEQ :
                //'+='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTLT :
                //'<<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTLTEQ :
                //'<<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSEQ :
                //'-='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTGT :
                //'>>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTGTEQ :
                //'>>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTGTGT :
                //'>>>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTGTGTEQ :
                //'>>>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ABSTRACT :
                //abstract
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BASE :
                //base
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BOOLEAN :
                //boolean
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BOOLEANLITERAL :
                //BooleanLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BREAK :
                //break
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BYTE :
                //byte
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CATCH :
                //catch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CHAR :
                //char
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASS :
                //class
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONTINUE :
                //continue
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //default
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXTENDS :
                //extends
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FINALLY :
                //finally
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOATINGPOINTLITERAL :
                //FloatingPointLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOATINGPOINTLITERALEXPONENT :
                //FloatingPointLiteralExponent
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GET :
                //get
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_HEXESCAPECHARLITERAL :
                //HexEscapeCharLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_HEXINTEGERLITERAL :
                //HexIntegerLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INDIRECTCHARLITERAL :
                //IndirectCharLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INSTANCEOF :
                //instanceof
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTERFACE :
                //interface
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTERNAL :
                //internal
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LONG :
                //long
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NAMESPACE :
                //namespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEW :
                //new
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NULLLITERAL :
                //NullLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OCTALESCAPECHARLITERAL :
                //OctalEscapeCharLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OCTALINTEGERLITERAL :
                //OctalIntegerLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OUT :
                //out
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMS :
                //params
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIVATE :
                //private
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROTECTED :
                //protected
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PUBLIC :
                //public
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REF :
                //ref
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURN :
                //return
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEALED :
                //sealed
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SET :
                //set
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SHORT :
                //short
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STANDARDESCAPECHARLITERAL :
                //StandardEscapeCharLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STARTWITHNOZERODECIMALINTEGERLITERAL :
                //StartWithNoZeroDecimalIntegerLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STARTWITHZERODECIMALINTEGERLITERAL :
                //StartWithZeroDecimalIntegerLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATIC :
                //static
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SYNCHRONIZED :
                //synchronized
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_THIS :
                //this
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_THROW :
                //throw
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRY :
                //try
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_UNSAFE :
                //unsafe
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_USING :
                //using
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VOLATILE :
                //volatile
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ABSTRACTMETHODDECLARATION :
                //<AbstractMethodDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDITIVEEXPRESSION :
                //<AdditiveExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ALIASDECLARATION :
                //<AliasDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ANDEXPRESSION :
                //<AndExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGUMENTLIST :
                //<ArgumentList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARRAYACCESS :
                //<ArrayAccess>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARRAYCREATIONEXPRESSION :
                //<ArrayCreationExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARRAYINITIALIZER :
                //<ArrayInitializer>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARRAYTYPE :
                //<ArrayType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<Assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENTEXPRESSION :
                //<AssignmentExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENTOPERATOR :
                //<AssignmentOperator>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCK :
                //<Block>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCKSTATEMENT :
                //<BlockStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCKSTATEMENTS :
                //<BlockStatements>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BREAKSTATEMENT :
                //<BreakStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASTEXPRESSION :
                //<CastExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CATCHCLAUSE :
                //<CatchClause>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CATCHDEFAULT :
                //<CatchDefault>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CATCHES :
                //<Catches>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CHARACTERLITERAL :
                //<CharacterLiteral>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSBODY :
                //<ClassBody>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSBODYDECLARATION :
                //<ClassBodyDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSBODYDECLARATIONS :
                //<ClassBodyDeclarations>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSDECLARATION :
                //<ClassDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSINSTANCECREATIONEXPRESSION :
                //<ClassInstanceCreationExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSMEMBERDECLARATION :
                //<ClassMemberDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSORINTERFACETYPE :
                //<ClassOrInterfaceType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSTYPE :
                //<ClassType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSTYPELIST :
                //<ClassTypeList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CODE :
                //<Code>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CODES :
                //<Codes>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMPILATIONUNIT :
                //<CompilationUnit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITIONALANDEXPRESSION :
                //<ConditionalAndExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITIONALEXPRESSION :
                //<ConditionalExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITIONALOREXPRESSION :
                //<ConditionalOrExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONSTANTDECLARATION :
                //<ConstantDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONSTANTEXPRESSION :
                //<ConstantExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONSTRUCTORBODY :
                //<ConstructorBody>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONSTRUCTORDECLARATION :
                //<ConstructorDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONSTRUCTORDECLARATOR :
                //<ConstructorDeclarator>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONTINUESTATEMENT :
                //<ContinueStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECIMALINTEGERLITERAL :
                //<DecimalIntegerLiteral>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIMEXPR :
                //<DimExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIMEXPRS :
                //<DimExprs>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIMS :
                //<Dims>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOSTATEMENT :
                //<DoStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EMPTYSTATEMENT :
                //<EmptyStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQUALITYEXPRESSION :
                //<EqualityExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLUSIVEOREXPRESSION :
                //<ExclusiveOrExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPLICITCONSTRUCTORINVOCATION :
                //<ExplicitConstructorInvocation>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSIONSTATEMENT :
                //<ExpressionStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXTENDSINTERFACES :
                //<ExtendsInterfaces>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FIELDACCESS :
                //<FieldAccess>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FIELDDECLARATION :
                //<FieldDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FINALLY2 :
                //<Finally>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOATINGPOINTTYPE :
                //<FloatingPointType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOATPOINTLITERAL :
                //<FloatPointLiteral>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORINIT :
                //<ForInit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORMALPARAMETER :
                //<FormalParameter>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORMALPARAMETERLIST :
                //<FormalParameterList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORSTATEMENT :
                //<ForStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORSTATEMENTNOSHORTIF :
                //<ForStatementNoShortIf>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORUPDATE :
                //<ForUpdate>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GENERICTYPE :
                //<GenericType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFTHENELSESTATEMENT :
                //<IfThenElseStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFTHENELSESTATEMENTNOSHORTIF :
                //<IfThenElseStatementNoShortIf>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFTHENSTATEMENT :
                //<IfThenStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INCLUSIVEOREXPRESSION :
                //<InclusiveOrExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INHERITS :
                //<Inherits>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGERLITERAL :
                //<IntegerLiteral>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGRALTYPE :
                //<IntegralType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTERFACEBODY :
                //<InterfaceBody>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTERFACEDECLARATION :
                //<InterfaceDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTERFACEMEMBERDECLARATION :
                //<InterfaceMemberDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTERFACEMEMBERDECLARATIONS :
                //<InterfaceMemberDeclarations>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTERFACETYPE :
                //<InterfaceType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LABELEDSTATEMENT :
                //<LabeledStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LABELEDSTATEMENTNOSHORTIF :
                //<LabeledStatementNoShortIf>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LEFTHANDSIDE :
                //<LeftHandSide>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LITERAL :
                //<Literal>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOCALVARIABLEDECLARATION :
                //<LocalVariableDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOCALVARIABLEDECLARATIONSTATEMENT :
                //<LocalVariableDeclarationStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODBODY :
                //<MethodBody>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODDECLARATION :
                //<MethodDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODDECLARATOR :
                //<MethodDeclarator>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODHEADER :
                //<MethodHeader>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODINVOCATION :
                //<MethodInvocation>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MODIFIER :
                //<Modifier>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MODIFIERS :
                //<Modifiers>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULTIPLICATIVEEXPRESSION :
                //<MultiplicativeExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NAME :
                //<Name>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NAMESPACEDECLARATION :
                //<NamespaceDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NAMESPACEDECLARATIONBODY :
                //<NamespaceDeclarationBody>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NAMESPACEDECLARATIONHEADER :
                //<NamespaceDeclarationHeader>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMERICTYPE :
                //<NumericType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OUTARG :
                //<OutArg>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OUTFORMALPARAMETER :
                //<OutFormalParameter>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMSARG :
                //<ParamsArg>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMSFORMALPARAMETER :
                //<ParamsFormalParameter>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_POSTDECREMENTEXPRESSION :
                //<PostDecrementExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_POSTFIXEXPRESSION :
                //<PostfixExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_POSTINCREMENTEXPRESSION :
                //<PostIncrementExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PREDECREMENTEXPRESSION :
                //<PreDecrementExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PREINCREMENTEXPRESSION :
                //<PreIncrementExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARY :
                //<Primary>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARYNONEWARRAY :
                //<PrimaryNoNewArray>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMITIVETYPE :
                //<PrimitiveType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROPERTYBODY :
                //<PropertyBody>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROPERTYDECLARATION :
                //<PropertyDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROPERTYDECLARATOR :
                //<PropertyDeclarator>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROPERTYGETBODY :
                //<PropertyGetBody>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROPERTYHEADER :
                //<PropertyHeader>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROPERTYSETBODY :
                //<PropertySetBody>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_QUALIFIEDNAME :
                //<QualifiedName>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REFARG :
                //<RefArg>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REFERENCETYPE :
                //<ReferenceType>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REFFORMALPARAMETER :
                //<RefFormalParameter>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RELATIONALEXPRESSION :
                //<RelationalExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURNSTATEMENT :
                //<ReturnStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SHIFTEXPRESSION :
                //<ShiftExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SIMPLENAME :
                //<SimpleName>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SPECIALARG :
                //<SpecialArg>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SPECIALFORMALPARAMETER :
                //<SpecialFormalParameter>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<Statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTEXPRESSION :
                //<StatementExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTEXPRESSIONLIST :
                //<StatementExpressionList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTNOSHORTIF :
                //<StatementNoShortIf>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTWITHOUTTRAILINGSUBSTATEMENT :
                //<StatementWithoutTrailingSubstatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATICINITIALIZER :
                //<StaticInitializer>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHBLOCK :
                //<SwitchBlock>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHBLOCKSTATEMENTGROUP :
                //<SwitchBlockStatementGroup>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHBLOCKSTATEMENTGROUPS :
                //<SwitchBlockStatementGroups>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHLABEL :
                //<SwitchLabel>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHLABELS :
                //<SwitchLabels>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHSTATEMENT :
                //<SwitchStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SYNCHRONIZEDSTATEMENT :
                //<SynchronizedStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_THROWSTATEMENT :
                //<ThrowStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRYSTATEMENT :
                //<TryStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE :
                //<Type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPEDECLARATION :
                //<TypeDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPELIST :
                //<TypeList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_UNARYEXPRESSION :
                //<UnaryExpression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_UNARYEXPRESSIONNOTPLUSMINUS :
                //<UnaryExpressionNotPlusMinus>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_USINGDECLARATION :
                //<UsingDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATOR :
                //<VariableDeclarator>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATORID :
                //<VariableDeclaratorId>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATORS :
                //<VariableDeclarators>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEINITIALIZER :
                //<VariableInitializer>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEINITIALIZERS :
                //<VariableInitializers>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILESTATEMENT :
                //<WhileStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILESTATEMENTNOSHORTIF :
                //<WhileStatementNoShortIf>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        private void ReduceEvent(LALRParser parser, ReduceEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                args.Continue = false;
                //todo: Report message to UI?
            }
        }

        public static Object CreateObject(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_CHARACTERLITERAL_INDIRECTCHARLITERAL :
                //<CharacterLiteral> ::= IndirectCharLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CHARACTERLITERAL_STANDARDESCAPECHARLITERAL :
                //<CharacterLiteral> ::= StandardEscapeCharLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CHARACTERLITERAL_OCTALESCAPECHARLITERAL :
                //<CharacterLiteral> ::= OctalEscapeCharLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CHARACTERLITERAL_HEXESCAPECHARLITERAL :
                //<CharacterLiteral> ::= HexEscapeCharLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DECIMALINTEGERLITERAL_STARTWITHZERODECIMALINTEGERLITERAL :
                //<DecimalIntegerLiteral> ::= StartWithZeroDecimalIntegerLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DECIMALINTEGERLITERAL_STARTWITHNOZERODECIMALINTEGERLITERAL :
                //<DecimalIntegerLiteral> ::= StartWithNoZeroDecimalIntegerLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FLOATPOINTLITERAL_FLOATINGPOINTLITERAL :
                //<FloatPointLiteral> ::= FloatingPointLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FLOATPOINTLITERAL_FLOATINGPOINTLITERALEXPONENT :
                //<FloatPointLiteral> ::= FloatingPointLiteralExponent
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTEGERLITERAL :
                //<IntegerLiteral> ::= <DecimalIntegerLiteral>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTEGERLITERAL_HEXINTEGERLITERAL :
                //<IntegerLiteral> ::= HexIntegerLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTEGERLITERAL_OCTALINTEGERLITERAL :
                //<IntegerLiteral> ::= OctalIntegerLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL :
                //<Literal> ::= <IntegerLiteral>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL2 :
                //<Literal> ::= <FloatPointLiteral>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_BOOLEANLITERAL :
                //<Literal> ::= BooleanLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL3 :
                //<Literal> ::= <CharacterLiteral>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_STRINGLITERAL :
                //<Literal> ::= StringLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_NULLLITERAL :
                //<Literal> ::= NullLiteral
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPE :
                //<Type> ::= <PrimitiveType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPE2 :
                //<Type> ::= <ReferenceType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPE3 :
                //<Type> ::= <GenericType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_GENERICTYPE_LT_GT :
                //<GenericType> ::= <Name> '<' <TypeList> '>'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPELIST :
                //<TypeList> ::= <Type>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPELIST_COMMA :
                //<TypeList> ::= <TypeList> ',' <Type>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMITIVETYPE :
                //<PrimitiveType> ::= <NumericType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMITIVETYPE_BOOLEAN :
                //<PrimitiveType> ::= boolean
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NUMERICTYPE :
                //<NumericType> ::= <IntegralType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NUMERICTYPE2 :
                //<NumericType> ::= <FloatingPointType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTEGRALTYPE_BYTE :
                //<IntegralType> ::= byte
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTEGRALTYPE_SHORT :
                //<IntegralType> ::= short
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTEGRALTYPE_INT :
                //<IntegralType> ::= int
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTEGRALTYPE_LONG :
                //<IntegralType> ::= long
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTEGRALTYPE_CHAR :
                //<IntegralType> ::= char
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FLOATINGPOINTTYPE_FLOAT :
                //<FloatingPointType> ::= float
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FLOATINGPOINTTYPE_DOUBLE :
                //<FloatingPointType> ::= double
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_REFERENCETYPE :
                //<ReferenceType> ::= <ClassOrInterfaceType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_REFERENCETYPE2 :
                //<ReferenceType> ::= <ArrayType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSORINTERFACETYPE :
                //<ClassOrInterfaceType> ::= <Name>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSTYPE :
                //<ClassType> ::= <ClassOrInterfaceType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACETYPE :
                //<InterfaceType> ::= <ClassOrInterfaceType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYTYPE_LBRACKET_RBRACKET :
                //<ArrayType> ::= <PrimitiveType> '[' ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYTYPE_LBRACKET_RBRACKET2 :
                //<ArrayType> ::= <Name> '[' ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYTYPE_LBRACKET_RBRACKET3 :
                //<ArrayType> ::= <ArrayType> '[' ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NAME :
                //<Name> ::= <SimpleName>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NAME2 :
                //<Name> ::= <QualifiedName>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SIMPLENAME_IDENTIFIER :
                //<SimpleName> ::= Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_QUALIFIEDNAME_DOT_IDENTIFIER :
                //<QualifiedName> ::= <Name> '.' Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_COMPILATIONUNIT :
                //<CompilationUnit> ::= <Codes>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CODES :
                //<Codes> ::= <Code> <Codes>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CODES2 :
                //<Codes> ::= <Code>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CODE :
                //<Code> ::= <TypeDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CODE2 :
                //<Code> ::= <NamespaceDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CODE3 :
                //<Code> ::= <UsingDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CODE4 :
                //<Code> ::= <AliasDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NAMESPACEDECLARATION :
                //<NamespaceDeclaration> ::= <NamespaceDeclarationHeader> <NamespaceDeclarationBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NAMESPACEDECLARATIONHEADER_NAMESPACE :
                //<NamespaceDeclarationHeader> ::= namespace <Name>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NAMESPACEDECLARATIONBODY_LBRACE_RBRACE :
                //<NamespaceDeclarationBody> ::= '{' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NAMESPACEDECLARATIONBODY_LBRACE_RBRACE2 :
                //<NamespaceDeclarationBody> ::= '{' <Codes> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_USINGDECLARATION_USING_SEMI :
                //<UsingDeclaration> ::= using <Name> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ALIASDECLARATION_USING_EQ_SEMI :
                //<AliasDeclaration> ::= using <SimpleName> '=' <Name> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPEDECLARATION :
                //<TypeDeclaration> ::= <ClassDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPEDECLARATION2 :
                //<TypeDeclaration> ::= <InterfaceDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPEDECLARATION_SEMI :
                //<TypeDeclaration> ::= ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIERS :
                //<Modifiers> ::= <Modifier>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIERS2 :
                //<Modifiers> ::= <Modifiers> <Modifier>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_PUBLIC :
                //<Modifier> ::= public
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_PROTECTED :
                //<Modifier> ::= protected
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_PRIVATE :
                //<Modifier> ::= private
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_STATIC :
                //<Modifier> ::= static
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_ABSTRACT :
                //<Modifier> ::= abstract
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_SEALED :
                //<Modifier> ::= sealed
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_UNSAFE :
                //<Modifier> ::= unsafe
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_INTERNAL :
                //<Modifier> ::= internal
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MODIFIER_VOLATILE :
                //<Modifier> ::= volatile
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSDECLARATION_CLASS_IDENTIFIER :
                //<ClassDeclaration> ::= <Modifiers> class Identifier <Inherits> <ClassBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSDECLARATION_CLASS_IDENTIFIER2 :
                //<ClassDeclaration> ::= <Modifiers> class Identifier <ClassBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSDECLARATION_CLASS_IDENTIFIER3 :
                //<ClassDeclaration> ::= class Identifier <Inherits> <ClassBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSDECLARATION_CLASS_IDENTIFIER4 :
                //<ClassDeclaration> ::= class Identifier <ClassBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INHERITS_COLON :
                //<Inherits> ::= ':' <ClassTypeList>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSBODY_LBRACE_RBRACE :
                //<ClassBody> ::= '{' <ClassBodyDeclarations> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSBODY_LBRACE_RBRACE2 :
                //<ClassBody> ::= '{' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSBODYDECLARATIONS :
                //<ClassBodyDeclarations> ::= <ClassBodyDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSBODYDECLARATIONS2 :
                //<ClassBodyDeclarations> ::= <ClassBodyDeclarations> <ClassBodyDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSBODYDECLARATION :
                //<ClassBodyDeclaration> ::= <ClassMemberDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSBODYDECLARATION2 :
                //<ClassBodyDeclaration> ::= <StaticInitializer>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSBODYDECLARATION3 :
                //<ClassBodyDeclaration> ::= <ConstructorDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBERDECLARATION :
                //<ClassMemberDeclaration> ::= <FieldDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBERDECLARATION2 :
                //<ClassMemberDeclaration> ::= <PropertyDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBERDECLARATION3 :
                //<ClassMemberDeclaration> ::= <MethodDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FIELDDECLARATION_SEMI :
                //<FieldDeclaration> ::= <Modifiers> <Type> <VariableDeclarators> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FIELDDECLARATION_SEMI2 :
                //<FieldDeclaration> ::= <Type> <VariableDeclarators> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATORS :
                //<VariableDeclarators> ::= <VariableDeclarator>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATORS_COMMA :
                //<VariableDeclarators> ::= <VariableDeclarators> ',' <VariableDeclarator>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR :
                //<VariableDeclarator> ::= <VariableDeclaratorId>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_EQ :
                //<VariableDeclarator> ::= <VariableDeclaratorId> '=' <VariableInitializer>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATORID_IDENTIFIER :
                //<VariableDeclaratorId> ::= Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATORID_LBRACKET_RBRACKET :
                //<VariableDeclaratorId> ::= <VariableDeclaratorId> '[' ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZER :
                //<VariableInitializer> ::= <Expression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZER2 :
                //<VariableInitializer> ::= <ArrayInitializer>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODDECLARATION :
                //<MethodDeclaration> ::= <MethodHeader> <MethodBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODHEADER :
                //<MethodHeader> ::= <Modifiers> <Type> <MethodDeclarator>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODHEADER2 :
                //<MethodHeader> ::= <Type> <MethodDeclarator>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODDECLARATOR_IDENTIFIER_LPARAN_RPARAN :
                //<MethodDeclarator> ::= Identifier '(' <FormalParameterList> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODDECLARATOR_IDENTIFIER_LPARAN_RPARAN2 :
                //<MethodDeclarator> ::= Identifier '(' ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODDECLARATOR_LBRACKET_RBRACKET :
                //<MethodDeclarator> ::= <MethodDeclarator> '[' ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORMALPARAMETERLIST :
                //<FormalParameterList> ::= <FormalParameter>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORMALPARAMETERLIST_COMMA :
                //<FormalParameterList> ::= <FormalParameterList> ',' <FormalParameter>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORMALPARAMETER :
                //<FormalParameter> ::= <Type> <VariableDeclaratorId>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORMALPARAMETER2 :
                //<FormalParameter> ::= <SpecialFormalParameter>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SPECIALFORMALPARAMETER :
                //<SpecialFormalParameter> ::= <ParamsFormalParameter>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SPECIALFORMALPARAMETER2 :
                //<SpecialFormalParameter> ::= <RefFormalParameter>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SPECIALFORMALPARAMETER3 :
                //<SpecialFormalParameter> ::= <OutFormalParameter>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PARAMSFORMALPARAMETER_PARAMS :
                //<ParamsFormalParameter> ::= params <ArrayType> <VariableDeclaratorId>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_REFFORMALPARAMETER_REF :
                //<RefFormalParameter> ::= ref <Type> <VariableDeclaratorId>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_OUTFORMALPARAMETER_OUT :
                //<OutFormalParameter> ::= out <Type> <VariableDeclaratorId>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYDECLARATION :
                //<PropertyDeclaration> ::= <PropertyHeader> <PropertyBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYHEADER :
                //<PropertyHeader> ::= <Modifiers> <Type> <PropertyDeclarator>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYHEADER2 :
                //<PropertyHeader> ::= <Type> <PropertyDeclarator>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYDECLARATOR_IDENTIFIER :
                //<PropertyDeclarator> ::= Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYBODY_LBRACE_RBRACE :
                //<PropertyBody> ::= '{' <PropertyGetBody> <PropertySetBody> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYBODY_LBRACE_RBRACE2 :
                //<PropertyBody> ::= '{' <PropertyGetBody> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYBODY_LBRACE_RBRACE3 :
                //<PropertyBody> ::= '{' <PropertySetBody> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYGETBODY_GET :
                //<PropertyGetBody> ::= get <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYGETBODY_GET_SEMI :
                //<PropertyGetBody> ::= get ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYSETBODY_SET :
                //<PropertySetBody> ::= set <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PROPERTYSETBODY_SET_SEMI :
                //<PropertySetBody> ::= set ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSTYPELIST :
                //<ClassTypeList> ::= <ClassType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSTYPELIST_COMMA :
                //<ClassTypeList> ::= <ClassTypeList> ',' <ClassType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODBODY :
                //<MethodBody> ::= <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODBODY_SEMI :
                //<MethodBody> ::= ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATICINITIALIZER_STATIC :
                //<StaticInitializer> ::= static <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTRUCTORDECLARATION :
                //<ConstructorDeclaration> ::= <Modifiers> <ConstructorDeclarator> <ConstructorBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTRUCTORDECLARATION2 :
                //<ConstructorDeclaration> ::= <ConstructorDeclarator> <ConstructorBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTRUCTORDECLARATOR_LPARAN_RPARAN :
                //<ConstructorDeclarator> ::= <SimpleName> '(' <FormalParameterList> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTRUCTORDECLARATOR_LPARAN_RPARAN2 :
                //<ConstructorDeclarator> ::= <SimpleName> '(' ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTRUCTORDECLARATOR_LPARAN_RPARAN_COLON :
                //<ConstructorDeclarator> ::= <SimpleName> '(' <FormalParameterList> ')' ':' <ExplicitConstructorInvocation>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTRUCTORDECLARATOR_LPARAN_RPARAN_COLON2 :
                //<ConstructorDeclarator> ::= <SimpleName> '(' ')' ':' <ExplicitConstructorInvocation>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTRUCTORBODY_LBRACE_RBRACE :
                //<ConstructorBody> ::= '{' <BlockStatements> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTRUCTORBODY_LBRACE_RBRACE2 :
                //<ConstructorBody> ::= '{' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXPLICITCONSTRUCTORINVOCATION_THIS_LPARAN_RPARAN :
                //<ExplicitConstructorInvocation> ::= this '(' <ArgumentList> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXPLICITCONSTRUCTORINVOCATION_THIS_LPARAN_RPARAN2 :
                //<ExplicitConstructorInvocation> ::= this '(' ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXPLICITCONSTRUCTORINVOCATION_BASE_LPARAN_RPARAN :
                //<ExplicitConstructorInvocation> ::= base '(' <ArgumentList> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXPLICITCONSTRUCTORINVOCATION_BASE_LPARAN_RPARAN2 :
                //<ExplicitConstructorInvocation> ::= base '(' ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEDECLARATION_INTERFACE_IDENTIFIER :
                //<InterfaceDeclaration> ::= <Modifiers> interface Identifier <ExtendsInterfaces> <InterfaceBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEDECLARATION_INTERFACE_IDENTIFIER2 :
                //<InterfaceDeclaration> ::= <Modifiers> interface Identifier <InterfaceBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEDECLARATION_INTERFACE_IDENTIFIER3 :
                //<InterfaceDeclaration> ::= interface Identifier <ExtendsInterfaces> <InterfaceBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEDECLARATION_INTERFACE_IDENTIFIER4 :
                //<InterfaceDeclaration> ::= interface Identifier <InterfaceBody>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXTENDSINTERFACES_EXTENDS :
                //<ExtendsInterfaces> ::= extends <InterfaceType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXTENDSINTERFACES_COMMA :
                //<ExtendsInterfaces> ::= <ExtendsInterfaces> ',' <InterfaceType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEBODY_LBRACE_RBRACE :
                //<InterfaceBody> ::= '{' <InterfaceMemberDeclarations> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEBODY_LBRACE_RBRACE2 :
                //<InterfaceBody> ::= '{' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEMEMBERDECLARATIONS :
                //<InterfaceMemberDeclarations> ::= <InterfaceMemberDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEMEMBERDECLARATIONS2 :
                //<InterfaceMemberDeclarations> ::= <InterfaceMemberDeclarations> <InterfaceMemberDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEMEMBERDECLARATION :
                //<InterfaceMemberDeclaration> ::= <ConstantDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INTERFACEMEMBERDECLARATION2 :
                //<InterfaceMemberDeclaration> ::= <AbstractMethodDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTANTDECLARATION :
                //<ConstantDeclaration> ::= <FieldDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ABSTRACTMETHODDECLARATION_SEMI :
                //<AbstractMethodDeclaration> ::= <MethodHeader> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYINITIALIZER_LBRACE_COMMA_RBRACE :
                //<ArrayInitializer> ::= '{' <VariableInitializers> ',' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYINITIALIZER_LBRACE_RBRACE :
                //<ArrayInitializer> ::= '{' <VariableInitializers> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYINITIALIZER_LBRACE_COMMA_RBRACE2 :
                //<ArrayInitializer> ::= '{' ',' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYINITIALIZER_LBRACE_RBRACE2 :
                //<ArrayInitializer> ::= '{' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZERS :
                //<VariableInitializers> ::= <VariableInitializer>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZERS_COMMA :
                //<VariableInitializers> ::= <VariableInitializers> ',' <VariableInitializer>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE :
                //<Block> ::= '{' <BlockStatements> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE2 :
                //<Block> ::= '{' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_BLOCKSTATEMENTS :
                //<BlockStatements> ::= <BlockStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_BLOCKSTATEMENTS2 :
                //<BlockStatements> ::= <BlockStatements> <BlockStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_BLOCKSTATEMENT :
                //<BlockStatement> ::= <LocalVariableDeclarationStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_BLOCKSTATEMENT2 :
                //<BlockStatement> ::= <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LOCALVARIABLEDECLARATIONSTATEMENT_SEMI :
                //<LocalVariableDeclarationStatement> ::= <LocalVariableDeclaration> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LOCALVARIABLEDECLARATION :
                //<LocalVariableDeclaration> ::= <Type> <VariableDeclarators>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<Statement> ::= <StatementWithoutTrailingSubstatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<Statement> ::= <LabeledStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<Statement> ::= <IfThenStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<Statement> ::= <IfThenElseStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENT5 :
                //<Statement> ::= <WhileStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENT6 :
                //<Statement> ::= <ForStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTNOSHORTIF :
                //<StatementNoShortIf> ::= <StatementWithoutTrailingSubstatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTNOSHORTIF2 :
                //<StatementNoShortIf> ::= <LabeledStatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTNOSHORTIF3 :
                //<StatementNoShortIf> ::= <IfThenElseStatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTNOSHORTIF4 :
                //<StatementNoShortIf> ::= <WhileStatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTNOSHORTIF5 :
                //<StatementNoShortIf> ::= <ForStatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT :
                //<StatementWithoutTrailingSubstatement> ::= <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT2 :
                //<StatementWithoutTrailingSubstatement> ::= <EmptyStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT3 :
                //<StatementWithoutTrailingSubstatement> ::= <ExpressionStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT4 :
                //<StatementWithoutTrailingSubstatement> ::= <SwitchStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT5 :
                //<StatementWithoutTrailingSubstatement> ::= <DoStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT6 :
                //<StatementWithoutTrailingSubstatement> ::= <BreakStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT7 :
                //<StatementWithoutTrailingSubstatement> ::= <ContinueStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT8 :
                //<StatementWithoutTrailingSubstatement> ::= <ReturnStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT9 :
                //<StatementWithoutTrailingSubstatement> ::= <SynchronizedStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT10 :
                //<StatementWithoutTrailingSubstatement> ::= <ThrowStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTWITHOUTTRAILINGSUBSTATEMENT11 :
                //<StatementWithoutTrailingSubstatement> ::= <TryStatement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EMPTYSTATEMENT_SEMI :
                //<EmptyStatement> ::= ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LABELEDSTATEMENT_IDENTIFIER_COLON :
                //<LabeledStatement> ::= Identifier ':' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LABELEDSTATEMENTNOSHORTIF_IDENTIFIER_COLON :
                //<LabeledStatementNoShortIf> ::= Identifier ':' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXPRESSIONSTATEMENT_SEMI :
                //<ExpressionStatement> ::= <StatementExpression> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSION :
                //<StatementExpression> ::= <Assignment>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSION2 :
                //<StatementExpression> ::= <PreIncrementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSION3 :
                //<StatementExpression> ::= <PreDecrementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSION4 :
                //<StatementExpression> ::= <PostIncrementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSION5 :
                //<StatementExpression> ::= <PostDecrementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSION6 :
                //<StatementExpression> ::= <MethodInvocation>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSION7 :
                //<StatementExpression> ::= <ClassInstanceCreationExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_IFTHENSTATEMENT_IF_LPARAN_RPARAN :
                //<IfThenStatement> ::= if '(' <Expression> ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_IFTHENELSESTATEMENT_IF_LPARAN_RPARAN_ELSE :
                //<IfThenElseStatement> ::= if '(' <Expression> ')' <StatementNoShortIf> else <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_IFTHENELSESTATEMENTNOSHORTIF_IF_LPARAN_RPARAN_ELSE :
                //<IfThenElseStatementNoShortIf> ::= if '(' <Expression> ')' <StatementNoShortIf> else <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHSTATEMENT_SWITCH_LPARAN_RPARAN :
                //<SwitchStatement> ::= switch '(' <Expression> ')' <SwitchBlock>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHBLOCK_LBRACE_RBRACE :
                //<SwitchBlock> ::= '{' <SwitchBlockStatementGroups> <SwitchLabels> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHBLOCK_LBRACE_RBRACE2 :
                //<SwitchBlock> ::= '{' <SwitchBlockStatementGroups> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHBLOCK_LBRACE_RBRACE3 :
                //<SwitchBlock> ::= '{' <SwitchLabels> '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHBLOCK_LBRACE_RBRACE4 :
                //<SwitchBlock> ::= '{' '}'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHBLOCKSTATEMENTGROUPS :
                //<SwitchBlockStatementGroups> ::= <SwitchBlockStatementGroup>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHBLOCKSTATEMENTGROUPS2 :
                //<SwitchBlockStatementGroups> ::= <SwitchBlockStatementGroups> <SwitchBlockStatementGroup>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHBLOCKSTATEMENTGROUP :
                //<SwitchBlockStatementGroup> ::= <SwitchLabels> <BlockStatements>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHLABELS :
                //<SwitchLabels> ::= <SwitchLabel>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHLABELS2 :
                //<SwitchLabels> ::= <SwitchLabels> <SwitchLabel>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHLABEL_CASE_COLON :
                //<SwitchLabel> ::= case <ConstantExpression> ':'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SWITCHLABEL_DEFAULT_COLON :
                //<SwitchLabel> ::= default ':'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_WHILESTATEMENT_WHILE_LPARAN_RPARAN :
                //<WhileStatement> ::= while '(' <Expression> ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_WHILESTATEMENTNOSHORTIF_WHILE_LPARAN_RPARAN :
                //<WhileStatementNoShortIf> ::= while '(' <Expression> ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DOSTATEMENT_DO_WHILE_LPARAN_RPARAN_SEMI :
                //<DoStatement> ::= do <Statement> while '(' <Expression> ')' ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN :
                //<ForStatement> ::= for '(' <ForInit> ';' <Expression> ';' <ForUpdate> ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN2 :
                //<ForStatement> ::= for '(' <ForInit> ';' <Expression> ';' ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN3 :
                //<ForStatement> ::= for '(' <ForInit> ';' ';' <ForUpdate> ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN4 :
                //<ForStatement> ::= for '(' <ForInit> ';' ';' ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN5 :
                //<ForStatement> ::= for '(' ';' <Expression> ';' <ForUpdate> ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN6 :
                //<ForStatement> ::= for '(' ';' <Expression> ';' ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN7 :
                //<ForStatement> ::= for '(' ';' ';' <ForUpdate> ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_LPARAN_SEMI_SEMI_RPARAN8 :
                //<ForStatement> ::= for '(' ';' ';' ')' <Statement>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN :
                //<ForStatementNoShortIf> ::= for '(' <ForInit> ';' <Expression> ';' <ForUpdate> ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN2 :
                //<ForStatementNoShortIf> ::= for '(' <ForInit> ';' <Expression> ';' ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN3 :
                //<ForStatementNoShortIf> ::= for '(' <ForInit> ';' ';' <ForUpdate> ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN4 :
                //<ForStatementNoShortIf> ::= for '(' <ForInit> ';' ';' ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN5 :
                //<ForStatementNoShortIf> ::= for '(' ';' <Expression> ';' <ForUpdate> ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN6 :
                //<ForStatementNoShortIf> ::= for '(' ';' <Expression> ';' ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN7 :
                //<ForStatementNoShortIf> ::= for '(' ';' ';' <ForUpdate> ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENTNOSHORTIF_FOR_LPARAN_SEMI_SEMI_RPARAN8 :
                //<ForStatementNoShortIf> ::= for '(' ';' ';' ')' <StatementNoShortIf>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORINIT :
                //<ForInit> ::= <StatementExpressionList>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORINIT2 :
                //<ForInit> ::= <LocalVariableDeclaration>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORUPDATE :
                //<ForUpdate> ::= <StatementExpressionList>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSIONLIST :
                //<StatementExpressionList> ::= <StatementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPRESSIONLIST_COMMA :
                //<StatementExpressionList> ::= <StatementExpressionList> ',' <StatementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_BREAKSTATEMENT_BREAK_IDENTIFIER_SEMI :
                //<BreakStatement> ::= break Identifier ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_BREAKSTATEMENT_BREAK_SEMI :
                //<BreakStatement> ::= break ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONTINUESTATEMENT_CONTINUE_IDENTIFIER_SEMI :
                //<ContinueStatement> ::= continue Identifier ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONTINUESTATEMENT_CONTINUE_SEMI :
                //<ContinueStatement> ::= continue ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_RETURNSTATEMENT_RETURN_SEMI :
                //<ReturnStatement> ::= return <Expression> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_RETURNSTATEMENT_RETURN_SEMI2 :
                //<ReturnStatement> ::= return ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_THROWSTATEMENT_THROW_SEMI :
                //<ThrowStatement> ::= throw <Expression> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_THROWSTATEMENT_THROW_SEMI2 :
                //<ThrowStatement> ::= throw ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SYNCHRONIZEDSTATEMENT_SYNCHRONIZED_LPARAN_RPARAN :
                //<SynchronizedStatement> ::= synchronized '(' <Expression> ')' <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TRYSTATEMENT_TRY :
                //<TryStatement> ::= try <Block> <Catches>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TRYSTATEMENT_TRY2 :
                //<TryStatement> ::= try <Block> <CatchDefault>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TRYSTATEMENT_TRY3 :
                //<TryStatement> ::= try <Block> <Catches> <CatchDefault>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TRYSTATEMENT_TRY4 :
                //<TryStatement> ::= try <Block> <Catches> <Finally>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TRYSTATEMENT_TRY5 :
                //<TryStatement> ::= try <Block> <Catches> <CatchDefault> <Finally>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TRYSTATEMENT_TRY6 :
                //<TryStatement> ::= try <Block> <CatchDefault> <Finally>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TRYSTATEMENT_TRY7 :
                //<TryStatement> ::= try <Block> <Finally>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CATCHES :
                //<Catches> ::= <CatchClause>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CATCHES2 :
                //<Catches> ::= <Catches> <CatchClause>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CATCHCLAUSE_CATCH_LPARAN_RPARAN :
                //<CatchClause> ::= catch '(' <FormalParameter> ')' <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CATCHDEFAULT_CATCH :
                //<CatchDefault> ::= catch <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FINALLY_FINALLY :
                //<Finally> ::= finally <Block>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARY :
                //<Primary> ::= <PrimaryNoNewArray>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARY2 :
                //<Primary> ::= <ArrayCreationExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARYNONEWARRAY :
                //<PrimaryNoNewArray> ::= <Literal>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARYNONEWARRAY_THIS :
                //<PrimaryNoNewArray> ::= this
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARYNONEWARRAY_LPARAN_RPARAN :
                //<PrimaryNoNewArray> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARYNONEWARRAY2 :
                //<PrimaryNoNewArray> ::= <ClassInstanceCreationExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARYNONEWARRAY3 :
                //<PrimaryNoNewArray> ::= <FieldAccess>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARYNONEWARRAY4 :
                //<PrimaryNoNewArray> ::= <MethodInvocation>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARYNONEWARRAY5 :
                //<PrimaryNoNewArray> ::= <ArrayAccess>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSINSTANCECREATIONEXPRESSION_NEW_LPARAN_RPARAN :
                //<ClassInstanceCreationExpression> ::= new <ClassType> '(' <ArgumentList> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CLASSINSTANCECREATIONEXPRESSION_NEW_LPARAN_RPARAN2 :
                //<ClassInstanceCreationExpression> ::= new <ClassType> '(' ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTLIST :
                //<ArgumentList> ::= <SpecialArg>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTLIST_COMMA :
                //<ArgumentList> ::= <ArgumentList> ',' <SpecialArg>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTLIST2 :
                //<ArgumentList> ::= <Expression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTLIST_COMMA2 :
                //<ArgumentList> ::= <ArgumentList> ',' <Expression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SPECIALARG :
                //<SpecialArg> ::= <ParamsArg>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SPECIALARG2 :
                //<SpecialArg> ::= <RefArg>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SPECIALARG3 :
                //<SpecialArg> ::= <OutArg>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PARAMSARG_PARAMS_IDENTIFIER :
                //<ParamsArg> ::= params Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_REFARG_REF_IDENTIFIER :
                //<RefArg> ::= ref Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_OUTARG_OUT_IDENTIFIER :
                //<OutArg> ::= out Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYCREATIONEXPRESSION_NEW :
                //<ArrayCreationExpression> ::= new <PrimitiveType> <DimExprs> <Dims>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYCREATIONEXPRESSION_NEW2 :
                //<ArrayCreationExpression> ::= new <PrimitiveType> <DimExprs>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYCREATIONEXPRESSION_NEW3 :
                //<ArrayCreationExpression> ::= new <ClassOrInterfaceType> <DimExprs> <Dims>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYCREATIONEXPRESSION_NEW4 :
                //<ArrayCreationExpression> ::= new <ClassOrInterfaceType> <DimExprs>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DIMEXPRS :
                //<DimExprs> ::= <DimExpr>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DIMEXPRS2 :
                //<DimExprs> ::= <DimExprs> <DimExpr>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DIMEXPR_LBRACKET_RBRACKET :
                //<DimExpr> ::= '[' <Expression> ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DIMS_LBRACKET_RBRACKET :
                //<Dims> ::= '[' ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DIMS_LBRACKET_RBRACKET2 :
                //<Dims> ::= <Dims> '[' ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FIELDACCESS_DOT_IDENTIFIER :
                //<FieldAccess> ::= <Primary> '.' Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FIELDACCESS_BASE_DOT_IDENTIFIER :
                //<FieldAccess> ::= base '.' Identifier
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODINVOCATION_LPARAN_RPARAN :
                //<MethodInvocation> ::= <Name> '(' <ArgumentList> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODINVOCATION_LPARAN_RPARAN2 :
                //<MethodInvocation> ::= <Name> '(' ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODINVOCATION_DOT_IDENTIFIER_LPARAN_RPARAN :
                //<MethodInvocation> ::= <Primary> '.' Identifier '(' <ArgumentList> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODINVOCATION_DOT_IDENTIFIER_LPARAN_RPARAN2 :
                //<MethodInvocation> ::= <Primary> '.' Identifier '(' ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODINVOCATION_BASE_DOT_IDENTIFIER_LPARAN_RPARAN :
                //<MethodInvocation> ::= base '.' Identifier '(' <ArgumentList> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_METHODINVOCATION_BASE_DOT_IDENTIFIER_LPARAN_RPARAN2 :
                //<MethodInvocation> ::= base '.' Identifier '(' ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYACCESS_LBRACKET_RBRACKET :
                //<ArrayAccess> ::= <Name> '[' <Expression> ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ARRAYACCESS_LBRACKET_RBRACKET2 :
                //<ArrayAccess> ::= <PrimaryNoNewArray> '[' <Expression> ']'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_POSTFIXEXPRESSION :
                //<PostfixExpression> ::= <Primary>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_POSTFIXEXPRESSION2 :
                //<PostfixExpression> ::= <Name>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_POSTFIXEXPRESSION3 :
                //<PostfixExpression> ::= <PostIncrementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_POSTFIXEXPRESSION4 :
                //<PostfixExpression> ::= <PostDecrementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_POSTINCREMENTEXPRESSION_PLUSPLUS :
                //<PostIncrementExpression> ::= <PostfixExpression> '++'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_POSTDECREMENTEXPRESSION_MINUSMINUS :
                //<PostDecrementExpression> ::= <PostfixExpression> '--'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSION :
                //<UnaryExpression> ::= <PreIncrementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSION2 :
                //<UnaryExpression> ::= <PreDecrementExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSION_PLUS :
                //<UnaryExpression> ::= '+' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSION_MINUS :
                //<UnaryExpression> ::= '-' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSION3 :
                //<UnaryExpression> ::= <UnaryExpressionNotPlusMinus>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PREINCREMENTEXPRESSION_PLUSPLUS :
                //<PreIncrementExpression> ::= '++' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PREDECREMENTEXPRESSION_MINUSMINUS :
                //<PreDecrementExpression> ::= '--' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSIONNOTPLUSMINUS :
                //<UnaryExpressionNotPlusMinus> ::= <PostfixExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSIONNOTPLUSMINUS_TILDE :
                //<UnaryExpressionNotPlusMinus> ::= '~' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSIONNOTPLUSMINUS_EXCLAM :
                //<UnaryExpressionNotPlusMinus> ::= '!' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPRESSIONNOTPLUSMINUS2 :
                //<UnaryExpressionNotPlusMinus> ::= <CastExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CASTEXPRESSION_LPARAN_RPARAN :
                //<CastExpression> ::= '(' <PrimitiveType> <Dims> ')' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CASTEXPRESSION_LPARAN_RPARAN2 :
                //<CastExpression> ::= '(' <PrimitiveType> ')' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CASTEXPRESSION_LPARAN_RPARAN3 :
                //<CastExpression> ::= '(' <Expression> ')' <UnaryExpressionNotPlusMinus>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CASTEXPRESSION_LPARAN_RPARAN4 :
                //<CastExpression> ::= '(' <Name> <Dims> ')' <UnaryExpressionNotPlusMinus>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MULTIPLICATIVEEXPRESSION :
                //<MultiplicativeExpression> ::= <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MULTIPLICATIVEEXPRESSION_TIMES :
                //<MultiplicativeExpression> ::= <MultiplicativeExpression> '*' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MULTIPLICATIVEEXPRESSION_DIV :
                //<MultiplicativeExpression> ::= <MultiplicativeExpression> '/' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MULTIPLICATIVEEXPRESSION_PERCENT :
                //<MultiplicativeExpression> ::= <MultiplicativeExpression> '%' <UnaryExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ADDITIVEEXPRESSION :
                //<AdditiveExpression> ::= <MultiplicativeExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ADDITIVEEXPRESSION_PLUS :
                //<AdditiveExpression> ::= <AdditiveExpression> '+' <MultiplicativeExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ADDITIVEEXPRESSION_MINUS :
                //<AdditiveExpression> ::= <AdditiveExpression> '-' <MultiplicativeExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SHIFTEXPRESSION :
                //<ShiftExpression> ::= <AdditiveExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SHIFTEXPRESSION_LTLT :
                //<ShiftExpression> ::= <ShiftExpression> '<<' <AdditiveExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SHIFTEXPRESSION_GTGT :
                //<ShiftExpression> ::= <ShiftExpression> '>>' <AdditiveExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SHIFTEXPRESSION_GTGTGT :
                //<ShiftExpression> ::= <ShiftExpression> '>>>' <AdditiveExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXPRESSION :
                //<RelationalExpression> ::= <ShiftExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXPRESSION_LT :
                //<RelationalExpression> ::= <RelationalExpression> '<' <ShiftExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXPRESSION_GT :
                //<RelationalExpression> ::= <RelationalExpression> '>' <ShiftExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXPRESSION_LTEQ :
                //<RelationalExpression> ::= <RelationalExpression> '<=' <ShiftExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXPRESSION_GTEQ :
                //<RelationalExpression> ::= <RelationalExpression> '>=' <ShiftExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXPRESSION_INSTANCEOF :
                //<RelationalExpression> ::= <RelationalExpression> instanceof <ReferenceType>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXPRESSION :
                //<EqualityExpression> ::= <RelationalExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXPRESSION_EQEQ :
                //<EqualityExpression> ::= <EqualityExpression> '==' <RelationalExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXPRESSION_EXCLAMEQ :
                //<EqualityExpression> ::= <EqualityExpression> '!=' <RelationalExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ANDEXPRESSION :
                //<AndExpression> ::= <EqualityExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ANDEXPRESSION_AMP :
                //<AndExpression> ::= <AndExpression> '&' <EqualityExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXCLUSIVEOREXPRESSION :
                //<ExclusiveOrExpression> ::= <AndExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXCLUSIVEOREXPRESSION_CARET :
                //<ExclusiveOrExpression> ::= <ExclusiveOrExpression> '^' <AndExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INCLUSIVEOREXPRESSION :
                //<InclusiveOrExpression> ::= <ExclusiveOrExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INCLUSIVEOREXPRESSION_PIPE :
                //<InclusiveOrExpression> ::= <InclusiveOrExpression> '|' <ExclusiveOrExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONDITIONALANDEXPRESSION :
                //<ConditionalAndExpression> ::= <InclusiveOrExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONDITIONALANDEXPRESSION_AMPAMP :
                //<ConditionalAndExpression> ::= <ConditionalAndExpression> '&&' <InclusiveOrExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONDITIONALOREXPRESSION :
                //<ConditionalOrExpression> ::= <ConditionalAndExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONDITIONALOREXPRESSION_PIPEPIPE :
                //<ConditionalOrExpression> ::= <ConditionalOrExpression> '||' <ConditionalAndExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONDITIONALEXPRESSION :
                //<ConditionalExpression> ::= <ConditionalOrExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONDITIONALEXPRESSION_QUESTION_COLON :
                //<ConditionalExpression> ::= <ConditionalOrExpression> '?' <Expression> ':' <ConditionalExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTEXPRESSION :
                //<AssignmentExpression> ::= <ConditionalExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTEXPRESSION2 :
                //<AssignmentExpression> ::= <Assignment>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT :
                //<Assignment> ::= <LeftHandSide> <AssignmentOperator> <AssignmentExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LEFTHANDSIDE :
                //<LeftHandSide> ::= <Name>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LEFTHANDSIDE2 :
                //<LeftHandSide> ::= <FieldAccess>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LEFTHANDSIDE3 :
                //<LeftHandSide> ::= <ArrayAccess>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_EQ :
                //<AssignmentOperator> ::= '='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_TIMESEQ :
                //<AssignmentOperator> ::= '*='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_DIVEQ :
                //<AssignmentOperator> ::= '/='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_PERCENTEQ :
                //<AssignmentOperator> ::= '%='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_PLUSEQ :
                //<AssignmentOperator> ::= '+='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_MINUSEQ :
                //<AssignmentOperator> ::= '-='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_LTLTEQ :
                //<AssignmentOperator> ::= '<<='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_GTGTEQ :
                //<AssignmentOperator> ::= '>>='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_GTGTGTEQ :
                //<AssignmentOperator> ::= '>>>='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_AMPEQ :
                //<AssignmentOperator> ::= '&='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_CARETEQ :
                //<AssignmentOperator> ::= '^='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENTOPERATOR_PIPEEQ :
                //<AssignmentOperator> ::= '|='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <AssignmentExpression>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONSTANTEXPRESSION :
                //<ConstantExpression> ::= <Expression>
                //todo: Create a new object using the stored user objects.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
        {
            //todo: Use your fully reduced args.Token.UserObject
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            //todo: Report message to UI?
        }


    }
}
