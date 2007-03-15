' *
' * Copyright (C) 2007 Matthijs Hulsbeek : http://www.puzzleframework.com
' *
' * This library is free software; you can redistribute it and/or modify it
' * under the terms of the GNU Lesser General Public License 2.1 or later, as
' * published by the Free Software Foundation. See the included license.txt
' * or http://www.gnu.org/copyleft/lesser.html for details.
' *
' *

Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Imports Puzzle.NPersist.Framework
Imports Puzzle.NPersist.Framework.Mapping
Imports Puzzle.NPersist.Framework.Persistence

Public Class SourceToTablesOracle
    Inherits SourceToTablesBase

    Public Overloads Overrides Sub SourceToTables(ByVal sourceMap As Puzzle.NPersist.Framework.Mapping.ISourceMap, ByRef hashDiff As Hashtable)

    End Sub

    Public Overloads Overrides Sub SourceToTables(ByVal sourceMap As Puzzle.NPersist.Framework.Mapping.ISourceMap, ByVal addToDomainMap As IDomainMap, ByRef hashDiff As Hashtable)

        Dim ctx As IContext = New Context(sourceMap.DomainMap)
        Dim sql As String = "SELECT TABLE_NAME FROM ALL_TABLES WHERE OWNER = '" & sourceMap.Schema & "'"
        Dim ds As IDataSource = ctx.DataSourceManager.GetDataSource(sourceMap)

        Dim i As Long

        Dim name As String
        Dim tableMap As ITableMap
        Dim result As Object

        Try
            result = ctx.SqlExecutor.ExecuteArray(sql, ds)
        Catch ex As Exception
            ds.ReturnConnection()
            ctx.Dispose()
            Throw New Exception("Encountered an exception when communicating with database: " & ex.Message, ex)
        End Try

        ds.ReturnConnection()

        Try
            If IsArray(result) Then
                For i = 0 To UBound(result, 2)
                    name = result(0, i)
                    If Not name.Equals("MICROSOFTDTPROPERTIES") Then
                        tableMap = sourceMap.GetTableMap(name)
                        If tableMap Is Nothing Then
                            tableMap = New tableMap
                            tableMap.Name = name
                            If addToDomainMap Is Nothing Then
                                tableMap.SourceMap = sourceMap
                            Else
                                tableMap.SourceMap = addToDomainMap.GetSourceMap(sourceMap.Name)
                            End If
                            LogDiff(tableMap, "The table '" & tableMap.Name & "' was found in the source but not in the model", DiffInfoEnum.NotFound, "Name", hashDiff)
                        End If
                        GetColumns(tableMap, ctx, addToDomainMap, hashDiff)
                    End If
                Next
            End If

        Catch ex As Exception
            ctx.Dispose()
            Throw ex
        End Try

        ctx.Dispose()

    End Sub

    Public Overloads Overrides Sub SourceToColumns(ByVal tableMap As Puzzle.NPersist.Framework.Mapping.ITableMap, ByVal addToDomainMap As IDomainMap, ByRef hashDiff As Hashtable)

        Dim ctx As IContext = New Context(tableMap.SourceMap.DomainMap)

        Try
            GetColumns(tableMap, ctx, addToDomainMap, hashDiff)
        Catch ex As Exception
            ctx.Dispose()
            Throw ex
        End Try

        ctx.Dispose()

    End Sub

    Public Overloads Overrides Sub SourceToColumns(ByVal tableMap As Puzzle.NPersist.Framework.Mapping.ITableMap, ByRef hashDiff As Hashtable)

        Dim ctx As IContext = New Context(tableMap.SourceMap.DomainMap)

        Try
            GetColumns(tableMap, ctx, Nothing, hashDiff)
        Catch ex As Exception
            ctx.Dispose()
            Throw ex
        End Try

        ctx.Dispose()

    End Sub


    Protected Overridable Sub GetColumns(ByVal tableMap As Puzzle.NPersist.Framework.Mapping.ITableMap, ByVal ctx As IContext, ByVal addToDomainMap As IDomainMap, ByRef hashDiff As Hashtable)
        Dim sql As String = "SELECT COLUMN_NAME,DATA_TYPE,DATA_LENGTH,NULLABLE,DATA_PRECISION,DATA_SCALE,DATA_DEFAULT FROM ALL_TAB_COLUMNS WHERE UPPER(TABLE_NAME) = UPPER('" & tableMap.Name & "') AND UPPER(OWNER) = UPPER('" & tableMap.SourceMap.Schema & "')"
        Dim sqlKeys As String = "SELECT col.CONSTRAINT_NAME, col.COLUMN_NAME, constr.CONSTRAINT_TYPE, constr.R_CONSTRAINT_NAME, foreigh_constr.TABLE_NAME, foreigh_constr.COLUMN_NAME FROM ALL_CONS_COLUMNS col INNER JOIN ALL_CONSTRAINTS constr on col.CONSTRAINT_NAME = constr.CONSTRAINT_NAME LEFT JOIN ALL_CONS_COLUMNS foreigh_constr ON constr.R_CONSTRAINT_NAME = foreigh_constr.CONSTRAINT_NAME WHERE UPPER(col.TABLE_NAME) = UPPER('" & tableMap.Name & "') AND (constr.CONSTRAINT_TYPE = 'R' OR constr.CONSTRAINT_TYPE = 'P')"

        Dim ds As IDataSource = ctx.DataSourceManager.GetDataSource(tableMap.SourceMap)
        Dim result As Object
        Dim resultKeys As Object

        Dim i As Long
        Dim j As Int32
        Dim k As Int32
        Dim primaryKeys As New ArrayList
        Dim foreighKeys As New ArrayList
        Dim columnMap As IColumnMap
        Dim columnMapOld As IColumnMap

        Dim addToSourceMap As ISourceMap
        Dim addToTableMap As ITableMap

        Dim keyType As Char

        Dim colName As String
        Dim colDataType As String
        Dim colLength As Integer
        Dim colIsNullable As Char
        Dim colPrec As Integer
        Dim colScale As Integer
        Dim colDefaultVal As String

        Dim diffMsgs As New ArrayList
        Dim diffSettings As New ArrayList

        Try
            result = ctx.SqlExecutor.ExecuteArray(sql, ds)
            resultKeys = ctx.SqlExecutor.ExecuteArray(sqlKeys, ds)

        Catch ex As Exception
            ds.ReturnConnection()
            Throw New Exception("Encountered an exception when communicating with database: " & ex.Message, ex)
        End Try

        ds.ReturnConnection()

        If IsArray(resultKeys) Then
            For i = 0 To UBound(resultKeys, 2)
                If Not IsDBNull(resultKeys(2, i)) Then
                    If resultKeys(2, i) = "P" Then
                        primaryKeys.Add(resultKeys(1, i))
                    ElseIf resultKeys(2, i) = "R" Then
                        foreighKeys.Add(resultKeys(1, i))
                    End If
                End If
            Next
        End If

        If IsArray(result) Then

            For i = 0 To UBound(result, 2)
                diffMsgs.Clear()
                diffSettings.Clear()
                
                If Not IsDBNull(result(0, i)) Then colName = result(0, i)
                If Not IsDBNull(result(1, i)) Then colDataType = result(1, i)
                If Not IsDBNull(result(3, i)) Then colIsNullable = result(3, i)

                'Reset the values that can be null. We don't want the values from the previous column.
                If Not IsDBNull(result(2, i)) Then
                    colLength = result(2, i)
                Else
                    colLength = 0
                End If

                If Not IsDBNull(result(4, i)) Then
                    colPrec = result(4, i)
                Else
                    colPrec = 0
                End If

                If Not IsDBNull(result(5, i)) Then
                    colScale = result(5, i)
                Else
                    colScale = 0
                End If

                If Not IsDBNull(result(6, i)) Then
                    colDefaultVal = result(6, i)
                Else
                    colDefaultVal = ""
                End If

                columnMap = New columnMap
                columnMap.Name = colName
                If addToDomainMap Is Nothing Then
                    columnMap.TableMap = tableMap
                Else
                    addToSourceMap = addToDomainMap.GetSourceMap(tableMap.SourceMap.Name)
                    addToTableMap = addToSourceMap.GetTableMap(tableMap.Name)
                    If addToTableMap Is Nothing Then
                        addToTableMap = New tableMap
                        addToTableMap.Name = tableMap.Name
                        addToTableMap.SourceMap = addToSourceMap
                    End If

                    columnMap.TableMap = addToTableMap
                End If

                columnMap.DataType = GetDatatype(colDataType, colScale)
                columnMap.Length = colLength
                columnMap.Precision = colPrec
                columnMap.Scale = colScale
                columnMap.DefaultValue = colDefaultVal

                If colIsNullable = "Y" Then
                    columnMap.AllowNulls = True
                Else
                    columnMap.AllowNulls = False
                End If

                If primaryKeys.Contains(columnMap.Name) Then
                    columnMap.IsPrimaryKey = True
                Else
                    columnMap.IsPrimaryKey = False
                End If

                If foreighKeys.Contains(columnMap.Name) Then
                    columnMap.IsForeignKey = True

                    For j = 0 To UBound(resultKeys, 2)
                        If Not IsDBNull(resultKeys(2, j)) Then
                            If resultKeys(2, j) = "R" Then
                                If Not IsDBNull(resultKeys(1, j)) Then
                                    If resultKeys(1, j) = columnMap.Name Then
                                        If Not IsDBNull(resultKeys(0, j)) Then
                                            columnMap.ForeignKeyName = resultKeys(0, j)
                                        End If
                                        If Not IsDBNull(resultKeys(4, j)) Then
                                            columnMap.PrimaryKeyTable = resultKeys(4, j)
                                        End If
                                        If Not IsDBNull(resultKeys(5, j)) Then
                                            columnMap.PrimaryKeyColumn = resultKeys(5, j)
                                        End If
                                    End If
                                End If
                            End If
                        End If

                    Next

                Else
                    columnMap.IsForeignKey = False
                End If

                'Oracle has sequences which are not table/field based. Impossible to assign it automatically.
                'Maybe based on naming scheme?
                'columnMap.Sequence = ""

                'Look for changes in the current model.
                columnMapOld = tableMap.GetColumnMap(colName)

                If Not columnMapOld Is Nothing Then

                    If Not columnMapOld.DataType = columnMap.DataType Then
                        diffMsgs.Add("The data type for the column '" & columnMap.Name & "' in the data source did not match the model. (data source: '" & columnMap.DataType.ToString & "', model: '" & columnMapOld.DataType.ToString & "')")
                        diffSettings.Add("DataType")
                    End If

                    If Not columnMapOld.Length = columnMap.Length Then
                        diffMsgs.Add("The length for the column '" & columnMap.Name & "' in the data source did not match the model. (data source: '" & columnMapOld.Length & "', model: '" & columnMapOld.Length & "')")
                        diffSettings.Add("Length")
                    End If

                    If Not columnMapOld.Precision = columnMap.Precision Then
                        diffMsgs.Add("The precision for the column '" & columnMap.Name & "' in the data source did not match the model. (data source: '" & columnMap.Precision & "', model: '" & columnMapOld.Precision & "')")
                        diffSettings.Add("Precision")
                    End If

                    If Not columnMapOld.Scale = columnMap.Scale Then
                        diffMsgs.Add("The scale for the column '" & columnMap.Name & "' in the data source did not match the model. (data source: '" & columnMap.Scale & "', model: '" & columnMapOld.Scale & "')")
                        diffSettings.Add("Scale")
                    End If

                    If Not columnMapOld.DefaultValue = columnMap.DefaultValue Then
                        diffMsgs.Add("The default value for the column '" & columnMap.Name & "' in the data source did not match the model. (data source: '" & columnMap.DefaultValue & "', model: '" & columnMapOld.DefaultValue & "')")
                        diffSettings.Add("DefaultValue")
                    End If

                    If Not columnMapOld.AllowNulls = columnMap.AllowNulls Then
                        diffMsgs.Add("The nullability for the column '" & columnMap.Name & "' in the data source did not match the model. (data source: '" & columnMap.AllowNulls & "', model: '" & columnMapOld.AllowNulls & "')")
                        diffSettings.Add("AllowNulls")
                    End If

                    If Not columnMapOld.IsPrimaryKey = columnMap.IsPrimaryKey Then
                        If columnMap.IsPrimaryKey Then
                            diffMsgs.Add("The column '" & columnMap.Name & "' in the data source is a primary key.")
                            diffSettings.Add("IsPrimaryKey")
                        End If

                        If columnMapOld.IsPrimaryKey Then
                            diffMsgs.Add("The column '" & columnMap.Name & "' in the data source is not a primary key.")
                            diffSettings.Add("IsPrimaryKey")
                        End If
                    End If

                    If Not columnMapOld.IsForeignKey = columnMap.IsForeignKey Then
                        If columnMap.IsForeignKey Then
                            diffMsgs.Add("The column '" & columnMap.Name & "' in the data source is a foreign key.")
                            diffSettings.Add("IsForeignKey")
                        End If

                        If columnMapOld.IsForeignKey Then
                            diffMsgs.Add("The column '" & columnMap.Name & "' in the data source is a foreign key.")
                            diffSettings.Add("IsForeignKey")
                        End If
                    End If

                    'For now just delete the old one.
                    columnMapOld = Nothing
                End If

                If diffMsgs.Count > 0 Then
                    For k = 0 To diffMsgs.Count - 1
                        LogDiff(columnMap, diffMsgs(k), DiffInfoEnum.NotEqual, diffSettings(k), hashDiff)
                    Next
                Else
                    LogDiff(columnMap, "The column '" & columnMap.Name & "' was found in the source but not in the model", DiffInfoEnum.NotFound, "Name", hashDiff)
                End If
            Next

        End If

    End Sub


    Protected Overridable Function GetDatatype(ByVal type As String, ByVal scale As Int32) As DbType
        Select Case type
            Case "VARCHAR2"
                Return DbType.String
            Case "VARCHAR"
                Return DbType.String
            Case "VARCHAR"
                Return DbType.String
            Case "CHAR"
                Return DbType.String
            Case "NCHAR"
                Return DbType.String
            Case "NUMBER"
                If (scale > 0) Then
                    Return DbType.Double
                Else
                    Return DbType.Int32
                End If
            Case "LONG"
                Return DbType.Int64
            Case "DATE"
                Return DbType.Date
            Case "TIMESTAMP"
                Return DbType.Time
            Case "BLOB"
                Return DbType.Binary
            Case "INTEGER"
                Return DbType.Int32
            Case "FLOAT"
                Return DbType.Double
        End Select
        Return False
    End Function


    Protected Overridable Sub LogDiff(ByVal mapObject As IMap, ByVal message As String, ByVal diffInfo As DiffInfoEnum, ByVal setting As String, ByRef hashDiff As Hashtable)

        Dim key As String = mapObject.GetKey

        If Not hashDiff.ContainsKey(key) Then

            hashDiff(key) = New ArrayList

        End If

        CType(hashDiff(key), ArrayList).Add(New diffInfo(mapObject, message, diffInfo, setting))

    End Sub

End Class
