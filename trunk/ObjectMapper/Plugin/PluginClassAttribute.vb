' *
' * Copyright (C) 2006 Mats Helander : http://www.puzzleframework.com
' *
' * This library is free software; you can redistribute it and/or modify it
' * under the terms of the GNU Lesser General Public License 2.1 or later, as
' * published by the Free Software Foundation. See the included license.txt
' * or http://www.gnu.org/copyleft/lesser.html for details.
' *
' *

<AttributeUsage(AttributeTargets.Class, Inherited:=True)> Public Class PluginClassAttribute
    Inherits Attribute

    Private m_PluginGroup As String
    Private m_DisplayName As String

    Public Sub New(ByVal pluginGroup As String)
        MyBase.New()

        m_PluginGroup = pluginGroup

    End Sub

    Public Sub New(ByVal pluginGroup As String, ByVal displayName As String)
        MyBase.New()

        m_PluginGroup = pluginGroup
        m_DisplayName = displayName

    End Sub

    Public Property PluginGroup() As String
        Get
            Return m_PluginGroup
        End Get
        Set(ByVal Value As String)
            m_PluginGroup = Value
        End Set
    End Property


    Public Property DisplayName() As String
        Get
            Return m_DisplayName
        End Get
        Set(ByVal Value As String)
            m_DisplayName = Value
        End Set
    End Property

End Class
