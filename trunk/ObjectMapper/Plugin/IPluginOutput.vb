' *
' * Copyright (C) 2008 Jeremy Longo : http://www.puzzleframework.com
' *
' * This library is free software; you can redistribute it and/or modify it
' * under the terms of the GNU Lesser General Public License 2.1 or later, as
' * published by the Free Software Foundation. See the included license.txt
' * or http://www.gnu.org/copyleft/lesser.html for details.
' *
' *

Public Interface IPluginOutput
    Property Content() As String
    Property Filename() As String
    ReadOnly Property Extension() As String
    ReadOnly Property TypeDescription() As String
    ReadOnly Property DocumentType() As Integer
End Interface