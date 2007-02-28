using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Text;
using Microsoft.CSharp;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Mapping;

namespace Puzzle.ObjectMapper.Plugins.ServiceLayerGenerator
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

		#region General

		public static void GenerateConstructor(CodeTypeDeclaration classDecl)
		{
			CodeConstructor constructorMember = new CodeConstructor() ;
			
			constructorMember.Attributes = MemberAttributes.Public;

			CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(new CodeTypeReference("IContext"), "context");
			constructorMember.Parameters.Add(parameter);
 
			CodeThisReferenceExpression thisExp = new CodeThisReferenceExpression();
			CodeFieldReferenceExpression ctxFieldExp = new CodeFieldReferenceExpression(thisExp, "context");

			CodeArgumentReferenceExpression argExp = new CodeArgumentReferenceExpression("context");
			CodeAssignStatement assignStatement = new CodeAssignStatement(ctxFieldExp, argExp);
			constructorMember.Statements.Add(assignStatement);

			classDecl.Members.Add(constructorMember);					
		}

		public static void GenerateContextField(CodeTypeDeclaration classDecl)
		{
			CodeMemberField contextField = new CodeMemberField("IContext", "context");
 
			classDecl.Members.Add(contextField);					
		}

		#endregion

		#region Repository

		public static CodeCompileUnit GetRepositoryClassCompileUnit(IClassMap classMap)
		{			
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();

			codeCompileUnit.Namespaces.Add(GenerateRepositoryClass(classMap));

			return codeCompileUnit ;
		}

		public static CodeNamespace GenerateRepositoryClass(IClassMap classMap)
		{
			CodeNamespace domainNamespace = new CodeNamespace(classMap.DomainMap.RootNamespace + ".Repositories" ) ;
			CodeTypeDeclaration classDecl = new CodeTypeDeclaration(GetRepositoryClassName(classMap)) ;

			CodeNamespaceImport import = new CodeNamespaceImport(classMap.GetFullNamespace()) ;
			CodeNamespaceImport importNPersist = new CodeNamespaceImport("Puzzle.NPersist.Framework") ;
			domainNamespace.Imports.Add(import);
			domainNamespace.Imports.Add(importNPersist);

			classDecl.IsClass = true;

			GenerateConstructor(classDecl);
			GenerateContextField(classDecl);

			GenerateRepositoryGetByIdentityMethod(classMap, classDecl, false);
			GenerateRepositoryGetByIdentityMethod(classMap, classDecl, true);
			GenerateRepositoryGetAllObjectsMethod(classMap, classDecl);
			//GenerateRepositoryGetByNPathMethod(classMap, classDecl);
			GenerateRepositoryUpdateObjectMethod(classMap, classDecl);
			GenerateRepositoryDeleteObjectMethod(classMap, classDecl);

			domainNamespace.Types.Add(classDecl);

			return domainNamespace;
		}

		public static void GenerateRepositoryGetByIdentityMethod(IClassMap classMap, CodeTypeDeclaration classDecl, bool tryMethod)
		{
			IList propertyMaps = classMap.GetIdentityPropertyMaps();

			CodeMemberMethod methodMember = new CodeMemberMethod() ;
			if (tryMethod)
				methodMember.Name = GetRepositoryGetByIdentityMethodName(classMap);
			else
				methodMember.Name = GetRepositoryTryGetByIdentityMethodName(classMap);

			CodeTypeReference typeReference = new CodeTypeReference(classMap.GetName());
			methodMember.ReturnType = typeReference;
			
			methodMember.Attributes = MemberAttributes.Public;

			foreach(IPropertyMap propertyMap in propertyMaps)
			{
				CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(new CodeTypeReference(propertyMap.DataType), MakeCamelCase(propertyMap.Name));
				methodMember.Parameters.Add(parameter);
			}

			CodeThisReferenceExpression thisExp = new CodeThisReferenceExpression();
			CodeFieldReferenceExpression contextVar = new CodeFieldReferenceExpression(thisExp, "context");

			CodeVariableDeclarationStatement dummyObjectVarDecl = new CodeVariableDeclarationStatement(classMap.GetName(), "identityObject") ;
			CodeVariableReferenceExpression dummyObjectVar = new CodeVariableReferenceExpression("identityObject");

			CodeObjectCreateExpression dummyObjectCreateExpr = new CodeObjectCreateExpression(typeReference, new CodeExpression[] {} );

			CodeAssignStatement assignDummyStatement = new CodeAssignStatement(dummyObjectVar, dummyObjectCreateExpr);

			methodMember.Statements.Add(dummyObjectVarDecl);
			methodMember.Statements.Add(assignDummyStatement);

			foreach(IPropertyMap propertyMap in propertyMaps)
			{
				CodeArgumentReferenceExpression argExp = new CodeArgumentReferenceExpression(MakeCamelCase(propertyMap.Name));
				CodeVariableReferenceExpression propExp = new CodeVariableReferenceExpression("identityObject" + "." + propertyMap.Name);
				CodeAssignStatement assignStatement = new CodeAssignStatement(propExp, argExp);
				methodMember.Statements.Add(assignStatement);
			}

			CodeTypeOfExpression typeOfExp = new CodeTypeOfExpression(typeReference) ;
			CodeMethodInvokeExpression newObjectInit = null;
			if (tryMethod)
				newObjectInit = new CodeMethodInvokeExpression(contextVar, "GetObjectById", new CodeExpression[] { dummyObjectVar, typeOfExp } ) ;
			else
				newObjectInit = new CodeMethodInvokeExpression(contextVar, "TryGetObjectById", new CodeExpression[] { dummyObjectVar, typeOfExp } ) ;

			CodeCastExpression castExp = new CodeCastExpression(typeReference, newObjectInit) ;

			CodeVariableDeclarationStatement newObjectVarDecl = new CodeVariableDeclarationStatement(classMap.GetName(), MakeCamelCase(classMap.GetName()), castExp) ;
			CodeVariableReferenceExpression newObjectVar = new CodeVariableReferenceExpression(MakeCamelCase(classMap.GetName()));

			methodMember.Statements.Add(newObjectVarDecl);
 

			CodeMethodReturnStatement returnStmt = new CodeMethodReturnStatement(newObjectVar) ;

			methodMember.Statements.Add(returnStmt);

			classDecl.Members.Add(methodMember);					
		
		}

		public static void GenerateRepositoryGetAllObjectsMethod(IClassMap classMap, CodeTypeDeclaration classDecl)
		{
			CodeMemberMethod methodMember = new CodeMemberMethod() ;
			methodMember.Name = GetRepositoryGetAllObjectsMethodName(classMap);

			CodeTypeReference typeReference = new CodeTypeReference(classMap.GetName());
			CodeTypeReference listTypeReference = new CodeTypeReference(typeof(IList));
			methodMember.ReturnType = listTypeReference;
			
			methodMember.Attributes = MemberAttributes.Public;

			CodeThisReferenceExpression thisExp = new CodeThisReferenceExpression();
			CodeFieldReferenceExpression contextVar = new CodeFieldReferenceExpression(thisExp, "context");

			CodeTypeOfExpression typeOfExp = new CodeTypeOfExpression(typeReference) ;

			CodeMethodInvokeExpression listInit = new CodeMethodInvokeExpression(contextVar, "GetObjects", new CodeExpression[] { typeOfExp } ) ;

			CodeVariableDeclarationStatement listVarDecl = new CodeVariableDeclarationStatement(typeof(IList), "list", listInit) ;
			CodeVariableReferenceExpression listVar = new CodeVariableReferenceExpression("list");

			methodMember.Statements.Add(listVarDecl);
 
			CodeMethodReturnStatement returnStmt = new CodeMethodReturnStatement(listVar) ;

			methodMember.Statements.Add(returnStmt);

			classDecl.Members.Add(methodMember);					
		
		}

		public static void GenerateRepositoryUpdateObjectMethod(IClassMap classMap, CodeTypeDeclaration classDecl)
		{
			IList propertyMaps = classMap.GetAllPropertyMaps();
			IList idPropertyMaps = classMap.GetIdentityPropertyMaps();

			CodeMemberMethod methodMember = new CodeMemberMethod() ;
			methodMember.Name = GetRepositoryUpdateObjectMethodName(classMap);

			CodeTypeReference typeReference = new CodeTypeReference(classMap.GetName());
			methodMember.ReturnType = typeReference;
			
			methodMember.Attributes = MemberAttributes.Public;

			foreach(IPropertyMap propertyMap in propertyMaps)
			{
				if (!propertyMap.IsCollection)
				{
					CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(new CodeTypeReference(propertyMap.DataType), MakeCamelCase(propertyMap.Name));
					methodMember.Parameters.Add(parameter);					
				}
			}

			CodeThisReferenceExpression thisExp = new CodeThisReferenceExpression();
			CodeFieldReferenceExpression contextVar = new CodeFieldReferenceExpression(thisExp, "context");

			CodeExpression[] createParams = new CodeExpression[idPropertyMaps.Count];

			int i = 0;
			foreach(IPropertyMap idPropertyMap in idPropertyMaps)
			{
				CodeArgumentReferenceExpression argExp = new CodeArgumentReferenceExpression(MakeCamelCase(idPropertyMap.Name));
				createParams[i] = argExp;
				i++;
			}

			CodeMethodInvokeExpression newObjectInit = new CodeMethodInvokeExpression(thisExp, GetRepositoryGetByIdentityMethodName(classMap), createParams ) ;

			CodeVariableDeclarationStatement newObjectVarDecl = new CodeVariableDeclarationStatement(classMap.GetName(), MakeCamelCase(classMap.GetName()), newObjectInit) ;
			CodeVariableReferenceExpression newObjectVar = new CodeVariableReferenceExpression(MakeCamelCase(classMap.GetName()));

			methodMember.Statements.Add(newObjectVarDecl);
 
			foreach(IPropertyMap propertyMap in propertyMaps)
			{
				if (!propertyMap.IsIdentity)
				{
					if (!propertyMap.IsCollection)
					{
						CodeArgumentReferenceExpression argExp = new CodeArgumentReferenceExpression(MakeCamelCase(propertyMap.Name));
						CodeVariableReferenceExpression propExp = new CodeVariableReferenceExpression(MakeCamelCase(classMap.Name) + "." + propertyMap.Name);
						CodeAssignStatement assignStatement = new CodeAssignStatement(propExp, argExp);
						methodMember.Statements.Add(assignStatement);						
					}
				}
			}

			CodeMethodInvokeExpression commitCtx = new CodeMethodInvokeExpression(contextVar, "Commit", new CodeExpression[] {} ) ;

			methodMember.Statements.Add(commitCtx);

			CodeMethodReturnStatement returnStmt = new CodeMethodReturnStatement(newObjectVar) ;

			methodMember.Statements.Add(returnStmt);

			classDecl.Members.Add(methodMember);
		}

		public static void GenerateRepositoryDeleteObjectMethod(IClassMap classMap, CodeTypeDeclaration classDecl)
		{
			CodeMemberMethod methodMember = new CodeMemberMethod() ;
			methodMember.Name = GetRepositoryDeleteObjectMethodName(classMap);

			CodeTypeReference typeReference = new CodeTypeReference(classMap.GetName());
			
			methodMember.Attributes = MemberAttributes.Public;

			CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(typeReference, MakeCamelCase(classMap.GetName()));
			methodMember.Parameters.Add(parameter);

			CodeArgumentReferenceExpression argExp = new CodeArgumentReferenceExpression(MakeCamelCase(classMap.GetName()));

			CodeThisReferenceExpression thisExp = new CodeThisReferenceExpression();
			CodeFieldReferenceExpression contextVar = new CodeFieldReferenceExpression(thisExp, "context");

			CodeMethodInvokeExpression deleteCall = new CodeMethodInvokeExpression(contextVar, "DeleteObject", new CodeExpression[] { argExp } ) ;

			methodMember.Statements.Add(deleteCall);

			CodeMethodInvokeExpression commitCtx = new CodeMethodInvokeExpression(contextVar, "Commit", new CodeExpression[] {} ) ;

			methodMember.Statements.Add(commitCtx);
 
			classDecl.Members.Add(methodMember);							
		}

		#endregion

		#region Factory

		public static CodeCompileUnit GetFactoryClassCompileUnit(IClassMap classMap)
		{			
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();

			codeCompileUnit.Namespaces.Add(GenerateFactoryClass(classMap));

			return codeCompileUnit ;
		}

		public static CodeNamespace GenerateFactoryClass(IClassMap classMap)
		{
			CodeNamespace domainNamespace = new CodeNamespace(classMap.DomainMap.RootNamespace + ".Factories" ) ;
			CodeTypeDeclaration classDecl = new CodeTypeDeclaration(GetFactoryClassName(classMap)) ;

			CodeNamespaceImport import = new CodeNamespaceImport(classMap.GetFullNamespace()) ;
			CodeNamespaceImport importNPersist = new CodeNamespaceImport("Puzzle.NPersist.Framework") ;
			domainNamespace.Imports.Add(import);
			domainNamespace.Imports.Add(importNPersist);

			classDecl.IsClass = true;

			GenerateConstructor(classDecl);
			GenerateContextField(classDecl);
			GenerateFactoryMethods(classMap, classDecl);

			domainNamespace.Types.Add(classDecl);

			return domainNamespace;
			

		}


		public static void GenerateFactoryMethods(IClassMap classMap, CodeTypeDeclaration classDecl)
		{
			IList propertyMaps = GetRequiredPropertyMaps(classMap);
			classDecl.Members.Add(GenerateFactoryMethod(classMap, propertyMaps));					

			IList optionalPropertyMaps = GetOptionalPropertyMaps(classMap);
			if (optionalPropertyMaps.Count > 0)
			{
				foreach (IPropertyMap propertyMap in optionalPropertyMaps)
					propertyMaps.Add(propertyMap);

				classDecl.Members.Add(GenerateFactoryMethod(classMap, propertyMaps));					
			}
		}


		public static CodeMemberMethod GenerateFactoryMethod(IClassMap classMap, IList propertyMaps)
		{

			CodeMemberMethod methodMember = new CodeMemberMethod() ;
			methodMember.Name = GetFactoryMethodName(classMap);

			CodeTypeReference typeReference = new CodeTypeReference(classMap.GetName());
			methodMember.ReturnType = typeReference;
			
			methodMember.Attributes = MemberAttributes.Public;

			foreach(IPropertyMap propertyMap in propertyMaps)
			{
				CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(new CodeTypeReference(propertyMap.DataType), MakeCamelCase(propertyMap.Name));
				methodMember.Parameters.Add(parameter);
			}

			CodeThisReferenceExpression thisExp = new CodeThisReferenceExpression();
			CodeFieldReferenceExpression contextVar = new CodeFieldReferenceExpression(thisExp, "context");

			CodeTypeOfExpression typeOfExp = new CodeTypeOfExpression(typeReference) ;
			CodeMethodInvokeExpression newObjectInit = new CodeMethodInvokeExpression(contextVar, "CreateObject", new CodeExpression[] { typeOfExp } ) ;

			CodeCastExpression castExp = new CodeCastExpression(typeReference, newObjectInit) ;

			CodeVariableDeclarationStatement newObjectVarDecl = new CodeVariableDeclarationStatement(classMap.GetName(), MakeCamelCase(classMap.GetName()), castExp) ;
			CodeVariableReferenceExpression newObjectVar = new CodeVariableReferenceExpression(MakeCamelCase(classMap.GetName()));

			methodMember.Statements.Add(newObjectVarDecl);
 
			foreach(IPropertyMap propertyMap in propertyMaps)
			{
				CodeArgumentReferenceExpression argExp = new CodeArgumentReferenceExpression(MakeCamelCase(propertyMap.Name));
				CodeVariableReferenceExpression propExp = new CodeVariableReferenceExpression(MakeCamelCase(classMap.Name) + "." + propertyMap.Name);
				CodeAssignStatement assignStatement = new CodeAssignStatement(propExp, argExp);
				methodMember.Statements.Add(assignStatement);
			}

			CodeMethodInvokeExpression commitCtx = new CodeMethodInvokeExpression(contextVar, "Commit", new CodeExpression[] {} ) ;

			methodMember.Statements.Add(commitCtx);

			CodeMethodReturnStatement returnStmt = new CodeMethodReturnStatement(newObjectVar) ;

			methodMember.Statements.Add(returnStmt);

			return methodMember;

		}

		#endregion

		#region Template

		public static string GetRepositoryClassName(IClassMap classMap)
		{		
			string name = classMap.GetName() + "Repository" ;

			return name;
		}

		public static string GetRepositoryGetByIdentityMethodName(IClassMap classMap)
		{		
			string name = "GetByIdentity";

			return name;
		}

		public static string GetRepositoryTryGetByIdentityMethodName(IClassMap classMap)
		{		
			string name = "TryGetByIdentity";

			return name;
		}

		public static string GetRepositoryGetAllObjectsMethodName(IClassMap classMap)
		{		
			string name = "GetAll";

			return name;
		}

		public static string GetRepositoryUpdateObjectMethodName(IClassMap classMap)
		{		
			string name = "Update";

			return name;
		}


		public static string GetRepositoryDeleteObjectMethodName(IClassMap classMap)
		{		
			string name = "Delete";

			return name;
		}

		public static string GetFactoryClassName(IClassMap classMap)
		{		
			string name = classMap.GetName() + "Factory" ;

			return name;
		}

		public static string GetFactoryMethodName(IClassMap classMap)
		{		
			string name = "Create" + classMap.GetName() ;

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
