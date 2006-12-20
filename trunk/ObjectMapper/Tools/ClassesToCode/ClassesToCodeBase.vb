Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Imports Puzzle.NPersist.Framework.Mapping
Imports Puzzle.NPersist.Framework.Enumerations
Imports System.Text

Public Enum TargetPlatformEnum
    POCO = 0
    NPersist = 1
    NHibernate = 2
    '    ECO2 = 3
    'WilsonORMapper = 4
End Enum

Public Class ClassesToCodeBase
    Implements IClassesToCode

    Private m_UseAttributes As Boolean = True
    Private m_AttributeStart As String = "["
    Private m_AttributeEnd As String = "]"
    Private m_ImplementIInterceptable As Boolean = True
    Private m_ImplementIObjectHelper As Boolean = True
    Private m_ImplementIObservable As Boolean = True

    Private m_IncludeRegions As Boolean
    Private m_IncludeComments As Boolean = True
    Private m_IncludeDocComments As Boolean = True
    Private m_IncludeModelInfoInDocComments As Boolean = True

    Private m_NotifyOnlyWhenRequired As Boolean = True
    Private m_NotifyLightWeight As Boolean = True
    Private m_NotifyAfterGet As Boolean = True
    Private m_NotifyAfterSet As Boolean = True

    Private m_ImplementShadows As Boolean = False
    Private m_AddPropertyNotifyMethods As Boolean = False

    Private m_GeneratePOCO As Boolean = False

    Private m_TargetPlatform As TargetPlatformEnum = TargetPlatformEnum.NPersist
    Private m_ReallyNoRegions As Boolean = False

    Private m_UseTypedCollections As Boolean = False
    Private m_WrapCollections As Boolean = False
    Private m_XmlFilePerClass As Boolean = False
    Private m_EmbedXml As Boolean = False

    Private m_DocCommentPrefix As String

    Private m_UseGenericCollections As Boolean = True

    Public tab As String = "    "

    Public Function GetTabs(ByVal index As Integer) As String

        Dim i As Long
        Dim str As String

        For i = 0 To index

            str += tab

        Next

        Return str

    End Function

    Public Overridable Function DomainToCode(ByVal domainMap As Puzzle.NPersist.Framework.Mapping.IDomainMap, ByVal noRootNamespace As Boolean) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.DomainToCode

    End Function

    Public Overridable Function NamespaceToCode(ByVal domainMap As Puzzle.NPersist.Framework.Mapping.IDomainMap, ByVal name As String, ByVal noRootNamespace As Boolean) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.NamespaceToCode

    End Function

    Public Overridable Overloads Function ClassToCode(ByVal classMap As Puzzle.NPersist.Framework.Mapping.IClassMap) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.ClassToCode

        Return ClassToCode(classMap, False, False, "")

    End Function

    Public Overridable Overloads Function ClassToCode(ByVal classMap As Puzzle.NPersist.Framework.Mapping.IClassMap, ByVal noNamespace As Boolean, ByVal noRootNamespace As Boolean) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.ClassToCode

        Return ClassToCode(classMap, noNamespace, noRootNamespace, "")

    End Function

    Public Overridable Overloads Function ClassToCode(ByVal classMap As Puzzle.NPersist.Framework.Mapping.IClassMap, ByVal noNamespace As Boolean, ByVal noRootNamespace As Boolean, ByVal customCOde As String) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.ClassToCode

    End Function

    Public Overridable Function ClassToTypedCollection(ByVal classMap As IClassMap, ByVal noNamespace As Boolean, ByVal noRootNamespace As Boolean) As String Implements IClassesToCode.ClassToTypedCollection

    End Function

    Public Overridable Function PropertyToCode(ByVal propertyMap As Puzzle.NPersist.Framework.Mapping.IPropertyMap) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.PropertyToCode

        Dim code As String

        Return code

    End Function


    Public Overridable Function InheritedPropertyToCode(ByVal propertyMap As Puzzle.NPersist.Framework.Mapping.IPropertyMap) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.InheritedPropertyToCode

    End Function

    Public Overridable Overloads Function GetIndentation(ByVal IndentationLevel As Puzzle.ObjectMapper.Tools.IClassesToCode.IndentationLevelEnum) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.GetIndentation

        Return GetIndentation(IndentationLevel, 0)

    End Function

    Public Overridable Overloads Function GetIndentation(ByVal IndentationLevel As Puzzle.ObjectMapper.Tools.IClassesToCode.IndentationLevelEnum, ByVal additionalLevel As Integer) As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.GetIndentation

        Dim level As Long = CLng(IndentationLevel) + additionalLevel
        Dim i As Long
        Dim identBuilder As New StringBuilder

        For i = 0 To level - 1

            identBuilder.Append("    ")

        Next

        Return identBuilder.ToString

    End Function

    Public Overridable Property UseAttributes() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.UseAttributes
        Get
            Return m_UseAttributes
        End Get
        Set(ByVal Value As Boolean)
            m_UseAttributes = Value
        End Set
    End Property

    Public Overridable Property AttributeStart() As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.AttributeStart
        Get
            Return m_AttributeStart
        End Get
        Set(ByVal Value As String)
            m_AttributeStart = Value
        End Set
    End Property

    Public Overridable Property AttributeEnd() As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.AttributeEnd
        Get
            Return m_AttributeEnd
        End Get
        Set(ByVal Value As String)
            m_AttributeEnd = Value
        End Set
    End Property


    Public Overridable Property ImplementIInterceptable() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.ImplementIInterceptable
        Get
            If GeneratePOCO Then Return False
            Return m_ImplementIInterceptable
        End Get
        Set(ByVal Value As Boolean)
            m_ImplementIInterceptable = Value
        End Set
    End Property


    Public Overridable Property ImplementIObjectHelper() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.ImplementIObjectHelper
        Get
            If GeneratePOCO Then Return False
            Return m_ImplementIObjectHelper
        End Get
        Set(ByVal Value As Boolean)
            m_ImplementIObjectHelper = Value
        End Set
    End Property

    Public Overridable Property ImplementIObservable() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.ImplementIObservable
        Get
            If GeneratePOCO Then Return False
            Return m_ImplementIObservable
        End Get
        Set(ByVal Value As Boolean)
            m_ImplementIObservable = Value
        End Set
    End Property


    Public Overridable Property IncludeRegions() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.IncludeRegions
        Get
            If GeneratePOCO Then Return False
            Return m_IncludeRegions
        End Get
        Set(ByVal Value As Boolean)
            m_IncludeRegions = Value
        End Set
    End Property

    Public Overridable Property IncludeComments() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.IncludeComments
        Get
            Return m_IncludeComments
        End Get
        Set(ByVal Value As Boolean)
            m_IncludeComments = Value
        End Set
    End Property

    Public Overridable Property IncludeDocComments() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.IncludedocComments
        Get
            Return m_IncludeDocComments
        End Get
        Set(ByVal Value As Boolean)
            m_IncludeDocComments = Value
        End Set
    End Property

    Public Overridable Property IncludeModelInfoInDocComments() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.IncludeModelInfoInDocComments
        Get
            Return m_IncludeModelInfoInDocComments
        End Get
        Set(ByVal Value As Boolean)
            m_IncludeModelInfoInDocComments = Value
        End Set
    End Property

    Public Overridable Property DocCommentPrefix() As String Implements Puzzle.ObjectMapper.Tools.IClassesToCode.DocCommentPrefix
        Get
            Return m_DocCommentPrefix
        End Get
        Set(ByVal Value As String)
            m_DocCommentPrefix = Value
        End Set
    End Property


    Public Overridable Property NotifyOnlyWhenRequired() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.NotifyOnlyWhenRequired
        Get
            If GeneratePOCO Then Return False
            Return m_NotifyOnlyWhenRequired
        End Get
        Set(ByVal Value As Boolean)
            m_NotifyOnlyWhenRequired = Value
        End Set
    End Property

    Public Overridable Property NotifyLightWeight() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.NotifyLightWeight
        Get
            If GeneratePOCO Then Return False
            Return m_NotifyLightWeight
        End Get
        Set(ByVal Value As Boolean)
            m_NotifyLightWeight = Value
        End Set
    End Property

    Public Overridable Property NotifyAfterGet() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.NotifyAfterGet
        Get
            If GeneratePOCO Then Return False
            Return m_NotifyAfterGet
        End Get
        Set(ByVal Value As Boolean)
            m_NotifyAfterGet = Value
        End Set
    End Property

    Public Overridable Property NotifyAfterSet() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.NotifyAfterSet
        Get
            If GeneratePOCO Then Return False
            Return m_NotifyAfterSet
        End Get
        Set(ByVal Value As Boolean)
            m_NotifyAfterSet = Value
        End Set
    End Property

    Public Overridable Property ImplementShadows() As Boolean Implements Puzzle.ObjectMapper.Tools.IClassesToCode.ImplementShadows
        Get
            Return m_ImplementShadows
        End Get
        Set(ByVal Value As Boolean)
            m_ImplementShadows = Value
        End Set
    End Property


    Public Property AddPropertyNotifyMethods() As Boolean Implements IClassesToCode.AddPropertyNotifyMethods
        Get
            If GeneratePOCO Then Return False
            Return m_AddPropertyNotifyMethods
        End Get
        Set(ByVal Value As Boolean)
            m_AddPropertyNotifyMethods = Value
        End Set
    End Property

    Public Property GeneratePOCO() As Boolean Implements IClassesToCode.GeneratePOCO
        Get
            If TargetPlatform = TargetPlatformEnum.NHibernate Then Return True
            If TargetPlatform = TargetPlatformEnum.POCO Then Return True
            Return m_GeneratePOCO
        End Get
        Set(ByVal Value As Boolean)
            m_GeneratePOCO = Value
        End Set
    End Property

    Public Property TargetPlatform() As TargetPlatformEnum Implements IClassesToCode.TargetPlatform
        Get
            Return m_TargetPlatform
        End Get
        Set(ByVal Value As TargetPlatformEnum)
            m_TargetPlatform = Value
        End Set
    End Property

    Public Property ReallyNoRegions() As Boolean Implements IClassesToCode.ReallyNoRegions
        Get
            Return m_ReallyNoRegions
        End Get
        Set(ByVal Value As Boolean)
            m_ReallyNoRegions = Value
        End Set
    End Property

    Public Property UseTypedCollections() As Boolean Implements IClassesToCode.UseTypedCollections
        Get
            Return m_UseTypedCollections
        End Get
        Set(ByVal Value As Boolean)
            m_UseTypedCollections = Value
        End Set
    End Property

    Public Property WrapCollections() As Boolean Implements IClassesToCode.WrapCollections
        Get
            Return m_WrapCollections
        End Get
        Set(ByVal Value As Boolean)
            m_WrapCollections = Value
        End Set
    End Property

    Public Property XmlFilePerClass() As Boolean Implements IClassesToCode.XmlFilePerClass
        Get
            Return m_XmlFilePerClass
        End Get
        Set(ByVal Value As Boolean)
            m_XmlFilePerClass = Value
        End Set
    End Property

    Public Property EmbedXml() As Boolean Implements IClassesToCode.EmbedXml
        Get
            Return m_EmbedXml
        End Get
        Set(ByVal Value As Boolean)
            m_EmbedXml = Value
        End Set
    End Property

    Public Property UseGenericCollections() As Boolean Implements IClassesToCode.UseGenericCollections
        Get
            Return m_UseGenericCollections
        End Get
        Set(ByVal Value As Boolean)
            m_UseGenericCollections = Value
        End Set
    End Property

    Public Overridable Function GetFilePathRelativeToProject(ByVal projPath As String, ByVal filePath As String) As String Implements IClassesToCode.GetFilePathRelativeToProject

        Dim arrProj() As String = Split(projPath, "\")
        Dim arrFile() As String = Split(filePath, "\")
        Dim path As String

        Dim i As Integer
        Dim str As String
        Dim str2 As String

        If UBound(arrFile) > UBound(arrProj) Then

            For i = 0 To UBound(arrProj)

                If LCase(arrFile(i)) = LCase(arrProj(i)) Then

                Else

                    path = "..\" & path & arrFile(i) & "\"

                End If

            Next

            For i = UBound(arrProj) + 1 To UBound(arrFile)

                If i > UBound(arrProj) + 1 Then

                    path += "\"

                End If

                path += arrFile(i)

            Next

        Else

            For i = 0 To UBound(arrFile)

                If LCase(arrFile(i)) = LCase(arrProj(i)) Then

                Else

                    path = "..\" & path & arrFile(i)

                    If i < UBound(arrFile) Then

                        path += "\"

                    End If

                End If

            Next

            For i = UBound(arrFile) + 1 To UBound(arrProj)

                If i > UBound(arrFile) + 1 Then

                    path = "..\" & path

                End If

            Next

        End If

        Return path

    End Function

    Public Overridable Function DomainToProject(ByVal domainMap As IDomainMap, ByVal projPath As String, ByVal classMapsAndFiles As Hashtable, ByVal embeddedFiles As ArrayList) As String Implements IClassesToCode.DomainToProject

    End Function

    Public Overridable Function DomainToAssemblyInfo(ByVal domainMap As IDomainMap) As String Implements IClassesToCode.DomainToAssemblyInfo

    End Function

    Public Overloads Function GetInfoComments(ByVal obj As IMap) As String Implements IClassesToCode.GetInfoComments

        Return GetInfoComments(obj, "")

    End Function

    Public Overloads Function GetInfoComments(ByVal obj As IMap, ByVal indent As String) As String Implements IClassesToCode.GetInfoComments

        Return GetInfoComments(obj, indent, "")

    End Function

    Public Overloads Function GetInfoComments(ByVal obj As IMap, ByVal indent As String, ByVal commentPrefix As String) As String Implements IClassesToCode.GetInfoComments

        If Not CType(obj, Object).GetType.GetInterface(GetType(IDomainMap).ToString) Is Nothing Then

            Return ModelDescription(CType(obj, IDomainMap), indent, commentPrefix)

        ElseIf Not CType(obj, Object).GetType.GetInterface(GetType(IClassMap).ToString) Is Nothing Then

            Return ModelDescription(CType(obj, IClassMap), indent, commentPrefix)

        ElseIf Not CType(obj, Object).GetType.GetInterface(GetType(IPropertyMap).ToString) Is Nothing Then

            Return ModelDescription(CType(obj, IPropertyMap), indent, commentPrefix)

        ElseIf Not CType(obj, Object).GetType.GetInterface(GetType(ISourceMap).ToString) Is Nothing Then

            Return ModelDescription(CType(obj, ISourceMap), indent, commentPrefix)

        ElseIf Not CType(obj, Object).GetType.GetInterface(GetType(ITableMap).ToString) Is Nothing Then

            Return ModelDescription(CType(obj, ITableMap), indent, commentPrefix)

        ElseIf Not CType(obj, Object).GetType.GetInterface(GetType(IColumnMap).ToString) Is Nothing Then

            Return ModelDescription(CType(obj, IColumnMap), indent, commentPrefix)

        End If

    End Function








    Public Overloads Function ModelDescription(ByVal domainMap As IDomainMap, ByVal indent As String, ByVal commentPrefix As String) As String

        Dim strBuilder As New StringBuilder

        Dim classMap As IClassMap
        Dim sourceMap As ISourceMap

        If Len(domainMap.RootNamespace) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The root namespace for classes in this domain is '" & domainMap.RootNamespace & "'." & vbCrLf)

        End If

        If Len(domainMap.Source) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The default data source for this domain is '" & domainMap.Source & "'." & vbCrLf)

        End If

        If Len(domainMap.FieldPrefix) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The default field prefix for fields of classes in this domain is '" & domainMap.FieldPrefix & "'." & vbCrLf)

        End If

        If domainMap.IsReadOnly Then

            strBuilder.Append(indent & commentPrefix & "This domain is marked as Read-Only." & vbCrLf)

        End If

        If Not domainMap.MergeBehavior = MergeBehaviorType.DefaultBehavior Then

            strBuilder.Append(indent & commentPrefix & "The merging behavior for classes in this domain is '" & domainMap.MergeBehavior & "'." & vbCrLf)

        End If

        Return strBuilder.ToString

    End Function


    Public Overloads Function ModelDescription(ByVal classMap As IClassMap, ByVal indent As String, ByVal commentPrefix As String) As String

        Dim strBuilder As New StringBuilder
        Dim propertyMap As IPropertyMap
        Dim superClassMap As IClassMap = classMap.GetInheritedClassMap

        If Not superClassMap Is Nothing Then

            strBuilder.Append(indent & commentPrefix & "This class inherits from the class '" & superClassMap.Name & "'." & vbCrLf)

        End If

        If classMap.IsAbstract Then

            strBuilder.Append(indent & commentPrefix & "This class is abstract (objects can not be instantiated from it, only from conrete subclasses)." & vbCrLf)

        End If


        If classMap.IsReadOnly Then

            strBuilder.Append(indent & commentPrefix & "This class is marked as Read-Only." & vbCrLf)

        End If

        If Not classMap.MergeBehavior = MergeBehaviorType.DefaultBehavior Then

            strBuilder.Append(indent & commentPrefix & "The merging behavior for this class is '" & classMap.MergeBehavior & "'." & vbCrLf)

        End If

        If Len(classMap.Source) > 0 Or Len(classMap.Source) > 0 Or Len(classMap.Table) > 0 Or Len(classMap.TypeColumn) Or Len(classMap.TypeValue) > 0 Or Len(classMap.IdentitySeparator) > 0 Or classMap.InheritanceType <> InheritanceType.None Then

            strBuilder.Append(indent & commentPrefix & vbCrLf)
            strBuilder.Append(indent & commentPrefix & "Mapping information:" & vbCrLf)

        End If


        If Len(classMap.Source) > 0 Then

            strBuilder.Append(indent & commentPrefix & "This class maps to the '" & classMap.Source & "' data source." & vbCrLf)

        End If

        If Len(classMap.Table) > 0 Then

            strBuilder.Append(indent & commentPrefix & "This class maps to the '" & classMap.Table & "' table in the data source." & vbCrLf)

        End If

        If Not classMap.InheritanceType = InheritanceType.None Then

            strBuilder.Append(indent & commentPrefix & "This class uses the '" & classMap.InheritanceType.ToString & "' [Fowler] inheritance hierarchy implementation pattern in the data source." & vbCrLf)

        End If

        If Len(classMap.TypeColumn) > 0 Then

            strBuilder.Append(indent & commentPrefix & "This class uses the '" & classMap.TypeColumn & "' column as type discriminator column." & vbCrLf)

        End If


        If Len(classMap.TypeValue) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The type discriminator value for this class is '" & classMap.TypeValue & "'." & vbCrLf)

        End If

        If Len(classMap.IdentitySeparator) > 0 Then

            strBuilder.Append(indent & commentPrefix & "This class uses the string '" & classMap.IdentitySeparator & "' as separator between the values of its identity properties during identity generation." & vbCrLf)

        End If

        Return strBuilder.ToString

    End Function

    Public Overloads Function ModelDescription(ByVal propertyMap As IPropertyMap, ByVal indent As String, ByVal commentPrefix As String) As String

        Dim strBuilder As New StringBuilder
        Dim column As String
        Dim columns As String
        Dim hasMulti As Boolean
        Dim dataType As String


        If propertyMap.IsCollection Then

            dataType = propertyMap.ItemType

        Else

            dataType = propertyMap.DataType

        End If


        If propertyMap.IsIdentity Then

            strBuilder.Append(indent & commentPrefix & "This property is an identity property." & vbCrLf)

            If propertyMap.ClassMap.GetIdentityPropertyMaps.Count > 0 Then

                strBuilder.Append(indent & commentPrefix & "The identity index for this property is '" & propertyMap.IdentityIndex & "'." & vbCrLf)

            End If

        End If

        If propertyMap.IsCollection Then

            If propertyMap.ReferenceType = ReferenceType.None Then

                strBuilder.Append(indent & commentPrefix & "This property accepts multiple values of the type '" & propertyMap.ItemType & "'." & vbCrLf)

            Else

                strBuilder.Append(indent & commentPrefix & "This property accepts multiple references to objects of the type '" & propertyMap.ItemType & "'." & vbCrLf)
                strBuilder.Append(indent & commentPrefix & "This property is part of a '" & propertyMap.ReferenceType.ToString & "' relationship." & vbCrLf)

            End If

            strBuilder.Append(indent & commentPrefix & "The data type for this property is '" & propertyMap.DataType & "'." & vbCrLf)

        Else

            If propertyMap.ReferenceType = ReferenceType.None Then

                strBuilder.Append(indent & commentPrefix & "This property accepts values of the type '" & propertyMap.DataType & "'." & vbCrLf)

            Else

                strBuilder.Append(indent & commentPrefix & "This property accepts references to objects of the type '" & propertyMap.DataType & "'." & vbCrLf)
                strBuilder.Append(indent & commentPrefix & "This property is part of a '" & propertyMap.ReferenceType.ToString & "' relationship." & vbCrLf)

            End If

        End If

        If Len(propertyMap.Inverse) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The inverse property for this property is '" & dataType & "." & propertyMap.Inverse & "'." & vbCrLf)

            If propertyMap.InheritInverseMappings Then

                strBuilder.Append(indent & commentPrefix & "This property inherits its mapping information from its inverse property." & vbCrLf)

            End If

            If propertyMap.NoInverseManagement Then

                strBuilder.Append(indent & commentPrefix & "This property does not use the default framework inverse management." & vbCrLf)

            End If

        End If

        strBuilder.Append(indent & commentPrefix & "The accessibility level for this property is '" & propertyMap.Accessibility.ToString & "'." & vbCrLf)
        strBuilder.Append(indent & commentPrefix & "The accessibility level for the field '" & propertyMap.GetFieldName & "' that holds the value for this property is '" & propertyMap.FieldAccessibility.ToString & "'." & vbCrLf)

        If propertyMap.CascadingCreate Then

            strBuilder.Append(indent & commentPrefix & "This property uses 'Cascading Create': When a new object of the class this property belongs to is created, a new object of the class '" & dataType & "' will be created and a reference to the new object will be stored in this property." & vbCrLf)

        End If

        If propertyMap.CascadingDelete Then

            strBuilder.Append(indent & commentPrefix & "This property uses 'Cascading Delete': When the object this property belongs to is deleted, any object referenced by this property will be deleted as well." & vbCrLf)

        End If

        If Len(propertyMap.DefaultValue) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The default value for this property is '" & propertyMap.DefaultValue & "'." & vbCrLf)

        End If


        If propertyMap.IsReadOnly Then

            strBuilder.Append(indent & commentPrefix & "This property is marked as Read-Only." & vbCrLf)

        End If

        If propertyMap.IsSlave Then

            strBuilder.Append(indent & commentPrefix & "This property is marked as slave." & vbCrLf)

        End If


        If propertyMap.CascadingCreate Then

            strBuilder.Append(indent & commentPrefix & "This property uses 'Cascading Create': When a new object of the class this property belongs to is created, a new object of the class '" & dataType & "' will be created and a reference to the new object will be stored in this property." & vbCrLf)

        End If

        If propertyMap.CascadingDelete Then

            strBuilder.Append(indent & commentPrefix & "This property uses 'Cascading Delete': When the object this property belongs to is deleted, any object referenced by this property will be deleted as well." & vbCrLf)

        End If


        strBuilder.Append(indent & commentPrefix & vbCrLf)
        strBuilder.Append(indent & commentPrefix & "Mapping information:" & vbCrLf)

        If Len(propertyMap.Source) > 0 Then

            strBuilder.Append(indent & commentPrefix & "This class maps to the '" & propertyMap.Source & "' data source." & vbCrLf)

        End If

        If Len(propertyMap.Table) > 0 Then

            strBuilder.Append(indent & commentPrefix & "This class maps to the '" & propertyMap.Table & "' table in the data source." & vbCrLf)

        End If


        columns = propertyMap.Column

        For Each column In propertyMap.AdditionalColumns

            If Len(columns) > 0 Then
                columns += ", "
                hasMulti = True
            End If

            columns += column

        Next

        If Len(columns) > 0 Then

            If hasMulti Then

                strBuilder.Append(indent & commentPrefix & "The property maps to the following columns in the data source: '" & columns & "'." & vbCrLf)

            Else

                strBuilder.Append(indent & commentPrefix & "The property maps to the column '" & columns & "' in the data source." & vbCrLf)

            End If

        End If

        columns = propertyMap.IdColumn

        hasMulti = False

        For Each column In propertyMap.AdditionalIdColumns

            If Len(columns) > 0 Then
                columns += ", "
                hasMulti = True
            End If

            columns += column

        Next

        If Len(columns) > 0 Then

            If hasMulti Then

                strBuilder.Append(indent & commentPrefix & "The property maps to the following identity columns in the data source: '" & columns & "'." & vbCrLf)

            Else

                strBuilder.Append(indent & commentPrefix & "The property maps to the identity column '" & columns & "' in the data source." & vbCrLf)

            End If

        End If


        If propertyMap.LazyLoad Then

            strBuilder.Append(indent & commentPrefix & "This property is marked as Lazy-Loading." & vbCrLf)

        End If

        If Len(propertyMap.NullSubstitute) > 0 Then

            strBuilder.Append(indent & commentPrefix & "This property returns the following value instead of Null values: '" & propertyMap.NullSubstitute & "'." & vbCrLf)

        End If


        Return strBuilder.ToString

    End Function

    Public Overloads Function ModelDescription(ByVal sourceMap As ISourceMap, ByVal indent As String, ByVal commentPrefix As String) As String

        Dim strBuilder As New StringBuilder
        Dim tableMap As ITableMap

        strBuilder.Append(indent & commentPrefix & "The data source is of the type '" & sourceMap.SourceType.ToString & "'." & vbCrLf)

        If Len(sourceMap.ConnectionString) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The connection string to this data source is '" & sourceMap.ConnectionString & "'." & vbCrLf)

        End If


        If Len(sourceMap.Catalog) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The catalog for this data source is '" & sourceMap.Catalog & "'." & vbCrLf)

        End If

        If Len(sourceMap.Schema) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The schema for this data source is '" & sourceMap.Schema & "'." & vbCrLf)

        End If

        strBuilder.Append(indent & commentPrefix & "The provider type to be used for this data source is '" & sourceMap.ProviderType.ToString & "'." & vbCrLf)


        Return strBuilder.ToString

    End Function



    Public Overloads Function ModelDescription(ByVal tableMap As ITableMap, ByVal indent As String, ByVal commentPrefix As String) As String

        Dim strBuilder As New StringBuilder
        Dim columnMap As IColumnMap


        Return strBuilder.ToString

    End Function

    Public Overloads Function ModelDescription(ByVal columnMap As IColumnMap, ByVal indent As String, ByVal commentPrefix As String) As String

        Dim strBuilder As New StringBuilder

        If columnMap.IsPrimaryKey Then

            strBuilder.Append(indent & commentPrefix & "This is a primary key column." & vbCrLf)

        End If

        strBuilder.Append(indent & commentPrefix & "The data type for this column is '" & columnMap.DataType.ToString & "'." & vbCrLf)

        If columnMap.AllowNulls Then

            strBuilder.Append(indent & commentPrefix & "This column accepts NULL values." & vbCrLf)

        Else

            strBuilder.Append(indent & commentPrefix & "This column does not accept NULL values." & vbCrLf)

        End If

        If Len(columnMap.DefaultValue) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The default value for this column is '" & columnMap.DefaultValue & "'." & vbCrLf)

        End If


        If Len(columnMap.Format) > 0 Then

            strBuilder.Append(indent & commentPrefix & "The format that will be used for formatting the dates in this column is '" & columnMap.Format & "'." & vbCrLf)

        End If


        If columnMap.IsAutoIncrease Then

            If Len(columnMap.Sequence) > 0 Then

                strBuilder.Append(indent & commentPrefix & "This column is auto increasing column. (Sequence: '" & columnMap.Sequence & "'." & vbCrLf)

            Else

                strBuilder.Append(indent & commentPrefix & "This column is auto increasing column. (Seed value: '" & columnMap.Seed & "', Increment: '" & columnMap.Increment & "'." & vbCrLf)

            End If

        End If


        strBuilder.Append(indent & commentPrefix & "This length for this column is '" & columnMap.Length & "'." & vbCrLf)
        strBuilder.Append(indent & commentPrefix & "This precision for this column is '" & columnMap.Precision & "'." & vbCrLf)
        strBuilder.Append(indent & commentPrefix & "This scale for this column is '" & columnMap.Scale & "'." & vbCrLf)


        If columnMap.IsForeignKey Then

            If Len(columnMap.PrimaryKeyTable) > 0 Then

                strBuilder.Append(indent & commentPrefix & "This primary table for this foreign key column is '" & columnMap.PrimaryKeyTable & "'." & vbCrLf)

            End If

            If Len(columnMap.PrimaryKeyTable) > 0 Then

                strBuilder.Append(indent & commentPrefix & "This primary column for this foreign key column is '" & columnMap.PrimaryKeyColumn & "'." & vbCrLf)

            End If

        End If


        Return strBuilder.ToString

    End Function

    Public Overridable Overloads Function GetDocComment(ByVal classMap As IClassMap, ByVal prefix As String) As String Implements IClassesToCode.GetDocComment

        If Not IncludeDocComments Then Return ""

        Dim strBuilder As New StringBuilder
        Dim indent As String = GetIndentation(IClassesToCode.IndentationLevelEnum.ClassIndent)

        Dim indentPrefix As String = indent & prefix

        strBuilder.Append(indentPrefix & "--------------------------------------------------------------------------------" & vbCrLf)
        strBuilder.Append(indentPrefix & "<summary>" & vbCrLf)
        strBuilder.Append(indentPrefix & "Persistent domain entity class representing '" & classMap.Name & "' entities." & vbCrLf)
        strBuilder.Append(indentPrefix & "</summary>" & vbCrLf)
        strBuilder.Append(indentPrefix & "<remarks>" & vbCrLf)
        If IncludeModelInfoInDocComments Then
            strBuilder.Append(ModelDescription(classMap, indent, prefix))
        End If
        strBuilder.Append(indentPrefix & "</remarks>" & vbCrLf)
        strBuilder.Append(indentPrefix & "--------------------------------------------------------------------------------" & vbCrLf)


        Return strBuilder.ToString

    End Function

    Public Overridable Overloads Function GetDocComment(ByVal propertyMap As IPropertyMap, ByVal prefix As String) As String Implements IClassesToCode.GetDocComment

        If Not IncludeDocComments Then Return ""

        Dim strBuilder As New StringBuilder
        Dim indent As String = GetIndentation(IClassesToCode.IndentationLevelEnum.MemberIndent)

        Dim identity As String = ""

        If propertyMap.IsIdentity Then

            identity = "identity "

        End If

        Dim indentPrefix As String = indent & prefix

        strBuilder.Append(indentPrefix & "--------------------------------------------------------------------------------" & vbCrLf)
        strBuilder.Append(indentPrefix & "<summary>" & vbCrLf)
        If propertyMap.ReferenceType = ReferenceType.None Then
            If propertyMap.IsCollection Then
                strBuilder.Append(indentPrefix & "Persistent collection property. Takes values of type <c>" & propertyMap.ItemType & "</c>" & vbCrLf)
            Else
                strBuilder.Append(indentPrefix & "Persistent primitive " & identity & "property." & vbCrLf)
            End If
        Else
            Select Case propertyMap.ReferenceType

                Case ReferenceType.ManyToMany

                    strBuilder.Append(indentPrefix & "Persistent many-many reference property." & vbCrLf)

                Case ReferenceType.ManyToOne

                    strBuilder.Append(indentPrefix & "Persistent many-one reference property." & vbCrLf)

                Case ReferenceType.OneToMany

                    strBuilder.Append(indentPrefix & "Persistent one-many reference " & identity & "property." & vbCrLf)

                Case ReferenceType.OneToOne

                    strBuilder.Append(indentPrefix & "Persistent one-one reference " & identity & "property." & vbCrLf)

            End Select
        End If
        strBuilder.Append(indentPrefix & "</summary>" & vbCrLf)
        strBuilder.Append(indentPrefix & "<remarks>" & vbCrLf)
        If IncludeModelInfoInDocComments Then
            strBuilder.Append(ModelDescription(propertyMap, indent, prefix))
        End If
        strBuilder.Append(indentPrefix & "</remarks>" & vbCrLf)
        strBuilder.Append(indentPrefix & "--------------------------------------------------------------------------------" & vbCrLf)

        Return strBuilder.ToString

    End Function

    Public Overridable Overloads Function GetDocComment(ByVal prefix As String, ByVal summary As String, ByVal remarks As String, ByVal params As ArrayList, ByVal returns As String) As String Implements IClassesToCode.GetDocComment

        If Not IncludeDocComments Then Return ""

        Dim strBuilder As New StringBuilder
        Dim indent As String = GetIndentation(IClassesToCode.IndentationLevelEnum.MemberIndent)

        Dim indentPrefix As String = indent & prefix

        Dim param As String
        Dim desc As String
        Dim i As Long

        strBuilder.Append(indentPrefix & "--------------------------------------------------------------------------------" & vbCrLf)
        strBuilder.Append(indentPrefix & "<summary>" & vbCrLf)
        strBuilder.Append(indentPrefix & summary & vbCrLf)
        strBuilder.Append(indentPrefix & "</summary>" & vbCrLf)
        If Len(remarks) > 0 Then
            strBuilder.Append(indentPrefix & "<remarks>" & vbCrLf)
            strBuilder.Append(indentPrefix & remarks & vbCrLf)
            strBuilder.Append(indentPrefix & "</remarks>" & vbCrLf)
        End If
        For i = 0 To params.Count - 1 Step 2

            param = params(i)
            desc = params(i + 1)

            strBuilder.Append(indentPrefix & "<param name=""" & param & """>" & desc & "</param>" & vbCrLf)

        Next
        If Len(returns) > 0 Then
            strBuilder.Append(indentPrefix & "<returns>" & returns & "</returns>" & vbCrLf)
        End If
        strBuilder.Append(indentPrefix & "--------------------------------------------------------------------------------" & vbCrLf)

        Return strBuilder.ToString

    End Function

    Public Function HasReferencesByIdentityProperties(ByVal classMap As IClassMap) As Boolean Implements IClassesToCode.HasReferencesByIdentityProperties

        Dim propertyMap As IPropertyMap

        Dim checkClassMap As IClassMap
        Dim refClassMap As IClassMap

        For Each checkClassMap In classMap.DomainMap.ClassMaps

            For Each propertyMap In checkClassMap.PropertyMaps

                If propertyMap.IsIdentity Then

                    If Not propertyMap.ReferenceType = ReferenceType.None Then

                        refClassMap = propertyMap.GetReferencedClassMap

                        If Not refClassMap Is Nothing Then

                            If refClassMap Is classMap Then

                                Return True

                            End If

                        End If

                    End If

                End If

            Next

        Next

        Return False

    End Function

    Protected Overridable Function IsStringProperty(ByVal propertyMap As IPropertyMap) As Boolean

        Dim columnMap As IColumnMap
        Dim isString As Boolean

        Try

            columnMap = propertyMap.GetColumnMap

        Catch ex As Exception

            columnMap = Nothing

        End Try

        If Not columnMap Is Nothing Then

            Select Case columnMap.DataType

                Case DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.String, DbType.StringFixedLength

                    isString = True

            End Select

        End If

        Return isString

    End Function


    Protected Overridable Function GetListType(ByVal refClassMap As ClassMap, ByVal defaultName As String) As String

        If m_UseTypedCollections Then

            If Not refClassMap Is Nothing Then

                Return refClassMap.Name & "Collection"

            End If

        End If

        Return defaultName

    End Function

    Protected Overridable Function GetGenericListType(ByVal refClassMap As ClassMap, ByVal defaultName As String) As String

        If m_UseGenericCollections Then

            If Not refClassMap Is Nothing Then

                Return "System.Collections.Generic.IList<" & refClassMap.Name & ">"

            End If

        End If

        Return defaultName

    End Function

    Protected Overridable Function GetClassMapAttribute(ByVal classMap As IClassMap) As String

        Dim codeBuilder As StringBuilder = New StringBuilder

        If UseAttributes Then

            codeBuilder.Append(GetIndentation(IClassesToCode.IndentationLevelEnum.ClassIndent))
            codeBuilder.Append(AttributeStart + "ClassMap(")

            If classMap.Source.Length > 0 Then
                codeBuilder.Append("Source = """ + classMap.Source + """, ")
            End If

            If classMap.Table.Length > 0 Then
                codeBuilder.Append("Table = """ + classMap.Table + """, ")
            End If

            If classMap.TypeColumn.Length > 0 Then
                codeBuilder.Append("TypeColumn = """ + classMap.TypeColumn + """, ")
            End If

            If classMap.TypeValue.Length > 0 Then
                codeBuilder.Append("TypeValue = """ + classMap.TypeValue + """, ")
            End If


            If classMap.SourceClass.Length > 0 Then
                codeBuilder.Append("SourceClass = """ + classMap.SourceClass + """, ")
            End If


            If classMap.DocSource.Length > 0 Then
                codeBuilder.Append("DocSource = """ + classMap.DocSource + """, ")
            End If

            If classMap.DocElement.Length > 0 Then
                codeBuilder.Append("DocElement = """ + classMap.DocElement + """, ")
            End If

            If classMap.DocRoot.Length > 0 Then
                codeBuilder.Append("DocRoot = """ + classMap.DocRoot + """, ")
            End If

            If classMap.DocParentProperty.Length > 0 Then
                codeBuilder.Append("DocParentProperty = """ + classMap.DocParentProperty + """, ")
            End If

            If Not classMap.DocClassMapMode = DocClassMapMode.Default Then
                codeBuilder.Append("DocClassMapMode = DocClassMapMode." + classMap.DocClassMapMode.ToString() + ", ")
            End If


            If Not classMap.InheritanceType = InheritanceType.None Then
                codeBuilder.Append("InheritanceType = InheritanceType." + classMap.InheritanceType.ToString() + ", ")
            End If

            If classMap.IsReadOnly = True Then
                codeBuilder.Append("IsReadOnly = true, ")
            End If

            If classMap.IdentitySeparator.Length > 0 Then
                codeBuilder.Append("IdentitySeparator = """ + classMap.IdentitySeparator + """, ")
            End If

            If classMap.KeySeparator.Length > 0 Then
                codeBuilder.Append("KeySeparator = """ + classMap.KeySeparator + """, ")
            End If

            If classMap.CommitRegions.Length > 0 Then
                codeBuilder.Append("CommitRegions = """ + classMap.CommitRegions + """, ")
            End If

            If classMap.LoadSpan.Length > 0 Then
                codeBuilder.Append("LoadSpan = """ + classMap.LoadSpan + """, ")
            End If

            If classMap.ValidateMethod.Length > 0 Then
                codeBuilder.Append("ValidateMethod = """ + classMap.ValidateMethod + """, ")
            End If

            If Not classMap.ValidationMode = ValidationMode.Default Then
                codeBuilder.Append("ValidationMode = ValidationMode." + classMap.ValidationMode.ToString() + ", ")
            End If

            If Not classMap.UpdateOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType.DefaultBehavior Then
                codeBuilder.Append("UpdateOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType." + classMap.UpdateOptimisticConcurrencyBehavior.ToString() + ", ")
            End If

            If Not classMap.DeleteOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType.DefaultBehavior Then
                codeBuilder.Append("DeleteOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType." + classMap.DeleteOptimisticConcurrencyBehavior.ToString() + ", ")
            End If

            If Not classMap.MergeBehavior = MergeBehaviorType.DefaultBehavior Then
                codeBuilder.Append("MergeBehavior = MergeBehaviorType." + classMap.MergeBehavior.ToString() + ", ")
            End If

            If Not classMap.RefreshBehavior = RefreshBehaviorType.DefaultBehavior Then
                codeBuilder.Append("RefreshBehavior = RefreshBehaviorType." + classMap.RefreshBehavior.ToString() + ", ")
            End If

            If Not classMap.LoadBehavior = LoadBehavior.Default Then
                codeBuilder.Append("LoadBehavior = LoadBehavior." + classMap.LoadBehavior.ToString() + ", ")
            End If

            If Not classMap.TimeToLiveBehavior = TimeToLiveBehavior.Default Then
                codeBuilder.Append("TimeToLiveBehavior = TimeToLiveBehavior." + classMap.TimeToLiveBehavior.ToString() + ", ")
            End If

            If classMap.TimeToLive > -1 Then
                codeBuilder.Append("TimeToLive = " + classMap.TimeToLive.ToString() + ", ")
            End If


            Dim result As String = codeBuilder.ToString()
            If Right(result, 2) = ", " Then
                codeBuilder.Length = codeBuilder.Length - 2
            End If

            codeBuilder.Append(")" + AttributeEnd)
            codeBuilder.Append(vbCrLf)

        End If

        Return codeBuilder.ToString()

    End Function


    Protected Overridable Function GetPropertyMapAttribute(ByVal propertyMap As IPropertyMap) As String

        Dim codeBuilder As StringBuilder = New StringBuilder

        If UseAttributes Then

            codeBuilder.Append(GetIndentation(IClassesToCode.IndentationLevelEnum.MemberIndent))
            codeBuilder.Append(AttributeStart + "PropertyMap(")

            If propertyMap.Source.Length > 0 Then
                codeBuilder.Append("Source = """ + propertyMap.Source + """, ")
            End If

            If propertyMap.Table.Length > 0 Then
                codeBuilder.Append("Table = """ + propertyMap.Table + """, ")
            End If

            Dim columns As String = ""
            For Each column As IColumnMap In propertyMap.GetAllColumnMaps
                columns = columns + column.Name + ", "
            Next

            If Right(columns, 2) = ", " Then
                columns = Left(columns, Len(columns) - 2)
            End If

            If columns.Length > 0 Then
                codeBuilder.Append("Columns = """ + columns + """, ")
            End If

            columns = ""
            For Each column As IColumnMap In propertyMap.GetAllIdColumnMaps
                columns = columns + column.Name + ", "
            Next

            If Right(columns, 2) = ", " Then
                columns = Left(columns, Len(columns) - 2)
            End If

            If columns.Length > 0 Then
                codeBuilder.Append("IdColumns = """ + columns + """, ")
            End If


            If propertyMap.SourceProperty.Length > 0 Then
                codeBuilder.Append("SourceProperty = """ + propertyMap.SourceProperty + """, ")
            End If


            If propertyMap.DocSource.Length > 0 Then
                codeBuilder.Append("DocSource = """ + propertyMap.DocSource + """, ")
            End If

            If propertyMap.DocAttribute.Length > 0 Then
                codeBuilder.Append("DocAttribute = """ + propertyMap.DocAttribute + """, ")
            End If

            If propertyMap.DocElement.Length > 0 Then
                codeBuilder.Append("DocElement = """ + propertyMap.DocElement + """, ")
            End If

            If Not propertyMap.DocPropertyMapMode = DocPropertyMapMode.Default Then
                codeBuilder.Append("DocPropertyMapMode = DocPropertyMapMode." + propertyMap.DocPropertyMapMode.ToString() + ", ")
            End If


            If propertyMap.IsIdentity Then
                codeBuilder.Append("IsIdentity = true, ")
            End If

            If propertyMap.IdentityIndex > 0 Then
                codeBuilder.Append("IdentityIndex = " + propertyMap.IdentityIndex.ToString() + ", ")
            End If

            If propertyMap.IdentityGenerator.Length > 0 Then
                codeBuilder.Append("IdentityGenerator = """ + propertyMap.IdentityGenerator + """, ")
            End If

            If propertyMap.IsKey Then
                codeBuilder.Append("IsKey = true, ")
            End If

            If propertyMap.KeyIndex > 0 Then
                codeBuilder.Append("KeyIndex = " + propertyMap.KeyIndex.ToString() + ", ")
            End If

            If propertyMap.GetIsAssignedBySource Then
                codeBuilder.Append("IsAssignedBySource = true, ")
            End If

            If propertyMap.GetIsNullable Then
                codeBuilder.Append("IsNullable = true, ")
            End If

            If propertyMap.NullSubstitute.Length > 0 Then
                codeBuilder.Append("NullSubstitute = """ + propertyMap.NullSubstitute + """, ")
            End If

            If propertyMap.ItemType.Length > 0 Then
                codeBuilder.Append("ItemType = """ + propertyMap.ItemType + """, ")
            End If

            If propertyMap.FieldName.Length > 0 Then
                codeBuilder.Append("FieldName = """ + propertyMap.FieldName + """, ")
            End If

            If propertyMap.IsCollection Then
                codeBuilder.Append("IsCollection = true, ")
            End If

            If propertyMap.Inverse.Length > 0 Then
                codeBuilder.Append("Inverse = """ + propertyMap.Inverse + """, ")
            End If

            If propertyMap.InheritInverseMappings Then
                codeBuilder.Append("InheritInverseMappings = true, ")
            End If

            If propertyMap.NoInverseManagement Then
                codeBuilder.Append("NoInverseManagement = true, ")
            End If

            If propertyMap.IsReadOnly Then
                codeBuilder.Append("IsReadOnly = true, ")
            End If

            If propertyMap.IsSlave Then
                codeBuilder.Append("IsSlave = true, ")
            End If

            If propertyMap.LazyLoad Then
                codeBuilder.Append("LazyLoad = true, ")
            End If

            If Not propertyMap.ReferenceType = ReferenceType.None Then
                codeBuilder.Append("ReferenceType = ReferenceType." + propertyMap.ReferenceType.ToString() + ", ")
            End If

            If Not propertyMap.ReferenceQualifier = ReferenceQualifier.Default Then
                codeBuilder.Append("ReferenceQualifier = ReferenceQualifier." + propertyMap.ReferenceQualifier.ToString() + ", ")
            End If

            If propertyMap.CascadingCreate Then
                codeBuilder.Append("CascadingCreate = true, ")
            End If

            If propertyMap.CascadingDelete Then
                codeBuilder.Append("CascadingDelete = true, ")
            End If

            If propertyMap.OrderBy.Length > 0 Then
                codeBuilder.Append("OrderBy = """ + propertyMap.OrderBy + """, ")
            End If

            If propertyMap.CommitRegions.Length > 0 Then
                codeBuilder.Append("CommitRegions = """ + propertyMap.CommitRegions + """, ")
            End If

            If propertyMap.ValidateMethod.Length > 0 Then
                codeBuilder.Append("ValidateMethod = """ + propertyMap.ValidateMethod + """, ")
            End If

            If Not propertyMap.ValidationMode = ValidationMode.Default Then
                codeBuilder.Append("ValidationMode = ValidationMode." + propertyMap.ValidationMode.ToString() + ", ")
            End If

            If Not propertyMap.UpdateOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType.DefaultBehavior Then
                codeBuilder.Append("UpdateOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType." + propertyMap.UpdateOptimisticConcurrencyBehavior.ToString() + ", ")
            End If

            If Not propertyMap.DeleteOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType.DefaultBehavior Then
                codeBuilder.Append("DeleteOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType." + propertyMap.DeleteOptimisticConcurrencyBehavior.ToString() + ", ")
            End If

            If Not propertyMap.MergeBehavior = MergeBehaviorType.DefaultBehavior Then
                codeBuilder.Append("MergeBehavior = MergeBehaviorType." + propertyMap.MergeBehavior.ToString() + ", ")
            End If

            If Not propertyMap.RefreshBehavior = RefreshBehaviorType.DefaultBehavior Then
                codeBuilder.Append("RefreshBehavior = RefreshBehaviorType." + propertyMap.RefreshBehavior.ToString() + ", ")
            End If

            If Not propertyMap.OnCreateBehavior = PropertySpecialBehaviorType.None Then
                codeBuilder.Append("OnCreateBehavior = PropertySpecialBehaviorType." + propertyMap.OnCreateBehavior.ToString() + ", ")
            End If

            If Not propertyMap.OnPersistBehavior = PropertySpecialBehaviorType.None Then
                codeBuilder.Append("OnPersistBehavior = PropertySpecialBehaviorType." + propertyMap.OnPersistBehavior.ToString() + ", ")
            End If

            If propertyMap.GetMaxLength > -1 Then
                codeBuilder.Append("MaxLength = " + propertyMap.GetMaxLength().ToString() + ", ")
            End If

            If propertyMap.MinLength > -1 Then
                codeBuilder.Append("MinLength = " + propertyMap.MinLength.ToString() + ", ")
            End If

            If propertyMap.MaxValue.Length > 0 Then
                codeBuilder.Append("MaxValue = """ + propertyMap.MaxValue + """, ")
            End If

            If propertyMap.MinValue.Length > 0 Then
                codeBuilder.Append("MinValue = """ + propertyMap.MinValue + """, ")
            End If

            If Not propertyMap.TimeToLiveBehavior = TimeToLiveBehavior.Default Then
                codeBuilder.Append("TimeToLiveBehavior = TimeToLiveBehavior." + propertyMap.TimeToLiveBehavior.ToString() + ", ")
            End If

            If propertyMap.TimeToLive > -1 Then
                codeBuilder.Append("TimeToLive = " + propertyMap.TimeToLive.ToString() + ", ")
            End If



            Dim result As String = codeBuilder.ToString()
            If Right(result, 2) = ", " Then
                codeBuilder.Length = codeBuilder.Length - 2
            End If

            codeBuilder.Append(")" + AttributeEnd)
            codeBuilder.Append(vbCrLf)

        End If

        Return codeBuilder.ToString()

    End Function


    Protected Overridable Function GetDomainMapAttribute(ByVal domainMap As IDomainMap) As String

        Dim codeBuilder As StringBuilder = New StringBuilder

        If UseAttributes Then

            codeBuilder.Append(AttributeStart + "assembly: DomainMap(")

            If domainMap.Source.Length > 0 Then
                codeBuilder.Append("Source = """ + domainMap.Source + """, ")
            End If

            If domainMap.DocSource.Length > 0 Then
                codeBuilder.Append("DocSource = """ + domainMap.DocSource + """, ")
            End If

            If domainMap.IsReadOnly Then
                codeBuilder.Append("IsReadOnly = true, ")
            End If

            If domainMap.RootNamespace.Length > 0 Then
                codeBuilder.Append("RootNamespace = """ + domainMap.RootNamespace + """, ")
            End If

            If domainMap.FieldPrefix.Length > 0 And domainMap.FieldPrefix <> "_m" Then
                codeBuilder.Append("FieldPrefix = """ + domainMap.FieldPrefix + """, ")
            End If

            If Not domainMap.FieldNameStrategy = FieldNameStrategyType.None Then
                codeBuilder.Append("FieldNameStrategy = FieldNameStrategyType." + domainMap.FieldNameStrategy.ToString() + ", ")
            End If

            If Not domainMap.ValidationMode = ValidationMode.Default Then
                codeBuilder.Append("ValidationMode = ValidationMode." + domainMap.ValidationMode.ToString() + ", ")
            End If

            If Not domainMap.UpdateOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType.DefaultBehavior Then
                codeBuilder.Append("UpdateOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType." + domainMap.UpdateOptimisticConcurrencyBehavior.ToString() + ", ")
            End If

            If Not domainMap.DeleteOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType.DefaultBehavior Then
                codeBuilder.Append("DeleteOptimisticConcurrencyBehavior = OptimisticConcurrencyBehaviorType." + domainMap.DeleteOptimisticConcurrencyBehavior.ToString() + ", ")
            End If

            If Not domainMap.MergeBehavior = MergeBehaviorType.DefaultBehavior Then
                codeBuilder.Append("MergeBehavior = MergeBehaviorType." + domainMap.MergeBehavior.ToString() + ", ")
            End If

            If Not domainMap.RefreshBehavior = RefreshBehaviorType.DefaultBehavior Then
                codeBuilder.Append("RefreshBehavior = RefreshBehaviorType." + domainMap.RefreshBehavior.ToString() + ", ")
            End If

            If Not domainMap.LoadBehavior = LoadBehavior.Default Then
                codeBuilder.Append("LoadBehavior = LoadBehavior." + domainMap.LoadBehavior.ToString() + ", ")
            End If

            If Not domainMap.TimeToLiveBehavior = TimeToLiveBehavior.Default Then
                codeBuilder.Append("TimeToLiveBehavior = TimeToLiveBehavior." + domainMap.TimeToLiveBehavior.ToString() + ", ")
            End If

            If domainMap.TimeToLive > -1 Then
                codeBuilder.Append("TimeToLive = " + domainMap.TimeToLive.ToString() + ", ")
            End If

            Dim result As String = codeBuilder.ToString()
            If Right(result, 2) = ", " Then
                codeBuilder.Length = codeBuilder.Length - 2
            End If

            codeBuilder.Append(")" + AttributeEnd)
            codeBuilder.Append(vbCrLf)

        End If

        Return codeBuilder.ToString()

    End Function

    Protected Overridable Function GetSourceMapAttribute(ByVal sourceMap As ISourceMap) As String

        Dim codeBuilder As StringBuilder = New StringBuilder

        If UseAttributes Then

            codeBuilder.Append(AttributeStart + "assembly: SourceMap(")

            If sourceMap.Name.Length > 0 Then
                codeBuilder.Append("Name = """ + sourceMap.Name + """, ")
            End If

            If Not sourceMap.PersistenceType = PersistenceType.Default Then
                codeBuilder.Append("PersistenceType = PersistenceType." + sourceMap.PersistenceType.ToString() + ", ")
            End If

            If sourceMap.Compute Then
                codeBuilder.Append("Compute = true, ")
            End If


            If sourceMap.DocPath.Length > 0 Then
                codeBuilder.Append("DocPath = """ + sourceMap.DocPath + """, ")
            End If

            If sourceMap.DocRoot.Length > 0 Then
                codeBuilder.Append("DocRoot = """ + sourceMap.DocRoot + """, ")
            End If

            If sourceMap.DocEncoding.Length > 0 Then
                codeBuilder.Append("DocEncoding = """ + sourceMap.DocEncoding + """, ")
            End If


            If sourceMap.Url.Length > 0 Then
                codeBuilder.Append("Url = """ + sourceMap.Url + """, ")
            End If

            If sourceMap.DomainKey.Length > 0 Then
                codeBuilder.Append("DomainKey = """ + sourceMap.DomainKey + """, ")
            End If


            If (sourceMap.PersistenceType = PersistenceType.Default Or sourceMap.PersistenceType = PersistenceType.ObjectRelational) Then

                codeBuilder.Append("SourceType = SourceType." + sourceMap.SourceType.ToString() + ", ")
                codeBuilder.Append("ProviderType = ProviderType." + sourceMap.ProviderType.ToString() + ", ")

            End If

            If sourceMap.Schema.Length > 0 Then
                codeBuilder.Append("Schema = """ + sourceMap.Schema + """, ")
            End If

            If sourceMap.Catalog.Length > 0 Then
                codeBuilder.Append("Catalog = """ + sourceMap.Catalog + """, ")
            End If

            If sourceMap.ProviderAssemblyPath.Length > 0 Then
                codeBuilder.Append("ProviderAssemblyPath = """ + sourceMap.ProviderAssemblyPath + """, ")
            End If

            If sourceMap.ProviderConnectionTypeName.Length > 0 Then
                codeBuilder.Append("ProviderConnectionTypeName = """ + sourceMap.ProviderConnectionTypeName + """, ")
            End If

            If sourceMap.ConnectionString.Length > 0 Then
                codeBuilder.Append("ConnectionString = """ + sourceMap.ConnectionString + """, ")
            End If


            Dim result As String = codeBuilder.ToString()
            If Right(result, 2) = ", " Then
                codeBuilder.Length = codeBuilder.Length - 2
            End If

            codeBuilder.Append(")" + AttributeEnd)
            codeBuilder.Append(vbCrLf)

        End If

        Return codeBuilder.ToString()

    End Function



End Class
