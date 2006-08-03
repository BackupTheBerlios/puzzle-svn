using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Text;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Mapping;

namespace Puzzle.ObjectMapper.Plugins.DLinqGenerator
{
	/// <summary>
	/// Summary description for CodeDomGenerator.
	/// </summary>
	public class CodeDomGenerator
	{
		public CodeDomGenerator()
		{
		}

		public static string ToCode(CodeCompileUnit compileunit, CodeDomProvider provider)
		{

			StringBuilder sb = new StringBuilder() ;
			StringWriter sw = new StringWriter(sb);
			IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");

			ICodeGenerator gen = provider.CreateGenerator(tw);

			gen.GenerateCodeFromCompileUnit(compileunit, tw, 
				new CodeGeneratorOptions());

			string code = sb.ToString();

			return code;
		}


		public static CodeCompileUnit GetDLinqClassCompileUnit(IClassMap classMap)
		{			
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();

			codeCompileUnit.Namespaces.Add(GenerateDLinqClass(classMap));

			return codeCompileUnit ;
		}

		#region Factory

		public static CodeNamespace GenerateDLinqClass(IClassMap classMap)
		{
			CodeNamespace domainNamespace = new CodeNamespace(classMap.GetFullNamespace()) ;
			CodeTypeDeclaration classDecl = new CodeTypeDeclaration(GetDLinqClassName(classMap)) ;

			classDecl.IsClass = true;

			//Fields
			foreach (IPropertyMap propertyMap in classMap.PropertyMaps)
			{
				GenerateDLinqField(classMap, propertyMap, classDecl);
			}

			//Properties
			foreach (IPropertyMap propertyMap in classMap.PropertyMaps)
			{
				GenerateDLinqProperty(classMap, propertyMap, classDecl);
			}

			domainNamespace.Types.Add(classDecl);

			return domainNamespace;			

		}

		public static void GenerateDLinqField(IClassMap classMap, IPropertyMap propertyMap, CodeTypeDeclaration classDecl)
		{

			switch (propertyMap.ReferenceType)
			{
				case ReferenceType.None :
					classDecl.Members.Add(GenerateDLinqPrimitiveField(classMap, propertyMap));									
				break;
				case ReferenceType.OneToMany :
					GenerateDLinqOneToManyFields(classMap, propertyMap, classDecl);									
				break;
			}
			
		}

		public static void GenerateDLinqProperty(IClassMap classMap, IPropertyMap propertyMap, CodeTypeDeclaration classDecl)
		{

			switch (propertyMap.ReferenceType)
			{
				case ReferenceType.None :
					classDecl.Members.Add(GenerateDLinqPrimitiveProperty(classMap, propertyMap));									
				break;
				case ReferenceType.OneToMany :
					GenerateDLinqOneToManyProperties(classMap, propertyMap, classDecl);									
				break;
			}
			
		}

		#region Field generation

		public static CodeMemberField GenerateDLinqPrimitiveField(IClassMap classMap, IPropertyMap propertyMap)
		{
			string fieldName = propertyMap.GetFieldName();

			CodeMemberField fieldMember = new CodeMemberField() ;
			fieldMember.Name = fieldName;

			CodeTypeReference typeReference = new CodeTypeReference(propertyMap.DataType);
			fieldMember.Type = typeReference;
			

			return fieldMember;

		}
/*
     
    private System.Nullable<int> _ReportsTo;
    
    private EntityRef<Employee> _ReportsToEmployee;

 * */
		public static void GenerateDLinqOneToManyFields(IClassMap classMap, IPropertyMap propertyMap, CodeTypeDeclaration classDecl)
		{
			foreach (IColumnMap columnMap in propertyMap.GetAllColumnMaps())
			{
				string fieldName = propertyMap.GenerateMemberName(columnMap.Name);

				CodeMemberField fieldMember = new CodeMemberField() ;
				fieldMember.Name = fieldName;

				//Add code for adding Nullable generics when OM is ported to .NET 2.0

				CodeTypeReference typeReference = new CodeTypeReference(columnMap.GetSystemType());
				fieldMember.Type = typeReference;
			
				classDecl.Members.Add(fieldMember);				
			}

		}

		#endregion

		#region Property generation

		public static CodeMemberProperty GenerateDLinqPrimitiveProperty(IClassMap classMap, IPropertyMap propertyMap)
		{
			string fieldName = propertyMap.GetFieldName();

			return GenerateDLinqPrimitiveProperty(propertyMap, propertyMap.Name, fieldName, propertyMap.DataType);

		}

