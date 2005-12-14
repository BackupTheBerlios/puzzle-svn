Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Xml
Imports Puzzle.NPersist.Framework
Imports Puzzle.NPersist.Framework.Mapping
Imports Puzzle.NPersist.Framework.Enumerations

Public Class NPersistToNHibernate

    Private m_ClassesToCodeCs As IClassesToCode = New ClassesToCodeCs
    Private m_ClassesToCodeVb As IClassesToCode = New ClassesToCodeVb
    Private m_ClassesToCodeDelphi As IClassesToCode = New ClassesToCodeDelphi


    Public Sub New()
        MyBase.new()

    End Sub

    'Public Overrides Function Deserialize(ByVal xml As String) As IDomainMap

    '    Dim xmlDoc As XmlDocument = New XmlDocument
    '    Dim xmlDom As XmlNode

    '    xmlDoc.LoadXml(xml)

    '    xmlDom = xmlDoc.SelectSingleNode("domain")

    '    Return DeserializeDomainMap(xmlDom)

    'End Function

    Public Overloads Function Serialize(ByVal domainMap As IDomainMap) As String

        Return SerializeDomainMap(domainMap, Nothing)

    End Function

    Public Overloads Function Serialize(ByVal classMap As IClassMap) As String

        Return SerializeDomainMap(classMap.DomainMap, classMap)

    End Function

    Public Function IsRootClass(ByVal classMap As IClassMap) As Boolean

        Dim rootClassMaps As ArrayList = GetRootClasses(classMap.DomainMap)
        Dim rootClassMap As IClassMap

        For Each rootClassMap In rootClassMaps

            If rootClassMap Is classMap Then

                Return True

            End If

        Next

        Return False

    End Function

    Public Function GetRootClasses(ByVal domainMap As IDomainMap) As ArrayList

        Dim classMap As IClassMap
        Dim classMaps As New ArrayList

        For Each classMap In domainMap.ClassMaps

            'Obs! Only serialize root classes - and concrete-table inheritance!
            'Subclasses need to be mapped as nested
            'elements inside the root classes
            If Len(classMap.InheritsClass) < 1 OrElse classMap.InheritanceType = InheritanceType.ConcreteTableInheritance Then

                classMaps.Add(classMap)

            End If

        Next

        Return classMaps

    End Function

    Protected Overridable Function SerializeDomainMap(ByVal domainMap As IDomainMap, ByVal justThisClass As IClassMap) As String

        Dim xml As New StringBuilder
        Dim classMap As IClassMap
        Dim sourceMap As ISourceMap
        Dim str As String
        Dim meta As String

        Dim ok As Boolean

        xml.Append("<?xml version=""1.0"" encoding=""utf-8"" ?>" & vbCrLf)

        xml.Append("<hibernate-mapping xmlns=""urn:nhibernate-mapping-2.0""")

        sourceMap = domainMap.GetSourceMap

        If Not sourceMap Is Nothing Then

            If Len(sourceMap.Schema) > 0 Then

                xml.Append(" schema=""" & sourceMap.Schema & """")

            End If

        End If

        meta = domainMap.GetMetaData("nh-default-cascade")

        If Len(meta) > 0 Then

            xml.Append(" default-cascade=""" & meta & """")

        End If

        meta = domainMap.GetMetaData("nh-auto-import")

        If Len(meta) > 0 Then

            xml.Append(" auto-import=""" & meta & """")

        End If

        xml.Append(">" & vbCrLf)

        For Each classMap In GetRootClasses(domainMap)

            ok = True

            If Not justThisClass Is Nothing Then

                ok = False

                If justThisClass Is classMap Then

                    ok = True

                End If

            End If

            If ok Then

                xml.Append(SerializeClassMap(classMap))

            End If

        Next

        xml.Append("</hibernate-mapping>" & vbCrLf)

        Return xml.ToString()

    End Function

    Protected Overridable Function SerializeClassMap(ByVal classMap As IClassMap, Optional ByVal isSubClass As Boolean = False, Optional ByVal level As Integer = 0) As String

        Dim xml As New StringBuilder
        Dim propertyMap As IPropertyMap
        Dim str As String
        Dim subClassMap As IClassMap
        Dim tagName As String
        Dim ident As String
        Dim i As Integer
        Dim singleIdent As String = "  "
        Dim sourceMap As ISourceMap
        Dim defSchema As String
        Dim meta As String
        Dim columnMap As IColumnMap
        Dim columnMaps As New ArrayList

        If isSubClass Then

            Select Case classMap.InheritanceType

                Case InheritanceType.ClassTableInheritance

                    tagName = "joined-subclass"

                Case InheritanceType.ConcreteTableInheritance

                    tagName = "class"

                Case InheritanceType.SingleTableInheritance

                    tagName = "subclass"

                Case Else

                    tagName = "subclass"

            End Select

        Else

            tagName = "class"

        End If

        For i = 0 To level

            ident += singleIdent

        Next

        xml.Append(ident & "<" & tagName & " name=""" & GetNhClassName(classMap) & """")


        If Len(classMap.Table) > 0 Then

            If Not classMap.InheritanceType = InheritanceType.SingleTableInheritance Then

                xml.Append(" table=""" & classMap.Table & """")

            End If

        End If

        If Len(classMap.TypeValue) > 0 Then

            If classMap.InheritanceType = InheritanceType.SingleTableInheritance Then

                xml.Append(" discriminator-value=""" & classMap.TypeValue & """")

            End If

        End If

        If Not classMap.GetTableMap Is Nothing Then

            sourceMap = classMap.GetTableMap.SourceMap

            If Not sourceMap Is Nothing Then

                If Not classMap.DomainMap.GetSourceMap Is Nothing Then

                    defSchema = classMap.DomainMap.GetSourceMap.Schema

                End If

                If Len(sourceMap.Schema) > 0 Then

                    If Not LCase(defSchema) = LCase(sourceMap.Schema) Then

                        xml.Append(" schema=""" & sourceMap.Schema & """")

                    End If

                End If

            End If

        End If


        meta = classMap.GetMetaData("nh-mutable")

        If Len(meta) > 0 Then

            xml.Append(" mutable=""" & meta & """")

        End If


        meta = classMap.GetMetaData("nh-proxy")

        If Len(meta) > 0 Then

            xml.Append(" proxy=""" & meta & """")

        End If


        meta = classMap.GetMetaData("nh-dynamic-update")

        If Len(meta) > 0 Then

            xml.Append(" dynamic-update=""" & meta & """")

        End If


        meta = classMap.GetMetaData("nh-dynamic-insert")

        If Len(meta) > 0 Then

            xml.Append(" dynamic-insert=""" & meta & """")

        End If


        meta = classMap.GetMetaData("nh-polymorphism")

        If Len(meta) > 0 Then

            xml.Append(" polymorphism=""" & meta & """")

        End If

        meta = classMap.GetMetaData("nh-where")

        If Len(meta) > 0 Then

            xml.Append(" where=""" & meta & """")

        End If

        meta = classMap.GetMetaData("nh-persister")

        If Len(meta) > 0 Then

            xml.Append(" persister=""" & meta & """")

        End If



        xml.Append(">" & vbCrLf)

        If Not isSubClass Then

            If classMap.InheritanceType = InheritanceType.SingleTableInheritance Then

                If Len(classMap.TypeColumn) > 0 Then

                    xml.Append(ident & singleIdent & "<discriminator column=""" & classMap.TypeColumn & """")

                    columnMap = classMap.GetTypeColumnMap

                    If Not columnMap Is Nothing Then

                        'OBS!
                        '????
                        'Should it be the type of some property instead???
                        xml.Append(" type=""" & GetColumnType(columnMap) & """")

                    End If

                    meta = classMap.GetMetaData("nh-force")

                    If Len(meta) > 0 Then

                        xml.Append(" force=""" & meta & """")

                    End If


                    xml.Append(" />" & vbCrLf)

                End If

            End If

        Else

            Select Case classMap.InheritanceType

                Case InheritanceType.ClassTableInheritance


                    columnMaps.Clear()

                    columnMap = classMap.GetTypeColumnMap

                    If Not columnMap Is Nothing Then

                        If columnMap.IsPrimaryKey Then

                            columnMaps.Add(columnMap)

                        End If

                    End If

                    For Each propertyMap In classMap.GetNonInheritedIdentityPropertyMaps

                        columnMap = propertyMap.GetColumnMap

                        If Not columnMap Is Nothing Then

                            columnMaps.Add(columnMap)

                        End If

                        For Each columnMap In propertyMap.GetAdditionalColumnMaps

                            columnMaps.Add(columnMap)

                        Next

                    Next

                    xml.Append(ident & singleIdent & "<key")

                    If columnMaps.Count > 1 Then

                        xml.Append(">" & vbCrLf)

                        For Each columnMap In columnMaps

                            xml.Append(ident & singleIdent & singleIdent & "<column name=""" & columnMap.Name & """ />")

                        Next

                        xml.Append(ident & singleIdent & "</key>" & vbCrLf)

                    ElseIf columnMaps.Count = 1 Then

                        xml.Append(" column=""" & CType(columnMaps(0), IColumnMap).Name & """ />" & vbCrLf)

                    End If


            End Select

        End If

        If isSubClass = False Or classMap.InheritanceType = InheritanceType.ConcreteTableInheritance Then

            'If classMap.GetNonInheritedIdentityPropertyMaps.Count > 1 Then
            If classMap.GetIdentityPropertyMaps.Count > 1 Then

                xml.Append(SerializeCompositeIdPropertyMap(classMap, ident & singleIdent))

            ElseIf classMap.GetIdentityPropertyMaps.Count = 1 Then

                propertyMap = classMap.GetIdentityPropertyMaps(0)

                If Not propertyMap Is Nothing Then

                    xml.Append(SerializeIdPropertyMap(propertyMap, ident & singleIdent))

                End If

            End If

        End If

        For Each propertyMap In classMap.PropertyMaps

            If Not propertyMap.IsIdentity Then

                xml.Append(SerializePropertyMap(propertyMap, ident & singleIdent))

            End If

        Next

        For Each subClassMap In GetDirectSubClassMapsExceptConcreteTableInheritance(classMap)

            xml.Append(SerializeClassMap(subClassMap, True, level + 1))

        Next

        xml.Append(ident & "</" & tagName & ">" & vbCrLf)

        Return xml.ToString()

    End Function

    Protected Overridable Function SerializePropertyMap(ByVal propertyMap As IPropertyMap, ByVal ident As String) As String

        'Ok - N/Hibernate calls many-to-one one-to-many and vice versa
        'this is where the switcharoo takes place...
        'Also: One of life's great mysteries:
        'Why does N/Hibernate want to call the read-write side
        'of a one-to-one relationship a many-to-one??
        'Anyway, that hack goes here too...
        'Update: It seems both sides are called one-to-one after all...
        'hack removed!
        Select Case propertyMap.ReferenceType

            Case ReferenceType.None

                If propertyMap.IsCollection Then

                    Return SerializeCollectionPropertyMap(propertyMap, ident)

                Else

                    Return SerializePrimitivePropertyMap(propertyMap, ident)

                End If

            Case ReferenceType.ManyToOne

                Return SerializeOneManyPropertyMap(propertyMap, ident)

            Case ReferenceType.OneToMany

                Return SerializeManyOnePropertyMap(propertyMap, ident)

            Case ReferenceType.ManyToMany

                Return SerializeManyManyPropertyMap(propertyMap, ident)

            Case ReferenceType.OneToOne

                Return SerializeOneOnePropertyMap(propertyMap, ident)

                'If propertyMap.IsReadOnly Then

                '    Return SerializeOneOnePropertyMap(propertyMap, ident)

                'Else

                '    Return SerializeManyOnePropertyMap(propertyMap, ident)

                'End If

        End Select

    End Function

    Protected Overridable Function SerializeIdPropertyMap(ByVal propertyMap As IPropertyMap, ByVal ident As String) As String

        Dim xml As New StringBuilder
        Dim xmlParams As New StringBuilder
        Dim str As String

        Dim ok As Boolean

        Dim meta As String
        Dim singleIdent As String = "  "

        Dim columnMap As IColumnMap

        Dim arr() As String
        Dim arr2() As String

        Dim i As Integer

        xml.Append(ident & "<id name=""" & propertyMap.Name & """")


        If Len(propertyMap.DataType) > 0 Then

            xml.Append(" type=""" & GetPropertyType(propertyMap) & """")

        End If

        If Len(propertyMap.Column) > 0 Then

            If propertyMap.AdditionalColumns.Count < 1 Then

                xml.Append(" column=""" & propertyMap.Column & """")

            End If

        End If


        meta = propertyMap.GetMetaData("nh-unsaved-value")

        If Len(meta) > 0 Then

            xml.Append(" unsaved-value=""" & meta & """")

        End If

        xml.Append(">" & vbCrLf)


        If Len(propertyMap.Column) > 0 Then

            If propertyMap.AdditionalColumns.Count > 0 Then

                xml.Append(ident & singleIdent & "<column name=""" & propertyMap.Column & """ />" & vbCrLf)

                For Each str In propertyMap.AdditionalColumns

                    xml.Append(ident & singleIdent & "<column name=""" & str & """ />" & vbCrLf)

                Next

            End If

        End If



        xml.Append(ident & singleIdent & "<generator")


        meta = propertyMap.GetMetaData("nh-generator-class")

        If Len(meta) > 0 Then

            xml.Append(" class=""" & meta & """")

        Else

            columnMap = propertyMap.GetColumnMap

            If Not columnMap Is Nothing Then

                If Len(columnMap.Sequence) > 0 Then

                    xml.Append(" class=""sequence""")

                    xmlParams.Append(ident & singleIdent & singleIdent & "<param name=""sequence"">" & columnMap.Sequence & "</param>" & vbCrLf)

                Else

                    If columnMap.IsAutoIncrease Then

                        xml.Append(" class=""identity""")

                    Else

                        If columnMap.DataType = DbType.Guid Then

                            'OBS! ???
                            xml.Append(" class=""assigned""")

                        Else

                            xml.Append(" class=""assigned""")

                        End If

                    End If

                End If

            End If


        End If


        meta = propertyMap.GetMetaData("nh-generator-parameters")

        If xmlParams.Length > 0 Or Len(meta) > 0 Then

            xml.Append(">" & vbCrLf)

            If xmlParams.Length > 0 Then

                xml.Append(xmlParams.ToString)

            End If

            If Len(meta) > 0 Then

                arr = Split(meta, ",")

                For i = 0 To UBound(arr)

                    If Len(arr(i)) > 0 Then

                        arr2 = Split(arr(i), "=")

                        If UBound(arr2) = 1 Then

                            If Len(arr2(0)) > 0 And Len(arr2(1)) > 0 Then

                                xml.Append(ident & singleIdent & singleIdent & "<param name=""" & arr2(0) & """>" & arr2(1) & "</param>" & vbCrLf)

                            End If

                        End If

                    End If

                Next

            End If

            xml.Append(ident & singleIdent & "</generator>" & vbCrLf)

        Else

            xml.Append(" />" & vbCrLf)

        End If


        xml.Append(ident & "</id>" & vbCrLf)

        Return xml.ToString()

    End Function

    Protected Overridable Function SerializeCompositeIdPropertyMap(ByVal classMap As IClassMap, ByVal ident As String) As String

        Dim xml As New StringBuilder
        Dim str As String

        Dim ok As Boolean

        Dim meta As String
        Dim singleIdent As String = "  "

        Dim propertyMap As IPropertyMap
        'Dim propertyMaps As ArrayList = classMap.GetNonInheritedIdentityPropertyMaps
        Dim propertyMaps As ArrayList = classMap.GetIdentityPropertyMaps

        Dim columnMap As IColumnMap

        xml.Append(ident & "<composite-id")

        meta = classMap.GetMetaData("nh-composite-name")

        If Len(meta) > 0 Then

            xml.Append(" name=""" & meta & """")

        End If

        meta = classMap.GetMetaData("nh-composite-class")

        If Len(meta) > 0 Then

            xml.Append(" class=""" & meta & """")

        End If


        meta = classMap.GetMetaData("nh-composite-unsaved-value")

        If Len(meta) > 0 Then

            xml.Append(" unsaved-value=""" & meta & """")

        End If

        xml.Append(">" & vbCrLf)

        For Each propertyMap In propertyMaps

            If propertyMap.ReferenceType = ReferenceType.None Then

                xml.Append(ident & singleIdent & "<key-property name=""" & propertyMap.Name & """")


                If Len(propertyMap.DataType) > 0 Then

                    xml.Append(" type=""" & GetPropertyType(propertyMap) & """")

                End If

                If Len(propertyMap.Column) > 0 Then

                    If propertyMap.AdditionalColumns.Count < 1 Then

                        xml.Append(" column=""" & propertyMap.Column & """ />" & vbCrLf)

                    Else

                        xml.Append(">" & vbCrLf)

                        xml.Append(ident & singleIdent & "<column name=""" & propertyMap.Column & """ />" & vbCrLf)

                        For Each str In propertyMap.AdditionalColumns

                            xml.Append(ident & singleIdent & "<column name=""" & str & """ />" & vbCrLf)

                        Next

                        xml.Append(ident & singleIdent & "</key-property>")

                    End If

                Else

                    xml.Append(" />" & vbCrLf)

                End If

            Else

                xml.Append(ident & singleIdent & "<key-many-to-one name=""" & propertyMap.Name & """")

                If Len(propertyMap.DataType) > 0 Then

                    'xml.Append(" class=""" & GetPropertyType(propertyMap) & """")
                    xml.Append(" class=""" & GetNhClassName(propertyMap.GetReferencedClassMap) & """")

                End If


                If Len(propertyMap.Column) > 0 Then

                    If propertyMap.AdditionalColumns.Count < 1 Then

                        xml.Append(" column=""" & propertyMap.Column & """ />" & vbCrLf)

                    Else

                        xml.Append(">" & vbCrLf)

                        xml.Append(ident & singleIdent & "<column name=""" & propertyMap.Column & """ />" & vbCrLf)

                        For Each str In propertyMap.AdditionalColumns

                            xml.Append(ident & singleIdent & "<column name=""" & str & """ />" & vbCrLf)

                        Next

                        xml.Append(ident & singleIdent & "</key-many-to-one>")

                    End If

                Else

                    xml.Append(" />" & vbCrLf)

                End If


            End If

        Next


        xml.Append(ident & "</composite-id>" & vbCrLf)

        Return xml.ToString()




    End Function

    Protected Overridable Function SerializeManyOnePropertyMap(ByVal propertyMap As IPropertyMap, ByVal ident As String) As String

        Dim xml As New StringBuilder
        Dim str As String

        Dim ok As Boolean

        Dim meta As String
        Dim singleIdent As String = "  "


        xml.Append(ident & "<many-to-one name=""" & propertyMap.Name & """")



        If Len(propertyMap.Column) > 0 Then

            If propertyMap.AdditionalColumns.Count < 1 Then

                xml.Append(" column=""" & propertyMap.Column & """")

            End If

        End If


        If Len(propertyMap.DataType) > 0 Then

            'xml.Append(" class=""" & GetPropertyType(propertyMap) & """")
            xml.Append(" class=""" & GetNhClassName(propertyMap.GetReferencedClassMap) & """")

        End If



        meta = propertyMap.GetMetaData("nh-cascade")

        If Len(meta) > 0 Then

            xml.Append(" cascade=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-outer-join")

        If Len(meta) > 0 Then

            xml.Append(" outer-join=""" & meta & """")

        End If


        meta = propertyMap.GetMetaData("nh-update")

        If Len(meta) > 0 Then

            xml.Append(" update=""" & meta & """")

        End If


        meta = propertyMap.GetMetaData("nh-insert")

        If Len(meta) > 0 Then

            xml.Append(" insert=""" & meta & """")

        End If


        If Len(propertyMap.Column) > 0 Then

            If propertyMap.AdditionalColumns.Count > 0 Then

                xml.Append(">" & vbCrLf)

                xml.Append(ident & singleIdent & "<column name=""" & propertyMap.Column & """ />" & vbCrLf)

                For Each str In propertyMap.AdditionalColumns

                    xml.Append(ident & singleIdent & "<column name=""" & str & """ />" & vbCrLf)

                Next

                xml.Append(ident & "</many-to-one>" & vbCrLf)

            Else

                xml.Append(" />" & vbCrLf)

            End If

        Else

            xml.Append(" />" & vbCrLf)

        End If


        Return xml.ToString()


    End Function

    Protected Overridable Function SerializeOneManyPropertyMap(ByVal propertyMap As IPropertyMap, ByVal ident As String) As String

        Dim xml As New StringBuilder
        Dim str As String

        Dim ok As Boolean

        Dim meta As String
        Dim singleIdent As String = "  "

        Dim sourceMap As ISourceMap

        xml.Append(ident & "<bag name=""" & propertyMap.Name & """")


        meta = propertyMap.GetMetaData("nh-lazy")

        If Len(meta) > 0 Then

            If meta.ToLower = "auto" Then meta = "true"

            xml.Append(" lazy=""" & meta & """")

            'Else

            '    xml.Append(" lazy=""true""")

        End If


        If Not propertyMap.GetInversePropertyMap Is Nothing Then

            If propertyMap.IsSlave Then

                xml.Append(" inverse=""true""")

            End If

        End If

        meta = propertyMap.GetMetaData("nh-cascade")

        If Len(meta) > 0 Then

            xml.Append(" cascade=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-sort")

        If Len(meta) > 0 Then

            xml.Append(" sort=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-order-by")

        If Len(meta) > 0 Then

            xml.Append(" order-by=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-where")

        If Len(meta) > 0 Then

            xml.Append(" where=""" & meta & """")

        End If


        xml.Append(">" & vbCrLf)


        If propertyMap.AdditionalIdColumns.Count > 0 Then

            xml.Append(ident & singleIdent & "<key>" & vbCrLf)

            If Len(propertyMap.IdColumn) > 0 Then

                xml.Append(ident & singleIdent & singleIdent & "<column name=""" & propertyMap.IdColumn & """ />" & vbCrLf)

            End If

            For Each str In propertyMap.AdditionalIdColumns

                xml.Append(ident & singleIdent & singleIdent & "<column name=""" & str & """ />" & vbCrLf)

            Next

            xml.Append(ident & singleIdent & "</key>" & vbCrLf)

        Else

            If Len(propertyMap.IdColumn) > 0 Then

                xml.Append(ident & singleIdent & "<key column=""" & propertyMap.IdColumn & """ />" & vbCrLf)

            End If

        End If


        xml.Append(ident & singleIdent & "<one-to-many")

        If Len(propertyMap.ItemType) > 0 Then

            'xml.Append(" class=""" & propertyMap.ItemType & """")
            xml.Append(" class=""" & GetNhClassName(propertyMap.GetReferencedClassMap) & """")

        End If

        xml.Append(" />" & vbCrLf)




        xml.Append(ident & "</bag>" & vbCrLf)

        Return xml.ToString()



    End Function

    Protected Overridable Function SerializeManyManyPropertyMap(ByVal propertyMap As IPropertyMap, ByVal ident As String) As String

        Dim xml As New StringBuilder
        Dim str As String

        Dim ok As Boolean

        Dim meta As String
        Dim singleIdent As String = "  "

        Dim sourceMap As ISourceMap

        xml.Append(ident & "<bag name=""" & propertyMap.Name & """")


        If Len(propertyMap.Table) > 0 Then

            xml.Append(" table=""" & propertyMap.Table & """")

        End If

        If Not propertyMap.GetTableMap Is Nothing Then

            sourceMap = propertyMap.GetTableMap.SourceMap

            If Not propertyMap.ClassMap.GetTableMap Is Nothing Then

                If Not sourceMap Is propertyMap.ClassMap.GetTableMap.SourceMap Then

                    If Len(sourceMap.Schema) > 0 Then

                        xml.Append(" schema=""" & sourceMap.Schema & """")

                    End If

                End If

            End If

        End If


        meta = propertyMap.GetMetaData("nh-lazy")

        If Len(meta) > 0 Then

            If meta.ToLower() = "auto" Then meta = "true"

            xml.Append(" lazy=""" & meta & """")

            'Else

            '    xml.Append(" lazy=""true""")

        End If


        If Not propertyMap.GetInversePropertyMap Is Nothing Then

            If propertyMap.IsSlave Then

                xml.Append(" inverse=""true""")

            End If

        End If

        meta = propertyMap.GetMetaData("nh-cascade")

        If Len(meta) > 0 Then

            xml.Append(" cascade=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-sort")

        If Len(meta) > 0 Then

            xml.Append(" sort=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-order-by")

        If Len(meta) > 0 Then

            xml.Append(" order-by=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-where")

        If Len(meta) > 0 Then

            xml.Append(" where=""" & meta & """")

        End If


        xml.Append(">" & vbCrLf)


        If propertyMap.AdditionalIdColumns.Count > 0 Then

            xml.Append(ident & singleIdent & "<key>" & vbCrLf)

            If Len(propertyMap.IdColumn) > 0 Then

                xml.Append(ident & singleIdent & singleIdent & "<column name=""" & propertyMap.IdColumn & """ />" & vbCrLf)

            End If

            For Each str In propertyMap.AdditionalIdColumns

                xml.Append(ident & singleIdent & singleIdent & "<column name=""" & str & """ />" & vbCrLf)

            Next

            xml.Append(ident & singleIdent & "</key>" & vbCrLf)

        Else

            If Len(propertyMap.IdColumn) > 0 Then

                xml.Append(ident & singleIdent & "<key column=""" & propertyMap.IdColumn & """ />" & vbCrLf)

            End If

        End If


        If propertyMap.AdditionalIdColumns.Count > 0 Then

            xml.Append(ident & singleIdent & "<many-to-many")

            If Len(propertyMap.ItemType) > 0 Then

                'xml.Append(" class=""" & propertyMap.ItemType & """")
                xml.Append(" class=""" & GetNhClassName(propertyMap.GetReferencedClassMap) & """")

            End If

            xml.Append(">" & vbCrLf)



            If Len(propertyMap.Column) > 0 Then

                xml.Append(ident & singleIdent & singleIdent & "<column name=""" & propertyMap.Column & """ />" & vbCrLf)

            End If

            For Each str In propertyMap.AdditionalColumns

                xml.Append(ident & singleIdent & singleIdent & "<column name=""" & str & """ />" & vbCrLf)

            Next


            xml.Append(ident & singleIdent & "</many-to-many>" & vbCrLf)

        Else

            xml.Append(ident & singleIdent & "<many-to-many")

            If Len(propertyMap.Column) > 0 Then

                xml.Append(" column=""" & propertyMap.Column & """")

            End If

            If Len(propertyMap.ItemType) > 0 Then

                'xml.Append(" class=""" & propertyMap.ItemType & """")
                xml.Append(" class=""" & GetNhClassName(propertyMap.GetReferencedClassMap) & """")

            End If

            xml.Append(" />" & vbCrLf)

        End If




        xml.Append(ident & "</bag>" & vbCrLf)

        Return xml.ToString()


    End Function

    Protected Overridable Function SerializeOneOnePropertyMap(ByVal propertyMap As IPropertyMap, ByVal ident As String) As String

        Dim xml As New StringBuilder
        Dim str As String

        Dim ok As Boolean

        Dim meta As String
        Dim singleIdent As String = "  "


        xml.Append(ident & "<one-to-one name=""" & propertyMap.Name & """")


        If Len(propertyMap.DataType) > 0 Then

            'xml.Append(" class=""" & GetPropertyType(propertyMap) & """")
            xml.Append(" class=""" & GetNhClassName(propertyMap.GetReferencedClassMap) & """")

        End If



        meta = propertyMap.GetMetaData("nh-cascade")

        If Len(meta) > 0 Then

            xml.Append(" cascade=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-outer-join")

        If Len(meta) > 0 Then

            xml.Append(" outer-join=""" & meta & """")

        End If


        xml.Append(" />" & vbCrLf)


        Return xml.ToString()



    End Function

    Protected Overridable Function SerializeCollectionPropertyMap(ByVal propertyMap As IPropertyMap, ByVal ident As String) As String

    End Function

    Protected Overridable Function SerializePrimitivePropertyMap(ByVal propertyMap As IPropertyMap, ByVal ident As String) As String

        Dim xml As New StringBuilder
        Dim str As String

        Dim ok As Boolean

        Dim meta As String
        Dim singleIdent As String = "  "

        xml.Append(ident & "<property name=""" & propertyMap.Name & """")



        If Len(propertyMap.Column) > 0 Then

            If propertyMap.AdditionalColumns.Count < 1 Then

                xml.Append(" column=""" & propertyMap.Column & """")

            End If

        End If


        If Len(propertyMap.DataType) > 0 Then

            xml.Append(" type=""" & GetPropertyType(propertyMap) & """")

        End If


        meta = propertyMap.GetMetaData("nh-update")

        If Len(meta) > 0 Then

            xml.Append(" update=""" & meta & """")

        End If


        meta = propertyMap.GetMetaData("nh-insert")

        If Len(meta) > 0 Then

            xml.Append(" insert=""" & meta & """")

        End If


        meta = propertyMap.GetMetaData("nh-formula")

        If Len(meta) > 0 Then

            xml.Append(" formula=""" & meta & """")

        End If

        meta = propertyMap.GetMetaData("nh-length")

        If Len(meta) > 0 Then

            xml.Append(" length=""" & meta & """")

        End If

        If Len(propertyMap.Column) > 0 Then

            If propertyMap.AdditionalColumns.Count > 0 Then

                xml.Append(">" & vbCrLf)

                xml.Append(ident & singleIdent & "<column name=""" & propertyMap.Column & """ />" & vbCrLf)

                For Each str In propertyMap.AdditionalColumns

                    xml.Append(ident & singleIdent & "<column name=""" & str & """ />" & vbCrLf)

                Next

                xml.Append(ident & "</property>" & vbCrLf)

            Else

                xml.Append(" />" & vbCrLf)

            End If

        Else

            xml.Append(" />" & vbCrLf)

        End If


        Return xml.ToString()

    End Function

    Protected Overridable Function SerializeSourceMap(ByVal sourceMap As ISourceMap) As String

        Dim xml As New StringBuilder
        Dim tableMap As ITableMap

        xml.Append("  <source name=""" & sourceMap.Name & """")

        xml.Append(" type=""" & sourceMap.SourceType.ToString & """")

        xml.Append(" provider=""" & sourceMap.ProviderType.ToString & """")

        If Len(sourceMap.Schema) > 0 Then

            xml.Append(" schema=""" & sourceMap.Schema & """")

        End If

        If Len(sourceMap.Catalog) > 0 Then

            xml.Append(" catalog=""" & sourceMap.Catalog & """")

        End If

        If Len(sourceMap.ProviderAssemblyPath) > 0 Then

            xml.Append(" provider-path=""" & sourceMap.ProviderAssemblyPath & """")

        End If

        If Len(sourceMap.ProviderConnectionTypeName) > 0 Then

            xml.Append(" provider-conn=""" & sourceMap.ProviderConnectionTypeName & """")

        End If

        xml.Append(">" & vbCrLf)

        xml.Append("    <connection-string>" & sourceMap.ConnectionString & "</connection-string>" & vbCrLf)

        For Each tableMap In sourceMap.TableMaps

            xml.Append(SerializeTableMap(tableMap))

        Next

        xml.Append("  </source>" & vbCrLf)

        Return xml.ToString()

    End Function

    Protected Overridable Function SerializeTableMap(ByVal tableMap As ITableMap) As String

        Dim xml As New StringBuilder
        Dim columnMap As IColumnMap

        xml.Append("    <table name=""" & tableMap.Name & """")

        If tableMap.IsView Then

            xml.Append(" view=""true""")

        End If

        xml.Append(">" & vbCrLf)

        For Each columnMap In tableMap.ColumnMaps

            xml.Append(SerializeColumnMap(columnMap))

        Next

        xml.Append("    </table>" & vbCrLf)

        Return xml.ToString()

    End Function

    Protected Overridable Function SerializeColumnMap(ByVal columnMap As IColumnMap) As String

        Dim xml As New StringBuilder

        xml.Append("      <column name=""" & columnMap.Name & """")

        xml.Append(" type=""" & columnMap.DataType.ToString & """")

        xml.Append(" prec=""" & columnMap.Precision & """")

        If columnMap.AllowNulls Then

            xml.Append(" allow-null=""true""")

        End If

        xml.Append(" length=""" & columnMap.Length & """")

        xml.Append(" scale=""" & columnMap.Scale & """")

        If columnMap.IsFixedLength Then

            xml.Append(" fixed-length=""true""")

        End If

        If columnMap.IsPrimaryKey Then

            xml.Append(" primary=""true""")

        End If

        If Len(columnMap.Sequence) > 0 Then

            xml.Append(" sequence=""" & columnMap.Sequence & """")

        End If

        If columnMap.IsForeignKey Then

            xml.Append(" foreign=""true""")

            If Len(columnMap.PrimaryKeyTable) > 0 Then

                xml.Append(" primary-table=""" & columnMap.PrimaryKeyTable & """")

            End If

            If Len(columnMap.PrimaryKeyColumn) > 0 Then

                xml.Append(" primary-column=""" & columnMap.PrimaryKeyColumn & """")

            End If

            If Len(columnMap.ForeignKeyName) > 0 Then

                xml.Append(" foreign-key=""" & columnMap.ForeignKeyName & """")

            End If

        End If

        If columnMap.IsAutoIncrease Then

            xml.Append(" auto-inc=""true""")

            xml.Append(" seed=""" & columnMap.Seed & """")

            xml.Append(" inc=""" & columnMap.Increment & """")

        End If

        If Len(columnMap.DefaultValue) > 0 Then

            xml.Append(" default=""" & columnMap.DefaultValue & """")

        End If


        If Len(columnMap.Format) > 0 Then

            xml.Append(" format=""" & columnMap.Format & """")

        End If


        If Len(columnMap.SpecificDataType) > 0 Then

            xml.Append(" specific-type=""" & columnMap.SpecificDataType & """")

        End If

        xml.Append(" />" & vbCrLf)

        Return xml.ToString()

    End Function


    Protected Overridable Function ParseBool(ByVal str As String) As Boolean

        Select Case LCase(str)

            Case "false", "0", "off", "no"

                Return False

            Case Else

                Return True

        End Select

    End Function

    Protected Overridable Function GetTrue() As String

        Return "=""true"""

    End Function


    Protected Overridable Function GetPropertyType(ByVal propertyMap As IPropertyMap) As String

        Dim propType As String
        Dim tag As String = "system."

        propType = propertyMap.DataType

        If Len(propType) > Len(tag) Then

            If Left(LCase(propType), Len(tag)) = tag Then

                propType = Right(propType, Len(propType) - Len(tag))

            End If

        End If

        Return propType

    End Function

    Protected Overridable Function GetColumnType(ByVal columnMap As IColumnMap) As String

        Dim colType As String

        If Len(columnMap.SpecificDataType) > 0 Then

            colType = columnMap.SpecificDataType

        Else

            colType = columnMap.DataType.ToString

        End If

        Return colType

    End Function


    Private Function GetDirectSubClassMapsExceptConcreteTableInheritance(ByVal ownerClassMap As IClassMap) As System.Collections.ArrayList

        Dim classMaps As New ArrayList
        Dim classMap As IClassMap
        Dim superClassMap As IClassMap

        For Each classMap In ownerClassMap.DomainMap.ClassMaps

            If Not classMap Is ownerClassMap Then

                superClassMap = classMap.GetInheritedClassMap

                If Not superClassMap Is Nothing Then

                    If superClassMap Is ownerClassMap Then

                        If Not classMap.InheritanceType = InheritanceType.ConcreteTableInheritance Then

                            classMaps.Add(classMap)

                        End If

                    End If

                End If

            End If

        Next

        Return classMaps

    End Function

    Private Sub SetupCodeGens(Optional ByVal ReallyNoRegions As Boolean = True)

        m_ClassesToCodeCs.GeneratePOCO = True
        m_ClassesToCodeCs.IncludeComments = False
        m_ClassesToCodeCs.IncludeDocComments = False
        m_ClassesToCodeCs.IncludeModelInfoInDocComments = False
        m_ClassesToCodeCs.IncludeRegions = False
        m_ClassesToCodeCs.TargetPlatform = TargetPlatformEnum.NHibernate
        m_ClassesToCodeCs.ReallyNoRegions = ReallyNoRegions

        m_ClassesToCodeVb.GeneratePOCO = True
        m_ClassesToCodeVb.IncludeComments = False
        m_ClassesToCodeVb.IncludeDocComments = False
        m_ClassesToCodeVb.IncludeModelInfoInDocComments = False
        m_ClassesToCodeVb.IncludeRegions = False
        m_ClassesToCodeVb.TargetPlatform = TargetPlatformEnum.NHibernate
        m_ClassesToCodeVb.ReallyNoRegions = ReallyNoRegions

        m_ClassesToCodeDelphi.GeneratePOCO = True
        m_ClassesToCodeDelphi.IncludeComments = False
        m_ClassesToCodeDelphi.IncludeDocComments = False
        m_ClassesToCodeDelphi.IncludeModelInfoInDocComments = False
        m_ClassesToCodeDelphi.IncludeRegions = False
        m_ClassesToCodeDelphi.TargetPlatform = TargetPlatformEnum.NHibernate
        m_ClassesToCodeDelphi.ReallyNoRegions = ReallyNoRegions


    End Sub



    Public Overloads Function ToCSharp(ByVal domainMap As IDomainMap, Optional ByVal ReallyNoRegions As Boolean = True) As String

        Dim code As String

        SetupCodeGens(ReallyNoRegions)

        code = m_ClassesToCodeCs.DomainToCode(domainMap, False)


        Return code

    End Function

    Public Overloads Function ToVb(ByVal domainMap As IDomainMap, Optional ByVal ReallyNoRegions As Boolean = True) As String

        Dim code As String

        SetupCodeGens(ReallyNoRegions)


        code = m_ClassesToCodeVb.DomainToCode(domainMap, False)


        Return code

    End Function

    Public Overloads Function ToDelphi(ByVal domainMap As IDomainMap, Optional ByVal ReallyNoRegions As Boolean = True) As String

        Dim code As String

        SetupCodeGens(ReallyNoRegions)

        code = m_ClassesToCodeDelphi.DomainToCode(domainMap, False)


        Return code

    End Function


    Public Overloads Function ToCSharp(ByVal classMap As IClassMap, Optional ByVal ReallyNoRegions As Boolean = True) As String

        Dim code As String

        SetupCodeGens(ReallyNoRegions)

        code = m_ClassesToCodeCs.ClassToCode(classMap, False, False)


        Return code

    End Function

    Public Overloads Function ToVb(ByVal classMap As IClassMap, Optional ByVal ReallyNoRegions As Boolean = True) As String

        Dim code As String

        SetupCodeGens(ReallyNoRegions)

        code = m_ClassesToCodeVb.ClassToCode(classMap, False, False)


        Return code

    End Function

    Public Overloads Function ToDelphi(ByVal classMap As IClassMap, Optional ByVal ReallyNoRegions As Boolean = True) As String

        Dim code As String

        SetupCodeGens(ReallyNoRegions)

        code = m_ClassesToCodeDelphi.ClassToCode(classMap, False, False)


        Return code

    End Function


    Public Overloads Function ToProjectCSharp(ByVal domainMap As IDomainMap, ByVal projPath As String, ByVal classMapsAndFiles As Hashtable, ByVal embeddedFiles As ArrayList) As String

        Dim code As String

        SetupCodeGens()

        code = m_ClassesToCodeCs.DomainToProject(domainMap, projPath, classMapsAndFiles, embeddedFiles)

        Return code

    End Function

    Public Overloads Function ToProjectVb(ByVal domainMap As IDomainMap, ByVal projPath As String, ByVal classMapsAndFiles As Hashtable, ByVal embeddedFiles As ArrayList) As String

        Dim code As String

        SetupCodeGens()

        code = m_ClassesToCodeVb.DomainToProject(domainMap, projPath, classMapsAndFiles, embeddedFiles)

        Return code

    End Function

    Public Overloads Function ToProjectDelphi(ByVal domainMap As IDomainMap, ByVal projPath As String, ByVal classMapsAndFiles As Hashtable, ByVal embeddedFiles As ArrayList) As String

        Dim code As String

        SetupCodeGens()

        code = m_ClassesToCodeDelphi.DomainToProject(domainMap, projPath, classMapsAndFiles, embeddedFiles)

        Return code

    End Function



    Public Overloads Function ToAssemblyInfoCSharp(ByVal domainMap As IDomainMap) As String

        Dim code As String

        SetupCodeGens()

        code = m_ClassesToCodeCs.DomainToAssemblyInfo(domainMap)

        Return code

    End Function

    Public Overloads Function ToAssemblyInfoVb(ByVal domainMap As IDomainMap) As String

        Dim code As String

        SetupCodeGens()

        code = m_ClassesToCodeVb.DomainToAssemblyInfo(domainMap)

        Return code

    End Function

    Public Overloads Function ToAssemblyInfoDelphi(ByVal domainMap As IDomainMap) As String

        Dim code As String

        SetupCodeGens()

        code = m_ClassesToCodeDelphi.DomainToAssemblyInfo(domainMap)

        Return code

    End Function


    Public Function GetNhClassName(ByVal classMap As IClassMap) As String

        Dim className As String = ""

        className = classMap.DomainMap.RootNamespace

        If Len(className) > 0 Then

            If Not className.EndsWith(".") Then

                className += "."

            End If

        End If

        className += classMap.Name & ", " & classMap.GetAssemblyName

        Return className

    End Function



End Class
