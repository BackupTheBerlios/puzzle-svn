' *
' * Copyright (C) 2006 Mats Helander : http://www.puzzleframework.com
' *
' * This library is free software; you can redistribute it and/or modify it
' * under the terms of the GNU Lesser General Public License 2.1 or later, as
' * published by the Free Software Foundation. See the included license.txt
' * or http://www.gnu.org/copyleft/lesser.html for details.
' *
' *

<AttributeUsage(AttributeTargets.Method, Inherited:=True)> Public Class ConverterMethodAttribute
    Inherits Attribute


    Private m_DisplayName As String

    Public Sub New()

        m_DisplayName = Me.GetType.Name

    End Sub

    Public Sub New(ByVal displayName As String)

        m_DisplayName = displayName

    End Sub

    Public Property DisplayName() As String
        Get
            Return m_DisplayName
        End Get
        Set(ByVal Value As String)
            m_DisplayName = Value
        End Set
    End Property


End Class