		public static void GenerateDLinqOneToManyProperties(IClassMap classMap, IPropertyMap propertyMap, CodeTypeDeclaration classDecl)
		{
			foreach (IColumnMap columnMap in propertyMap.GetAllColumnMaps())
			{		
				string fieldName = propertyMap.GenerateMemberName(columnMap.Name);
				string typeName = columnMap.GetSystemType().ToString();

				CodeMemberProperty propertyMember = GenerateDLinqPrimitiveProperty(propertyMap, columnMap.Name, fieldName, typeName);

				classDecl.Members.Add(propertyMember);				
			}	
		}


		private static CodeMemberProperty GenerateDLinqPrimitiveProperty(IPropertyMap propertyMap, string propertyName, string fieldName, string typeName)
		{
			CodeMemberProperty propertyMember = new CodeMemberProperty() ;
			propertyMember.Name = propertyName;
	
			CodeTypeReference typeReference = new CodeTypeReference(typeName);
			propertyMember.Type = typeReference;
	
			propertyMember.Attributes = MemberAttributes.Public;
	
			//Column attribute
			CodeAttributeDeclaration columnAttrib = new CodeAttributeDeclaration("Column");
	
			CodeAttributeArgument columnNameArg = new CodeAttributeArgument("Name", new CodePrimitiveExpression(propertyMap.Column)) ; 
			columnAttrib.Arguments.Add(columnNameArg);
	
			CodeAttributeArgument storageArg = new CodeAttributeArgument("Storage", new CodePrimitiveExpression(fieldName)) ; 
			columnAttrib.Arguments.Add(storageArg);
	
			if (propertyMap.IsIdentity)
			{				
				CodeAttributeArgument idArg = new CodeAttributeArgument("Id", new CodePrimitiveExpression(true)) ; 
				columnAttrib.Arguments.Add(idArg);
			}
	
			if (propertyMap.IsAssignedBySource)
			{				
				CodeAttributeArgument autoGenArg = new CodeAttributeArgument("AutoGen", new CodePrimitiveExpression(true)) ; 
				columnAttrib.Arguments.Add(autoGenArg);
			}				
	
			propertyMember.CustomAttributes.Add(columnAttrib); 
	
	
			CodeFieldReferenceExpression fieldExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName);
			CodeVariableReferenceExpression valueVar = new CodeVariableReferenceExpression("value");
			CodePrimitiveExpression propNameEpr = new CodePrimitiveExpression(propertyMap.Name);
	
			//Getter method
			CodeMethodReturnStatement returnStmt = new CodeMethodReturnStatement(fieldExpression) ;
	
			propertyMember.GetStatements.Add(returnStmt);
	
	
			//Setter method
			CodeAssignStatement setStmt = new CodeAssignStatement(fieldExpression, valueVar) ;
	
			CodeExpressionStatement onPropertyChangingStmt = new CodeExpressionStatement( new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "OnPropertyChanging", new CodeExpression[] { propNameEpr } ) );
			CodeExpressionStatement onPropertyChangedStmt = new CodeExpressionStatement( new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "OnPropertyChanged", new CodeExpression[] { propNameEpr } ) ) ;
	
			CodeBinaryOperatorExpression ifExpression = new CodeBinaryOperatorExpression(fieldExpression, CodeBinaryOperatorType.IdentityInequality, valueVar) ;
	
			CodeConditionStatement ifStmt = new CodeConditionStatement(ifExpression, new CodeStatement[]
				{
					onPropertyChangingStmt, 
					setStmt,
					onPropertyChangedStmt
				} ) ;
	
			propertyMember.SetStatements.Add(ifStmt);
	
			return propertyMember;
		}



		#endregion


//		public static void GenerateFactoryMethods(IClassMap classMap, CodeTypeDeclaration classDecl)
//		{
//			IList propertyMaps = GetRequiredPropertyMaps(classMap);
//			classDecl.Members.Add(GenerateFactoryMethod(classMap, propertyMaps));					
//
//			IList optionalPropertyMaps = GetOptionalPropertyMaps(classMap);
//			if (optionalPropertyMaps.Count > 0)
//			{
//				foreach (IPropertyMap propertyMap in optionalPropertyMaps)
//					propertyMaps.Add(propertyMap);
//
//				classDecl.Members.Add(GenerateFactoryMethod(classMap, propertyMaps));					
//			}
//		}


//		public static CodeMemberMethod GenerateFactoryMethod(IClassMap classMap, IList propertyMaps)
//		{
//
//			CodeMemberMethod methodMember = new CodeMemberMethod() ;
//			methodMember.Name = GetFactoryMethodName(classMap);
//
//			CodeTypeReference typeReference = new CodeTypeReference(classMap.GetName());
//			methodMember.ReturnType = typeReference;
//			
//			methodMember.Attributes = MemberAttributes.Public;
//
//			foreach(IPropertyMap propertyMap in propertyMaps)
//			{
//				CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(new CodeTypeReference(propertyMap.DataType), MakeCamelCase(propertyMap.Name));
//				methodMember.Parameters.Add(parameter);
//			}
//
//			CodeVariableDeclarationStatement contextVarDecl = new CodeVariableDeclarationStatement("IContext", "context", null) ;
//			CodeVariableReferenceExpression contextVar = new CodeVariableReferenceExpression("context");
//
//			methodMember.Statements.Add(contextVarDecl);
//
//			CodeTypeOfExpression typeOfExp = new CodeTypeOfExpression(typeReference) ;
//			CodeMethodInvokeExpression newObjectInit = new CodeMethodInvokeExpression(contextVar, "CreateObject", new CodeExpression[] { typeOfExp } ) ;
//
//			CodeCastExpression castExp = new CodeCastExpression(typeReference, newObjectInit) ;
//
//			CodeVariableDeclarationStatement newObjectVarDecl = new CodeVariableDeclarationStatement(classMap.GetName(), MakeCamelCase(classMap.GetName()), castExp) ;
//			CodeVariableReferenceExpression newObjectVar = new CodeVariableReferenceExpression(MakeCamelCase(classMap.GetName()));
//
//			methodMember.Statements.Add(newObjectVarDecl);
// 
//			foreach(IPropertyMap propertyMap in propertyMaps)
//			{
//				CodeArgumentReferenceExpression argExp = new CodeArgumentReferenceExpression(MakeCamelCase(propertyMap.Name));
//				CodeVariableReferenceExpression propExp = new CodeVariableReferenceExpression(MakeCamelCase(classMap.Name) + "." + propertyMap.Name);
//				CodeAssignStatement assignStatement = new CodeAssignStatement(propExp, argExp);
//				methodMember.Statements.Add(assignStatement);
//			}
//
//			CodeMethodInvokeExpression commitCtx = new CodeMethodInvokeExpression(contextVar, "Commit", new CodeExpression[] {} ) ;
//
//			methodMember.Statements.Add(commitCtx);
//
//			CodeMethodReturnStatement returnStmt = new CodeMethodReturnStatement(newObjectVar) ;
//
//			methodMember.Statements.Add(returnStmt);
//
//			return methodMember;
//
//		}

		#endregion


		#region Template

		public static string GetDLinqClassName(IClassMap classMap)
		{		
			string name = classMap.GetName() ;

			return name;
		}
	
		#endregion

		#region Helper Methods

		private static IList GetRequiredPropertyMaps(IClassMap classMap)
		{
			IList requiredPropertyMaps = new ArrayList();
			foreach (IPropertyMap propertyMap in classMap.GetAllPropertyMaps())
			{
				if (propertyMap.IsIdentity)
				{
					if (!propertyMap.GetIsAssignedBySource())
						requiredPropertyMaps.Add(propertyMap);				
				}
				else
				{
					if (!propertyMap.IsCollection)
						if (!propertyMap.GetIsNullable())
							requiredPropertyMaps.Add(propertyMap);								
				}				
			}

			return requiredPropertyMaps;
		}

		private static IList GetOptionalPropertyMaps(IClassMap classMap)
		{
			IList optionalPropertyMaps = new ArrayList();
			foreach (IPropertyMap propertyMap in classMap.GetAllPropertyMaps())
			{
				if (!propertyMap.IsIdentity)
				{
					if (!propertyMap.IsCollection)
						if (propertyMap.GetIsNullable())
							optionalPropertyMaps.Add(propertyMap);								
				}				
			}

			return optionalPropertyMaps;
		}

		private static string MakeCamelCase(string name)
		{
			return name.Substring(0, 1).ToLower() + name.Substring(1);
		}


		#endregion
	}
}
